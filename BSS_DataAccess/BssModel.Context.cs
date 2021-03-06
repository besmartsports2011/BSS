﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BSS_DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class BeSmartSportsEntities : DbContext
    {
        public BeSmartSportsEntities()
            : base("name=BeSmartSportsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<AttributeLov> AttributeLovs { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<GlobalSetting> GlobalSettings { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<StockPurchase> StockPurchases { get; set; }
        public virtual DbSet<StockSale> StockSales { get; set; }
        public virtual DbSet<StockSaleDispute> StockSaleDisputes { get; set; }
        public virtual DbSet<StockShipment> StockShipments { get; set; }
        public virtual DbSet<StockShipmentAttribute> StockShipmentAttributes { get; set; }
        public virtual DbSet<SuperAdmin> SuperAdmins { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<UserLogin_Result> UserLogin(string userName, string password)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UserLogin_Result>("UserLogin", userNameParameter, passwordParameter);
        }
    }
}
