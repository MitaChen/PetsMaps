using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PetsMapsCF.Models
{
    public class Context:DbContext
    {
        public Context() :base("name=PetsMapsCFConnection")
        { }

        public DbSet<Administrators> Administrators { get; set; }
        public DbSet<GroupBuyingOrders> GroupBuyingOrders { get; set; }
        public DbSet<Maps> Maps  { get; set; }
        public DbSet<Members> Members  { get; set; }
        public DbSet<PayTypes> PayTypes  { get; set; }
        public DbSet<Products> Products  { get; set; }
        public DbSet<Shippers> Shippers { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public System.Data.Entity.DbSet<PetsMapsCF.Models.VMMember> VMMembers { get; set; }
    }
}