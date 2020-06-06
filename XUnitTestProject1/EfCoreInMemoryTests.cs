using Microsoft.EntityFrameworkCore;

namespace EfCoreBugExperiment
{
    public class EfCoreInMemoryTests: DatabaseTests
    {
        public EfCoreInMemoryTests()
            : base(
                new DbContextOptionsBuilder<MyDbContext>()
                    .UseInMemoryDatabase("TestDatabase")
                    .Options)
        {
        }
    }
}
