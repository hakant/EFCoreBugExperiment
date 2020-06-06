using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestProject1
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }

    public class Person
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
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
    }
}
