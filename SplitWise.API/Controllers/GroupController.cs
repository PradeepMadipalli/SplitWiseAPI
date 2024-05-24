using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SplitWise.Business;
using SplitWise.Business.Interface;
using SplitWise.Model.Models;
using SplitWise.Model.RequestModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Group = SplitWise.Model.Models.Group;

namespace SplitWise.API.Controllers
{
    [Route("api/")]
    [ApiController]

    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpPost]
        [Route("creategroup")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateGroup(RequestGroup request)
        {
            var groups = await _groupService.CreateGroup(request.GroupId, request.GroupName, request.Userlist, request.UserId, request.Category, request.SimplifyDebts, request.Comments);

            return Ok();

        }
        [HttpGet]
        [Route("GetGroups")]
        [AllowAnonymous]
        public async Task<IActionResult> getGroups()
        {

            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<RealGroup> groups = await _groupService.GetGroups(user);
            return Ok(groups);
        }
        [HttpGet]
        [Route("getCategories")]
        public async Task<IActionResult> GetOnlineStatus()
        {
            List<Categories> onlinecategories = await _groupService.GetCategories();
            return Ok(onlinecategories);
        }
        [HttpPost]
        [Route("editgroupdetails")]
        public async Task<IActionResult> EditGroupName(EditGroupDetails request)
        {
            Group group = await _groupService.EdituserGroup(request);
            return Ok(group);
        }

        [HttpPost]
        [Route("getEditGroup")]
        public async Task<IActionResult> GetEditGroupsd(RequesteditGroup req)
        {
            getEditGroup group = await _groupService.GeteditGroups(req.groupid);

            return Ok(group);
        }
        [HttpGet]
        [Route("GetCurrency")]
        [AllowAnonymous]
        public async Task<IActionResult> getCurrency()
        {
            List<Currency> currs = await _groupService.GetCurrencies();
            return Ok(currs);
        }
        [HttpPost]
        [Route("createexpense")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateExpense(RequestExpense request)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Expense expense = await _groupService.CreateExpense(request, user);
            if (expense == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }
        [HttpPost]
        [Route("getexpensesbyGroupid")]
        [AllowAnonymous]
        public async Task<IActionResult> GetExpensesDetails(GetExpenseDetailsRequest request)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            List<ExpenseShareResponse> expense = await _groupService.getExpensesOnId(request, user);
            return Ok(expense);
        }
        [HttpPost]
        [Route("getexpensesbyGidandExpId")]
        [AllowAnonymous]
        public async Task<IActionResult> GetExpensesByGroupIDandExpid(GetExpenseDetailsRequest request)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Expenseresponse expenseresponse = await _groupService.GetExpenseResponse(request);
            return Ok(expenseresponse);
        }
        [HttpPost]
        [Route("gettransactionsbygroupid")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTransactionResponse(GetTransactionRequest request)
        {
            List<GetTransactionResponse> responses = await _groupService.Gettransaction(request);
            return Ok(responses);
        }
        [HttpPost]
        [Route("createinvitation")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateInvitation(FriendRequest request)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Invitation responses = await _groupService.CreateInvitaion(request, user);
            if (responses != null)
            {
                return Ok(responses);
            }
            return Ok(null);
        }
        [HttpPost]
        [Route("GetFriendInvi")]
        [AllowAnonymous]
        public async Task<IActionResult> GetInvitations(GetInvitationRequest request)
        {
            Invitation invitation = await _groupService.GetInvitation(request);
            return Ok(invitation);
        }
        [HttpPost]
        [Route("GetFriends")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFriends(GetInvitationRequest request)
        {
            Invitation invitation = await _groupService.GetInvitation(request);
            return Ok(invitation);
        }
        [HttpGet]
        [Route("getfriendslist")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFriendsList()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<GetUsersOwns> friends = await _groupService.GetFriendsList(user);
            return Ok(friends);
        }
        [HttpGet]
        [Route("gettotalamount")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTotalAmountByUSerId()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _groupService.GetTotalAmountByUserId(user);
            return Ok(result);
        }
        [HttpGet]
        [Route("gettotalamountdetails")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTotakAmountDetails()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<GetAllPaidDetails> result = await _groupService.GetTotalAmountDetails(user);
            return Ok(result);
        }
        [HttpPost]
        [Route("insertsettleup")]
        [AllowAnonymous]
        public async Task<IActionResult> InsertSettleUp(SettleupRequest request)
        {
            SettleUp settleUp = await _groupService.InsertSettleUp(request);
            return Ok(settleUp);
        }
        [HttpGet]
        [Route("getactivities")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActivities()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<getActivity> activities = await _groupService.GetActivities(user);
            return Ok(activities);
        }
    }


}
