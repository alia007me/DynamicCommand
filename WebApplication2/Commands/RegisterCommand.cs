using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Commands
{
    public class RegisterCommand : BaseCommand
    {
        public CommandField<string> Name { get; set; }
        public override void Validate()
        {
            Console.WriteLine("Validated!");
        }
    }
}
