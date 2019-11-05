using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ContactsApp.Models;
using ContactsApp.DAL;
using ContactsApp.Providers;

namespace ContactsApp.ViewModels
{
    public class ManageContactsViewModel : ViewModelBase
    {
        ContactService service;
        IMessageBoxProvider messageBox = new MessageBoxProvider();
        #region Commands
        public RelayCommand AddCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        #endregion

        #region Properties

        private ObservableCollection<Contact> _contacts;

        public ObservableCollection<Contact> Contacts
        {
            get { return _contacts; }
            set { _contacts = value; OnPropertyChanged("Contacts"); }

        }

        private Contact currentContact;

        public Contact CurrentContact
        {
            get { return currentContact; }
            set { currentContact = value; OnPropertyChanged("CurrentContact"); }
        }


        #endregion

        public ManageContactsViewModel()
        {
            AddCommand = new RelayCommand(OnAdd);
            EditCommand = new RelayCommand(OnEdit);
            DeleteCommand = new RelayCommand(OnDelete);
            BackCommand = new RelayCommand(OnBack);
            Header = "Manage Contacts";
            LoadContacts();
        }

        private void LoadContacts()
        {
            service = new ContactService();
            List<Contact> listContact = service.GetAllContacts();
            Contacts = new ObservableCollection<Contact>(listContact);
        }

        private void OnBack(object obj)
        {
            ViewModelMain.Instance.CurrentView = new MainMenuViewModel();
        }

        private void OnDelete(object obj)
        {
            if(currentContact !=null)
            {
                bool confirm = messageBox.AskForConfirmation("Are you sure you want to delete");
                if(confirm)
                {
                    bool isDeleted = service.DeleteContact(currentContact.Id);
                    if(isDeleted)
                    {
                        messageBox.ShowNotification("Contact Deleted!");
                        LoadContacts();       
                    }
                }
            }
        }

        private void OnEdit(object obj)
        {
            ViewModelMain.Instance.CurrentView = new AddUpdateViewModel(currentContact);
        }

        private void OnAdd(object obj)
        {
            ViewModelMain.Instance.CurrentView = new AddUpdateViewModel();
        }
    }
}
