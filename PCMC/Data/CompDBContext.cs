using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCMC.Models;
using Microsoft.EntityFrameworkCore;
using PCMC.Entities;

namespace PCMC.Data
{
    public class CompDBContext : DbContext
    {
        public CompDBContext(DbContextOptions<CompDBContext> options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<School>().ToTable("School");
        }
    }
}