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
    public class PaymentTypesRepo : IPaymentTypesRepo
    {
        public List<PaymentType> GetAll()
        {
            List<PaymentType> types = new List<PaymentType>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PaymentTypesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PaymentType currentRow = new PaymentType();
                        currentRow.PaymentTypeId = (int)dr["PaymentTypeId"];
                        currentRow.PayType = dr["PayType"].ToString();

                        types.Add(currentRow);
                    }
                }
            }
            return types;
        }
    }
}
