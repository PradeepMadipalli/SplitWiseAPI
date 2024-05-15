using Newtonsoft.Json;
using SplitWise.Business.Interface;
using SplitWise.DataAccess.Interface;
using SplitWise.Model.Models;
using SplitWise.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Business
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ILoginService _loginService;

        public GroupService(IGroupRepository groupRepository, ILoginService loginService)
        {
            _groupRepository = groupRepository;
            _loginService = loginService;
        }
        public async Task<Group> CreateGroup(string groupname, string userlist, string userid, string category, string simplifydebts, string comments)
        {
            List<UserList> request = JsonConvert.DeserializeObject<List<UserList>>(userlist);
            Group group = await _groupRepository.FindByGroupName(groupname);

            await _groupRepository.insertGruop(groupname, userid, category, simplifydebts, comments);
            Group group1 = await _groupRepository.FindByGroupName(groupname);
            List<UsersGroup> groups = new List<UsersGroup>();
            foreach (var item in request)
            {
                UsersGroup userGroup = new UsersGroup()
                {
                    UserId = item.UserId,
                    GroupId = group1.GroupId,
                    Status = 1,
                };
                var usergroup = await _groupRepository.CheckGroupNameandIdExists(group1.GroupId.ToString(), item.UserId);
                if (usergroup == null)
                {
                    groups.Add(userGroup);
                }
            }
            await _groupRepository.InsertUserGroup(groups);
            return group1;

        }
        public async Task<List<UsersGroup>> GetGroupOfUsers(GroupUserRequest groupId)
        {
            List<UsersGroup> userGroups = await _groupRepository.GetGroupOfUsers(groupId);
            return userGroups;
        }
        public async Task<List<Group>> GetGroups(GroupUserIdRequest userId)
        {
            List<Group> groups = await _groupRepository.GetGroupUserId(userId);
            return groups;
        }

        public async Task<List<RealGroup>> GetGroups()
        {
            List<Group> groups = await _groupRepository.GetGroups();
            List<RealGroup> realGroups = new List<RealGroup>();
            if (groups != null)
            {
                foreach (var group in groups)
                {

                    RealGroup realGroup = new RealGroup()
                    {
                        GroupId = group.GroupId,
                        GroupName = group.GroupName,
                    };
                    realGroups.Add(realGroup);
                }
            }
            return realGroups;
        }
        public async Task<List<Categories>> GetCategories()
        {
            List<Categories> catess = await _groupRepository.getcategories();
            return catess;
        }
        public async Task<Group> EdituserGroup(EditGroupDetails request)
        {
            Group group = await _groupRepository.Editgroupdetails(request);
            return group;
        }

        public async Task<getEditGroup> GeteditGroups(string groupid)
        {
            getEditGroup getEdit = new getEditGroup();

            Group group = await _groupRepository.FindByGroupId(groupid);
            if (group != null)
            {
                getEdit.GroupId = group.GroupId;
                getEdit.GroupName = group.GroupName;
            }
            List<UsersGroup> usersGroups = await _groupRepository.GetGroupsbyGroupid(groupid);
            List<GetUsers> splitUsers = await _loginService.GetGetUsers();
            List<GetUsers> gofus = usersGroups.Join(splitUsers, u => u.UserId, ug => ug.UserId, (u, ug) => new GetUsers
            {
                UserId = ug.UserId,
                UserName = ug.UserName,
                UserEmail = ug.UserEmail,
            }).ToList();
            {
                getEdit.usersGroups = gofus;
            }
            return getEdit;
        }
    }
}
