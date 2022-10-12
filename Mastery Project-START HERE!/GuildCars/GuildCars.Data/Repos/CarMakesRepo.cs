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
    public class CarMakesRepo : ICarMakesRepo
    {
        public bool IsDuplicateCarMake(string carMakeCandidate, List<ShowCarMake> carMakes)
        {
            bool isDuplicateCarMake = false;

            string makeToCheck = carMakeCandidate.ToUpper();

            foreach (var make in carMakes)
            {
                if (make.CarMakeName.ToUpper() == makeToCheck)
                {
                    isDuplicateCarMake = true;
                    return isDuplicateCarMake;
                }
            }
            return isDuplicateCarMake;
        }

        public IEnumerable<ShowCarMake> GetAll()
        {
            List<ShowCarMake> types = new List<ShowCarMake>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarMakesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ShowCarMake currentRow = new ShowCarMake();
                        currentRow.CarMakeId = (int)dr["CarMakeId"];
                        currentRow.CarMakeName = dr["CarMakeName"].ToString();
                        currentRow.UserId = dr["UserId"].ToString();
                        currentRow.CarMakeAddedDate = (DateTime)dr["CarMakeAddedDate"];
                        currentRow.UserEmail = dr["Email"].ToString();

                        types.Add(currentRow);
                    }
                }
            }
            return types;
        }

        public void Insert(CarMake carMake)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CarMakesInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@CarMakeId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@CarMakeName", carMake.CarMakeName);
                cmd.Parameters.AddWithValue("@UserId", carMake.UserId);

                cn.Open();

                cmd.ExecuteNonQuery();

                carMake.CarMakeId = (int)param.Value;

            }
        }

        public bool AddMakeIfNotDuplicate(CarMake carMake)
        {
            bool addSuccessful = false;
            //load List of Makes
            var carMakes = GetAll().ToList();

            //Check if the candidate Make Name is a duplicate
            bool isDuplicate = IsDuplicateCarMake(carMake.CarMakeName, carMakes);

            //Add the make if not a duplicate
            if (isDuplicate == false)
            {
                Insert(carMake);
                addSuccessful = true;
                return addSuccessful;
            }
            return addSuccessful;
        }
    }
}
