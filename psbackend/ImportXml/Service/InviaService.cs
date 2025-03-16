using ImportXml.AfiTravelModel;
using ImportXml.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImportXml.Service
{
    public class InviaService
    {
        private readonly CestovkaRepository _cestovkaRepository;
        private readonly CountryRepository _countryRepository;
        private readonly LocalityRepository _localityRepository;
        private readonly HotelRepository _hotelRepository;
        private readonly ImagesRepository _imagesRepository;
        private readonly OfferRepository _offerRepository;

        
        //private readonly JobRepository _jobRepository;



        // Vytvorenie inštancie repository cez DI (dependency injection)
        public InviaService(CestovkaRepository cestovkaRepository, CountryRepository countryRepository, LocalityRepository localityRepository,
                            HotelRepository hotelRepository, ImagesRepository imagesRepository, OfferRepository offerRepository)
        {
            _cestovkaRepository = cestovkaRepository;
            _countryRepository = countryRepository;
            _localityRepository = localityRepository;
            _hotelRepository = hotelRepository;
            _imagesRepository = imagesRepository;
            _offerRepository = offerRepository;

 
            //_jobRepository = jobRepository;
        }

        public async Task ProcessImportXmlFeedAsync(string inputParameter)
        {
            if (!string.IsNullOrEmpty(inputParameter))
            {
                ImputParameter? inputParam = JsonConvert.DeserializeObject<ImputParameter>(inputParameter);
                var cestovkaId = await _cestovkaRepository.GetIdByCestovkaNameAsync("Invia.sk, s.r.o.");
                if (inputParam != null)
                {
                    XDocument? xDoc = await DownloadXmlDocumentAsync(inputParam.Url);
                    if (xDoc != null)
                    {
                        await InviaParseXmlDoc(xDoc, cestovkaId);
                    }
                }
            }
        }

        private async Task InviaParseXmlDoc(XDocument xDoc, Guid cestovkaId)
        {
            foreach (var offer in xDoc.Descendants("offer"))
            {
                var countryId = await _countryRepository.GetOrCreateCountryByNameAsync(offer.Element("destination")?.Element("country")?.Value ?? string.Empty);
                if (!countryId.Equals(Guid.Empty))
                {
                    Guid? localityId = null;
                    localityId = await _localityRepository.GetIdOrCreateLocalityByNameAsync(countryId, offer.Element("destination")?.Element("locality")?.Value ?? string.Empty);
                    var hotel = await _hotelRepository.GetOrCreateHotelByNameCountryLocalityAsync(offer.Element("hotel")?.Value, offer, countryId, localityId, cestovkaId);
                    if (hotel != null)
                    {
                        await _imagesRepository.GetOrCreateImagesByOfferAsync(offer, hotel.Id);
                        var offerId = await _offerRepository.GetIdOrCreateOfferAsync(offer, cestovkaId, hotel.Id);
                    }
                }
            }
        }

        private async Task<XDocument?> DownloadXmlDocumentAsync(string url)
        {
            XDocument xdoc;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var xmlData = await client.GetStringAsync(url);
                    if (!string.IsNullOrEmpty(xmlData))
                    {
                        xdoc = XDocument.Parse(xmlData);
                        return xdoc;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Neočakávaná chyba: {ex.Message}");
                    return null;
                }
            }
            return null;
        }
    }
}
