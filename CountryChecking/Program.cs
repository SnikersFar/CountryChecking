using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountryChecking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var service = new ServiceReference1.CheckAddressRequestBody();

            //var post = new ServiceReference1.QACWebServiceSoapClient(ServiceReference1.QACWebServiceSoapClient.EndpointConfiguration.QACWebServiceSoap12);

            //var adress = new ServiceReference1.ClQACAddress()
            //{
            //    m_sCountry = "DE",
            //    m_sCity = "Munchen",
            //    m_sStreet = "Offenbachstr",
            //    m_sZIP = "81245",
            //    m_sDistrict = "",
            //    m_iHouseNo = 4,
            //};

            
            //var resAuto = post.UCheckAddressAsync("testuser", "TgNmUh-uw!sl$", 10, adress);
            //resAuto.Wait();

            //Console.WriteLine();
            //    Console.WriteLine("123");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
