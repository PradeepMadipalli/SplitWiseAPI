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
        Task<Model.Models.Group> CreateGroup(string groupid ,string groupname, string userlist, string userid, string category, bool simplifydebts, string comments);
        Task<List<UsersGroup>> GetGroupOfUsers(GroupUserRequest groupId);
        Task<List<Group>> GetGroups(GroupUserIdRequest userId);

        Task<List<RealGroup>> GetGroups(string userid);
        Task<List<Categories>> GetCategories();
        Task<Group> EdituserGroup(EditGroupDetails request);

        Task<getEditGroup> GeteditGroups(string groupid);
        Task<List<Currency>> GetCurrencies();
        Task<Expense> CreateExpense(RequestExpense request, string userID);
        Task<List<ExpenseShareResponse>> getExpensesOnId(GetExpenseDetailsRequest request, string uid);
        Task<Expenseresponse> GetExpenseResponse(GetExpenseDetailsRequest request);
        Task<List<GetTransactionResponse>> Gettransaction(GetTransactionRequest request);
        Task<Invitation> CreateInvitaion(FriendRequest request, string usrid);
        Task<Invitation> GetInvitation(GetInvitationRequest request);
        Task<List<GetUsersOwns>> GetFriendsList(string userid);
        Task<GetAllAmount> GetTotalAmountByUserId(string userId);
        Task<List<GetAllPaidDetails>> GetTotalAmountDetails(string userId);
        Task<SettleUp> InsertSettleUp(SettleupRequest request);
        Task<List<getActivity>> GetActivities(string UserId);
    }
}

