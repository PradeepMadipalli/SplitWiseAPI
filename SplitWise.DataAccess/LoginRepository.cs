using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SplitWise.DataAccess.Interface;
using SplitWise.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.DataAccess
{
    public class LoginRepository: ILoginRepository
    {
        private readonly UserManager<SplitUser> _userManager;

        public LoginRepository(UserManager<SplitUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<SplitUser> userFindByEmail(string email)
        {
            if (email == null) throw new ArgumentNullException("email");
            else
            {
                return await _userManager.FindByEmailAsync(email);
            }

        }
        public async Task<bool> userCheckPassword(SplitUser users, string password)
        {
            return await _userManager.CheckPasswordAsync(users, password);
        }
        public async Task<IdentityResult> userCreate(SplitUser identityUser, string password)
        {
           return await _userManager.CreateAsync(identityUser, password);
        }
        public async Task<SplitUser> userFindById(string Id)
        {
            SplitUser appUser = await _userManager.FindByIdAsync(Id);
            return appUser;
        }
        public async Task<string> userGetUserId(SplitUser identityUser)
        {
            return await _userManager.GetUserIdAsync(identityUser);
        }
        public async Task<List<SplitUser>> GetUsersList()
        {
            return await _userManager.Users.ToListAsync();
        }

    }
}
