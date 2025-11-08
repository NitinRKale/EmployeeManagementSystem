using EmployeeManagementSystemUI.Models;
using EmployeeManagementSystemUI.IRepository;
using Microsoft.Data.SqlClient;

namespace EmployeeManagementSystemUI.Repository
{
    public class DesignationMasterRepository_IQ : IDesignationMasterRepository_IQ
    {
        private readonly string _connectionString;

        public DesignationMasterRepository_IQ(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProjectConnectionString");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            _connectionString = connectionString;
        }

        public IEnumerable<DesignationMaster> GetAll()
        {
            var designations = new List<DesignationMaster>();
            using (var conn = new SqlConnection(_connectionString)) // Uses Microsoft.Data.SqlClient.SqlConnection
            {
                var cmd = new SqlCommand("SELECT DesignationId, DesignationName FROM DesignationMasters", conn);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        designations.Add(new DesignationMaster
                        {
                            DesignationId = (int)reader["DesignationId"],
                            DesignationName = reader["DesignationName"]?.ToString() ?? string.Empty
                        });
                    }
                }
            }
            return designations;
        }

        public DesignationMaster GetById(int id)
        {
            DesignationMaster designation = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT DesignationId, DesignationName FROM DesignationMasters WHERE DesignationId = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        designation = new DesignationMaster
                        {
                            DesignationId = (int)reader["DesignationId"],
                            DesignationName = reader["DesignationName"]?.ToString() ?? string.Empty
                        };
                    }
                }
            }
            return designation;
        }
        public void Add(DesignationMaster designationMaster)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("INSERT INTO DesignationMasters (DesignationName) VALUES (@DesignationName)", conn);
                cmd.Parameters.AddWithValue("@DesignationName", designationMaster.DesignationName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void Update(DesignationMaster designationMaster)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("UPDATE DesignationMasters SET DesignationName = @DesignationName WHERE DesignationId = @DesignationId", conn);
                cmd.Parameters.AddWithValue("@DesignationName", designationMaster.DesignationName);
                cmd.Parameters.AddWithValue("@DesignationId", designationMaster.DesignationId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("DELETE FROM DesignationMasters WHERE DesignationId = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
