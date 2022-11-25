using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaymentFlow.Domain
{
    public class Site : Entity
    {
        public string Name { get; set; }
        public IEnumerable<Pump> Pumps { get; set; }
        public Site(int id, string name) : base(id)
        {
            Name = name;
        }
    }
}
