using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemExam.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Service { get; set; }
        public string StatusOrder { get; set; }
        public string StatusServiceInOrder { get; set; }
        public string LeadTime { get; set; }
        public string PriceOrder { get; set; }
    }
}
