using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SplitWise.Business;
using SplitWise.Business.Interface;
using SplitWise.Model.Models;
using SplitWise.Model.RequestModels;
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
            var groups = await _groupService.CreateGroup(request.GroupName, request.Userlist, request.UserId, request.Category, request.SimplifyDebts, request.Comments);

            return Ok();

        }
        [HttpGet]
        [Route("GetGroups")]
        [AllowAnonymous]
        public async Task<IActionResult> getGroups()
        {
            List<RealGroup> groups = await _groupService.GetGroups();
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (user == null)
            {
                return BadRequest();
            }
            else
            {
                groups = await _groupService.GetGroups();
            }
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

            return Ok( group );
        }

    }
}
