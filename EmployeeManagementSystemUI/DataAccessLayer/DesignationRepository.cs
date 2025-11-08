using EmployeeManagementSystemUI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagementSystemUI.DataAccessLayer
{
    public class DesignationRepository
    {
        private readonly string _connectionString;

        public DesignationRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProjectConnectionString");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            _connectionString = connectionString;
        }

        public IEnumerable<DesignationMaster2> GetAll()
        {
            var designations = new List<DesignationMaster2>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_Designation_GetAll", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        designations.Add(new DesignationMaster2
                        {
                            DesignationId = (int)reader["DesignationId"],
                            DesignationName = reader["DesignationName"] as string ?? string.Empty
                        });
                    }                    
                }   
            }
            return designations;
        }

        public DesignationMaster2? GetById(int id)
        {
            DesignationMaster2? designation = null;
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_Designation_GetById", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DesignationId", id);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        designation = new DesignationMaster2
                        {
                            DesignationId = (int)reader["DesignationId"],
                            DesignationName = reader["DesignationName"] as string ?? string.Empty
                        };
                    }
                }
            }
            return designation;
        }

        public void Insert(DesignationMaster2 designation)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_Designation_Insert", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DesignationName", designation.DesignationName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(DesignationMaster2 designation)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_Designation_Update", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DesignationId", designation.DesignationId);
                cmd.Parameters.AddWithValue("@DesignationName", designation.DesignationName);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("sp_Designation_Delete", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DesignationId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}