using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using Microsoft.AspNet.Identity.EntityFramework;
using OurMemory.Data.Mapping;
using OurMemory.Domain.Entities;

namespace OurMemory.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
     
        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Veteran> Veterans { get; set; }
        public DbSet<ImageVeteran> ImageVeterans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new VeteranMap());
            modelBuilder.Configurations.Add(new ImageVeteranMap());


            // Needed to ensure subclasses share the same table
            var user = modelBuilder.Entity<User>().ToTable("AspNetUsers");
            user.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            user.HasMany(u => u.Claims).WithRequired().HasForeignKey(uc => uc.UserId);
            user.HasMany(u => u.Logins).WithRequired().HasForeignKey(ul => ul.UserId);
            user.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true }));

            // CONSIDER: u.Email is Required if set on options?
            user.Property(u => u.Email).HasMaxLength(256);

            modelBuilder.Entity<IdentityUserRole>()
                .HasKey(r => new { r.UserId, r.RoleId })
                .ToTable("AspNetUserRoles");

            modelBuilder.Entity<IdentityUserLogin>()

                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                .ToTable("AspNetUserLogins");

            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("AspNetUserClaims");

            var role = modelBuilder.Entity<IdentityRole>()
                .ToTable("AspNetRoles");
            role.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex") { IsUnique = true }));
            role.HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);

        }

        
        public static ApplicationDbContext Create()
        {            
            return new ApplicationDbContext();
        }


        public void Commit()
        {
            base.SaveChanges();
        }

    }
}