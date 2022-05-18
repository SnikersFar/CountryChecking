using CountryChecking.Models;
using ServiceReference1;
using System.Collections.Generic;

namespace CountryChecking.Services
{
    public class CheckerService
    {

        public List<AdressViewModel> GetAdressesInfo(string Country, string City, string Street, string District, string Zip, int HouseNumber)
        {
            var service = new ServiceReference1.CheckAddressRequestBody();

            var post = new ServiceReference1.QACWebServiceSoapClient(ServiceReference1.QACWebServiceSoapClient.EndpointConfiguration.QACWebServiceSoap12);

            var adress = new ServiceReference1.ClQACAddress()
            {
                m_sCountry = Country,
                m_sCity = Street,
                m_sStreet = Street,
                m_sZIP = Zip,
                m_iHouseNo = HouseNumber,
                m_sDistrict = District,
            };
           
            
            var res = post.UCheckAddressAsync("testuser", "TgNmUh-uw!sl$", 10, adress);
            res.Wait();

            if(res.Result.Body.UCheckAddressResult.SimilarAddresses.Count <= 0 || res.Result.Body.UCheckAddressResult.ResultStatus != -1)
                return null;

            var ListAdresses = new List<AdressViewModel>();
            foreach(ClQACAddress MyAdress in res.Result.Body.UCheckAddressResult.SimilarAddresses)
            {
                var AdressViewModel = new AdressViewModel()
                {
                    Country = MyAdress.m_sCountry,
                    City = MyAdress.m_sCity,
                    District = MyAdress.m_sDistrict,
                    Street = MyAdress.m_sStreet,
                    HouseNumber = MyAdress.m_iHouseNo,
                    PostalCode = MyAdress.m_sZIP
                };
                ListAdresses.Add(AdressViewModel);
            }
            return ListAdresses;
        }
    }
}
