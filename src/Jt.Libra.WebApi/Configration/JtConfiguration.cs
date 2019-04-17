using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Jt.Libra.WebApi.Configuration
{
    public class JtConfiguration
    {
        public static IConfigurationRoot Instance { get; private set; }

        private JtConfiguration() { }
        public static IConfigurationRoot GetInstance()
        {
            if (Instance == null)
            {
                var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var appRoot = Path.GetDirectoryName(location);
                var builder = new ConfigurationBuilder()
                    .SetBasePath(appRoot)
                    .AddJsonFile("appsettings.json");

                Instance = builder.Build();
            }
            return Instance;
        }
    }
}
