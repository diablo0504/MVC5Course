using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ClientBatchUpdateVM
    {
        public int ClientId { get; set; }
    
        public string FirstName { get; set; }
       
        public string MiddleName { get; set; }

        public string LastName { get; set; }
    }
}