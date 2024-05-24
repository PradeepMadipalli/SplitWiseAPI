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
            builder.Entity<ExpenseDetails>()
                .HasOne(x => x.Expenses)
                .WithMany(d=>d.Expensess)
                .HasForeignKey(x => x.expId);
        }
        public DbSet<Group> Group { get; set; }
        public DbSet<UsersGroup> UserGroups { get; set; }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseDetails> ExpensesDetails { get; set;}
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<SettleUp> SettleUps { get; set; }

    }
}
