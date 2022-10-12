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
    public class CarBodiesRepo : ICarBodiesRepo
    {
        public List<CarBody> GetAll()
        {
            List<CarBody> types = new List<CarBody>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarBodiesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarBody currentRow = new CarBody();
                        currentRow.CarBodyTypeId = (int)dr["CarBodyTypeId"];
                        currentRow.CarBodyType = dr["CarBodyType"].ToString();

                        types.Add(currentRow);
                    }
                }
            }
            return types;
        }
    }
}
