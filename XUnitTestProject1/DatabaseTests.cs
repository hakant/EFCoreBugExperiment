using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EfCoreBugExperiment
{
    public abstract class DatabaseTests
    {
        protected DbContextOptions<MyDbContext> ContextOptions;
        private readonly string _personId = Guid.NewGuid().ToString();

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

            context.Persons.Add(new Person()
            {
                Id = _personId,
                Name = "Hakan Tuncer"
            });

            context.SaveChanges();
        }

        [Fact]
        public async Task Adding_ChainOfEntities_OnTheFly_Works()
        {
            // Act
            await using var context = new MyDbContext(ContextOptions);
            await context.Persons.AddAsync(new Person()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Hakan Tuncer",
                Address = new Address
                {
                    Id = Guid.NewGuid().ToString(),
                    Street = "Damrak",
                    PostCode = "1000AB",
                    City = "Amsterdam",
                    Extension = new AddressExtension()
                }
            });

            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task Adding_Related_Entity_ToAnExisting_Entity_OnTheFly_Works()
        {
            // Act
            await using (var context = new MyDbContext(ContextOptions))
            {
                var person = await context.Persons.FirstOrDefaultAsync(p => p.Id == _personId);
                person.Address = new Address
                {
                    Id = Guid.NewGuid().ToString(),
                    Street = "Damrak",
                    PostCode = "1000AB",
                    City = "Amsterdam",
                    Extension = new AddressExtension()
                };
                await context.SaveChangesAsync();
            }
        }
    }
}
