using System;
using System.Collections.Generic;

namespace DietTracking.API.DTOs
{
    public class CreateDietListDto
    {
        public int ClientId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DietListDetailDto> Details { get; set; }
    }

    public class DietListDetailDto
    {
        public string Day { get; set; }
        public string Meal { get; set; }
        public string Content { get; set; }
    }
}