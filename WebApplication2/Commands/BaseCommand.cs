using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Commands
{
    public class BaseCommand
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public virtual void Validate() { }
    }
}
