using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace BianCore.Modules.BianAccount
{
    public class MySql
    {
        private MySqlConnection conn;

        public MySql() { }

        public void Connect(string server, int port, string database, string username, string password)
        {
            conn = new MySqlConnection($"server = {server}; user = {username}; database = {database}; port = {port}; password = {password}");
            conn.Open();
        }
    }
}
