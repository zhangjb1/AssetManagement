using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssetManagement.Models;

namespace AssetManagement.ViewModels
{
    public class AssignmentVM
    {
        public Assignment assignment { get; set; }
        public List<SelectListItem> employeeList { get; set; }
        public List<SelectListItem> computerList { get; set; }
        public List<SelectListItem> msofficeList { get; set; }
        public List<SelectListItem> msvisioLst { get; set; }
    }
}