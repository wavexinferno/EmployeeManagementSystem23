using EmployeeManagementSystem.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataAccess
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            var employees = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("spGetAllEmployees", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            employees.Add(new Employee
                            {
                                EmployeeId = (int)row["EmployeeId"],
                                FirstName = (string)row["FirstName"],
                                LastName = (string)row["LastName"],
                                DepartmentId = (int)row["DepartmentId"],
                                RoleId = (int)row["RoleId"],
                                Email = (string)row["Email"],
                                Phone = (string)row["Phone"]
                            });
                        }
                    }
                }
            }

            return await Task.FromResult(employees);
        }

        public async Task<bool> InsertEmployeeAsync(Employee employee)
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                using (SqlCommand command = new SqlCommand("spInsertEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);
                    command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                    command.Parameters.AddWithValue("@RoleId", employee.RoleId);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Phone", employee.Phone);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result == 0 ? false : true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(_connectionString);

                using (SqlCommand command = new SqlCommand("spUpdateEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);
                    command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                    command.Parameters.AddWithValue("@RoleId", employee.RoleId);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Phone", employee.Phone);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    return result == 0 ? false : true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                if (connection != null)
                {
                   connection.Close();
                }
            }
        }


        public async Task<bool> DeleteEmployeeAsync(int employeeId)
        {
            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(_connectionString);
                using (SqlCommand command = new SqlCommand("spDeleteEmployee", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    return result == 0 ? false : true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
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
