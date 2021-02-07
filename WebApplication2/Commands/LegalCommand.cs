using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Commands
{
    public class LegalCommand : BaseCommand
    {
        public CommandField<int> Age { get; set; }
        public override void Validate()
        {
            Console.WriteLine("Validated!");
        }
    }
}
