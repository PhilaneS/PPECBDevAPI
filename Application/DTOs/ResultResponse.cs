using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ResultResponse<T>
    {
        public bool Success { get; set; }        
        public T Data { get; set; } = default!;
    }
}
