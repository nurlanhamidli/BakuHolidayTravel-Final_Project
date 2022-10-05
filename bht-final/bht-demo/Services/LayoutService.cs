using bht_demo.Controllers;
using bht_demo.DAL;
using bht_demo.Models;
using bht_demo.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace bht_demo.Services
{
    public class LayoutService
    {
        private AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public Setting GetSetting()
        {
            return _context.Settings.FirstOrDefault();
        }
        public List<BlogCategory> GetBlogCategory()
        {
            return _context.BlogCategories.ToList();
        }
        public List<ServiceAndAbout> GetAbouts()
        {
            return _context.ServiceAndAbouts.Where(s => s.IsDeleted == false && s.Type == 1).ToList();
        }
        public BlogCategory GetBlog()
        {
            return _context.BlogCategories.Where(c => c.Id == 3).FirstOrDefault();
        }
        public List<Blogs> GetNews()
        {
            return _context.Blogs.Where(b => b.BlogCategoryId == 2).OrderByDescending(b => b.Id).Take(3).ToList();
        }
        public List<Blogs> GetAch()
        {
            return _context.Blogs.Where(b => b.BlogCategoryId == 3).OrderByDescending(b => b.Id).Take(3).ToList();
        }
    }
}
