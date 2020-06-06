using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace XUnitTestProject1
{
    public abstract class DatabaseTests
    {
        protected DbContextOptions<MyDbContext> ContextOptions;

        protected DatabaseTests(DbContextOptions<MyDbContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        private void Seed()
        {
            using (var context = new MyDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        [Fact]
        public async Task Adding_Related_Entity_OnTheFly_Works()
        {
            // Act
            using (var context = new MyDbContext(ContextOptions))
            {
                await context.Addresses.AddAsync(new Address()
                {
                    Id = Guid.NewGuid().ToString(),
                    Street = "Damrak",
                    PostCode = "1000AB",
                    City = "Amsterdam",
                    Person = new Person
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Hakan Tuncer"
                    }
                });

                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new MyDbContext(ContextOptions))
            {
                var personCount = await context.Persons.CountAsync();
                var addressCount = await context.Addresses.CountAsync();

                Assert.Equal(1, personCount);
                Assert.Equal(1, addressCount);
            }
        }
    }
}
