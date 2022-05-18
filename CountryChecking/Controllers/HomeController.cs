using CountryChecking.Models;
using CountryChecking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CountryChecking.Controllers
{
    public class HomeController : Controller
    {
        private CheckerService _checkerService;
        public HomeController(CheckerService checkerService)
        {

            _checkerService = checkerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetAdress(string Country, string City, string Street, string District, string Zip, string HouseNumber)
        {
            var ListAdresses = _checkerService.GetAdressesInfo(Country, City, Street, District, Zip, HouseNumber);
            return Json(ListAdresses);
        }
        [HttpGet]
        public IActionResult GetOutput(string Country, string City, string Street, string District, string Zip, string HouseNumber)
        {
            var first = _checkerService.GetAdressesInfo(Country, City, Street, District, Zip, HouseNumber).FirstOrDefault();
            if(first == null)
            {
                return Json("Answer By Server: Not Find");
            }
            return Json($"Sent to servers: Country / Postcode / City: {first.Country} " +
                $"{(first.PostalCode != "" || first.PostalCode != null ? first.PostalCode : "")} " +
                $"{(first.Street != "" || first.Street != null ? first.Street : "")} " +
                $"Street / {$"{(first.HouseNumber.ToString() != "" ? first.HouseNumber.ToString() + " /" : "")}"}  " +
                $"{(first.District != "" || first.District != null ? first.District : "")}.");
        }


    }
}
