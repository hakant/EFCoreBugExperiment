using Microsoft.EntityFrameworkCore;

namespace XUnitTestProject1
{
    public class EfCoreSqlTests : DatabaseTests
    {
        public EfCoreSqlTests()
            : base(
                new DbContextOptionsBuilder<MyDbContext>()
                    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TestDatabase;ConnectRetryCount=0")
                    .Options)
        { }
    }
}