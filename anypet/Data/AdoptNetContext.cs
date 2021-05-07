using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using anypet.Models;

namespace AdoptNet.Data
{
    public class AdoptNetContext : DbContext
    {
        public AdoptNetContext (DbContextOptions<AdoptNetContext> options)
            : base(options)
        {
        }

        public DbSet<anypet.Models.UserReg> UserReg { get; set; }
    }
}
