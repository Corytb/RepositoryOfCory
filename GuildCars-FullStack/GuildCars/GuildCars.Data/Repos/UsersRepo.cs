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
    public class UsersRepo : IUsersRepo
    {
        public IEnumerable<SalesReportSearchResult> SearchSalesReports(SalesReportSearchParameters parameters)
        {
            List<SalesReportSearchResult> results = new List<SalesReportSearchResult>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT users.Id AS 'UserId', CONCAT(users.Firstname, ' ', users.LastName) AS 'UserName', " +
                    "SUM(p.PurchasePrice) AS 'TotalSales', COUNT(p.UserId) AS 'TotalVehicles' " +
                    "FROM Purchases p LEFT JOIN AspNetUsers users ON p.UserId = users.Id " +
                    "WHERE 1=1 ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;


                if (!string.IsNullOrEmpty(parameters.UserId)
                    || parameters.MinDate.HasValue || parameters.MaxDate.HasValue)
                {

                    if (parameters.MinDate.HasValue)
                    {
                        query += "AND PurchaseDate >= @MinDate ";
                        cmd.Parameters.AddWithValue("@MinDate", parameters.MinDate.Value);
                    }
                    if (parameters.MaxDate.HasValue)
                    {
                        query += "AND PurchaseDate <= @MaxDate ";
                        cmd.Parameters.AddWithValue("@MaxDate", parameters.MaxDate.Value.AddDays(1));
                    }
                    if (!string.IsNullOrEmpty(parameters.UserId))
                    {
                        query += "AND UserId = @UserId ";
                        cmd.Parameters.AddWithValue("@UserId", parameters.UserId);
                    }
                }

                query += "GROUP BY users.Firstname, users.LastName, users.Id " +
                    "ORDER BY TotalSales DESC, TotalVehicles DESC";
                cmd.CommandText = query;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SalesReportSearchResult currentRow = new SalesReportSearchResult();
                        currentRow.UserId = dr["UserId"].ToString();
                        currentRow.UserName = dr["UserName"].ToString();
                        currentRow.TotalSales = (decimal)dr["TotalSales"];
                        currentRow.TotalVehicles = (int)dr["TotalVehicles"];

                        results.Add(currentRow);
                    }
                }
            }
            return results;
        }

        //Populates 'Admin/Users_Admin' page
        public IEnumerable<UserListItem> GetUserList()
        {
            List<UserListItem> users = new List<UserListItem>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetUserList", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        UserListItem currentRow = new UserListItem();
                        currentRow.UserId = dr["UserId"].ToString();
                        currentRow.FirstName = dr["FirstName"].ToString();
                        currentRow.LastName = dr["LastName"].ToString();
                        currentRow.Email = dr["Email"].ToString();
                        currentRow.UserRole = dr["UserRole"].ToString();

                        users.Add(currentRow);
                    }
                }
            }
            return users;
        }

        //Populates DropDownListFor in 'Reports/SalesReports_Reports
        public IEnumerable<UserWithSales> GetUsersWithSales()
        {
            List<UserWithSales> users = new List<UserWithSales>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetUsersWithSales", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        UserWithSales currentRow = new UserWithSales();
                        currentRow.UserId = dr["UserId"].ToString();
                        currentRow.UserName = dr["UserName"].ToString();

                        users.Add(currentRow);
                    }
                }
            }
            return users;
        }

        public IEnumerable<AspNetRole> GetAspNetRoles()
        {
            List<AspNetRole> roles = new List<AspNetRole>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetAspNetRoles", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AspNetRole aspNetRole = new AspNetRole();
                        aspNetRole.Id = dr["Id"].ToString();
                        aspNetRole.Name = dr["Name"].ToString();

                        roles.Add(aspNetRole);
                    }
                }
            }
            return roles;
        }

        public IEnumerable<AspNetUserRole> GetAllAspNetUserRoles()
        {
            List<AspNetUserRole> userRoles = new List<AspNetUserRole>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetAllUserRoles", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AspNetUserRole userRole = new AspNetUserRole();
                        userRole.UserId = dr["UserId"].ToString();
                        userRole.RoleId= dr["RoleId"].ToString();

                        userRoles.Add(userRole);
                    }
                }
            }
            return userRoles;
        }

        public void UpdateUserRole(string userId, string roleId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("UpdateUserRole", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@UserRoleId", roleId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public User GetUserById(string userId)
        {
            User user = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetUserById", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", userId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        user = new User();
                        user.UserId = dr["UserId"].ToString();
                        user.FirstName = dr["FirstName"].ToString();
                        user.LastName = dr["LastName"].ToString();
                        user.Email = dr["Email"].ToString();
                        user.UserRoleId = dr["UserRoleId"].ToString();
                    }
                }
            }
            return user;
        }
    }
}
