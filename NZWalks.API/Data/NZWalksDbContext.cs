using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
        {
            public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions): base(dbContextOptions)
            {
                
            }
             public DbSet<Difficulty> Difficulties { get; set; }
            public DbSet <Region>Regions { get; set; }
            public DbSet <Walk>Walks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id = Guid.Parse("67baf9d3-1f6a-4273-bbdb-0a97e42582cb"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("8d0618e1-5d64-4aa4-bb42-254767ec3752"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("812d6ace-115c-4866-83bc-1b162dce1d5a"),
                    Name = "Hard"
                }
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            var regions = new List<Region>
{
                new Region
                {
                    Id = Guid.Parse("e7044cdb-f20f-4a90-893a-e357f2aecdf0"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/4.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100001"),
                    Name = "Wellington",
                    Code = "WLG",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/5.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100002"),
                    Name = "Christchurch",
                    Code = "CHC",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/1.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100003"),
                    Name = "Hamilton",
                    Code = "HLZ",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/2.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100004"),
                    Name = "Tauranga",
                    Code = "TRG",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/3.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100005"),
                    Name = "Napier",
                    Code = "NPE",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/4.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100006"),
                    Name = "Dunedin",
                    Code = "DUD",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/5.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100007"),
                    Name = "Palmerston North",
                    Code = "PMR",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/1.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100008"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/2.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100009"),
                    Name = "Rotorua",
                    Code = "ROT",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/3.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100010"),
                    Name = "New Plymouth",
                    Code = "NPL",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/4.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100011"),
                    Name = "Invercargill",
                    Code = "IVC",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/5.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100012"),
                    Name = "Whangarei",
                    Code = "WRE",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/1.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100013"),
                    Name = "Gisborne",
                    Code = "GIS",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/2.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100014"),
                    Name = "Timaru",
                    Code = "TIU",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/3.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100015"),
                    Name = "Queenstown",
                    Code = "ZQN",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/4.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100016"),
                    Name = "Masterton",
                    Code = "MST",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/5.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100017"),
                    Name = "Kaikoura",
                    Code = "KKR",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/1.sm.jpg"
                },
                new Region
                {
                    Id = Guid.Parse("a18c7c1d-8a27-42f5-9e30-5a5cce100018"),
                    Name = "Taupo",
                    Code = "TUO",
                    RegionImageUrl = "https://www.gstatic.com/webp/gallery/2.sm.jpg"
                }
            };
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
