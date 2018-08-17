using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManagement.ViewModels
{
    public class SoftwareVM
    {
        public int ID { get; set; }
        public string SoftwareName { get; set; }
        public int? TotalLicNo { get; set; }
        public int UsedLicNo { get; set; }
        public string PONumber { get; set; }
    }
}