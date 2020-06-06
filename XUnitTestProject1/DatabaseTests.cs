using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EfCoreBugExperiment
{
    public abstract class DatabaseTests
    {
        protected DbContextOptions<MyDbContext> ContextOptions;
        private readonly string _addressId = Guid.NewGuid().ToString();

        protected DatabaseTests(DbContextOptions<MyDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        private void Seed()
        {
            using var context = new MyDbContext(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Addresses.Add(new Address()
            {
                Id = _addressId,
                Street = "Damrak",
                PostCode = "1000AB",
                City = "Amsterdam",
            });

            context.SaveChanges();
        }

        [Fact]
        public async Task Adding_Related_Entity_OnTheFly_Works()
        {
            // Act
            await using (var context = new MyDbContext(ContextOptions))
            {
                var address = await context.Addresses.FindAsync(_addressId);
                address.Person = new Person
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Hakan Tuncer"
                };
                await context.SaveChangesAsync();
            }

            // Assert
            await using (var context = new MyDbContext(ContextOptions))
            {
                var personCount = await context.Persons.CountAsync();
                var addressCount = await context.Addresses.CountAsync();

                Assert.Equal(1, personCount);
                Assert.Equal(1, addressCount);
            }
        }
    }
}
