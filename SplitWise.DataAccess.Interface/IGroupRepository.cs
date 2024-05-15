using SplitWise.Model.Models;
using SplitWise.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.DataAccess.Interface
{
    public interface IGroupRepository
    {
        Task<Group> insertGruop(string groupname, string userid, string category, string simplifydebts, string comments);
        Task<Group> FindByGroupName(string groupname);
        Task<List<UsersGroup>> InsertUserGroup(List<UsersGroup> usergroup);
        Task<UsersGroup> CheckGroupNameandIdExists(string groupId, string userId);
        Task<List<UsersGroup>> GetGroupOfUsers(GroupUserRequest request);
        Task<List<Group>> GetGroupUserId(GroupUserIdRequest request);

        Task<List<Group>> GetGroups();
        Task<List<Categories>> getcategories();

        Task<Group> Editgroupdetails(EditGroupDetails request);

        Task<List<UsersGroup>> GetGroupsbyGroupid(string groupid);

        Task<Group> FindByGroupId(string groupId);
    }
}
