using BianCore.Tools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.API
{
    public class News
    {
        Network network = new Network();
        public News() {
        }
        public  string GetUpdateNews()
        {
            var awa = (network.HttpGet("https://api.modrinth.com/v2/search")).Content;
            return awa.ToString();
        }
    }
}
