using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductCodeSequence
    {
        public required string Id { get; set; }
        public required int LastNumber { get; set; }
    }
}
