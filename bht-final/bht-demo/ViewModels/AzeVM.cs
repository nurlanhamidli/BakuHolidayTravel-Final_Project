using bht_demo.Models;
using System.Collections.Generic;

namespace bht_demo.ViewModels
{
    public class AzeVM
    {
        public List<Slider> sliders { get; set; }
        public IEnumerable<Tours> Tours { get; set; }
        public Tours Tour { get; set; }
    }
}
