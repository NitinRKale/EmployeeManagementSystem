using EmployeeManagementSystemUI.IRepository;
using EmployeeManagementSystemUI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagementSystemUI.Repository
{
    public class DepartmentMasterRepository_IQ : IDepartmentMasterRepository_IQ
    {
        private readonly string _connectionString;

        public DepartmentMasterRepository_IQ(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProjectConnectionString");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'ProjectConnectionString' not found.");
            _connectionString = connectionString;
        }

        public IEnumerable<DepartmentMaster> GetAllDepartments()
        {
            var departments = new List<DepartmentMaster>(); 
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT * FROM DepartmentMasters", conn);
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

        public DepartmentMaster GetDepartmentById(int id)
        {
            DepartmentMaster department = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT DepartmentId, DepartmentName FROM DepartmentMasters WHERE DepartmentId = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
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
        public void AddDepartment(DepartmentMaster department)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("INSERT INTO DepartmentMasters (DepartmentName) VALUES (@DepartmentName)", conn);
                cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateDepartment(DepartmentMaster department)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("UPDATE DepartmentMasters SET DepartmentName = @DepartmentName WHERE DepartmentId = @DepartmentId", conn);
                cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                cmd.Parameters.AddWithValue("@DepartmentId", department.DepartmentId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteDepartment(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("DELETE FROM DepartmentMasters WHERE DepartmentId = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<DepartmentMaster> GetAllDepartmentsUsingDataAdapter()
        {
            List<DepartmentMaster> departmentMasters = new List<DepartmentMaster>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT DepartmentId, DepartmentName FROM DepartmentMasters";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();  

                adapter.Fill(ds, "DepartmentMasters");

                var table = ds.Tables["DepartmentMasters"];
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        departmentMasters.Add(new DepartmentMaster
                        {
                            DepartmentId = Convert.ToInt32(row["DepartmentId"]),
                            DepartmentName = row["DepartmentName"]?.ToString() ?? string.Empty
                        });
                    }
                }
            }   
            return departmentMasters;
        }
    }
}
