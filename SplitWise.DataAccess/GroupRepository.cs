using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SplitWise.Common;
using SplitWise.DataAccess.Interface;
using SplitWise.Model.Models;
using SplitWise.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.DataAccess
{
    public class GroupRepository : IGroupRepository
    {
        private readonly SplitWiseDBContext _splitWiseDBContext;

        public GroupRepository(SplitWiseDBContext splitWiseDBContext)
        {
            _splitWiseDBContext = splitWiseDBContext;
        }

        public async Task<Group> insertGruop(string groupname, string userid, string category, string simplifydebts, string comments)
        {
            var group = new Group { GroupName = groupname,Status=1, UserId = userid ,Category=category,SimplifyDebts=simplifydebts,Comments=comments};
            await _splitWiseDBContext.Group.AddAsync(group);
            await _splitWiseDBContext.SaveChangesAsync();

            return group;
        }
        public async Task<Group> FindByGroupName(string groupname)
        {
            try
            {
                return await _splitWiseDBContext.Group.Where(u => u.GroupName == groupname).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<List<UsersGroup>> InsertUserGroup(List<UsersGroup> usergroup)
        {
            await _splitWiseDBContext.UserGroups.AddRangeAsync(usergroup);
            await _splitWiseDBContext.SaveChangesAsync();
            return usergroup;
        }
        public async Task<UsersGroup> CheckGroupNameandIdExists(string groupId, string userId)
        {
            return await _splitWiseDBContext.UserGroups.Where(a => (a.GroupId.ToString() == groupId) && (a.UserId == userId)).FirstOrDefaultAsync();
        }
        public async Task<List<UsersGroup>> GetGroupOfUsers(GroupUserRequest request)
        {
            return await _splitWiseDBContext.UserGroups.Where(u => u.GroupId.ToString() == request.groupId).ToListAsync();
        }
        public async Task<List<Group>> GetGroupUserId(GroupUserIdRequest request)
        {
            return await _splitWiseDBContext.Group.Where(u => u.UserId == request.UserId).ToListAsync();
        }

        public async Task<List<Group>> GetGroups()
        {
            return await _splitWiseDBContext.Group.ToListAsync();
        }
        public async Task<List<Categories>> getcategories()
        {
            List<Categories> categorieslist = await _splitWiseDBContext.Categories.ToListAsync();
            return categorieslist;
        }
        public async Task<Group> Editgroupdetails(EditGroupDetails request)
        {

            Group group = await _splitWiseDBContext.Group.Where(a => a.GroupId.ToString() == request.groupId).FirstOrDefaultAsync();

            if (group != null)
            {
                group.GroupName = request.groupName;
                group.Category = request.Category;
                group.SimplifyDebts=request.SimplifyDebts;
                group.Comments= request.Comments;
                await _splitWiseDBContext.SaveChangesAsync();
            }
            return group;
        }

        public async Task<List<UsersGroup>> GetGroupsbyGroupid(string groupid)
        {
            return await _splitWiseDBContext.UserGroups.Where(a=>a.GroupId.ToString()==groupid).ToListAsync();
        }

        public async Task<Group> FindByGroupId(string groupId)
        {
            return await _splitWiseDBContext.Group.Where(a => a.GroupId.ToString() == groupId).FirstOrDefaultAsync();
        }
    }
}
