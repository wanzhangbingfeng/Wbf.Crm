using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wbf.Crm.Honor
{
    public class AxureCon
    {
        public void testAsync()
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            string accessToken = azureServiceTokenProvider.GetAccessTokenAsync("https://honordev.crm5.dynamics.com", "hihonor.onmicrosoft.com").GetAwaiter().GetResult();



            //Principal principal = azureServiceTokenProvider.PrincipalUsed;

            //Console.WriteLine($"AppId: {principal.AppId}");

            //Console.WriteLine($"IsAuthenticated: {principal.IsAuthenticated}");

            //Console.WriteLine($"TenantId: {principal.TenantId}");

            //Console.WriteLine($"Type: {principal.Type}");

            //Console.WriteLine($"UserPrincipalName: {principal.UserPrincipalName}");



            using (var connection = new SqlConnection("Data Source=honordev.crm5.dynamics.com;Initial Catalog=orgad48402f;Persist Security Info=True;User ID=crmdev@hihonor.onmicrosoft.com;Authentication='honor@dev123'"))
            {

                connection.AccessToken = accessToken;

                //connection.OpenAsync().GetAwaiter();

                connection.Open();

                var command = new SqlCommand("select top 1 fullname from contact", connection);

                using (SqlDataReader reader =  command.ExecuteReaderAsync().GetAwaiter().GetResult())
                {

                    reader.ReadAsync().GetAwaiter();

                    Console.WriteLine(reader.GetValue(0));

                }

            }
            Console.ReadKey();

        }
    }
}
