using EmployeeManagementSystem.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace EmployeeManagementSystem.DataAccess
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public User ValidateUser(User user)
        {
            User _user = null;

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(_connectionString);

                SqlCommand command = new SqlCommand("spAuthenticateUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                connection.Open();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    _user = new User
                    {
                        Username = Convert.ToString(row["Username"]),
                        Password = Convert.ToString(row["Password"])
                    };
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return _user;
        }

        public bool InsertUser(User user)
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(_connectionString);

                SqlCommand command = new SqlCommand("spInsertUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result == 0 ? false : true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


    }
}
