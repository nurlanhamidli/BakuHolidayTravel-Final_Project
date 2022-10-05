using bht_demo.Models;
using System.Collections.Generic;
using X.PagedList;

namespace bht_demo.ViewModels
{
    public class BlogVM
    {
        public List<Blogs> Blogs { get; set; }
        public BlogCategory BlogCategory { get; set; }
    }
}
