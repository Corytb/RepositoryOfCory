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
    public class ColorsRepo : IColorsRepo
    {
        public List<Color> GetAll()
        {
            List<Color> colors = new List<Color>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ColorsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Color currentRow = new Color();
                        currentRow.ColorTypeId = (int)dr["ColorTypeId"];
                        currentRow.ColorType = dr["ColorType"].ToString();

                        colors.Add(currentRow);
                    }
                }
            }
            return colors;
        }
    }
}
