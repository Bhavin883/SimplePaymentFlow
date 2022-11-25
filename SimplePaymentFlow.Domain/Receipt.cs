using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimplePaymentFlow.Domain
{
    public class Receipt
    {
        public string Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public int PumpId { get; set; }
        public Receipt(string id, DateTime start, DateTime? end, int pumpId) 
        {
            Id = id;
            Start = start;
            End = end;
            PumpId = pumpId;
        }
    }
}
