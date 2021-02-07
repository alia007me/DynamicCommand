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

            List<BaseCommand> commands = new List<BaseCommand>();

            jsCommandsList.ForEach(x =>
            {
                string type = x["type"].ToString();

                x.ToList().RemoveAt(0);

                commands.Add((BaseCommand)x.ToObject(Type.GetType(type)));
            });

            UserService userService = new UserService();

            userService.Register(commands);

            return Ok();
        }
    }
}
