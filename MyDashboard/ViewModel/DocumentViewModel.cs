
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using MyDashboard.Model;

namespace MyDashboard.ViewModel
{
    public class DocumentViewModel : INotifyPropertyChanged
    {
        private CollectionViewSource DocumentItemsCollection;
        public ICollectionView DocumentSourceCollection => DocumentItemsCollection.View;       

        public DocumentViewModel()
        {
            ObservableCollection<DocumentItems> documentItems = new ObservableCollection<DocumentItems>
            {

                new DocumentItems { DocumentName = "Books", DocumentImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\book_icon.png")},
                new DocumentItems { DocumentName = "Studio", DocumentImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\studio_icon.png")},
                new DocumentItems { DocumentName = "Export", DocumentImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\export_icon.png")},
                new DocumentItems { DocumentName = "Print", DocumentImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\print_icon.png")},                  
                new DocumentItems { DocumentName = "Orders", DocumentImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\order_icon.png")},              
                new DocumentItems { DocumentName = "Stocks", DocumentImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\stock_icon.png")},
                new DocumentItems { DocumentName = "Invoice", DocumentImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\invoice_icon.png")}               

            };

            DocumentItemsCollection = new CollectionViewSource { Source = documentItems };
            DocumentItemsCollection.Filter += MenuItems_Filter;

        }

        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                DocumentItemsCollection.View.Refresh();
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

            DocumentItems _item = e.Item as DocumentItems;
            if (_item.DocumentName.ToUpper().Contains(FilterText.ToUpper()))
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
