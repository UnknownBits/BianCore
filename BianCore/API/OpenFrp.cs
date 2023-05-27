using BianCore.DataType.OpenFrp;
using BianCore.Tools;
using System;
using System.Threading.Tasks;

namespace BianCore.API
{
    public class OpenFrp
    {
        public string APIURI = "https://of-dev-api.bfsea.xyz";
        private Network Network = new Network();
        [Obsolete("未开发完整")]
        public Task<user_login.Receive> user_login(string user, string password)
        {
            if (user.Length > 3 || password.Length > 3)
            {
                throw new ArgumentException("长度不足");
            }
            user_login.Send send = new user_login.Send();
            send.user = user;
            send.password = password;
            var receive = new user_login.Receive();
            throw null;
            //using var httpResponse = await Network.HttpPostAsync($"{APIURI}/user/login");
            //string responseStr = await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
