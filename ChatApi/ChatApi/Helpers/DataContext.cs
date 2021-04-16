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
        public DbSet<Account> Accounts { get; set; }
    }
}
