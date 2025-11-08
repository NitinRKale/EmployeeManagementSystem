using EmployeeManagementSystemUI.IRepository;
using EmployeeManagementSystemUI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagementSystemUI.Repository
{
    public class DepartmentMasterRepository_usp : IDepartmentMasterRepository_usp
    {
        private readonly string _connectionString;

        public DepartmentMasterRepository_usp(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProjectConnectionString");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            _connectionString = connectionString;
        }

        public IEnumerable<DepartmentMaster> GetAll()
        {
            var departments = new List<DepartmentMaster>();
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("usp_DepartmentMasters_GetAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(new DepartmentMaster
                        {
                            DepartmentId = (int)reader["DepartmentId"],
                            DepartmentName = reader["DepartmentName"]?.ToString() ?? string.Empty
                        });
                    }
                }
            }
            return departments;
        }
        
        public DepartmentMaster GetById(int id)
        {
            DepartmentMaster department = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("usp_DepartmentMasters_GetById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("DepartmentId", id);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        department = new DepartmentMaster
                        {
                            DepartmentId = (int)reader["DepartmentId"],
                            DepartmentName = reader["DepartmentName"]?.ToString() ?? string.Empty
                        };
                    }
                }
            }
            return department;
        }

        public void Add(DepartmentMaster department)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("usp_DepartmentMasters_Insert", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(DepartmentMaster department)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("usp_DepartmentMasters_Update", conn))
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
            using (var cmd = new SqlCommand("usp_DepartmentMasters_Delete", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("DepartmentId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
