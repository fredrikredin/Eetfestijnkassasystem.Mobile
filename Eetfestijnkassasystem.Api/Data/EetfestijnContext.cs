using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Eetfestijnkassasystem.Shared.Model;
using Microsoft.Data.Sqlite;

namespace Eetfestijnkassasystem.Api.Data
{
    public class EetfestijnContext : DbContext
    {
        public EetfestijnContext (DbContextOptions<EetfestijnContext> options) : base(options)
        {
            
        }

        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Event> Event { get; set; }
    }
}
