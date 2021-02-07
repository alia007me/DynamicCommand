using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Commands;
using WebApplication2.Domain;
using WebApplication2.Repository;

namespace WebApplication2.Appication
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService()
        {
            _userRepository = new UserRepository();
        }
        public Task Register(List<BaseCommand> commands)
        {
            commands.ForEach(command =>
            {

                switch (command.Type)
                {
                    case "LegalCommand":
                        {
                            // Check by repository and dosn't exist
                            Legal legal = new Legal()
                            {
                                Id = (command as LegalCommand).Id.Value,
                                Age = (command as LegalCommand).Age.Value
                            };

                            _userRepository.AddLegal(legal);

                            // Check by repository and dose exist
                            var oldLegal = _userRepository.GetLegal((command as LegalCommand).Id.Value);
                            oldLegal.Id = (command as LegalCommand).Id.Enable ? (command as LegalCommand).Id.Value : oldLegal.Id;
                            oldLegal.Age = (command as LegalCommand).Age.Enable ? (command as LegalCommand).Age.Value : oldLegal.Age;
                            _userRepository.UpdateLegal(legal);

                            break;
                        }
                    case "RegisterCommand":
                        {
                            // Check by repository and dosn't exist
                            Register register = new Register()
                            {
                                Id = (command as RegisterCommand).Id.Value,
                                Name = (command as RegisterCommand).Name.Value
                            };

                            _userRepository.AddRegister(register);

                            // Check by repository and dosn't exist
                            var oldRegister = _userRepository.GetRegister((command as RegisterCommand).Id.Value);
                            oldRegister.Id = (command as RegisterCommand).Id.Enable ? (command as RegisterCommand).Id.Value : oldRegister.Id;
                            oldRegister.Name = (command as RegisterCommand).Name.Enable ? (command as RegisterCommand).Name.Value : oldRegister.Name;

                            _userRepository.UpdateRegister(register);

                            break;
                        }
                }
            });

            // Save

            return null;
        }
    }
}