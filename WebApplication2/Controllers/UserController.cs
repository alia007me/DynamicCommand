using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Commands;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using WebApplication2.Appication;
using System.Reflection;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult Register(object jsonCommands)
        {
            JObject jsonObjectCommands = JObject.Parse(jsonCommands.ToString());

            var jsCommandsList = jsonObjectCommands["commands"].ToList();
            var permitsList = jsonObjectCommands["Permit"].ToList();

            List<string> permits = new List<string>();
            permitsList.ForEach(p =>
            {
                permits.Add(p.ToString());
            });

            List<BaseCommand> commands = new List<BaseCommand>();

            jsCommandsList.ForEach(x =>
            {
                string type = ((JObject)x).GetValue("type").ToString();

                Type _type = Assembly.GetAssembly(typeof(BaseCommand)).GetTypes().SingleOrDefault(c => c.Name == type);

                commands.Add((BaseCommand)x.ToObject(_type, new JsonSerializer()
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                }));
            });

            UserService userService = new UserService();

            userService.Register(commands, permits);

            return Ok();
        }
    }
}
