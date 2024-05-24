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
        Task<Group> insertGruop(string groupname, string userid, string category, bool simplifydebts, string comments);
        Task<Group> FindByGroupName(string groupname);
        Task<List<UsersGroup>> InsertUserGroup(List<UsersGroup> usergroup);
        Task<UsersGroup> CheckGroupNameandIdExists(string groupId, string userId);
        Task<List<UsersGroup>> GetGroupOfUsers(GroupUserRequest request);
        Task<List<Group>> GetGroupUserId(GroupUserIdRequest request);

        Task<List<Group>> GetGroups(string userid);
        Task<List<Categories>> getcategories();

        Task<Group> Editgroupdetails(EditGroupDetails request);

        Task<List<UsersGroup>> GetGroupsbyGroupid(string groupid);

        Task<Group> FindByGroupId(string groupId);
        Task<Group> Updategroupdetails(Group groupRequest, string groupid);
        Task<List<Currency>> GetCurrencies();
        Task<Expense> AddExpenses(Expense request, bool Isupdate);
        Task<Expense> isExistExpense(string grouid, string expId);
        Task<ExpenseDetails> isExistExpensedetails(string expdid, string expId);
        Task<ExpenseDetails> AddExpensesDetails(ExpenseDetails request, bool Isupdate);
        Task<List<Model.Models.Expense>> getExpensesOnId(GetExpenseDetailsRequest request);
        Task<List<Model.Models.Expense>> GetExpensesList(string GroupId);
        Task<List<Model.Models.ExpenseDetails>> GetExpensesDeatilsList();
        Task<List<ExpenseDetailsresponse>> GetExpenseDeatilsResponse(GetExpenseDetailsRequest request);
        Task<Expenseresponse> GetExpenseResponse(GetExpenseDetailsRequest request);
        Task<Transaction> AddTransactions(Transaction transaction);
        Task<List<GetTransactionResponse>> GetTransaction(GetTransactionRequest request);
        Task<Invitation> CreatedFriend(Invitation request);

        Task<Invitation> UpdateFriend(Invitation request);
        Task<Invitation> GetInvitation(GetInvitationRequest request);

        Task<List<string>> GetUserFriendByUserID(string UserId);
        Task<List<ExpenseDetails>> GetOwsList(List<string> request);
        Task<GetAllAmount> GetTotalAmountByUserIds(string UserId);
       Task<List<GetAllPaidDetails>> GetTotalAmountDetails(string UserId);
        Task<SettleUp> InsertSettleUp(SettleupRequest request);
        Task<Activity?> InsertActivity(Activity request);
        Task<List<getActivity>> GetActivity(string UserId);

    }
}
