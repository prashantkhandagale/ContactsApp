using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Models
{
    public class Contact : INotifyPropertyChanged
    {
        #region Interface Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        #endregion

        #region Properties

        private int _id;

        public int Id 
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private string _firstName;

        [Required(ErrorMessage="Enter First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The username must only contain letters (a-z, A-Z).")]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _lastName;

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private string _email;

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private string _phoneNo;

        public string PhoneNo
        {
            get
            {
                return _phoneNo;
            }
            set
            {
                _phoneNo = value;
                OnPropertyChanged("PhoneNo");
            }
        }
        


        private bool _status;

        public bool Status
        {
            get
            {
                return _status;
            }
            set
            {

                _status = value;
                OnPropertyChanged("Status");
            }
        }
        #endregion

    }
}
