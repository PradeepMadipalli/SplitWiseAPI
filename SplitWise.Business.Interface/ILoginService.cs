using Microsoft.AspNetCore.Identity;
using SplitWise.Model.Models;
using SplitWise.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Business.Interface
{
    public interface ILoginService
    {
        Task<SplitUser> userFindByEmail(string email);
        Task<IdentityResult> Createuser(SplitUser identityUser, string password);
        Task<RegisterRequest> GetResponseRegister(SplitUser user, Register register);
        TokenResponse GetTokenResponse(SplitUser user);
        Task<bool> userCheckPassword(SplitUser users, string password);
        Task<List<GetUsers>> GetGetUsers();
    }
}
