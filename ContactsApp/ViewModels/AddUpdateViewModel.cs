using ContactsApp.DAL;
using ContactsApp.Models;
using ContactsApp.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.ViewModels
{
    public class AddUpdateViewModel : ViewModelBase
    {
        bool isEditMode = false;
        IMessageBoxProvider messageBox = new MessageBoxProvider();
        #region Commands

        public RelayCommand CancelCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }

        #endregion

        #region Properties
        private Contact currentContact;

        public Contact CurrentContact
        {
            get { return currentContact; }
            set { currentContact = value; OnPropertyChanged("CurrentContact"); }
        }


        #endregion

        public AddUpdateViewModel()
        {
            ViewModelMain.Instance.Header = "Add Contacts";
            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(OnCancel);
            CurrentContact = new Contact();
        }

        public AddUpdateViewModel(Contact _contact)
        {
            ViewModelMain.Instance.Header = "Add Contacts";
            SaveCommand = new RelayCommand(OnSave);
            CancelCommand = new RelayCommand(OnCancel);
            CurrentContact = _contact;
            isEditMode = true;


        }
        private void OnCancel(object obj)
        {
            ViewModelMain.Instance.CurrentView = new ManageContactsViewModel();
        }

        private void OnSave(object obj)
        {
            bool isSaved;
            string message = ValidToSave();
            if (string.IsNullOrEmpty(message))
            {
                ContactService service = new ContactService();
                if (isEditMode)
                {
                    isSaved = service.UpdateContact(currentContact);
                }
                else
                {
                    isSaved = service.AddContact(CurrentContact);
                }
                if (isSaved)
                {
                    messageBox.ShowNotification("Contact Saved Successfully!");
                    ViewModelMain.Instance.CurrentView = new ManageContactsViewModel();
                }
            }
            else
            {
                messageBox.ShowNotification(message);
            }

        }

        private string ValidToSave()
        {
            if (CurrentContact.FirstName == null)
            {
                return "First Name should not be empty";
            }
            if (CurrentContact.LastName == null)
            {
                return "Last Name should not be empty";
            }
            if (CurrentContact.Email == null)
            {
                return "Email should not be empty";
            }
            if (CurrentContact.PhoneNo == null)
            {
                return "Phone No should not be empty";
            }
            return string.Empty;
        }

    }

}
