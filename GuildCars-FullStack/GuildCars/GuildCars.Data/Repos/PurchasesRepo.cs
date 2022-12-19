using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
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
    public class PurchasesRepo : IPurchasesRepo
    {
        public PurchaseItem GetPurchaseById(int purchaseId)
        {
            PurchaseItem purchase = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PurchaseSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PurchaseId", purchaseId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        purchase = new PurchaseItem();
                        purchase.PurchaseId = (int)dr["PurchaseId"];
                        purchase.PurchasePaymentTypeId = (int)dr["PaymentTypeId"];
                        purchase.PurchasePaymentType = dr["PayType"].ToString();
                        purchase.UserId = dr["UserId"].ToString();
                        purchase.VehicleId = (int)dr["VehicleId"];
                        purchase.PurchasePrice = (decimal)dr["PurchasePrice"];
                        purchase.PurchaseDate = (DateTime)dr["PurchaseDate"];
                        purchase.CustomerName = dr["CustomerName"].ToString();
                        purchase.CustomerPhone = dr["CustomerPhone"].ToString();
                        purchase.CustomerEmail = dr["CustomerEmail"].ToString();
                        purchase.CustomerStreet1 = dr["CustomerStreet1"].ToString();
                        purchase.CustomerStreet2 = dr["CustomerStreet2"].ToString();
                        purchase.CustomerCity = dr["CustomerCity"].ToString();
                        purchase.StateId = dr["StateId"].ToString();
                        purchase.CustomerZip = dr["CustomerZip"].ToString();
                    }
                }
            }
            return purchase;
        }

        public void InsertNewPurchase(Purchase purchase)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PurchasesInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@PurchaseId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@PurchasePaymentTypeId", purchase.PurchasePaymentTypeId);
                cmd.Parameters.AddWithValue("@UserId", purchase.UserId);
                cmd.Parameters.AddWithValue("@VehicleId", purchase.VehicleId);
                cmd.Parameters.AddWithValue("@PurchasePrice", purchase.PurchasePrice);
                cmd.Parameters.AddWithValue("@CustomerName", purchase.CustomerName);
                if (string.IsNullOrEmpty(purchase.CustomerPhone))
                {
                    cmd.Parameters.AddWithValue("@CustomerPhone", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustomerPhone", purchase.CustomerPhone);
                }

                if (string.IsNullOrEmpty(purchase.CustomerEmail))
                {
                    cmd.Parameters.AddWithValue("@CustomerEmail", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustomerEmail", purchase.CustomerEmail);
                }

                cmd.Parameters.AddWithValue("@CustomerStreet1", purchase.CustomerStreet1);

                if (string.IsNullOrEmpty(purchase.CustomerStreet2))
                {
                    cmd.Parameters.AddWithValue("@CustomerStreet2", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustomerStreet2", purchase.CustomerStreet2);
                }
                cmd.Parameters.AddWithValue("@CustomerCity", purchase.CustomerCity);
                cmd.Parameters.AddWithValue("@StateId", purchase.StateId);
                cmd.Parameters.AddWithValue("@CustomerZip", purchase.CustomerZip);

                cn.Open();

                cmd.ExecuteNonQuery();

                purchase.PurchaseId= (int)param.Value;
            }
        }

    }
}
