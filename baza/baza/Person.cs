using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace baza
{
    public class Person //model bazy
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Subscribed { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}", Name, Status);
        }
    }
}
