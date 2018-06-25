using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Adressbuch.Server.DataService
{
    /// <summary>
    /// This class contains the entrypoint of the web api
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entrypoint of the web api
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Builds the webhost
        /// </summary>
        /// <param name="args">The arguments, e.g. received in <see cref="Main(string[])"/></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
