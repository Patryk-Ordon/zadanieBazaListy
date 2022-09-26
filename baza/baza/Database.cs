using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace baza
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Person>();
        }

        // ///
        public List<Person> GetPeople()
        {
            List<baza.Person> Person = _database.Table<Person>().ToListAsync().Result;
            if(Person.Count > 0)
                Person[0].Name += " to pierwsza zapisana w bazie osoba";
            Person.Add(new Person { Name = "Ostatnia osoba której nie ma w bazie", Subscribed = true });
            foreach (Person person in Person)
            {
                if (person.Subscribed == true)
                    person.Status = "Subskrybent";
                else
                    person.Status = "Jeszcze nie jest subsrybentem";
            }
            return Person;
        }
        // ///

        public Task<List<Person>> GetPeopleAsync()
        {
            //return _database.Table<Person>().ToListAsync();
            var result = Task.Run(() => GetPeople());
            return result;
        }

        public Task<int> SavePersonAsync(Person person)
        {
            return _database.InsertAsync(person);
        }

        // ///
        public Task DeleteAllItems()
        {
            return _database.DeleteAllAsync<Person>();
        }
        // ///
    }
}
