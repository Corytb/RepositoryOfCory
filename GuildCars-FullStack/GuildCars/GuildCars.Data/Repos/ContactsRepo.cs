using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repos
{
    public class ContactsRepo : IContactsRepo
    {
        public void DeleteContact(int contactId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactsDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ContactId", contactId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public List<Contact> GetAllContacts()
        {
            List<Contact> contacts = new List<Contact>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contact currentRow = new Contact();
                        currentRow.ContactId = (int)dr["ContactId"];
                        currentRow.ContactName = dr["ContactName"].ToString();
                        currentRow.ContactEmail = dr["ContactEmail"].ToString();
                        currentRow.ContactPhone = dr["ContactPhone"].ToString();
                        currentRow.ContactMessage = dr["ContactMessage"].ToString();
                        currentRow.ContactSentDate = (DateTime)dr["ContactSentDate"];

                        contacts.Add(currentRow);
                    }
                }
            }
            return contacts;
        }

        public void InsertNewContact(Contact contact)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ContactsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@ContactId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@ContactName", contact.ContactName);
                if (string.IsNullOrEmpty(contact.ContactEmail))
                {
                    cmd.Parameters.AddWithValue("@ContactEmail", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactEmail", contact.ContactEmail);
                }
                if (string.IsNullOrEmpty(contact.ContactPhone))
                {
                    cmd.Parameters.AddWithValue("@ContactPhone", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ContactPhone", contact.ContactPhone);
                }

                cmd.Parameters.AddWithValue("@ContactMessage", contact.ContactMessage);

                cn.Open();

                cmd.ExecuteNonQuery();

                contact.ContactId = (int)param.Value;
            }
        }
    }
}
