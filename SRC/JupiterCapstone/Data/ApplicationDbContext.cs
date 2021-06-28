using JupiterCapstone.Models;
using JupiterCapstone.Services.AuthorizationServices;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
        {
        }


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<CardDetail> CardDetails { get; set; }

        //public virtual DbSet<Cart> Carts { get; set; }

        public virtual DbSet<CartItem> ShoppingCartItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        //public virtual DbSet<WishList> WishLists { get; set; }
        public virtual DbSet<WishListItem> WishListItems { get; set; } 
        public virtual DbSet<UsersAddress> UsersAddresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.Property(e => e.JwtId).IsRequired();

                entity.Property(e => e.Token).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshToken)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RefreshToken_UsersMaster");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.UserName).ValueGeneratedNever();

                //entity.Property(e => e.PhoneNumber).IsRequired();

                //check this username, I dont need it
               // entity.Property(e => e.UserName).IsRequired();
            });

            //check it out incase it fails

           // OnModelCreatingPartial(modelBuilder);
        
        }
        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
