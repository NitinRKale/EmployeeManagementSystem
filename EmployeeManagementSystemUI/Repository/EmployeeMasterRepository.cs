using EmployeeManagementSystemUI.IRepository;
using EmployeeManagementSystemUI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagementSystemUI.Repository
{
    public class EmployeeMasterRepository : IEmployeeMasterUSP
    {
        private readonly string _connectionString;

        public EmployeeMasterRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProjectConnectionString");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'ProjectConnectionString' not found.");
            _connectionString = connectionString;
        }
        public IEnumerable<EmployeeMasterADO> getAllEmployees()
        {
            var employeeMasters = new List<EmployeeMasterADO>();
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_EmployeeMasters_SelectAll";
                cmd.Connection = conn;

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employeeMasters.Add(new EmployeeMasterADO
                        {
                            EmpId = (int)reader["EmpId"],
                            EmpFirstName = reader["EmpFirstName"]?.ToString() ?? string.Empty,
                            EmpMiddleName = reader["EmpMiddleName"]?.ToString() ?? string.Empty,
                            EmpLastName = reader["EmpLastName"]?.ToString() ?? string.Empty,
                            EmailId = reader["EmailId"]?.ToString() ?? string.Empty,
                            BirthDate = reader["BirthDate"] as DateTime?,
                            EmpGender = reader["EmpGender"]?.ToString() ?? string.Empty,
                            PhoneNumber = reader["PhoneNumber"]?.ToString() ?? string.Empty,
                            EmployeeAddress = reader["EmployeeAddress"]?.ToString() ?? string.Empty,
                            Salary = reader["Salary"] != DBNull.Value ? (decimal)reader["Salary"] : 0,
                            EmpStatus = reader["EmpStatus"] != DBNull.Value && (bool)reader["EmpStatus"],
                            DepartmentId = reader["DepartmentId"] != DBNull.Value ? (int)reader["DepartmentId"] : 0,
                            DesignationId = reader["DesignationId"] != DBNull.Value ? (int)reader["DesignationId"] : 0,
                            DepartmentName = reader["DepartmentName"]?.ToString() ?? string.Empty,
                            DesignationName = reader["DesignationName"]?.ToString() ?? string.Empty
                        });
                    }
                }
            }
            return employeeMasters;
        }

        
        public EmployeeMasterADO getEmployeeDetailsById(int Id)
        {
            var employeeMasters = new EmployeeMasterADO();
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_EmployeeMasters_SelectById";
                cmd.Parameters.AddWithValue("@EmpId", Id);
                cmd.Connection = conn;

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employeeMasters = new EmployeeMasterADO
                        {
                            EmpId = (int)reader["EmpId"],
                            EmpFirstName = reader["EmpFirstName"]?.ToString() ?? string.Empty,
                            EmpMiddleName = reader["EmpMiddleName"]?.ToString() ?? string.Empty,
                            EmpLastName = reader["EmpLastName"]?.ToString() ?? string.Empty,
                            EmailId = reader["EmailId"]?.ToString() ?? string.Empty,
                            BirthDate = reader["BirthDate"] as DateTime?,
                            EmpGender = reader["EmpGender"]?.ToString() ?? string.Empty,
                            PhoneNumber = reader["PhoneNumber"]?.ToString() ?? string.Empty,
                            EmployeeAddress = reader["EmployeeAddress"]?.ToString() ?? string.Empty,
                            Salary = reader["Salary"] != DBNull.Value ? (decimal)reader["Salary"] : 0,
                            EmpStatus = reader["EmpStatus"] != DBNull.Value && (bool)reader["EmpStatus"],
                            DepartmentId = reader["DepartmentId"] != DBNull.Value ? (int)reader["DepartmentId"] : 0,
                            DesignationId = reader["DesignationId"] != DBNull.Value ? (int)reader["DesignationId"] : 0,
                            DepartmentName = reader["DepartmentName"]?.ToString() ?? string.Empty,
                            DesignationName = reader["DesignationName"]?.ToString() ?? string.Empty
                        };
                    }
                }
            }
            return employeeMasters;

        }
        public void addEmployeeDetails(EmployeeMasterADO department)
        {
            throw new NotImplementedException();
        }

        public void updateEmployeeDetails(EmployeeMasterADO department)
        {
            throw new NotImplementedException();
        }

        public void deleteEmployeeDetails(int id)
        {
            throw new NotImplementedException();
        }
    }
}
