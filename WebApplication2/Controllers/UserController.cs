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
        public IActionResult Register(object commands)
        {
            #region Not Work Because Noob
            //var test = commands.ToString();
            //var test2 = test.Replace(" ", "");
            //var test3 = test2.Replace(Environment.NewLine, "");

            //var test4 = test3.Substring(test3.IndexOf('[') + 1, (test3.LastIndexOf(']') - test3.IndexOf('[') - 1));



            //var testNewTone = 

            //List<string> testt = new List<string>();
            //for (int i = 0; i < test4.Length; i++)
            //{
            //    if (test4[i] == '{')
            //        testt.Add("");

            //    testt[testt.Count - 1] += test4[i];
            //}

            //List<string> testt2 = new List<string>();

            //testt.ForEach(x =>
            //{
            //    x = x.Replace("\"", "");
            //    testt2.Add(x.Substring(x.IndexOf('{')+1 , (x.IndexOf('}') - x.IndexOf('{') )));
            //});

            //List<List<string>> commandProperties = new List<List<string>>();

            //testt2.ForEach(x =>
            //{
            //    commandProperties.Add(new List<string>());
            //    x.Split(',').ToList().ForEach(y =>
            //    {
            //        if (y != "")
            //            commandProperties[commandProperties.Count - 1].Add(y.Split(':')[0]);
            //    });
            //});
            #endregion

            JObject jsonCommands = JObject.Parse(commands.ToString());

            var commandsList = jsonCommands["commands"].ToList();

            List<BaseCommand> cmds = new List<BaseCommand>();

            commandsList.ForEach(x =>
            {
                string type = x["type"].ToString();

                x.ToList().RemoveAt(0);

                cmds.Add((BaseCommand)x.ToObject(Type.GetType(type)));
            });

            UserService userService = new UserService();

            userService.Register(cmds);

            return Ok();
        }
    }
}
