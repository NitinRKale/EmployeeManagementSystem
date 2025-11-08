using EmployeeManagementSystemUI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagementSystemUI.DataAccessLayer
{
    public class DepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProjectConnectionString");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            _connectionString = connectionString;
        }

        public IEnumerable<DepartmentMaster2> GetAll()
        {
            var departments = new List<DepartmentMaster2>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_DepartmentMasters_GetAll", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {                       
                        departments.Add(new DepartmentMaster2
                        {
                            DepartmentId = (int)reader["DepartmentId"],
                            DepartmentName = reader["DepartmentName"]?.ToString() ?? string.Empty
                        });
                    }   
                }
            }
            return departments;
        }

        public DepartmentMaster2 GetById(int id)
        {
            DepartmentMaster2 department = null;
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_DepartmentMasters_GetById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentId", id);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        department = new DepartmentMaster2
                        {
                            DepartmentId = (int)reader["DepartmentId"],
                            DepartmentName = reader["Name"]?.ToString() ?? string.Empty
                        };
                    }
                }
            }
            return department;
        }

        public void Insert(DepartmentMaster2 department)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_DepartmentMasters_Insert", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(DepartmentMaster2 department)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_DepartmentMasters_Update", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentId", department.DepartmentId);
                cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_DepartmentMasters_Delete", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
