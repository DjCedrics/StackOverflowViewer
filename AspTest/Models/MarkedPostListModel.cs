using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class MarkedPostListModel
    {
        public int Id { get; set; }
        public string PostTitle { get; set; }
        public string Notes {get; set; }

        public string Url {get; set;}

        //public string Body { get; set; }
        
    }
}
