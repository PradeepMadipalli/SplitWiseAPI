using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
using System.Xml.Linq;

namespace SplitWise.DataAccess
{
    public class GroupRepository : IGroupRepository
    {
        private readonly SplitWiseDBContext _splitWiseDBContext;

        public GroupRepository(SplitWiseDBContext splitWiseDBContext)
        {
            _splitWiseDBContext = splitWiseDBContext;
        }

        public async Task<Group> insertGruop(string groupname, string userid, string category, bool simplifydebts, string comments)
        {
            var group = new Group { GroupName = groupname, Status = 1, UserId = userid, Category = category, SimplifyDebts = simplifydebts, Comments = comments, CreatedDate = DateTime.Now, CreatedBy = userid };
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
        public async Task<UsersGroup?> CheckGroupNameandIdExists(string groupId, string userId)
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

        public async Task<List<Group>> GetGroups(string userid)
        {
            List<string?> userGroup = await _splitWiseDBContext.UserGroups.Where(a => a.UserId == userid).Select(a => a.GroupId.ToString()).ToListAsync();
            return await _splitWiseDBContext.Group.Where(g => userGroup.Contains(g.GroupId.ToString())).ToListAsync();
        }
        public async Task<List<Categories>> getcategories()
        {
            List<Categories> categorieslist = await _splitWiseDBContext.Categories.ToListAsync();
            return categorieslist;
        }
        public async Task<Group> Editgroupdetails(EditGroupDetails request)
        {

            Group? group = await _splitWiseDBContext.Group.Where(a => a.GroupId.ToString() == request.groupId).FirstOrDefaultAsync();

            if (group != null)
            {
                group.GroupName = request.groupName;
                group.Category = request.Category;
                group.SimplifyDebts = request.SimplifyDebts;
                group.Comments = request.Comments;
                await _splitWiseDBContext.SaveChangesAsync();
            }
            return group;
        }

        public async Task<List<UsersGroup>> GetGroupsbyGroupid(string groupid)
        {
            return await _splitWiseDBContext.UserGroups.Where(a => a.GroupId.ToString() == groupid).ToListAsync();
        }

        public async Task<Group?> FindByGroupId(string groupId)
        {
            return await _splitWiseDBContext.Group.Where(a => a.GroupId.ToString() == groupId).FirstOrDefaultAsync();
        }
        public async Task<Group> Updategroupdetails(Group grouprequrst, string groupid)
        {

            Group? group = await _splitWiseDBContext.Group.Where(a => a.GroupId.ToString() == groupid).FirstOrDefaultAsync();

            if (group != null)
            {
                group.Category = grouprequrst.Category;
                group.GroupName = grouprequrst.GroupName;
                group.SimplifyDebts = grouprequrst.SimplifyDebts;
                group.Comments = grouprequrst.Comments;
                _splitWiseDBContext.SaveChanges();
            }

            return group;
        }
        public async Task<List<Currency>> GetCurrencies()
        {
            return await _splitWiseDBContext.Currencies.ToListAsync();
        }
        public async Task<Expense> AddExpenses(Expense request, bool Isupdate)
        {

            if (!Isupdate)
            {
                await _splitWiseDBContext.Expenses.AddAsync(request);
                await _splitWiseDBContext.SaveChangesAsync();
            }
            else
            {
                await _splitWiseDBContext.SaveChangesAsync();
            }
            return request;
        }

        public async Task<Expense> isExistExpense(string grouid, string expId)
        {
            Expense? result = await _splitWiseDBContext.Expenses.Where(a => (a.groupId == grouid) && (a.expId.ToString() == expId)).FirstOrDefaultAsync();
            return result;
        }

        public async Task<ExpenseDetails> AddExpensesDetails(ExpenseDetails request, bool Isupdate)
        {

            if (!Isupdate)
            {
                await _splitWiseDBContext.ExpensesDetails.AddAsync(request);
                await _splitWiseDBContext.SaveChangesAsync();
            }
            else
            {
                await _splitWiseDBContext.SaveChangesAsync();
            }
            return request;
        }
        public async Task<ExpenseDetails> isExistExpensedetails(string userid, string expId)
        {
            ExpenseDetails? result = await _splitWiseDBContext.ExpensesDetails.Where(a => (a.ParticipantId == userid) && (a.expId.ToString() == expId)).FirstOrDefaultAsync();
            return result;
        }
        public async Task<List<Expense>> getExpensesOnId(GetExpenseDetailsRequest request)
        {

            List<Expense> result = new List<Expense>();
            return result;
        }
        public Task<List<Model.Models.Expense>> GetExpensesList(string GroupId)
        {
            return _splitWiseDBContext.Expenses.Where(a => a.groupId.ToString() == GroupId).ToListAsync();
        }

        public Task<List<Model.Models.ExpenseDetails>> GetExpensesDeatilsList()
        {
            return _splitWiseDBContext.ExpensesDetails.ToListAsync();
        }

        public async Task<Expenseresponse> GetExpenseResponse(GetExpenseDetailsRequest request)
        {
            Expenseresponse? expenseresponse = await _splitWiseDBContext.Expenses.Where(e => e.groupId.ToString() == request.groupId && e.expId.ToString() == request.expId).Select(a => new Expenseresponse
            {
                expId = a.expId.ToString(),
                groupId = a.groupId,
                Name = a.Name,
                Amount = a.Amount,
                Currency = a.Currency,
                Date = a.Date,
                Notes = a.Notes,
                GroupSelection = a.GroupSelection

            }).FirstOrDefaultAsync();
            return expenseresponse;
        }

        public async Task<List<ExpenseDetailsresponse>> GetExpenseDeatilsResponse(GetExpenseDetailsRequest request)
        {
            List<ExpenseDetailsresponse> expenseresponse = await _splitWiseDBContext.ExpensesDetails.Where(e => e.expId.ToString() == request.expId).Select(a => new ExpenseDetailsresponse
            {
                expdId = a.expdId.ToString(),
                expId = a.expdId.ToString(),
                Amount = a.Amount,
                Paidby = a.Paidby,
                ParticipantId = a.ParticipantId,
                ParticipantAmount = a.ParticipantAmount,
                Share = a.Share,
                SplitBy = a.SplitBy,

            }).ToListAsync();
            return expenseresponse;
        }
        public async Task<Transaction> AddTransactions(Transaction transaction)
        {
            await _splitWiseDBContext.Transactions.AddAsync(transaction);
            await _splitWiseDBContext.SaveChangesAsync();
            return transaction;

        }
        public async Task<List<GetTransactionResponse>> GetTransaction(GetTransactionRequest request)
        {
            List<GetTransactionResponse>? getTransactions = await _splitWiseDBContext.Transactions.Where(a => a.Groupid == request.GroupId).Select(t => new GetTransactionResponse
            {
                TransID = t.TransID,
                PaidId = t.PaidId,
                Groupid = t.Groupid,
                ReceiverId = t.ReceiverId,
                Amount = t.Amount,
                TransactionMessage = t.TransactionMessage,
                CreatedBy = t.CreatedBy,
                CreatedDate = t.CreatedDate,
                UpdateDBy = t.UpdateDBy,
                UpdatedDate = t.UpdatedDate,
            }).ToListAsync();
            return getTransactions;
        }
        public async Task<Invitation> CreatedFriend(Invitation request)
        {
            await _splitWiseDBContext.Invitations.AddAsync(request);
            await _splitWiseDBContext.SaveChangesAsync();
            return request;
        }
        public async Task<Invitation> UpdateFriend(Invitation? request)
        {
            await _splitWiseDBContext.SaveChangesAsync();
            return request;
        }
        public async Task<Invitation> GetInvitation(GetInvitationRequest request)
        {
            try
            {
                Invitation? invitation = await _splitWiseDBContext.Invitations.Where(a=>a.InvitationId==Guid.Parse(request.InvitationId)).SingleOrDefaultAsync();
                return invitation;
            }catch (Exception ex)
            {
                throw ex;
            }
          
        }
        public async Task<List<string>> GetUserFriendByUserID(string UserId)
        {
            List<string?> users = await _splitWiseDBContext.UserGroups.Where(a => a.UserId == UserId).Select(a => a.GroupId.ToString()
             ).ToListAsync();
            List<string?> usersGroups = await _splitWiseDBContext.UserGroups.Where(p => users.Contains(p.GroupId.ToString())).Select(p => p.UserId
           ).Distinct().ToListAsync();
            return usersGroups;
        }
        public async Task<List<ExpenseDetails>> GetOwsList(List<string> request)
        {
            return await _splitWiseDBContext.ExpensesDetails.Where(p => request.Contains(p.Paidby.ToString())).ToListAsync();
        }
        public async Task<GetAllAmount> GetTotalAmountByUserIds(string UserId)
        {
            List<string?> users = await _splitWiseDBContext.UserGroups.Where(a => a.UserId == UserId).Select(a => a.GroupId.ToString()
             ).ToListAsync();
            List<string> expids = await _splitWiseDBContext.Expenses.Where(a => users.Contains(a.groupId)).Select(a => a.expId.ToString()).ToListAsync();
            List<GetUserAmount> amounts = await _splitWiseDBContext.ExpensesDetails.Where(a => expids.Contains(a.expId.ToString())).Select(p => new GetUserAmount
            {
                Amount = p.Amount,
                expid = p.expId.ToString(),
                PaidBy = p.Paidby,
            }).ToListAsync();
            List<GetUserAmount> amo = amounts.DistinctBy(p => p.expid).ToList();
            List<GetUserAmount> ows = amounts.Where(a => a.PaidBy == UserId).DistinctBy(p => p.expid).ToList();
            decimal totalAmount = amo.Sum(a => decimal.Parse(a.Amount));
            decimal owsAmount = ows.Sum(a => decimal.Parse(a.Amount));
            GetAllAmount getAllAmount = new GetAllAmount
            {
                TotalAmount = totalAmount,
                OweAmount = owsAmount,
                OwedAmount = totalAmount - owsAmount,
            };

            return getAllAmount;
        }
        public async Task<List<GetAllPaidDetails>> GetTotalAmountDetails(string UserId)
        {
            List<string?> users = await _splitWiseDBContext.UserGroups.Where(a => a.UserId == UserId).Select(a => a.GroupId.ToString()
             ).ToListAsync();
            List<GetGridandExid> expids = await _splitWiseDBContext.Expenses.Where(a => users.Contains(a.groupId)).Select(a => new GetGridandExid
            {
                expId = a.expId.ToString(),
                Groupid = a.groupId,
                expName = a.Name,

            }).ToListAsync();

            List<string> expidss = await _splitWiseDBContext.Expenses.Where(a => users.Contains(a.groupId)).Select(a => a.expId.ToString()).ToListAsync();

            List<GetAllAmountDetails> amounts = await _splitWiseDBContext.ExpensesDetails.Where(a => expidss.Contains(a.expId.ToString())).Select(p => new GetAllAmountDetails
            {

                Amount = p.Amount,
                expId = p.expId.ToString(),
                PaidBy = p.Paidby,
                participantId = p.ParticipantId,
                ParticipantAmount = p.ParticipantAmount,
                share = p.Share

            }).ToListAsync();
            List<GetAllPaidDetails> GetAllPaidDetails = expids.Join(amounts, e => e.expId, ed => ed.expId, (e, ed) => new GetAllPaidDetails
            {
                expId = e.expId.ToString(),
                expName = e.expName,
                Groupid = e.Groupid,
                Amount = ed.Amount,
                PaidBy = ed.PaidBy,
                participantId = ed.participantId,
                ParticipantAmount = ed.ParticipantAmount,
                share = ed.share,
            }).ToList();

            return GetAllPaidDetails;

        }
        public async Task<SettleUp> InsertSettleUp(SettleupRequest request)
        {
            SettleUp settleup = new SettleUp
            {
                PayerId = request.PayerId,
                PayeeId = request.PayeeId,
                GroupId = request.GroupId,
                Amount = request.Amount,
                CreaqtedBy = request.CreaqtedBy,
                CreaqtedDate = request.CreaqtedDate,
            };
            await _splitWiseDBContext.SettleUps.AddAsync(settleup);
            await _splitWiseDBContext.SaveChangesAsync();

            return settleup;
        }
        public async Task<Activity?> InsertActivity(Activity request)
        {

            await _splitWiseDBContext.Activities.AddAsync(request);
            await _splitWiseDBContext.SaveChangesAsync();

            return null;
        }
        public async Task<List<getActivity>> GetActivity(string UserId)
        {
            List<string?> users = await _splitWiseDBContext.UserGroups.Where(a => a.UserId == UserId).Select(a => a.GroupId.ToString()
            ).ToListAsync();
            List<getActivity> acty = await _splitWiseDBContext.Activities.Where(a=>users.Contains(a.GroupId)).Select(a => new getActivity
            {
                Id = a.Id.ToString(),
                UserId = a.UserId,
                GroupId = a.GroupId,
                ReceiverId = a.ReceiverId,
                CreatedBy = a.CreatedBy,
                CreatedId = a.CreatedId,
                Message = a.Message,

            }).ToListAsync();
            return acty;
        }

      
    }
}
