using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using ContactsApp.Models;

namespace ContactsApp.DAL
{
    public class ContactService
    {
        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        public ContactService()
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ContactConnectionString"].ConnectionString);
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
        }

        public List<Contact> GetAllContacts()
        {
            List<Contact> contactList = new List<Contact>();
            try
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "sp_GetAllContacts";
                sqlConnection.Open();
                var sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    Contact contact = null;
                    while (sqlDataReader.Read())
                    {
                        contact = new Contact();
                        contact.FirstName = Convert.IsDBNull(sqlDataReader["FirstName"]) == true ? string.Empty : Convert.ToString(sqlDataReader["FirstName"]);
                        contact.LastName = Convert.IsDBNull(sqlDataReader["LastName"]) == true ? string.Empty : Convert.ToString(sqlDataReader["LastName"]);
                        contact.Email = Convert.IsDBNull(sqlDataReader["Email"]) == true ? string.Empty : Convert.ToString(sqlDataReader["Email"]);
                        contact.PhoneNo = Convert.IsDBNull(sqlDataReader["Phone"]) == true ? string.Empty : Convert.ToString(sqlDataReader["Phone"]);
                        //contact.Status = Convert.IsDBNull(sqlDataReader["Status"]) == true ? ContactStatus.InActive : (ContactStatus)Enum.ToObject(typeof(ContactStatus), Convert.ToInt16(sqlDataReader["Status"]));
                        contact.Status = Convert.IsDBNull(sqlDataReader["Status"]) == true ? false: Convert.ToBoolean(sqlDataReader["Status"]);
                        contact.Id = Convert.IsDBNull(sqlDataReader["Id"]) == true ? 0 : Convert.ToInt16(sqlDataReader["Id"]);
                        contactList.Add(contact);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                sqlConnection.Close();
            }

            return contactList;
        }

        public bool AddContact(Contact _contact)
        {
            bool isAdded = false;
            try
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "sp_AddContacts";
                sqlCommand.Parameters.AddWithValue("@FirstName", _contact.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", _contact.LastName);
                sqlCommand.Parameters.AddWithValue("@Email", _contact.Email);
                sqlCommand.Parameters.AddWithValue("@PhoneNo", _contact.PhoneNo);
                sqlCommand.Parameters.AddWithValue("@Status", _contact.Status);
                sqlConnection.Open();
                int noOfRowsAffected = sqlCommand.ExecuteNonQuery();
                isAdded = noOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {

                
            }
            finally
            {
                sqlConnection.Close();
            }
            return isAdded;
        }
        public bool UpdateContact(Contact _contact)
        {
            bool isUpdated = false;
            try
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "sp_UpdateContact";
                sqlCommand.Parameters.AddWithValue("@Id", _contact.Id);
                sqlCommand.Parameters.AddWithValue("@FirstName", _contact.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", _contact.LastName);
                sqlCommand.Parameters.AddWithValue("@Email", _contact.Email);
                sqlCommand.Parameters.AddWithValue("@PhoneNo", _contact.PhoneNo);
                sqlCommand.Parameters.AddWithValue("@Status", _contact.Status);
                sqlConnection.Open();
                int noOfRowsAffected = sqlCommand.ExecuteNonQuery();
                isUpdated = noOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {

                
            }
            finally
            {
                sqlConnection.Close();
            }
            return isUpdated;
        }
        public bool DeleteContact(int id)
        {
            bool isDeleted = false;
            try
            {
                sqlCommand.Parameters.Clear();
                sqlCommand.CommandText = "sp_DeleteContact";
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlConnection.Open();
                int noOfRowsAffected = sqlCommand.ExecuteNonQuery();
                isDeleted = noOfRowsAffected > 0;
            }
            catch (SqlException ex)
            {

            }
            finally
            {
                sqlConnection.Close();
            }
            return isDeleted;
        }
    }
}
