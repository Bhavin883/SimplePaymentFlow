using Microsoft.EntityFrameworkCore;
using SimplePaymentFlow.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaymentFlow.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Site> Sites { get; set; }
        public DbSet<Pump> Pumps { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
