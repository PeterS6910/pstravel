using ImportXml.AfiTravelModel;
using System;
using System.Xml.Serialization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Net.Http.Headers;

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
        //await LoginInvia();

        string filePath = "https://affil.invia.sk/direct/core/tool_xml-feed/download/id/8548540-67c1c02a82ffc/"; // Nahraď správnou cestou k XML

        Offers xmlContent = await DownloadXmlAsync(filePath);

        // Deserializácia XML do objektu
        //Offers offers = DeserializeXml<Offers>(xmlContent);

        //Console.WriteLine($"Počet ponúk: {offers.Count}");
        //foreach (var offer in offers.Offer)
        //{
        //    Console.WriteLine($"Hotel: {offer.Hotel}, ");
        //}
    }
    public static T DeserializeXml<T>(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (StreamReader reader = new StreamReader(filePath))
        {
            return (T)serializer.Deserialize(reader);
        }
    }

    static async Task<Offers> DownloadXmlAsync(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            var filePath = @"C:\\w4\\pstravel\\psbackend\\ImportXml\\Data\\response.xml";
            //var xmlData =  await client.GetStringAsync(url);
            string xmlData = File.ReadAllText(filePath);
            //string xmlData = await File.ReadAllTextAsync(filePath);
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

