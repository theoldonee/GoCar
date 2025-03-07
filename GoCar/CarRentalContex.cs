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
        DbSet<Client> Client { get; set; }
        DbSet<Car> Car { get; set; }

        DbSet<Rental> Rental { get; set; }


        // Configure connection string and options
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "YOUR CONNECTION STRING";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
