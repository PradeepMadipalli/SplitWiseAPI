using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SplitWise.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitWise.Common
{
    public class SplitWiseDBContext : IdentityDbContext
    {
        public SplitWiseDBContext(DbContextOptions<SplitWiseDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UsersGroup>()
                .HasOne(a => a.User)
                 .WithMany(d => d.Groups)
                 .HasForeignKey(d => d.UserId);
            builder.Entity<UsersGroup>()
                .HasOne(a => a.Group)
                .WithMany(d => d.Groups)
                .HasForeignKey(d => d.GroupId);
            builder.Entity<UsersGroup>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();
        }
        public DbSet<Group> Group { get; set; }
        public DbSet<UsersGroup> UserGroups { get; set; }

        public DbSet<Categories> Categories { get; set; }

    }
}
