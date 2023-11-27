using FormPractise.DataAccessLayer;
using FormPractise.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using FormPractise.Controllers;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace FormPractise.Controllers
{
    public class PatientAddTestController : Controller
    {
        private readonly PatientDAL con = new PatientDAL();
        private readonly HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<ActionResult> Create(PatientAddTestModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    con.AddTest(model);
            //    ModelState.Clear();
            //}

            //return View();

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://localhost:51361/api/uploadtest");

                    var content = await client.PostAsJsonAsync<PatientAddTestModel>("", model);
                    if (content.IsSuccessStatusCode)
                    {
                        return RedirectToAction("DisplayTest");
                    }

                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> DisplayTest()
        {
            //var result = con.GetTestDetails();
            //return View(result);
            List<PatientAddTestModel> tdata = new List<PatientAddTestModel>();
            client.BaseAddress = new Uri("http://localhost:51361/api/gettest");
            var response = await client.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var display = await response.Content.ReadAsAsync<List<PatientAddTestModel>>();
                tdata = display;
            }
            return View(tdata);
        }
       

        // GET: PatientAddTest/Details/5
        public async Task<ActionResult> Display(int id)
        {
            //This is for patient list 

            PatientModel data = new PatientModel();
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51361/api/");
                var response = await client.GetAsync($"getdata/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var display = await response.Content.ReadAsAsync<PatientModel>();
                    data = display;
                }
            }
           
            var patientList = new List<PatientModel>();
            if (data != null)
            {
                patientList.Add(data);
            }

            //This is for test list 

            List<PatientAddTestModel> tdata = new List<PatientAddTestModel>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:51361/api/gettest");
                var tresponse = await client.GetAsync("");
                if (tresponse.IsSuccessStatusCode)
                {
                    var display = await tresponse.Content.ReadAsAsync<List<PatientAddTestModel>>();
                    tdata = display;
                }
            }
                
            var testList = tdata;

            //if (patientList.Count == 0)
            //{
            //    ViewBag.PatientErrorMessage = "There is no Patient Data";
            //}

            if (testList.Count == 0)
            {
                ViewBag.TestErrorMessage = "There is no Test Data";
            }

            var combinedModel = new CombinedViewModel
            {
                PatientList = patientList,
                TestList = testList,
                SelectedPatientId = id
            };

            return View("Display", combinedModel);
        }
        [HttpPost]
        public async Task<bool> AddReport(int patientId, int[] testIds)
        {
            //con.SavePatientTest(patientId, testIds);
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://localhost:51361/api/");
                    var model = new CombinedViewModel
                    {
                        SelectedPatientId = patientId,
                        Ints= testIds
                    };

                    await client.PostAsJsonAsync<CombinedViewModel>($"addpatienttest", model);

                    
                }
                return true;
            }
            else { return false; }

        }

       
        public ActionResult DisplayIndividualReport(int id, string testIds)
        {
            // Retrieve the patient's name based on the patientId
            int[] testIdss = JsonConvert.DeserializeObject<int[]>(testIds);
            var patientList = con.GetPatientDetails();
            if (patientList == null)
            {
                ViewBag.ErrorMessage = "Patient not found.";
                return View();
            }

            // Retrieve the test details based on the testIds
            //var testList = new List<PatientAddTestModel>();
            List<PatientAddTestModel> testDetails = new List<PatientAddTestModel>();
            if(testIds != null)
            {
                foreach (int testId in testIdss)
                {
                    PatientAddTestModel testDetail = con.GetIndividualTestDetails(testId);
                    if (testDetail != null)
                    {
                        testDetails.Add(testDetail);
                    }
                }
            }
            

            // Create a CombinedViewModel object and populate it with patient and test details
            var combinedModel = new CombinedViewModel
            {
                PatientList = patientList,
                TestList = testDetails,
                SelectedPatientId = id
            };

            // Create a list of CombinedViewModel objects and add the current model
            _ = new List<CombinedViewModel>
            {
                combinedModel
            };

            return View(combinedModel);
        }












    }
}
