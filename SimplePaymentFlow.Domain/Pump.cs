using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimplePaymentFlow.Domain
{
    public class Pump: Entity
    {
        public string Name { get; set; }
        public bool Locked { get; set; }
        public int SiteId { get; set; }
        [JsonIgnore]
        public Site Site { get; set; }

        public Pump(int id, string name, bool locked, int siteId) : base(id)
        {
            Name = name;
            Locked = locked;
            SiteId = siteId;
        }
    }
}
