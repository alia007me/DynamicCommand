using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Commands
{
    public class CommandField<T>
    {
        public T Value { get; set; }
        public bool Enable { get; set; }
    }
}
