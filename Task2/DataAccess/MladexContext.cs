using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Models;

namespace Task2.DataAccess
{
    public class MladexContext : DbContext
    {
        public MladexContext() : base("name=MladexContext")
        {

        }

        public DbSet<Goods> Goods { get; set; }
        public DbSet<GoodsCategory> GoodsCategories { get; set; }
        public DbSet<Pharms> Pharms { get; set; }
        public DbSet<Producers> Producers { get; set; }
        public DbSet<Sales> Sales { get; set; }

    }
}
