using FourToWin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FourToWin.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Match>()
                .HasOne<AppUser>(a => a.User1)
                .WithMany(d => d.MatchesU1)
                .HasForeignKey(d => d.User1Id);

            builder.Entity<Match>()
                .HasOne<AppUser>(a => a.User2)
                .WithMany(d => d.MatchesU2)
                .HasForeignKey(d => d.User2Id);
        }

        public DbSet<FourToWin.Models.Match> Match { get; set; }
    }
}
