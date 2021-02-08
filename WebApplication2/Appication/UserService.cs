using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Commands;
using WebApplication2.Domain;
using WebApplication2.Repository;
using System.Linq;

namespace WebApplication2.Appication
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService()
        {
            _userRepository = new UserRepository();
        }
        public Task Register(List<BaseCommand> commands, List<string> permits)
        {
            var registerCommands = commands.OfType<RegisterCommand>().ToList();
            registerCommands.ForEach(registerCommand =>
            {
                AddOrUpdateRegisterCommand(registerCommand, permits);
            });

            var legalCommands = commands.OfType<LegalCommand>().ToList();
            legalCommands.ForEach(legalCommand =>
            {
                AddOrUpdateLegalCommand(legalCommand, permits);
            });

            // Save

            return null;
        }

        private void AddOrUpdateRegisterCommand(RegisterCommand command, List<string> permits)
        {
            // Check by repository and dosn't exist
            // Create
            Register register = new Register()
            {
                Id = command.Id,
                Name = command.Name
            };

            _userRepository.AddRegister(register);

            // Check by repository and dosn't exist
            // Update
            var oldRegister = _userRepository.GetRegister(command.Id);
            oldRegister.Id = IsInPermits(permits, "Id") ? command.Id : oldRegister.Id;
            oldRegister.Name = IsInPermits(permits, "Name") ? command.Name : oldRegister.Name;

            _userRepository.UpdateRegister(register);
        }
        private void AddOrUpdateLegalCommand(LegalCommand command, List<string> permits)
        {
            // Check by repository and dosn't exist
            // Create
            Legal legal = new Legal()
            {
                Id = command.Id,
                Age = command.Age
            };

            _userRepository.AddLegal(legal);

            // Check by repository and dose exist
            // Update
            var oldLegal = _userRepository.GetLegal(command.Id);
            oldLegal.Id = IsInPermits(permits, "Id") ? command.Id : oldLegal.Id;
            oldLegal.Age = IsInPermits(permits, "Age") ? command.Age : oldLegal.Age;

            _userRepository.UpdateLegal(oldLegal);
        }

        private bool IsInPermits(List<string> permits, string permit)
        {
            return permits.Any(p => p == permit);
        }
    }
}