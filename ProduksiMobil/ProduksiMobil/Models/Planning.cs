using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProduksiMobil.Models
{
    public class Planning
    {
        public int Id { get; set; }
        public string InputData { get; set; }
        public string OutputData { get; set; }
        public int TotalProduksi { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}