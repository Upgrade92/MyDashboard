
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using MyDashboard.Model;

namespace MyDashboard.ViewModel
{
    public class DownloadViewModel : INotifyPropertyChanged
    {
        private CollectionViewSource DownloadItemsCollection;
        public ICollectionView DownloadSourceCollection => DownloadItemsCollection.View;

        public DownloadViewModel()
        {            
            ObservableCollection<DownloadItems> downloadItems = new ObservableCollection<DownloadItems>
            {
                new DownloadItems { DownloadName = "Visual Studio 2019", DownloadImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\vs_icon.png")},
                new DownloadItems { DownloadName = "Android Studio", DownloadImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\android_icon.png")},
                new DownloadItems { DownloadName = "Python", DownloadImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\python_icon.png")},
                new DownloadItems { DownloadName = "Swift", DownloadImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\swift_icon.png")},
                new DownloadItems { DownloadName = "Visual Studio Code", DownloadImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\vsc_icon.png")},
                new DownloadItems { DownloadName = "Javascript", DownloadImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\javascript_icon.png")},
                new DownloadItems { DownloadName = "HTML 5", DownloadImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\html_icon.png")},
                new DownloadItems { DownloadName = "Angular", DownloadImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\angular_icon.png")},
                new DownloadItems { DownloadName = "Flutter", DownloadImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\flutter_icon.png")}
            };

            DownloadItemsCollection = new CollectionViewSource { Source = downloadItems };
            DownloadItemsCollection.Filter += MenuItems_Filter;

        }

        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                DownloadItemsCollection.View.Refresh();
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

            DownloadItems _item = e.Item as DownloadItems;
            if (_item.DownloadName.ToUpper().Contains(FilterText.ToUpper()))
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
