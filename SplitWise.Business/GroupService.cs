using Newtonsoft.Json;
using SplitWise.Business.Interface;
using SplitWise.DataAccess.Interface;
using SplitWise.Model.Models;
using SplitWise.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace SplitWise.Business
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ILoginService _loginService;
        private readonly ILoginRepository _loginRepository;

        public GroupService(IGroupRepository groupRepository, ILoginService loginService, ILoginRepository loginRepository)
        {
            _groupRepository = groupRepository;
            _loginService = loginService;
            _loginRepository = loginRepository;
        }
        public async Task<Model.Models.Group> CreateGroup(string groupid, string groupname, string userlist, string userid, string category, bool simplifydebts, string comments)
        {
            List<UserList> request = JsonConvert.DeserializeObject<List<UserList>>(userlist);
            Model.Models.Group group = await _groupRepository.FindByGroupName(groupname);
            if (group == null && groupid != null)
            {
                group = await _groupRepository.FindByGroupId(groupid);

                List<UsersGroup> groups = new List<UsersGroup>();



            }
            if (group == null && groupid == null)
            {
                await _groupRepository.insertGruop(groupname, userid, category, simplifydebts, comments);

                Activity activity = new Activity
                {
                    UserId = userid,
                    GroupId = groupid,
                    Message = "Created Group ",

                };
                await _groupRepository.InsertActivity(activity);
            
            }
            if (group != null && groupid != null)
            {
                group.GroupName = groupname;
                group.UserId = userid;
                group.Category = category;
                group.SimplifyDebts = simplifydebts;
                group.Comments = comments;
                group.SimplifyDebts = simplifydebts;
                await _groupRepository.Updategroupdetails(group, groupid);
                Activity activity = new Activity
                {
                    UserId = userid,
                    GroupId = groupid,
                    Message = "Updated Group ",
                };
                await _groupRepository.InsertActivity(activity);
            }
            Model.Models.Group group1 = await _groupRepository.FindByGroupName(groupname);
            if (group1 != null)
            {
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
            }
            return group1;

        }
        public async Task<List<UsersGroup>> GetGroupOfUsers(GroupUserRequest groupId)
        {
            List<UsersGroup> userGroups = await _groupRepository.GetGroupOfUsers(groupId);
            return userGroups;
        }
        public async Task<List<Model.Models.Group>> GetGroups(GroupUserIdRequest userId)
        {
            List<Model.Models.Group> groups = await _groupRepository.GetGroupUserId(userId);
            return groups;
        }

        public async Task<List<RealGroup>> GetGroups(string userid)
        {
            List<Model.Models.Group> groups = await _groupRepository.GetGroups(userid);
            List<RealGroup> realGroups = new List<RealGroup>();
            if (groups != null)
            {
                foreach (var group in groups)
                {

                    RealGroup realGroup = new RealGroup()
                    {
                        GroupId = group.GroupId,
                        GroupName = group.GroupName,
                        UserId = group.UserId
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
        public async Task<Model.Models.Group> EdituserGroup(EditGroupDetails request)
        {
            Model.Models.Group group = await _groupRepository.Editgroupdetails(request);
            return group;
        }

        public async Task<getEditGroup> GeteditGroups(string groupid)
        {
            getEditGroup getEdit = new getEditGroup();

            Model.Models.Group group = await _groupRepository.FindByGroupId(groupid);
            if (group != null)
            {
                getEdit.GroupId = group.GroupId;
                getEdit.GroupName = group.GroupName;
                getEdit.Comments = group.Comments;
                getEdit.Category = group.Category;
                getEdit.SimplifyDebts = group.SimplifyDebts;
                getEdit.CreatedDate = group.CreatedDate;


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
        public async Task<List<Currency>> GetCurrencies()
        {
            return await _groupRepository.GetCurrencies();
        }
        public async Task<Expense> CreateExpense(RequestExpense request, string userID)
        {
            try
            {
                string expenseId = null;
                List<string> spililist = JsonConvert.DeserializeObject<List<string>>(request.SpiltList);
                List<GetUsers> userlist = JsonConvert.DeserializeObject<List<GetUsers>>(request.UserList);
                Expense expense = await _groupRepository.isExistExpense(request.groupId, request.expid);
                Expense exp = new Expense
                {
                    groupId = request.groupId,
                    Name = request.Name,
                    Amount = request.Amount,
                    Currency = request.Currency,
                    Date = request.Date,
                    Notes = request.Notes,
                    GroupSelection = request.groupId,

                };

                if (expense == null)
                {
                    exp = await _groupRepository.AddExpenses(exp, false);
                    Transaction transaction = new Transaction
                    {
                        PaidId = request.PaidBy,
                        ReceiverId = request.groupId,
                        Groupid = request.groupId,
                        Amount = request.Amount,
                        TransactionMessage = "",
                        CreatedBy = userID,
                        CreatedDate = DateTime.Now,
                    };
                    Activity activity = new Activity
                    {
                        UserId = userID,
                        GroupId = exp.groupId,
                        Message = " Added  " + exp.Name,
                        ReceiverId=exp.expId.ToString()             
                    };
                    await _groupRepository.InsertActivity(activity);
                    await _groupRepository.AddTransactions(transaction);
                }
                else
                {
                    expense.Notes = exp.Notes;
                    expense.Amount = exp.Amount;
                    expense.Currency = exp.Currency;
                    expense.Name = exp.Name;
                    exp.expId = Guid.Parse(request.expid);
                    exp = await _groupRepository.AddExpenses(exp, true);
                    Activity activity = new Activity
                    {
                        UserId = userID,
                        GroupId = exp.groupId,
                        Message = "Updated" + exp.Name,
                        ReceiverId=exp.expId.ToString(),
                    };
                    await _groupRepository.InsertActivity(activity);

                }

                if (exp != null)
                {
                    if (userlist != null)
                    {
                        int i = 0;
                        foreach (var user in userlist)
                        {
                            ExpenseDetails details1 = await _groupRepository.isExistExpensedetails(user.UserId, exp.expId.ToString());

                            ExpenseDetails expenseDetails = new ExpenseDetails
                            {
                                expId = exp.expId,
                                Amount = exp.Amount,
                                Paidby = request.PaidBy,
                                ParticipantId = user.UserId,
                                ParticipantAmount = spililist[i],
                                SplitBy = request.SplitBy,
                                Share = spililist[i],

                            };
                            if (request.SplitBy == "percentage")
                            {
                                expenseDetails.ParticipantAmount = GetValueFromPercentage(Convert.ToDouble(spililist[i]), Convert.ToInt32(request.Amount));
                            }
                            if (details1 == null)
                            {
                                await _groupRepository.AddExpensesDetails(expenseDetails, false);
                                i++;

                            }
                            else
                            {
                                details1.Amount = expenseDetails.Amount;
                                details1.Paidby = expenseDetails.Paidby;
                                details1.ParticipantId = expenseDetails.ParticipantId;
                                details1.ParticipantAmount = expenseDetails.ParticipantAmount;
                                details1.SplitBy = expenseDetails.SplitBy;
                                details1.Share = expenseDetails.Share;
                                await _groupRepository.AddExpensesDetails(expenseDetails, true);
                                i++;
                            }

                        }
                    }
                }


                return expense;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        string GetValueFromPercentage(double percentage, int whole)
        {
            double bt = (percentage / 100) * whole;
            return bt.ToString();
        }
        public async Task<List<ExpenseShareResponse>> getExpensesOnId(GetExpenseDetailsRequest request, string uid)
        {

            List<Expense> expenses = await _groupRepository.GetExpensesList(request.groupId);
            List<ExpenseDetails> expenseDetails = await _groupRepository.GetExpensesDeatilsList();

            List<ExpenseShareResponse> expenseShareResponses = expenses.Join(expenseDetails, e => e.expId, ed => ed.expId, (e, ed) => new ExpenseShareResponse
            {
                expId = e.expId.ToString(),
                Name = e.Name,
                Amount = e.Amount,
                Paidby = ed.Paidby,
                ParticipantId = ed.ParticipantId,
                ParticipantAmount = ed.ParticipantAmount,
                Share = ed.Share,
            }).ToList();
            List<ExpenseShareResponse> result = expenseShareResponses.Where(a => a.ParticipantId == uid).ToList();
            return result;

        }
        public async Task<Expenseresponse> GetExpenseResponse(GetExpenseDetailsRequest request)
        {
            Expenseresponse expenseresponse = await _groupRepository.GetExpenseResponse(request);
            List<ExpenseDetailsresponse> expenseDetailsresponse = await _groupRepository.GetExpenseDeatilsResponse(request);
            expenseresponse.expenseDetailsresponses = expenseDetailsresponse;
            return expenseresponse;
        }
        public async Task<List<GetTransactionResponse>> Gettransaction(GetTransactionRequest request)
        {
            List<GetTransactionResponse> getTransaction = await _groupRepository.GetTransaction(request);
            return getTransaction;
        }
        public async Task<Invitation> CreateInvitaion(FriendRequest request, string usrid)
        {
            string url = "http://localhost:4200";
            Invitation invitation = new Invitation();
            SplitUser user = await _loginRepository.userFindByEmail(request.Email);
            if (user == null)
            {

                invitation.Name = request.Name;
                invitation.Email = request.Email;
                invitation.CreatedBy = usrid;
                invitation.CreatedDate = DateTime.UtcNow;
                invitation = await _groupRepository.CreatedFriend(invitation);
                if (invitation != null)
                {
                    invitation.Url = url + "/invitation/" + invitation.InvitationId;
                }
                await _groupRepository.UpdateFriend(invitation);


            }
            return invitation;
        }
        public async Task<Invitation> GetInvitation(GetInvitationRequest request)
        {
            Invitation invitation = await _groupRepository.GetInvitation(request);
            return invitation;
        }
        public async Task<List<GetUsersOwns>> GetFriendsList(string userid)
        {
            List<string> friends = await _groupRepository.GetUserFriendByUserID(userid);
            List<GetUsers> users = await _loginRepository.GetFriendsList(friends);
            List<ExpenseDetails> details = await _groupRepository.GetOwsList(friends);
            List<ExpenseDetails> de = details.Where(a => a.ParticipantId == userid).ToList();
            var joinResult = from user in users
                             join expense in de on user.UserId equals expense.ParticipantId into userExpenses
                             select new GetUsersOwns
                             {
                                 UserId = user.UserId,
                                 UserName = user.UserName,
                                 UserEmail = user.UserEmail,
                                 Amount = userExpenses.Sum(e => Convert.ToInt32(e.ParticipantAmount)).ToString(),
                             };

            List<GetUsersOwns> usersowns = joinResult.ToList();

            return usersowns;
        }
        public async Task<GetAllAmount> GetTotalAmountByUserId(string userId)
        {

            return await _groupRepository.GetTotalAmountByUserIds(userId);
        }
        public async Task<List<GetAllPaidDetails>> GetTotalAmountDetails(string userId)
        {
            return await _groupRepository.GetTotalAmountDetails(userId);

        }
        public async Task<SettleUp> InsertSettleUp(SettleupRequest request)
        {
            return await _groupRepository.InsertSettleUp(request);
        }
        public async Task<List<getActivity>> GetActivities(string UserId)
        {
            List<getActivity> acts= await _groupRepository.GetActivity(UserId);
            return acts;
        }

    }
}
