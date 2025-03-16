using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public  class ImputParameter
    {
        [Required]
        public string Url { get; set; }

        [Required]
        public string Minutes { get; set; }

        public ImputParameter(string url, string minutes)
        {
            Url = url;
            Minutes = minutes;
        }

    }
}
