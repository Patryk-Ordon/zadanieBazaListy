using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace baza
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // ////
        async Task Load()
        {
            List<Person> Person = await App.Database.GetPeopleAsync();
            foreach (Person person in Person)
            {
                person.Name = char.ToUpper(person.Name[0]).ToString() + person.Name.Substring(1);
            }
            collectionView.ItemsSource = Person;
        }
        // ///

        protected override async void OnAppearing()
        {
            base.OnAppearing(); // wymusza odświeżenie kolekcji
            //collectionView.ItemsSource = await App.Database.GetPeopleAsync();

            await Load();

        }
        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await App.Database.SavePersonAsync(new Person
                {
                    Name = nameEntry.Text,
                    Subscribed = subscribed.IsChecked
                });
                nameEntry.Text = string.Empty;
                subscribed.IsChecked = false;
                //collectionView.ItemsSource = await App.Database.GetPeopleAsync();

                await Load();
                string name = App.Database.GetPeopleAsync().Result[App.Database.GetPeopleAsync().Result.Count-2].Name;
                lblO.Text = "Ostatnio dodana do bazy osoba to " + name;

                await Load();
            }
        }

        // usuwanie wszystkiego z bazy
        private async void btnClear_Clicked(object sender, EventArgs e)
        {
            await App.Database.DeleteAllItems();
            await Load();
        }
        }
    }
