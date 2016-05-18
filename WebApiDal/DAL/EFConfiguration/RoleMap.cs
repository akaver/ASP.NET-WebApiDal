using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Domain.Identity;

namespace DAL.EFConfiguration
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex") {IsUnique = true}));
            HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);

            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);
        }
    }

    public class RoleIntMap : EntityTypeConfiguration<RoleInt>
    {
        public RoleIntMap()
        {
            Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoleNameIndex") {IsUnique = true}));
            HasMany(r => r.Users).WithRequired().HasForeignKey(ur => ur.RoleId);

            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .IsRequired();

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}