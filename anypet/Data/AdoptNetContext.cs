using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using anypet.Models;
using AdoptNet.Models;
using System.Collections;

namespace AdoptNet.Data
{
    public class AdoptNetContext : DbContext
    {
        public AdoptNetContext (DbContextOptions<AdoptNetContext> options)
            : base(options)
        {
        }
        


        public DbSet<anypet.Models.Animal> Animal { get; set; }

       // public DbSet<anypet.Models.Association> Association { get; set; }

        public DbSet<anypet.Models.AnimalImage> AnimalImage { get; set; }

       // public DbSet<anypet.Models.AssociationImage> AssociationImage { get; set; }

        public DbSet<AdoptNet.Models.AdoptionDays> AdoptionDays { get; set; }

       
        public DbSet<anypet.Models.User> User { get; set; }

       
        public DbSet<anypet.Models.Products> Products { get; set; }

       
        public DbSet<anypet.Models.Association> Association { get; set; }

       
        public DbSet<anypet.Models.AssociationImages> AssociationImages { get; set; }

       
  
        
    }
}
