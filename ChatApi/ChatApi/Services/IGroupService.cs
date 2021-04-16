using ChatApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Services
{
    public interface IGroupService
    {
        Task<ICollection<Group>> Groups();
        Task<Group> Group(int id);
        void CreateGroup(Group model);
        void UpdateGroup(Group model);
        void Delete(Group group);
    }
}
