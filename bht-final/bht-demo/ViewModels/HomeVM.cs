using bht_demo.Models;
using System.Collections;
using System.Collections.Generic;

namespace bht_demo.ViewModels
{
    public class HomeVM
    {
        public Setting settings { get; set; }
        public List<Slider> sliders { get; set; }
        public IEnumerable<Certificate> certificates { get; set; }
        public IEnumerable<Places> places { get; set; }
        public IEnumerable<Tours> ForeignTours { get; set; }
        public IEnumerable<Tours> KruizTours { get; set; }
        public IEnumerable<Tours> QrupTours { get; set; }
        public IEnumerable<Tours> FeaturedTours { get; set; }
    }
}
