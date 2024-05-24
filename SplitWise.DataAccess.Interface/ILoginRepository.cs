using Microsoft.AspNetCore.Identity;
using SplitWise.Model.Models;
using SplitWise.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.DataAccess.Interface
{
    public interface ILoginRepository
    {
        Task<SplitUser> userFindByEmail(string email);
        Task<bool> userCheckPassword(SplitUser users, string password);
        Task<IdentityResult> userCreate(SplitUser identityUser, string password);
        Task<SplitUser> userFindById(string Id);
        Task<string> userGetUserId(SplitUser identityUser);
        Task<List<SplitUser>> GetUsersList();
        Task<List<GetUsers>> GetFriendsList(List<string> request);
    }
}
