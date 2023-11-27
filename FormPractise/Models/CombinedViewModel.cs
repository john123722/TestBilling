using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormPractise.Models
{
    public class CombinedViewModel
    {
        public List<PatientModel> PatientList { get; set; }
        public List<PatientAddTestModel> TestList { get; set; }
        public int SelectedPatientId { get; set; }
        public int[] Ints { get; set; }
    }
}