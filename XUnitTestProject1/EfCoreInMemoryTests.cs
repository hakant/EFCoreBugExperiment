using Microsoft.EntityFrameworkCore;

namespace XUnitTestProject1
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
