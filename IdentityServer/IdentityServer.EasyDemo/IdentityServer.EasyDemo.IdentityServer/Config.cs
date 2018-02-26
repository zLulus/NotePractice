using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.EasyDemo.IdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                //参数是资源名称,资源显示名称
                new ApiResource("api1", "My API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // 用于验证的secret
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // 允许的范围
                    AllowedScopes = { "api1" }
                }
            };
        }
    }
}
