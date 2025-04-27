using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImportXml.Repository
{
    public class ImagesRepository : EntityRepository<Images, Guid>
    {
        public ImagesRepository(DbContext context) : base(context)
        {
        }

        // Špecifická metóda pre získanie krajiny podľa názvu
        public async Task GetOrCreateImagesByOfferAsync(XElement offer, Guid hotelId)
        {
            bool isSave = false;
            bool isData = offer.Element("photos")?.Elements("photo").Any() ?? false;
            if (isData)
            {
                foreach (var photo in offer?.Element("photos")?.Elements("photo"))
                {
                    if (photo != null)
                    {

                        var photoFind = await _context.Set<Images>().FirstOrDefaultAsync(j => j.HotelId == hotelId && j.Url == photo.Value);
                        if (photoFind == null)
                        {
                            photoFind = new Images
                            {
                                Id = Guid.NewGuid(),
                                HotelId = hotelId,
                                Url = photo.Value,
                                CreatedAt = DateTime.UtcNow,
                            };
                            await _context.Set<Images>().AddAsync(photoFind);
                            isSave = true;
                        }
                    }
                }
                if (isSave)
                {
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
