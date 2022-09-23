
using MyDashboard.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;

namespace MyDashboard.ViewModel
{
    public class PCViewModel : INotifyPropertyChanged
    {
        private readonly CollectionViewSource PCItemsCollection;
        public ICollectionView PCSourceCollection => PCItemsCollection.View;

        public PCViewModel()
        {
            ObservableCollection<PCItems> pcItems = new ObservableCollection<PCItems>
            {
                new PCItems { PCName = "Local Disk (C:)", PCImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\drive_icon.png")},
                new PCItems { PCName = "Local Disk (D:)", PCImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\drive_icon.png")},
                new PCItems { PCName = "Local Disk (E:)", PCImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\drive_icon.png")},
                new PCItems { PCName = "Local Disk (F:)", PCImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\drive_icon.png")}

            };

            PCItemsCollection = new CollectionViewSource { Source = pcItems };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
