using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _1670WebApplication.Models;

namespace _1670WebApplication.Data
{
    public class _1670WebApplicationContext : DbContext
    {
        public _1670WebApplicationContext (DbContextOptions<_1670WebApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<_1670WebApplication.Models.User> User { get; set; } = default!;

        public DbSet<_1670WebApplication.Models.JobList> JobList { get; set; } = default!;

        public DbSet<_1670WebApplication.Models.Admin> Admin { get; set; } = default!;

        public DbSet<_1670WebApplication.Models.Application> Application { get; set; } = default!;

        public DbSet<_1670WebApplication.Models.Category> Category { get; set; } = default!;

        public DbSet<_1670WebApplication.Models.Employer> Employer { get; set; } = default!;

        public DbSet<_1670WebApplication.Models.JobSeekers> JobSeekers { get; set; } = default!;
    }
}
