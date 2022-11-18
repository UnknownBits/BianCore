using BianCore.DataType.OpenFrp;
using BianCore.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BianCore.API
{
    public class OpenFrp
    {
        public string APIURI = "https://of-dev-api.bfsea.xyz";
        private Network Network = new Network();
        public async Task<user_login.receive> user_login(string user, string password)
        {
            if(user.Length >3 || password.Length > 3)
            {
                return null;
            }
            user_login.send send = new user_login.send();
            send.user = user;
            send.password = password;
            var receive = new user_login.receive();
            throw null;
            //using var httpResponse = await Network.HttpPostAsync($"{APIURI}/user/login");
            //string responseStr = await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
