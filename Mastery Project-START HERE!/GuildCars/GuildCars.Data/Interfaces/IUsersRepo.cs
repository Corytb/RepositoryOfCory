using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IUsersRepo
    {
        IEnumerable<UserListItem> GetUserList();
        IEnumerable<UserWithSales> GetUsersWithSales();
        IEnumerable<SalesReportSearchResult> SearchSalesReports(SalesReportSearchParameters parameters);
        IEnumerable<AspNetRole> GetAspNetRoles();
        IEnumerable<AspNetUserRole> GetAllAspNetUserRoles();
        void UpdateUserRole(string userId, string roleId);
        User GetUserById(string userId);
    }
}
