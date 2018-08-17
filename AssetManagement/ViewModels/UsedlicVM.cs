using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManagement.ViewModels
{
    public class UsedlicVM
    {
        public string SoftwareName { get; set; }
        public int UsedlicNo { get; set; }
        public List<UsedlicObj> UsedlicList { get; set; }
    }

    public class UsedlicObj
    {
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}