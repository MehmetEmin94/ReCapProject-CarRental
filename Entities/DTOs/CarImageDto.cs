using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.DTOs
{
    public class CarImageDto:IDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public string CarName { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public decimal ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
    }
}
