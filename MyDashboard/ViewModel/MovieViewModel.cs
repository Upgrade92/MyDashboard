
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using MyDashboard.Model;

namespace MyDashboard.ViewModel
{
    public class MovieViewModel : INotifyPropertyChanged
    {
        private CollectionViewSource MovieItemsCollection;
        public ICollectionView MovieSourceCollection => MovieItemsCollection.View;

        public MovieViewModel()
        {           
            ObservableCollection<MovieItems> movieItems = new ObservableCollection<MovieItems>
            {

                new MovieItems { MovieName = "Action", MovieImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\clap_icon.png")},
                new MovieItems { MovieName = "Thriller", MovieImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\clap_icon.png")},
                new MovieItems { MovieName = "Adventure", MovieImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\clap_icon.png")},
                new MovieItems { MovieName = "Drama", MovieImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\clap_icon.png")},
                new MovieItems { MovieName = "Fantasy", MovieImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\clap_icon.png")},
                new MovieItems { MovieName = "Mystery", MovieImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\clap_icon.png")}

            };

            MovieItemsCollection = new CollectionViewSource { Source = movieItems };
            MovieItemsCollection.Filter += MenuItems_Filter;

        }

        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                MovieItemsCollection.View.Refresh();
                OnPropertyChanged("FilterText");
            }
        }

        private void MenuItems_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }

            MovieItems _item = e.Item as MovieItems;
            if (_item.MovieName.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
