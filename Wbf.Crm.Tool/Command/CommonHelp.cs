using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wbf.Crm.Tool.Model;

namespace Wbf.Crm.Tool.Command
{
    public static class CommonHelp
    {
        public static List<ConnectionModel> GetConnectionConfig()
        {
            if (!File.Exists($"Config/ConnectionConfig.txt"))
            {
                return new List<ConnectionModel>();
            }
            string[] lines = File.ReadAllLines($"Config/ConnectionConfig.txt");

            return lines.Length > 0 && lines[0].Length > 0 ? JsonConvert.DeserializeObject<List<ConnectionModel>>(lines[0]) : new List<ConnectionModel>();
        }

        public static List<CombItem> GetConnectionList(List<ConnectionModel> connection)
        {
            return connection.Select(x => new CombItem() { Key = x.Key, Value = GetConnection(x) }).ToList();
        }   

        private static string GetConnection(ConnectionModel model)
        {
            //Url=https://honoruat.crm4.dynamics.com;Username=crmdev@hihonor.onmicrosoft.com;Password=honor@dev123;authtype=Office365
            string connection = string.Empty;
            switch (model.AuthType)
            {
                case "Office365":
                    connection = $@"Url={model.FullServer};Username={model.UserName};Password={model.Password};authtype=Office365";
                    break;
                default:
                    break;
            }
            return connection;
        }
    }
}
