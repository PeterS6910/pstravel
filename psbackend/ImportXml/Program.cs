using ImportXml.AfiTravelModel;
using System;
using System.Xml.Serialization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using ImportXml;
using System.Globalization;
using System.Xml;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using ImportXml.Repository;
using ImportXml.Service;

class Program
{
    static async Task Main(string[] args)
    {
        string virtualka = "1";
        if (args != null && args.Length > 0) virtualka = args[0];
        // Základná správa na konzolu
        Console.WriteLine("Hello, Import data!");
        Job? jobsToRun = null;
        while (true)
        {
            using (var dbContext = new OfferDbContex())
            {
                JobRepository? jobRepo = null;
                try
                {
                    var currencyRepo = new CurrencyRepository(dbContext);
                    var foodRepo = new FoodRepository(dbContext);
                    var countryRepo = new CountryRepository(dbContext);
                        jobRepo = new JobRepository(dbContext);
                    var cestovkaRepo = new CestovkaRepository(dbContext);
                    var localityRepo = new LocalityRepository(dbContext);
                    var hotelDetailsRepo = new HotelDetailsRepository(dbContext);
                    var hotelRepo = new HotelRepository(dbContext, hotelDetailsRepo);
                    var imagesRepo = new ImagesRepository(dbContext);
                    var transportRepo = new TransportRepository(dbContext);
                    var offerTourTypeRepo = new OfferTourTypesRepository(dbContext);
                    var offerRepo = new OfferRepository(dbContext, currencyRepo, foodRepo, transportRepo, offerTourTypeRepo);

                    var inviaService = new InviaService(cestovkaRepo, countryRepo, localityRepo, hotelRepo, imagesRepo, offerRepo);

                    // Načítanie úloh, ktoré sú naplánované na spustenie
                    jobsToRun = await jobRepo.GetNaplanovanyJobAsync(virtualka);


                    if (jobsToRun != null)
                    {
                        jobsToRun.JobStateId = ConstansJobState.SpracovavaSaJobId;
                        await jobRepo.UpdateAsync(jobsToRun);
                        // Pre každú úlohu vykonať potrebnú logiku
                        switch (jobsToRun.JobCodeId)
                        {
                            case ConstansJob.inviaJobId: await inviaService.ProcessImportXmlFeedAsync(jobsToRun.InputParameters); break;
                            default: break;
                        }  
                        await jobRepo.EndJobAndCreateJob(jobsToRun);
                    }
                }
                catch (Exception ex)
                {
                    if (jobsToRun != null && jobRepo != null)
                    {
                        jobsToRun.JobStateId = ConstansJobState.ChybaJobId;
                        await jobRepo.UpdateAsync(jobsToRun);
                    }
                    Console.WriteLine($"Job {jobsToRun?.Id} padol: {ex.Message}, inner:{ex.InnerException}");
                }
            }            
            await Task.Delay(120000);
        };
    }  
}

