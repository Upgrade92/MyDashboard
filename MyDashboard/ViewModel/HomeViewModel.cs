
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using MyDashboard.Model;

namespace MyDashboard.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private CollectionViewSource HomeItemsCollection;
        public ICollectionView HomeSourceCollection => HomeItemsCollection.View;

        public HomeViewModel()
        {           
            ObservableCollection<HomeItems> homeItems = new ObservableCollection<HomeItems>
            {
                new HomeItems { HomeName = "This PC", HomeImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\pc_icon.png")},               
            };

            HomeItemsCollection = new CollectionViewSource { Source = homeItems };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

}