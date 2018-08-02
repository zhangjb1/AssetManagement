using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssetManagement.Models;


namespace AssetManagement.ViewModels
{
    public class HardwareVM
    {
        public Hardware hardware { get; set; }
        public List<SelectListItem> typelist { get; set; } 
    }
}