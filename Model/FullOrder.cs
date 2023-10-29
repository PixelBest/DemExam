using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemExam.Model
{
    public class FullOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Deadline { get; set; }
        public string AvgDeviation { get; set; }
        public DateTime Date { get; set; }
        public string StatusOrder { get; set; }
        public string StatusServiceInOrder { get; set; }
    }
}
