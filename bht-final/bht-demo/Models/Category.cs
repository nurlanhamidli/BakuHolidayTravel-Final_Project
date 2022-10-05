using System.Collections.Generic;

namespace bht_demo.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<Tours> Tours { get; set; }
    }
}
