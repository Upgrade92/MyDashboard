
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using MyDashboard.Model;

namespace MyDashboard.ViewModel
{
    public class DesktopViewModel : INotifyPropertyChanged
    {
        private readonly CollectionViewSource DesktopItemsCollection;
        public ICollectionView DesktopSourceCollection => DesktopItemsCollection.View;

        public DesktopViewModel()
        {            
            ObservableCollection<DesktopItems> desktopItems = new ObservableCollection<DesktopItems>
            {
                new DesktopItems { DesktopName = "File", DesktopImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\file_icon.png")},
                new DesktopItems { DesktopName = "Music", DesktopImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\musical_icon.png")},
                new DesktopItems { DesktopName = "Pictures", DesktopImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\picture_icon.png")},
                new DesktopItems { DesktopName = "Analytics", DesktopImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\analytics_icon.png") },
                new DesktopItems { DesktopName = "Webcam", DesktopImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\webcam_icon.png")},
                new DesktopItems { DesktopName = "Printer", DesktopImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\printer_icon.png")},
                new DesktopItems { DesktopName = "Services", DesktopImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\services_icon.png")},               
                new DesktopItems { DesktopName = "Chart", DesktopImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\chart_icon.png")},
                new DesktopItems { DesktopName = "Film", DesktopImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\film_icon.png")},
                new DesktopItems { DesktopName = "Alarm", DesktopImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\alarm_icon.png")},
                new DesktopItems { DesktopName = "Headphone", DesktopImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\headphone_icon.png")},
                new DesktopItems { DesktopName = "Password", DesktopImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\password_icon.png")},
                new DesktopItems { DesktopName = "Calendar", DesktopImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\calendar_icon.png")}

            };

            DesktopItemsCollection = new CollectionViewSource { Source = desktopItems };
            DesktopItemsCollection.Filter += MenuItems_Filter;

        }

        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                DesktopItemsCollection.View.Refresh();
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

            DesktopItems _item = e.Item as DesktopItems;
            if (_item.DesktopName.ToUpper().Contains(FilterText.ToUpper()))
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
