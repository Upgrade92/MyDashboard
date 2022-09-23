using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.ObjectModel;
using MyDashboard.Model;
using System.Dynamic;
using System;
using System.IO;
using System.Windows;

namespace MyDashboard.ViewModel
{
    class NavigationViewModel :INotifyPropertyChanged
    {
        private CollectionViewSource MenuItemsCollection;
        public ICollectionView SourceCollection => MenuItemsCollection.View;

        public NavigationViewModel()
        {
            ObservableCollection<MenuItems> menuItems = new ObservableCollection<MenuItems>
            {
                new MenuItems {MenuName = "Home", MenuImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\Home_Icon.png")},
                new MenuItems {MenuName = "Desktop", MenuImage =Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\Desktop_Icon.png")},
                new MenuItems {MenuName = "Documents", MenuImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\Document_Icon.png")},
                new MenuItems {MenuName = "Downloads", MenuImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\Download_Icon.png")},
                new MenuItems {MenuName = "Pictures", MenuImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\Images_Icon.png")},
                new MenuItems {MenuName = "Music", MenuImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\Music_Icon.png")},
                new MenuItems {MenuName = "Movies", MenuImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\Movies_Icon.png")},
                new MenuItems {MenuName = "Trash", MenuImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\Trash_Icon.png")},
                new MenuItems {MenuName = "Close", MenuImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Assets\\close_Icon.png")}
            };

            MenuItemsCollection = new CollectionViewSource { Source = menuItems};
            MenuItemsCollection.Filter += MenuItems_Filter;

            //          Set Startup Page
            SelectedViewModel = new StartupViewModel();
        }


        //          Implement interface member of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        //          Text Search
        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                MenuItemsCollection.View.Refresh();
                OnPropertyChanged("FilterText");
            }
        }

        private void MenuItems_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(filterText))
            {
                e.Accepted = true;
                return;
            }
            
            MenuItems item = e.Item as MenuItems;
            if (item.MenuName.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        //      Select ViewModel
        private object selectedViewModel;
        public object SelectedViewModel
        {
            get => selectedViewModel;
            set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }

        // Switch Views 
        public void SwitchViews(object parameter)
        {
            switch (parameter)
            {
                case "Home":
                    SelectedViewModel = new HomeViewModel();
                    break;

                case "Desktop":
                    SelectedViewModel= new DesktopViewModel();
                    break;

                case "Documents":
                    SelectedViewModel = new DocumentViewModel();
                    break;

                case "Downloads":
                    SelectedViewModel = new DownloadViewModel();
                    break;

                case "Pictures":
                    SelectedViewModel = new PictureViewModel();
                    break;

                case "Music":
                    SelectedViewModel = new MusicViewModel();
                    break;

                case "Movies":
                    SelectedViewModel = new MovieViewModel();
                    break;

                case "Trash":
                    SelectedViewModel = new DocumentViewModel();
                    break;

                case "Close":
                    Application.Current.Shutdown();
                    break;

                default:
                    SelectedViewModel = new HomeViewModel();
                    break;
            }
        }

        //      Show PC View

        public void PCView()
        {
            SelectedViewModel = new PCViewModel();
        }

        //      This PC Command
        private ICommand pccommand;
        public ICommand PCCommand
        {
            get
            {
                if(pccommand == null)
                {
                    pccommand = new RelayCommand(param => PCView());
                }
                return pccommand;
            }
        }

        //      Menu Button Command
        private ICommand menuCommand;
        public ICommand MenuCommand
        {
            get
            {
                if(menuCommand == null)
                {
                    menuCommand = new RelayCommand(param => SwitchViews(param));
                }
                return menuCommand;
            }
        }

        //      Show Home View
        private void ShowHome()
        {
            SelectedViewModel = new HomeViewModel();
        }

        //      Back button Command
        private ICommand backHomeCommand;
        public ICommand BackHomeCommand
        {
            get
            {
                if(backHomeCommand == null)
                {
                    backHomeCommand = new RelayCommand(p => ShowHome());
                }
                return backHomeCommand; 
            }
        }

        //      Close App
        public void CloseApp(object obj)
        {
            MainWindow win = obj as MainWindow;
            win.Close();
        }

        //      Close App Command
        private ICommand closeCommand;
        public ICommand CloseAppCommand
        {

            get
            {
                if(closeCommand == null)
                {
                    closeCommand = new RelayCommand(p => CloseApp(p));
                }
                return closeCommand;
            }
        }
    }
}
