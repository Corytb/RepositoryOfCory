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
    public class CarModelsRepo : ICarModelsRepo
    {
        public IEnumerable<ShowCarModel> GetAll()
        {
            List<ShowCarModel> models = new List<ShowCarModel>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarModelsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ShowCarModel currentRow = new ShowCarModel();
                        currentRow.CarMakeId = (int)dr["CarMakeId"];
                        currentRow.CarMakeName = dr["CarMakeName"].ToString();
                        currentRow.CarModelId = (int)dr["CarModelId"];
                        currentRow.CarModelName = dr["CarModelName"].ToString();
                        currentRow.CarModelAddedDate = (DateTime)dr["CarModelAddedDate"];
                        currentRow.UserId = dr["UserId"].ToString();
                        currentRow.UserEmail = dr["Email"].ToString();

                        models.Add(currentRow);
                    }
                }
            }
            return models;
        }

        public IEnumerable<ShowCarModel> GetModelsByMake(int carMakeId)
        {
            List<ShowCarModel> models = new List<ShowCarModel>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarModelsSelectByMake", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CarMakeId", carMakeId);

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ShowCarModel currentRow = new ShowCarModel();
                        currentRow.CarModelId = (int)dr["CarModelId"];
                        currentRow.CarModelName = dr["CarModelName"].ToString();
                        currentRow.CarMakeId = (int)dr["CarMakeId"];
                        currentRow.CarMakeName = dr["CarMakeName"].ToString();
                        currentRow.CarModelAddedDate = (DateTime)dr["CarModelAddedDate"];
                        currentRow.UserId = dr["UserId"].ToString();
                        currentRow.UserEmail = dr["Email"].ToString();

                        models.Add(currentRow);
                    }
                }
            }
            return models;
        }

        public void Insert(CarModel carModel)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarModelsInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@CarModelId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@CarMakeId", carModel.CarMakeId);
                cmd.Parameters.AddWithValue("@CarModelName", carModel.CarModelName);
                cmd.Parameters.AddWithValue("@UserId", carModel.UserId);

                cn.Open();

                cmd.ExecuteNonQuery();

                carModel.CarModelId = (int)param.Value;

            }
        }

        public bool IsDuplicateCarModel(string carModelCandidate, int carMakeId)
        {
            List<ShowCarModel> carModels = GetAll().ToList();
            bool isDuplicateCarModel = false;

            string modelToCheck = carModelCandidate.ToUpper();

            foreach (var make in carModels)
            {
                if (make.CarModelName.ToUpper() == modelToCheck && make.CarMakeId == carMakeId)
                {
                    isDuplicateCarModel = true;
                    return isDuplicateCarModel;
                }
            }
            return isDuplicateCarModel;
        }
    }
}
