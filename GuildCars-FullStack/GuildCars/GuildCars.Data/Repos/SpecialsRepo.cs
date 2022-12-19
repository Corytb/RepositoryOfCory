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
    public class SpecialsRepo : ISpecialsRepo
    {
        public List<Special> GetAll()
        {
            List<Special> specials = new List<Special>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SpecialsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Special currentRow = new Special();
                        currentRow.SpecialId = (int)dr["SpecialId"];
                        currentRow.SpecialTitle = dr["SpecialTitle"].ToString();
                        currentRow.SpecialDescription = dr["SpecialDescription"].ToString();
                        currentRow.SpecialImageFileName = dr["SpecialImageFileName"].ToString();

                        specials.Add(currentRow);
                    }
                }
            }
            return specials;
        }

        public Special GetSpecialImageById(int specialId)
        {
            Special special = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetSpecialImageById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SpecialId", specialId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        special = new Special();
                        special.SpecialId = (int)dr["SpecialId"];
                        special.SpecialImageFileName = dr["SpecialImageFileName"].ToString();
                    }
                }
            }
            return special;
        }

        public void SpecialsDelete(int specialId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SpecialsDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SpecialId", specialId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void SpecialsInsert(Special special)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SpecialsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@SpecialId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@SpecialTitle", special.SpecialTitle);
                cmd.Parameters.AddWithValue("@SpecialDescription", special.SpecialDescription);

                if (string.IsNullOrEmpty(special.SpecialImageFileName))
                {

                    cmd.Parameters.AddWithValue("@SpecialImageFileName", DBNull.Value);
                }
                else
                {

                    cmd.Parameters.AddWithValue("@SpecialImageFileName", special.SpecialImageFileName);
                }

                cn.Open();

                cmd.ExecuteNonQuery();

                special.SpecialId = (int)param.Value;
            }
        }
    }
}
