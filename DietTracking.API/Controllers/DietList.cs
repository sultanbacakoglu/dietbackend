using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DietTracking.API.Models
{
    public class DietList
    {
        [Key]
        public int DietListId { get; set; }

        public string Title { get; set; } // Örn: "1. Hafta Detoks Listesi"
        public string Description { get; set; } // Diyetisyenin notu

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Hangi danışana ait?
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        // Listenin detayları (Pazartesi-Kahvaltı, Salı-Öğle vb.)
        public List<DietListDetail> Details { get; set; }
    }
}