﻿using Microsoft.EntityFrameworkCore;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ProblemCategory> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Ticket>Tickets { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Escalation> Escalations { get; set; }
        public DbSet<MessageDetail> MessageDetails { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
              .HasOne(a => a.Account)
              .WithOne(e => e.Employee)
              .HasForeignKey<Account>(e => e.NIK);

            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Account)
                .WithMany(a => a.AccountRole)
                .HasForeignKey(ar => ar.AccountId);

            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Role)
                .WithMany(r => r.AccountRole)
                .HasForeignKey(ar => ar.RoleId);

            modelBuilder.Entity<Ticket>()
                .HasOne(m => m.Message)
                .WithOne(t => t.Ticket)
                .HasForeignKey<Message>(m => m.TicketId);

            modelBuilder.Entity<Status>()
                .HasMany(t => t.Ticket)
                .WithOne(s => s.Status);

            modelBuilder.Entity<Employee>()
                .HasMany(md => md.MessageDetail)
                .WithOne(e => e.Employee)
                .HasForeignKey(t => t.NIK);

            modelBuilder.Entity<Message>()
                .HasMany(md => md.MessageDetail)
                .WithOne(m => m.Message);

            modelBuilder.Entity<Priority>()
                .HasMany(t => t.Ticket)
                .WithOne(p => p.Priority);

            modelBuilder.Entity<Escalation>()
                .HasMany(t => t.Ticket)
                .WithOne(e=>e.Escalation);

            modelBuilder.Entity<ProblemCategory>()
                .HasMany(t => t.Ticket)
                .WithOne(pc => pc.Category);

            modelBuilder.Entity<Employee>()
                .HasMany(t => t.Ticket)
                .WithOne(e => e.Employee)
                .HasForeignKey(t => t.NIK);
        }
    }
}
