using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GoCar
{
    internal class CarRentalContex : DbContext
    {
        public DbSet<Client> Client { get; set; }
        public DbSet<Car> Car { get; set; }

        public DbSet<Rental> Rental { get; set; }


        // Configure connection string and options
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=ROUVAF;Initial Catalog=GoCarDb;Integrated Security=True;Trust Server Certificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
