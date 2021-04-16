using ChatApi.Entities;
using ChatApi.Helpers;
using ChatApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupController : BaseController
    {
        private readonly IGroupService groupService;

        public GroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> Groups()
        {
            var result = await groupService.Groups();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> Group(int id)
        {
            return Ok(await groupService.Group(id));
        }

        [HttpPost]
        public ActionResult CreateGroup(Group model)
        {
            if (model == null)
                return BadRequest();
            model.Created = DateTime.UtcNow;
            groupService.CreateGroup(model);
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateGroup(int id, Group model)
        {
            if (id != model.Id)
                return BadRequest();
            groupService.UpdateGroup(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            var group =await groupService.Group(id);
            if(group == null)
                return BadRequest();
            groupService.Delete(group);
            return Ok();
        }

    }
}
