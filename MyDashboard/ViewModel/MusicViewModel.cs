
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using MyDashboard.Model;

namespace MyDashboard.ViewModel
{
    public class MusicViewModel : INotifyPropertyChanged
    {
        private CollectionViewSource MusicItemsCollection;
        public ICollectionView MusicSourceCollection => MusicItemsCollection.View;

        public MusicViewModel()
        {
            ObservableCollection<MusicItems> musicItems = new ObservableCollection<MusicItems>
            {

                new MusicItems { MusicName = "Bass", MusicImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\note_icon.png")},
                new MusicItems { MusicName = "Beats", MusicImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\note_icon.png")},
                new MusicItems { MusicName = "Electronic", MusicImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\/note_icon.png")},
                new MusicItems { MusicName = "Hip hop", MusicImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\note_icon.png")},
                new MusicItems { MusicName = "Deep House", MusicImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\note_icon.png")},
                new MusicItems { MusicName = "Instrumental", MusicImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\note_icon.png")}

            };

            MusicItemsCollection = new CollectionViewSource { Source = musicItems };
            MusicItemsCollection.Filter += MenuItems_Filter;
        }

        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                MusicItemsCollection.View.Refresh();
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

            MusicItems _item = e.Item as MusicItems;
            if (_item.MusicName.ToUpper().Contains(FilterText.ToUpper()))
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
