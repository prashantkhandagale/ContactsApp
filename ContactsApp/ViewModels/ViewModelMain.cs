using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.ViewModels
{
    public class ViewModelMain : ViewModelBase
    {
        public static ViewModelMain _instance;

        public static ViewModelMain Instance 
        { 
            get
            {
                if(_instance == null)
                {
                    _instance = new ViewModelMain();
                }

                return ViewModelMain._instance;
            }
        }

        private ViewModelBase _currentView;

        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set 
            {
                _currentView = value;
                OnPropertyChanged("CurrentView");
            }
        }

        private string _header;
        public new string Header
        {
            get { return _header; }
            set
            {
                _header= value;
                OnPropertyChanged("Header");
            }
        }

        private ViewModelMain()
        {
            CurrentView = new MainMenuViewModel();
            //Header = "Manage Contacts";
        }
    }
}
