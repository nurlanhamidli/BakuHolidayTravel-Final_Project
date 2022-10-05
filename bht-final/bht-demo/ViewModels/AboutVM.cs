using bht_demo.Models;
using System.Collections;
using System.Collections.Generic;

namespace bht_demo.ViewModels
{
    public class AboutVM
    {
        public IEnumerable<ServiceAndAbout> ServiceAndAbouts { get; set; }
        public List<Blogs> Blogs { get; set; }
        public BlogCategory BlogCategory { get; set; }
    }
}
