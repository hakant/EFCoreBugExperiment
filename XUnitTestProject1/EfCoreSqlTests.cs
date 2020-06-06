using Microsoft.EntityFrameworkCore;

namespace EfCoreBugExperiment
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