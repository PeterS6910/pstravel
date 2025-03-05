using ImportXml.AfiTravelModel;
using System;
using System.Xml.Serialization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main()
    {
        // Základná správa na konzolu
        Console.WriteLine("Hello, World!");

        // Volanie funkcie importInvia
        importInvia();
    }

    static async Task importInvia()
    {
        var ck = getCestovkaIdAsync("Invia.sk, s.r.o.");
        Guid cestovkaId = Guid.Parse("c87d1f2f-f8d7-11ef-9f2d-58879c777f19");        
        string filePath = "https://affil.invia.sk/direct/core/tool_xml-feed/download/id/8548540-67c1c02a82ffc/"; // Nahraď správnou cestou k XML

        Offers xmlContent = await DownloadXmlAsync(filePath);
        xmlContent.Id = xmlContent.Id.Equals(Guid.Empty) ? Guid.NewGuid() : xmlContent.Id;
        var newOffers = new Offers
        {
            Id = xmlContent.Id,
            Count = xmlContent.Count,            
            Timestamp = DateTime.Now,
        };
        await ulozEntityAsync<Offers>(newOffers);

        foreach (var offer in xmlContent.Offer)
        {            
            offer.Id =  Guid.NewGuid();
            offer.OffersId = newOffers.Id;
            offer.CestovkaId = cestovkaId;
            await ulozEntityAsync<Offer>(offer);


            //foreach (var photo in offer.Photos)
            //{
            //    photo.Id =  Guid.NewGuid();
            //    photo.OfferId = offer.Id;
            //    await ulozEntityAsync<Photo>(photo);
            //}
            
            //offer.Destination.Id =  Guid.NewGuid();
            //offer.Destination.OfferId = offer.Id;
            //await ulozEntityAsync<Destination>(offer.Destination);

            //foreach (var airport in offer.Airports)
            //{
            //    airport.Id = Guid.NewGuid();
            //    airport.OfferId = offer.Id;
            //    await ulozEntityAsync<Airports>(airport);
            //}

            //offer.Term.Id =  Guid.NewGuid();
            //offer.Term.OfferId = offer.Id;
            //await ulozEntityAsync<Term>(offer.Term);

            //foreach (var tourtype in offer.TourType)
            //{
            //    tourtype.Id =  Guid.NewGuid();
            //    tourtype.OfferId = offer.Id;
            //    await ulozEntityAsync<TourType>(tourtype);
            //}

            //offer.HotelInfo.HotelInfoId = Guid.NewGuid();
            //offer.HotelInfo.OfferId = offer.Id;
            //await ulozEntityAsync<HotelInfo>(offer.HotelInfo);


            //offer.HotelInfo.Coords.Id = Guid.NewGuid();
            //offer.HotelInfo.Coords.HotelInfoId = offer.HotelInfo.HotelInfoId;
            //await ulozEntityAsync<Coords>(offer.HotelInfo.Coords);

            
            //foreach (var actionattributes in offer.Actionattributes)
            //{
            //   actionattributes.Id = Guid.NewGuid();
            //    actionattributes.OfferId = offer.Id;
            //   await ulozEntityAsync<Actionattributes>(actionattributes);
            //}


        }

    }

    static async Task<Cestovka> getCestovkaIdAsync(string cestovka)
    {
        using (var dbContext = new OfferDbContex()) // Ensure the correct name of the context class (OfferDbContex -> OfferDbContext)
        {
            try
            {
                // Await the asynchronous call to get the result
                var result = await dbContext.Cestovka.FirstOrDefaultAsync(x => x.Nazov == cestovka);
                return result;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur
                Console.WriteLine($"Chyba: {ex.Message}");
                return null; 
            }
        }
    }

    static async Task ulozEntityAsync<T>(T entity) where T : class
    {
        using (var dbContext = new OfferDbContex())
        {
            try
            {
                // Pridáme jednu entitu do databázy
                dbContext.Set<T>().Add(entity);

                // Uložíme zmeny do databázy
                dbContext.SaveChanges();
            }

            catch (Exception ex) 
            {
                Console.WriteLine($"Chyba: {ex.InnerException.Message}");
            }
        }
    }


    static async Task<Offers> DownloadXmlAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var filePath = @"C:\\w4\\pstravel\\psbackend\\ImportXml\\Data\\response.xml";
            //var xmlData =  await client.GetStringAsync(url);
            string xmlData = File.ReadAllText(filePath);
            //string xmlData = await File.ReadAllTextAsync(filePath);
           
                XmlSerializer serializer = new XmlSerializer(typeof(Offers), new XmlRootAttribute("offers"));
                using (StringReader reader = new StringReader(xmlData))
                {
                    try
                    {
                        var result = (Offers)serializer.Deserialize(reader);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Chyba pri deserializácii XML: {ex.Message}");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Chyba pri načítaní URL: {ex.Message}");
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine($"Časový limit vypršaný: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neočakávaná chyba: {ex.Message}");
            }

        }
        return null;
    }

    static async Task<Offers> LoadOffersFromXmlFromUrl(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            // Stiahneme XML dáta zo zadaného URL
            var xmlData = await client.GetStringAsync(url);

            // Načítanie XML dát do C# objektov
            XmlSerializer serializer = new XmlSerializer(typeof(Offers), new XmlRootAttribute("Offers"));
            using (StringReader reader = new StringReader(xmlData))
            {
                try
                {
                    var result = (Offers)serializer.Deserialize(reader);
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Chyba pri deserializácii XML: {ex.Message}");
                }
            }
            return null;
        }
    }

    static async Task LoginInvia()
    {
        var url = "https://affil.invia.sk/user/login/";

        using (var client = new HttpClient())
        {
            // Pripravujeme dáta na odoslanie (prihlásenie)
            var postData = new Dictionary<string, string>
            {
                { "ac_email", "peter.soroka@ecoach.sk" },
                { "ac_password", "Dfgh456+" },
                { "submit", "Prihlásiť sa" }
            };

            // Pripravíme obsah pre POST požiadavku
            var content = new FormUrlEncodedContent(postData);

            // Nastavíme hlavičky (prípadne podľa potreby pridajte ďalšie hlavičky)
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            // Odosielame POST požiadavku
            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                // Ak je odpoveď úspešná, môžeme získať obsah stránky
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Prihlásenie bolo úspešné!");
                Console.WriteLine(responseBody);
            }
            else
            {
                Console.WriteLine("Chyba pri prihlasovaní. Kód chyby: " + response.StatusCode);
            }
        }
    }
}

