using bht_demo.Models;
using System.Collections.Generic;

namespace bht_demo.Areas.AdminPanel.ViewModels
{
    public class BlogViewModel
    {
        public List<Blogs> Blogs { get; set; }
        public BlogCategory BlogCategory { get; set; }
        public List<BlogCategory> BlogCategories { get; set; }
        public Blogs Blog { get; set; }
    }
}
