﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class Photo
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OfferId { get; set; }
        
        [MaxLength(150)]  // Nastaví dĺžku VARCHAR na 50
        public string Url { get; set; }
    }
}
