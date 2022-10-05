using System.Collections;
using System.Collections.Generic;

namespace bht_demo.Models
{
    public class BlogCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string ImgUrl { get; set; }
        public IEnumerable<Blogs> Blog { get; set; }
    }
}
