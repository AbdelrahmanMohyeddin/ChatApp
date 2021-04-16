using ChatApi.Entities;
using ChatApi.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Services
{
    public class GroupService : IGroupService
    {
        private readonly DataContext context;

        public GroupService(DataContext context)
        {
            this.context = context;
        }
        public void CreateGroup(Group model)
        {
            if(model != null)
            {
                context.Groups.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(Group group)
        {
            context.Groups.Remove(group);
            context.SaveChanges();
        }

        public async Task<Group> Group(int id)
        {
            var group = await context.Groups.FindAsync(id);
            return group;
        }

        public async Task<ICollection<Group>> Groups()
        {
            var groups = await context.Groups.ToListAsync();
            return groups;
        }

        public void UpdateGroup(Group model)
        {
            context.Groups.Update(model);
            context.SaveChanges();
        }
    }
}
