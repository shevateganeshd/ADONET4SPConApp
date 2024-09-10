using System;
using System.Data;
using System.Data.SqlClient;


namespace ADONET4SPConApp
{
    public class EmployeeRepository
    {
        readonly string connectionString = "Data Source=DESKTOP-S87FPOF\\SQLEXPRESS;Initial Catalog=CRM;UID=sa;Password=ABC@;TrustServerCertificate=True;MultipleActiveResultSets=True;Integrated Security=True;Trusted_Connection=True;";

        public void CreateEmployee()
        {
            Console.Write("Name : ");
            string name = Console.ReadLine();

            Console.Write("Address : ");
            string address = Console.ReadLine();

            Console.Write("Phone : ");
            string phoneNo = Console.ReadLine();

            Console.Write("BirthDate yyyy-MM-dd: ");
            DateTime birthDate = DateTime.Parse(Console.ReadLine());

            Console.Write("IsActive true/false: ");
            bool isActive = Boolean.Parse(Console.ReadLine());

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Name", name);
                cmd.Parameters.AddWithValue("Address", address);
                cmd.Parameters.AddWithValue("PhoneNo", phoneNo);
                cmd.Parameters.AddWithValue("BirthDate", birthDate);
                cmd.Parameters.AddWithValue("IsActive", isActive);

                cmd.ExecuteNonQuery();

                Console.WriteLine("Employee Added Successfully");
            }
        }

        public void ReadEmployees()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmdEmployee = new SqlCommand("ReadEmployees", con);
                cmdEmployee.CommandType = System.Data.CommandType.StoredProcedure;

                using (SqlDataReader reader = cmdEmployee.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Id: {reader["Id"]}, Name: {reader["Name"]}, Address: {reader["Address"]}, Phone: {reader["PhoneNo"]}, BirthDate: {reader["BirthDate"]}, IsActive: {reader["IsActive"]}");
                    }
                    reader.Close();
                    con.Close();
                }
            }
        }

        public void ReadEmployee()
        {
            Console.Write("Id : ");
            int Id = int.Parse(Console.ReadLine());

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ReadEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Id", Id);

                using (SqlDataReader readerEmployee1 = cmd.ExecuteReader())
                {
                    if (readerEmployee1.Read())
                    {
                        Console.WriteLine($"Id: {readerEmployee1["Id"]}");
                        Console.WriteLine($"Name: {readerEmployee1["Name"]}");
                        Console.WriteLine($"Address: {readerEmployee1["Address"]}");
                        Console.WriteLine($"PhoneNo: {readerEmployee1["PhoneNo"]}");
                        Console.WriteLine($"BirthDate: {readerEmployee1["BirthDate"]}");
                        Console.WriteLine($"IsActive: {readerEmployee1["IsActive"]}");
                    }
                    else
                        Console.WriteLine("No record found with Id : " + Id);
                }
            }
        }

        public void UpdateEmployee()
        {
            Console.Write("Id : ");
            int Id = int.Parse(Console.ReadLine());

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmdEmployee = new SqlCommand("ReadEmployee", con);
                cmdEmployee.CommandType = System.Data.CommandType.StoredProcedure;
                cmdEmployee.Parameters.AddWithValue("Id", Id);
                SqlDataReader readerEmployee = cmdEmployee.ExecuteReader();
                if (readerEmployee.HasRows == true)
                {
                    readerEmployee.Close();

                    Console.Write("Name : ");
                    string Name = Console.ReadLine();

                    Console.Write("Address : ");
                    string Address = Console.ReadLine();

                    Console.Write("Phone : ");
                    string PhoneNo = Console.ReadLine();

                    Console.Write("BirthDate yyyy-MM-dd: ");
                    DateTime BirthDate = DateTime.Parse(Console.ReadLine());

                    Console.Write("IsActive true/false: ");
                    bool IsActive = Boolean.Parse(Console.ReadLine());

                    SqlCommand cmd = new SqlCommand("UpdateEmployee", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id", Id);
                    cmd.Parameters.AddWithValue("Name", Name);
                    cmd.Parameters.AddWithValue("Address", Address);
                    cmd.Parameters.AddWithValue("PhoneNo", PhoneNo);
                    cmd.Parameters.AddWithValue("BirthDate", BirthDate);
                    cmd.Parameters.AddWithValue("IsActive", IsActive);

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("Employee Updated Successfully");
                }
                else
                    Console.WriteLine("No record found with Id: " + Id);
            }
        }

        
        public void DeleteEmployee()
        {
            Console.Write("Id : ");
            int Id = int.Parse(Console.ReadLine());

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string getEmployeeQuery = "SELECT * FROM Employee WHERE Id=" + Id;
                SqlCommand cmdEmployee = new SqlCommand(getEmployeeQuery, con);
                SqlDataReader readerEmployee = cmdEmployee.ExecuteReader();

                if (readerEmployee.HasRows == true)
                {
                    readerEmployee.Close();

                    SqlCommand cmd = new SqlCommand("DeleteEmployee", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id", Id);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Employee Deleted Successfully");
                }
                else
                    Console.WriteLine("No record found with Id : " + Id);
            }
        }
    }
}
