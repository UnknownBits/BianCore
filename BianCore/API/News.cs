using BianCore.Tools;

namespace BianCore.API
{
    public class News
    {
        Network network = new Network();
        public News()
        {
        }
        public string GetUpdateNews()
        {
            var awa = (network.HttpGet("https://api.modrinth.com/v2/search")).Content;
            return awa.ToString();
        }
    }
}
