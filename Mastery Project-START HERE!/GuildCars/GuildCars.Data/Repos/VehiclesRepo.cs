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
    public class VehiclesRepo : IVehiclesRepo
    {
        public IEnumerable<VehicleShortItem> GetFeaturedVehicles()
        {
            List<VehicleShortItem> vehicles = new List<VehicleShortItem>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SelectFeaturedVehicles", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleShortItem currentRow = new VehicleShortItem();
                        currentRow.VehicleId = (int)dr["VehicleId"];
                        currentRow.VinNumber = dr["VinNumber"].ToString();
                        currentRow.VehicleImageFileName = dr["VehicleImageFileName"].ToString();
                        currentRow.ModelYear = dr["ModelYear"].ToString();
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.CarMakeId = (int)dr["CarMakeId"];
                        currentRow.CarMakeName = dr["CarMakeName"].ToString();
                        currentRow.CarModelId = (int)dr["CarModelId"];
                        currentRow.CarModelName = dr["CarModelName"].ToString();

                        vehicles.Add(currentRow);
                    }
                }
            }
            return vehicles;
        }

        public VehicleDetails GetVehicleViewObjectById(int vehicleId)
        {
            VehicleDetails vehicle = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SelectVehicleDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        vehicle = new VehicleDetails();
                        vehicle.VehicleId = (int)dr["VehicleId"];
                        vehicle.VinNumber = dr["VinNumber"].ToString();
                        vehicle.ModelYear = dr["ModelYear"].ToString();
                        vehicle.MSRP = (decimal)dr["MSRP"];
                        vehicle.SalePrice = (decimal)dr["SalePrice"];
                        vehicle.VehicleDescription = dr["VehicleDescription"].ToString();
                        vehicle.VehicleImageFileName = dr["VehicleImageFileName"].ToString();
                        vehicle.IsNewCar = (bool)dr["IsNewCar"];
                        vehicle.IsFeaturedCar = (bool)dr["IsFeaturedCar"];
                        vehicle.VehicleMileage = (int)dr["VehicleMileage"];
                        vehicle.BodyColorTypeId = (int)dr["BodyColorTypeId"];
                        vehicle.BodyColorType = dr["BodyColor"].ToString();
                        vehicle.InteriorColorTypeId = (int)dr["InteriorColorTypeId"];
                        vehicle.InteriorColorType = dr["InteriorColor"].ToString();
                        vehicle.CarBodyTypeId = (int)dr["CarBodyTypeId"];
                        vehicle.CarBodyType = dr["CarBodyType"].ToString();
                        vehicle.CarMakeId = (int)dr["CarMakeId"];
                        vehicle.CarMakeName = dr["CarMakeName"].ToString();
                        vehicle.CarModelId = (int)dr["CarModelId"];
                        vehicle.CarModelName = dr["CarModelName"].ToString();
                        vehicle.TransmissionTypeId = (int)dr["TransmissionTypeId"];
                        vehicle.TransmissionType = dr["TransmissionType"].ToString();
                        vehicle.IsPurchasedCar = (bool)dr["IsPurchasedCar"];
                    }
                }
            }
            return vehicle;
        }

        public Vehicle GetVehicleTableObjectById(int vehicleId)
        {
            Vehicle vehicle = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SelectVehicleDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        vehicle = new Vehicle();
                        vehicle.VehicleId = (int)dr["VehicleId"];
                        vehicle.VinNumber = dr["VinNumber"].ToString();
                        vehicle.ModelYear = dr["ModelYear"].ToString();
                        vehicle.MSRP = (decimal)dr["MSRP"];
                        vehicle.SalePrice = (decimal)dr["SalePrice"];
                        vehicle.VehicleDescription = dr["VehicleDescription"].ToString();
                        vehicle.VehicleImageFileName = dr["VehicleImageFileName"].ToString();
                        vehicle.IsNewCar = (bool)dr["IsNewCar"];
                        vehicle.IsFeaturedCar = (bool)dr["IsFeaturedCar"];
                        vehicle.VehicleMileage = (int)dr["VehicleMileage"];
                        vehicle.BodyColorTypeId = (int)dr["BodyColorTypeId"];
                        vehicle.InteriorColorTypeId = (int)dr["InteriorColorTypeId"];
                        vehicle.CarBodyTypeId = (int)dr["CarBodyTypeId"];
                        vehicle.CarMakeId = (int)dr["CarMakeId"];
                        vehicle.CarModelId = (int)dr["CarModelId"];
                        vehicle.TransmissionTypeId = (int)dr["TransmissionTypeId"];
                        vehicle.IsPurchasedCar = (bool)dr["IsPurchasedCar"];
                    }
                }
            }
            return vehicle;
        }

        public IEnumerable<VehicleDetails> SearchNew(VehicleSearchParameters parameters)
        {
            List<VehicleDetails> vehicles = new List<VehicleDetails>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT TOP 20 v.VehicleId, v.VinNumber, v.ModelYear, v.MSRP, v.SalePrice, v.VehicleDescription, v.VehicleImageFileName, " +
                    "v.IsNewCar, v.VehicleMileage, v.BodyColorTypeId, bodyColor.ColorType AS BodyColor, v.InteriorColorTypeId, " +
                    "interiorColor.ColorType AS InteriorColor, v.CarBodyTypeId, body.CarBodyType, v.CarMakeId, make.CarMakeName, " +
                    "v.CarModelId, model.CarModelName, v.TransmissionTypeId, t.TransmissionType " +
                    "FROM Vehicles v " +
                    "LEFT JOIN Colors bodyColor ON v.BodyColorTypeId = bodyColor.ColorTypeId " +
                    "LEFT JOIN Colors interiorColor ON v.InteriorColorTypeId = interiorColor.ColorTypeId " +
                    "LEFT JOIN CarBodies body ON v.CarBodyTypeId = body.CarBodyTypeId " +
                    "LEFT JOIN CarMakes make ON v.CarMakeId = make.CarMakeId " +
                    "LEFT JOIN CarModels model ON v.CarModelId = model.CarModelId " +
                    "LEFT JOIN Transmissions t ON v.TransmissionTypeId = t.TransmissionTypeId " +
                    "WHERE v.IsNewCar = 1 AND v.IsPurchasedCar = 0 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (!string.IsNullOrEmpty(parameters.GeneralSearchParam) 
                    || parameters.MinPrice.HasValue || parameters.MaxPrice.HasValue 
                    || parameters.MinYear.HasValue || parameters.MaxYear.HasValue)
                {

                    if (parameters.MinPrice.HasValue)
                    {
                        query += "AND SalePrice >= @MinPrice ";
                        cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                    }
                    if (parameters.MaxPrice.HasValue)
                    {
                        query += "AND SalePrice <= @MaxPrice ";
                        cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                    }
                    if (parameters.MinYear.HasValue)
                    {
                        query += "AND ModelYear >= @MinYear ";
                        cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear.Value);
                    }
                    if (parameters.MaxYear.HasValue)
                    {
                        query += "AND ModelYear <= @MaxYear ";
                        cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear.Value);
                    }
                    if (!string.IsNullOrEmpty(parameters.GeneralSearchParam))
                    {
                        query += "AND (CarMakeName LIKE @GeneralSearchParam " +
                            "OR CarModelName LIKE @GeneralSearchParam " +
                            "OR ModelYear LIKE @GeneralSearchParam) ";
                        cmd.Parameters.AddWithValue("@GeneralSearchParam", '%' + parameters.GeneralSearchParam + '%');
                    }
                }

                query += "ORDER BY MSRP DESC";
                cmd.CommandText = query;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleDetails currentRow = new VehicleDetails();
                        currentRow.VehicleId = (int)dr["VehicleId"];
                        currentRow.VinNumber = dr["VinNumber"].ToString();
                        currentRow.ModelYear = dr["ModelYear"].ToString();
                        currentRow.MSRP = (decimal)dr["MSRP"];
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.VehicleDescription = dr["VehicleDescription"].ToString();
                        currentRow.VehicleImageFileName = dr["VehicleImageFileName"].ToString();
                        currentRow.IsNewCar = (bool)dr["IsNewCar"];
                        currentRow.VehicleMileage = (int)dr["VehicleMileage"];
                        currentRow.BodyColorTypeId = (int)dr["BodyColorTypeId"];
                        currentRow.BodyColorType = dr["BodyColor"].ToString();
                        currentRow.InteriorColorTypeId = (int)dr["InteriorColorTypeId"];
                        currentRow.InteriorColorType = dr["InteriorColor"].ToString();
                        currentRow.CarBodyTypeId = (int)dr["CarBodyTypeId"];
                        currentRow.CarBodyType = dr["CarBodyType"].ToString();
                        currentRow.CarMakeId = (int)dr["CarMakeId"];
                        currentRow.CarMakeName = dr["CarMakeName"].ToString();
                        currentRow.CarModelId = (int)dr["CarModelId"];
                        currentRow.CarModelName = dr["CarModelName"].ToString();
                        currentRow.TransmissionTypeId = (int)dr["TransmissionTypeId"];
                        currentRow.TransmissionType = dr["TransmissionType"].ToString();

                        vehicles.Add(currentRow);
                    }
                }
            }
            return vehicles;            
        }

        public IEnumerable<VehicleDetails> SearchUsed(VehicleSearchParameters parameters)
        {
            List<VehicleDetails> vehicles = new List<VehicleDetails>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT TOP 20 v.VehicleId, v.VinNumber, v.ModelYear, v.MSRP, v.SalePrice, v.VehicleDescription, v.VehicleImageFileName, " +
                    "v.IsNewCar, v.VehicleMileage, v.BodyColorTypeId, bodyColor.ColorType AS BodyColor, v.InteriorColorTypeId, " +
                    "interiorColor.ColorType AS InteriorColor, v.CarBodyTypeId, body.CarBodyType, v.CarMakeId, make.CarMakeName, " +
                    "v.CarModelId, model.CarModelName, v.TransmissionTypeId, t.TransmissionType " +
                    "FROM Vehicles v " +
                    "LEFT JOIN Colors bodyColor ON v.BodyColorTypeId = bodyColor.ColorTypeId " +
                    "LEFT JOIN Colors interiorColor ON v.InteriorColorTypeId = interiorColor.ColorTypeId " +
                    "LEFT JOIN CarBodies body ON v.CarBodyTypeId = body.CarBodyTypeId " +
                    "LEFT JOIN CarMakes make ON v.CarMakeId = make.CarMakeId " +
                    "LEFT JOIN CarModels model ON v.CarModelId = model.CarModelId " +
                    "LEFT JOIN Transmissions t ON v.TransmissionTypeId = t.TransmissionTypeId " +
                    "WHERE v.IsNewCar = 0 AND v.IsPurchasedCar = 0 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;


                if (!string.IsNullOrEmpty(parameters.GeneralSearchParam)
                    || parameters.MinPrice.HasValue || parameters.MaxPrice.HasValue
                    || parameters.MinYear.HasValue || parameters.MaxYear.HasValue)
                {

                    if (parameters.MinPrice.HasValue)
                    {
                        query += "AND SalePrice >= @MinPrice ";
                        cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                    }
                    if (parameters.MaxPrice.HasValue)
                    {
                        query += "AND SalePrice <= @MaxPrice ";
                        cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                    }
                    if (parameters.MinYear.HasValue)
                    {
                        query += "AND ModelYear >= @MinYear ";
                        cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear.Value);
                    }
                    if (parameters.MaxYear.HasValue)
                    {
                        query += "AND ModelYear <= @MaxYear ";
                        cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear.Value);
                    }
                    if (!string.IsNullOrEmpty(parameters.GeneralSearchParam))
                    {
                        query += "AND (CarMakeName LIKE @GeneralSearchParam " +
                            "OR CarModelName LIKE @GeneralSearchParam " +
                            "OR ModelYear LIKE @GeneralSearchParam) ";
                        cmd.Parameters.AddWithValue("@GeneralSearchParam", '%' + parameters.GeneralSearchParam + '%');
                    }
                }

                query += "ORDER BY MSRP DESC";
                cmd.CommandText = query;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleDetails currentRow = new VehicleDetails();
                        currentRow.VehicleId = (int)dr["VehicleId"];
                        currentRow.VinNumber = dr["VinNumber"].ToString();
                        currentRow.ModelYear = dr["ModelYear"].ToString();
                        currentRow.MSRP = (decimal)dr["MSRP"];
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.VehicleDescription = dr["VehicleDescription"].ToString();
                        currentRow.VehicleImageFileName = dr["VehicleImageFileName"].ToString();
                        currentRow.IsNewCar = (bool)dr["IsNewCar"];
                        currentRow.VehicleMileage = (int)dr["VehicleMileage"];
                        currentRow.BodyColorTypeId = (int)dr["BodyColorTypeId"];
                        currentRow.BodyColorType = dr["BodyColor"].ToString();
                        currentRow.InteriorColorTypeId = (int)dr["InteriorColorTypeId"];
                        currentRow.InteriorColorType = dr["InteriorColor"].ToString();
                        currentRow.CarBodyTypeId = (int)dr["CarBodyTypeId"];
                        currentRow.CarBodyType = dr["CarBodyType"].ToString();
                        currentRow.CarMakeId = (int)dr["CarMakeId"];
                        currentRow.CarMakeName = dr["CarMakeName"].ToString();
                        currentRow.CarModelId = (int)dr["CarModelId"];
                        currentRow.CarModelName = dr["CarModelName"].ToString();
                        currentRow.TransmissionTypeId = (int)dr["TransmissionTypeId"];
                        currentRow.TransmissionType = dr["TransmissionType"].ToString();

                        vehicles.Add(currentRow);
                    }
                }
            }
            return vehicles;
        }

        public IEnumerable<VehicleDetails> SearchAll(VehicleSearchParameters parameters)
        {
            List<VehicleDetails> vehicles = new List<VehicleDetails>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT TOP 20 v.VehicleId, v.VinNumber, v.ModelYear, v.MSRP, v.SalePrice, v.VehicleDescription, v.VehicleImageFileName, " +
                    "v.IsNewCar, v.VehicleMileage, v.BodyColorTypeId, bodyColor.ColorType AS BodyColor, v.InteriorColorTypeId, " +
                    "interiorColor.ColorType AS InteriorColor, v.CarBodyTypeId, body.CarBodyType, v.CarMakeId, make.CarMakeName, " +
                    "v.CarModelId, model.CarModelName, v.TransmissionTypeId, t.TransmissionType " +
                    "FROM Vehicles v " +
                    "LEFT JOIN Colors bodyColor ON v.BodyColorTypeId = bodyColor.ColorTypeId " +
                    "LEFT JOIN Colors interiorColor ON v.InteriorColorTypeId = interiorColor.ColorTypeId " +
                    "LEFT JOIN CarBodies body ON v.CarBodyTypeId = body.CarBodyTypeId " +
                    "LEFT JOIN CarMakes make ON v.CarMakeId = make.CarMakeId " +
                    "LEFT JOIN CarModels model ON v.CarModelId = model.CarModelId " +
                    "LEFT JOIN Transmissions t ON v.TransmissionTypeId = t.TransmissionTypeId " +
                    "WHERE v.IsPurchasedCar = 0 ";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;


                if (!string.IsNullOrEmpty(parameters.GeneralSearchParam)
                    || parameters.MinPrice.HasValue || parameters.MaxPrice.HasValue
                    || parameters.MinYear.HasValue || parameters.MaxYear.HasValue)
                {

                    if (parameters.MinPrice.HasValue)
                    {
                        query += "AND SalePrice >= @MinPrice ";
                        cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                    }
                    if (parameters.MaxPrice.HasValue)
                    {
                        query += "AND SalePrice <= @MaxPrice ";
                        cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                    }
                    if (parameters.MinYear.HasValue)
                    {
                        query += "AND ModelYear >= @MinYear ";
                        cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear.Value);
                    }
                    if (parameters.MaxYear.HasValue)
                    {
                        query += "AND ModelYear <= @MaxYear ";
                        cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear.Value);
                    }
                    if (!string.IsNullOrEmpty(parameters.GeneralSearchParam))
                    {
                        query += "AND (CarMakeName LIKE @GeneralSearchParam " +
                            "OR CarModelName LIKE @GeneralSearchParam " +
                            "OR ModelYear LIKE @GeneralSearchParam) ";
                        cmd.Parameters.AddWithValue("@GeneralSearchParam", '%' + parameters.GeneralSearchParam + '%');
                    }
                }

                query += "ORDER BY MSRP DESC";
                cmd.CommandText = query;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleDetails currentRow = new VehicleDetails();
                        currentRow.VehicleId = (int)dr["VehicleId"];
                        currentRow.VinNumber = dr["VinNumber"].ToString();
                        currentRow.ModelYear = dr["ModelYear"].ToString();
                        currentRow.MSRP = (decimal)dr["MSRP"];
                        currentRow.SalePrice = (decimal)dr["SalePrice"];
                        currentRow.VehicleDescription = dr["VehicleDescription"].ToString();
                        currentRow.VehicleImageFileName = dr["VehicleImageFileName"].ToString();
                        currentRow.IsNewCar = (bool)dr["IsNewCar"];
                        currentRow.VehicleMileage = (int)dr["VehicleMileage"];
                        currentRow.BodyColorTypeId = (int)dr["BodyColorTypeId"];
                        currentRow.BodyColorType = dr["BodyColor"].ToString();
                        currentRow.InteriorColorTypeId = (int)dr["InteriorColorTypeId"];
                        currentRow.InteriorColorType = dr["InteriorColor"].ToString();
                        currentRow.CarBodyTypeId = (int)dr["CarBodyTypeId"];
                        currentRow.CarBodyType = dr["CarBodyType"].ToString();
                        currentRow.CarMakeId = (int)dr["CarMakeId"];
                        currentRow.CarMakeName = dr["CarMakeName"].ToString();
                        currentRow.CarModelId = (int)dr["CarModelId"];
                        currentRow.CarModelName = dr["CarModelName"].ToString();
                        currentRow.TransmissionTypeId = (int)dr["TransmissionTypeId"];
                        currentRow.TransmissionType = dr["TransmissionType"].ToString();

                        vehicles.Add(currentRow);
                    }
                }
            }
            return vehicles;
        }

        public void InsertNewVehicle(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@VehicleId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@VinNumber", vehicle.VinNumber);
                cmd.Parameters.AddWithValue("@CarMakeId", vehicle.CarMakeId);
                cmd.Parameters.AddWithValue("@CarModelId", vehicle.CarModelId);
                cmd.Parameters.AddWithValue("@BodyColorTypeId", vehicle.BodyColorTypeId);
                cmd.Parameters.AddWithValue("@InteriorColorTYpeId", vehicle.InteriorColorTypeId);
                cmd.Parameters.AddWithValue("@TransmissionTypeId", vehicle.TransmissionTypeId);
                cmd.Parameters.AddWithValue("@CarBodyTypeId", vehicle.CarBodyTypeId);
                cmd.Parameters.AddWithValue("@ModelYear", vehicle.ModelYear);
                cmd.Parameters.AddWithValue("@VehicleMileage", vehicle.VehicleMileage);
                cmd.Parameters.AddWithValue("@MSRP", vehicle.MSRP);
                cmd.Parameters.AddWithValue("@SalePrice", vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@VehicleDescription", vehicle.VehicleDescription);
                cmd.Parameters.AddWithValue("@VehicleImageFileName", vehicle.VehicleImageFileName);
                cmd.Parameters.AddWithValue("@IsNewCar", vehicle.IsNewCar);

                cn.Open();

                cmd.ExecuteNonQuery();

                vehicle.VehicleId = (int)param.Value;
            }
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesUpdate", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);

                cmd.Parameters.AddWithValue("@VinNumber", vehicle.VinNumber);
                cmd.Parameters.AddWithValue("@CarMakeId", vehicle.CarMakeId);
                cmd.Parameters.AddWithValue("@CarModelId", vehicle.CarModelId);
                cmd.Parameters.AddWithValue("@BodyColorTypeId", vehicle.BodyColorTypeId);
                cmd.Parameters.AddWithValue("@InteriorColorTYpeId", vehicle.InteriorColorTypeId);
                cmd.Parameters.AddWithValue("@TransmissionTypeId", vehicle.TransmissionTypeId);
                cmd.Parameters.AddWithValue("@CarBodyTypeId", vehicle.CarBodyTypeId);
                cmd.Parameters.AddWithValue("@ModelYear", vehicle.ModelYear);
                cmd.Parameters.AddWithValue("@VehicleMileage", vehicle.VehicleMileage);
                cmd.Parameters.AddWithValue("@MSRP", vehicle.MSRP);
                cmd.Parameters.AddWithValue("@SalePrice", vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@VehicleDescription", vehicle.VehicleDescription);
                cmd.Parameters.AddWithValue("@VehicleImageFileName", vehicle.VehicleImageFileName);
                cmd.Parameters.AddWithValue("@IsNewCar", vehicle.IsNewCar);
                cmd.Parameters.AddWithValue("@IsFeaturedCar", vehicle.IsFeaturedCar);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<VehicleInventoryReport> GetNewVehicleInventoryReport()
        {
            List<VehicleInventoryReport> inventory = new List<VehicleInventoryReport>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetNewVehicleInventoryReport", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleInventoryReport currentRow = new VehicleInventoryReport();
                        currentRow.Year = dr["Year"].ToString();
                        currentRow.Make = dr["Make"].ToString();
                        currentRow.Model = dr["Model"].ToString();
                        currentRow.Count = (int)dr["Count"];
                        currentRow.StockValue = (decimal)dr["Stock Value"];

                        inventory.Add(currentRow);
                    }
                }
            }
            return inventory;
        }

        public IEnumerable<VehicleInventoryReport> GetUsedVehicleInventoryReport()
        {
            List<VehicleInventoryReport> inventory = new List<VehicleInventoryReport>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetUsedVehicleInventoryReport", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleInventoryReport currentRow = new VehicleInventoryReport();
                        currentRow.Year = dr["Year"].ToString();
                        currentRow.Make = dr["Make"].ToString();
                        currentRow.Model = dr["Model"].ToString();
                        currentRow.Count = (int)dr["Count"];
                        currentRow.StockValue = (decimal)dr["Stock Value"];

                        inventory.Add(currentRow);
                    }
                }
            }
            return inventory;
        }

        public void DeleteVehicle(int vehicleId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DeleteVehicle", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
