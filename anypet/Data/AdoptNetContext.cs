using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using anypet.Models;
using AdoptNet.Models;

namespace AdoptNet.Data
{
    public class AdoptNetContext : DbContext
    {
        public AdoptNetContext (DbContextOptions<AdoptNetContext> options)
            : base(options)
        {
        }

<<<<<<< HEAD
<<<<<<< HEAD
      
=======
        public DbSet<anypet.Models.UserReg> UserReg { get; set; }
>>>>>>> parent of beca518 (adding the users conrollers & chaing the about to cshtml & adding permissions for admin (only) and user.)
=======
        public DbSet<anypet.Models.UserReg> UserReg { get; set; }
>>>>>>> parent of beca518 (adding the users conrollers & chaing the about to cshtml & adding permissions for admin (only) and user.)

        public DbSet<anypet.Models.Animal> Animal { get; set; }

        public DbSet<anypet.Models.Association> Association { get; set; }

        public DbSet<anypet.Models.AnimalImage> AnimalImage { get; set; }

        public DbSet<anypet.Models.AssociationImage> AssociationImage { get; set; }

        public DbSet<AdoptNet.Models.AdoptionDays> AdoptionDays { get; set; }
<<<<<<< HEAD
<<<<<<< HEAD

        public DbSet<anypet.Models.User> User { get; set; }
=======
>>>>>>> parent of beca518 (adding the users conrollers & chaing the about to cshtml & adding permissions for admin (only) and user.)
=======
>>>>>>> parent of beca518 (adding the users conrollers & chaing the about to cshtml & adding permissions for admin (only) and user.)
    }
}
