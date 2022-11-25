using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimplePaymentFlow.Domain;

namespace SimplePaymentFlow.Persistence
{
    public class DbContextGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            if (context.Sites.Any()) return;

            SeedData(context);
        }

        public static void SeedData(AppDbContext context)
        {
            var sites = new List<Site>
            {
            new Site(id: 101, name: "Site 101"),
            new Site(id: 102, name: "Site 102"),
            new Site(id: 103, name: "Site 103"),
            new Site(id: 104, name: "Site 104"),
            new Site(id: 105, name: "Site 105"),

            };
            var pumps = new List<Pump> {
            new Pump(1,"Pump1",false,101),
            new Pump(2,"Pump2",false,101),
            new Pump(3,"Pump3",false,102),
            new Pump(4,"Pump4",false,102),
            new Pump(5,"Pump5",false,103),
            new Pump(6,"Pump6",false,103),
            new Pump(7,"Pump7",false,104),
            new Pump(8,"Pump8",false,104),
            new Pump(9,"Pump9",false,105),
            new Pump(10,"Pump10",false,105),

            };
            context.Sites.AddRange(sites);
            context.Pumps.AddRange(pumps);

            context.SaveChanges();
        }
    }
}
