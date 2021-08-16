using AutoMapper.Configuration;
using ChatApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Helpers
{
    public class DataContext : DbContext
    {

        //private readonly IConfiguration Configuration;

        public DataContext( DbContextOptions<DataContext> options) :base(options)
        {
            
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<GroupUser> groupUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GroupUser>().HasKey(
                k => new { k.GroupId, k.UserId }
                );
        }

    }
}
