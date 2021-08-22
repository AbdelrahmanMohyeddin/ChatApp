using AutoMapper.Configuration;
using ChatApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Helpers
{
    public class DataContext : IdentityDbContext
    {

        //private readonly IConfiguration Configuration;

        public DataContext( DbContextOptions<DataContext> options) :base(options)
        {
            
        }
        public DbSet<AppUser> users { get; set; }
        public DbSet<UserMessages> privateMessages { get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<GroupMessages> groupMessages { get; set; }
        public DbSet<UserGroup> userGroups { get; set; }
        public DbSet<PrivateChat> privateChats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserGroup>().HasKey(sc => new { sc.AppUserId, sc.GroupId });

        }

    }
}
