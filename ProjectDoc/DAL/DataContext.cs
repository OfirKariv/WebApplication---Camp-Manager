using ProjectDoc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ProjectDoc.DAL

{
    public class DataContext : DbContext
    {

        public DataContext() { }


        public DbSet<Player> Players { get; set; }
        public DbSet<Camp> Camps { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public DbSet<Admin> Admin { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}