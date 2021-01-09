using escout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace escoutTests.Resources
{
    public class TestUtils
    {
        public static DataContext GetMockContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("escout_db");
            var context = new DataContext(dbContextOptions.Options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
