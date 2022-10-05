using bht_demo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bht_demo.DAL
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Places> Places { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tours> Tours { get; set; }
        public DbSet<ServiceAndAbout> ServiceAndAbouts { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
    }
}
