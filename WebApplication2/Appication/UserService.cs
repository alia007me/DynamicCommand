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

            commands.ForEach(command =>
            {

                switch (command.Type)
                {

                    case "LegalCommand":
                        {
                            // Check by repository and dosn't exist
                            Legal legal = new Legal()
                            {
                                Id = (command as LegalCommand).Id,
                                Age = (command as LegalCommand).Age
                            };

                            _userRepository.AddLegal(legal);

                            // Check by repository and dose exist
                            var oldLegal = _userRepository.GetLegal((command as LegalCommand).Id);
                            ;
                            _userRepository.UpdateLegal(legal);

                            break;
                        }
                    case "RegisterCommand":
                        {
                            // Check by repository and dosn't exist
                            Register register = new Register()
                            {
                                Id = (command as RegisterCommand).Id,
                                Name = (command as RegisterCommand).Name
                            };

                            _userRepository.AddRegister(register);

                            // Check by repository and dosn't exist
                            var oldRegister = _userRepository.GetRegister((command as RegisterCommand).Id);

                            _userRepository.UpdateRegister(register);

                            break;
                        }
                }
            });

            // Save

            return null;
        }

        private void AddOrUpdateRegisterCommand(RegisterCommand command, List<string> permits)
        {
            // Check by repository and dosn't exist
            Register register = new Register()
            {
                Id = command.Id,
                Name = command.Name
            };

            _userRepository.AddRegister(register);

            // Check by repository and dosn't exist
            var oldRegister = _userRepository.GetRegister(command.Id);
            oldRegister.Id = IsInPermits(permits, "Id") ? command.Id : oldRegister.Id;
            oldRegister.Name = IsInPermits(permits, "Name") ? command.Name : oldRegister.Name;

            _userRepository.UpdateRegister(register);
        }
        private void AddOrUpdateLegalCommand(LegalCommand command, List<string> permits)
        {
            // Check by repository and dosn't exist
            Legal legal = new Legal()
            {
                Id = command.Id,
                Age = command.Age
            };

            _userRepository.AddLegal(legal);

            // Check by repository and dose exist
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