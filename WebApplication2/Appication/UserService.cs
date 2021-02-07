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
                string commandType = command.GetType().Name;

                switch (commandType)
                {
                    case "LegalCommand":
                        {
                            Legal legal = new Legal()
                            {
                                Id = (command as LegalCommand).Id,
                                Age = (command as LegalCommand).Age
                            };

                            // Check by repository and dosn't exist
                            _userRepository.AddLegal(legal);

                            // Check by repository and dose exist
                            _userRepository.UpdateLegal(legal);

                            break;
                        }
                    case "RegisterCommand":
                        {
                            Register register = new Register()
                            {
                                Id = (command as RegisterCommand).Id,
                                Name = (command as RegisterCommand).Name
                            };

                            // Check by repository and dosn't exist
                            _userRepository.AddRegister(register);

                            // Check by repository and dosn't exist
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