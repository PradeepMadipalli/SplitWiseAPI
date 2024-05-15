using SplitWise.Model.Models;
using SplitWise.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Business.Interface
{
    public interface IGroupService
    {
        Task<Group> CreateGroup(string groupname, string userlist, string userid, string category, string simplifydebts, string comments);
        Task<List<UsersGroup>> GetGroupOfUsers(GroupUserRequest groupId);
        Task<List<Group>> GetGroups(GroupUserIdRequest userId);

        Task<List<RealGroup>> GetGroups();
        Task<List<Categories>> GetCategories();
        Task<Group> EdituserGroup(EditGroupDetails request);

        Task<getEditGroup> GeteditGroups(string groupid);
    }
}

