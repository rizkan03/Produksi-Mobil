using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ProduksiMobil.Models;

namespace ProduksiMobil.DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("name=ApplicationContext")
        {
        }

        public DbSet<Planning> Planning { get; set; }
    }
}