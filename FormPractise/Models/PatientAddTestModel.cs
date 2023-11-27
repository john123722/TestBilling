using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace FormPractise.Models
{
    public class PatientAddTestModel
    {
        public int TId { get; set; }
        public string TestName { get; set; }
        public string GroupName { get; set; }
        public string Price { get; set; }
    }
}
 