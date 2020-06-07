using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EfCoreBugExperiment
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(e => e
                .HasOne(o => o.Extension)
                .WithOne(o => o.Address)
                .HasForeignKey<Address>(o => o.Id));

            modelBuilder.Entity<AddressExtension>(e =>
            {
                e.ToTable("Addresses");
                e.HasOne(o => o.Address)
                    .WithOne(o => o.Extension)
                    .HasForeignKey<AddressExtension>(o => o.Id);
            });
        }
    }

    public class Person
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        public Address Address { get; set; }
    }

    public class Address
    {
        [Key]
        public string Id { get; set; }
        public string PostCode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }

        public string PersonId { get; set; }
        public Person Person { get; set; }

        public AddressExtension Extension { get; set; }
    }

    public class AddressExtension
    {
        [Key]
        public string Id { get; set; }
        public string Extension { get; set; }

        public Address Address { get; set; }
    }
}
