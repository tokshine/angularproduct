using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApiTestStandAlone.Entities
{
    public class BetContext :DbContext
    {

        public DbSet<Bet> Bet { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Player> Player { get; set; }

        public BetContext(DbContextOptions<BetContext> options):base(options)
        {
            Database.EnsureCreated();
        }


        //one approach
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder )
        //{
        //    optionsBuilder.UseSqlServer("connectionString");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
