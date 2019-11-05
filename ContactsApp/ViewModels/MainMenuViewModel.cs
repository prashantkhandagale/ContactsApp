using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public RelayCommand ManageContactCommand { get; set; }
        public MainMenuViewModel()
        {
            ManageContactCommand = new RelayCommand(OnManageContact);
        }

        private void OnManageContact(object obj)
        {
            ViewModelMain.Instance.CurrentView = new ManageContactsViewModel();
        }
    }
}
