using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DietTracking.API.Models
{
    public class DietListDetail
    {
        [Key]
        public int DetailId { get; set; }

        public string Day { get; set; }   // Örn: "Pazartesi", "Salı" veya "1. Gün"
        public string Meal { get; set; }  // Örn: "Kahvaltı", "Ara Öğün", "Akşam"
        public string Content { get; set; } // Örn: "2 Haşlanmış yumurta, 1 dilim peynir"

        // Hangi listeye ait
        public int DietListId { get; set; }
        [ForeignKey("DietListId")]
        public DietList DietList { get; set; }
    }
}