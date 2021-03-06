using CountryChecking.Models;
using ServiceReference1;
using System.Collections.Generic;
using System.Linq;

namespace CountryChecking.Services
{
    public class CheckerService
    {
        private const string LOGIN = "testuser";
        private const string PASSWORD = "TgNmUh-uw!sl$";
        private const int TOOLERANCE = 2;
        private const int MIN_SIMILARITY_FOR_ONE = 95;
        public List<AdressViewModel> GetAdressesInfo(string Country, string City, string Street, string District, string Zip, string StrNumber)
        {
            var service = new ServiceReference1.CheckAddressRequestBody();

            var post = new ServiceReference1.QACWebServiceSoapClient(ServiceReference1.QACWebServiceSoapClient.EndpointConfiguration.QACWebServiceSoap12);

            ClQACAddress adress;
            if (int.TryParse(StrNumber, out int HouseNumber))
            {
                adress = new ServiceReference1.ClQACAddress()
                {
                    m_sCountry = Country,
                    m_sCity = City,
                    m_sStreet = Street,
                    m_sZIP = Zip,
                    m_iHouseNo = HouseNumber,
                    m_sDistrict = District,
                    m_iHouseNoStart = 1,
                    m_iHouseNoEnd = 100,

                };
            }
            else
            {
                adress = new ServiceReference1.ClQACAddress()
                {
                    m_sCountry = Country,
                    m_sCity = City,
                    m_sStreet = Street,
                    m_sZIP = Zip,
                    m_sDistrict = District,
                    m_iHouseNoStart = 1,
                    m_iHouseNoEnd = 100,

                };
            }

            var res = post.UCheckAddressAsync(LOGIN, PASSWORD, TOOLERANCE, adress);
            res.Wait();

            if (res.Result.Body.UCheckAddressResult.SimilarAddresses.Count <= 0 || res.Result.Body.UCheckAddressResult.ResultStatus == -1)
                return null;

            var ListAdresses = new List<AdressViewModel>();
            bool multiply = true;
            switch (res.Result.Body.UCheckAddressResult.ResultStatus)
            {
                case -2:
                case -1:
                case  0:
                    return null;
                case 1:
                case 2:
                    multiply = false;
                    break;
            }

            var listRes = res.Result.Body.UCheckAddressResult.SimilarAddresses;
            var addressView = new AdressViewModel() { Similarity = 0 };
            foreach (ClQACSimilarAddress MyAdress in listRes)
            {

                var adressViewModel = new AdressViewModel()
                {
                    Country = MyAdress.Address.m_sCountry,
                    City = MyAdress.Address.m_sCity,
                    District = MyAdress.Address.m_sDistrict,
                    Street = MyAdress.Address.m_sStreet,
                    HouseNumber = MyAdress.Address.m_iHouseNo,
                    PostalCode = MyAdress.Address.m_sZIP,
                    Similarity = MyAdress.Similarity,
                };
                ListAdresses.Add(adressViewModel);
            }
            if(!multiply)
            {
                return new List<AdressViewModel>() { ListAdresses.FirstOrDefault() };
            }
            return ListAdresses;
        }
    }
}
