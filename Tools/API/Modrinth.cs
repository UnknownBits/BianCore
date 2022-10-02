using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.Tools.API
{
    public static class Modrinth
    {
        public class V2
        {
            public string Search()
            {
                try
                {
                    return Tools.Network.HttpGet("https://api.modrinth.com/v2/search");
                }
                catch (Exception ex) 
                { 
                    return null; 
                }
            }

        }
    }
}
