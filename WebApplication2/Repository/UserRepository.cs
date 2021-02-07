using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Domain;

namespace WebApplication2.Repository
{
    public class UserRepository
    {
        public Task AddLegal(Legal legal)
        {
            Console.WriteLine("Legal Added");

            return null;
        }
        public Task UpdateLegal(Legal legal)
        {
            Console.WriteLine("Legal Updated");

            return Task.CompletedTask;
        }

        public Legal GetLegal(int id)
        {
            return new Legal() { Id = id, Age = 15 };
        }

        public Task AddRegister(Register register)
        {
            Console.WriteLine("Register Accepted");

            return Task.CompletedTask;
        }
        public Task UpdateRegister(Register register)
        {
            Console.WriteLine("Register Updated");

            return Task.CompletedTask;
        }
        public Register GetRegister(int id)
        {
            return new Register() { Id = id, Name = "Ali" };
        }
    }
}
