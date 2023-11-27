using FormPractise.DataAccessLayer;
using FormPractise.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FormPractise.Controllers
{
    public class HomeController : Controller
    {
       private readonly PatientDAL con = new PatientDAL();
       
        // GET: Home
        private readonly   HttpClient client = new HttpClient();
        public async Task<ActionResult> Display()
        {
            //var patientList = con.GetPatientDetails();
            //if (patientList.Count==0)
            //{
            //    ViewBag.ErrorMessage = "There is no Patient Data";
            //}
            //return View(patientList);

           using(HttpClient client = new HttpClient())
            {
                List<PatientModel> data = new List<PatientModel>();
                client.BaseAddress = new Uri("http://localhost:51361/api/");
                var response = await client.GetAsync($"getdata");
                if (response.IsSuccessStatusCode)
                {
                    var display = await response.Content.ReadAsAsync<List<PatientModel>>();
                    data = display;
                }
                return View(data);
            }
        }

        // GET: Home/Details/5
        public async Task<ActionResult> Details(int id) 
        {
            //var result = con.GetDetails(id);
            //return View(result)
            PatientModel data = new PatientModel();
            client.BaseAddress = new Uri("http://localhost:51361/api/");
            var response = await client.GetAsync($"getdata/{id}");

            if (response.IsSuccessStatusCode)
            {
                var display = await response.Content.ReadAsAsync<PatientModel>();
                data = display;
            }

            return View(data);
        }


        public ActionResult AddNewPatient()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> AddNewPatient(PatientModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    int id = con.AddPatient(model);
            //    ModelState.Clear();
            //    // Redirect to the Details action method with the id parameter
            //    return RedirectToAction("Display","PatientAddTest",new { id });
            //}
            //else
            //{
            //    ViewBag.Message = "User creation failed";
            //    return View(model);
            //}

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://localhost:51361/api/uploaddata");

                    var content = await client.PostAsJsonAsync<PatientModel>("", model);  
                    if (content.IsSuccessStatusCode)
                    {
                        long id = await content.Content.ReadAsAsync<long>();
                        return RedirectToAction("Display", "PatientAddTest", new { id });
                    }

                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
            return RedirectToAction("Error");
            
        }
        public ActionResult SelectedDetails()
        {
            var result = con.GetPatientDetails();
            return View(result);
        }

        // GET: Home/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //var result = con.GetDetails(id);
            //return View(result);

            PatientModel data = new PatientModel();
            client.BaseAddress = new Uri("http://localhost:51361/api/");
            var response = await client.GetAsync($"getdata/{id}");

            if (response.IsSuccessStatusCode)
            {
                var display = await response.Content.ReadAsAsync<PatientModel>();
                data = display;
            }

            return View(data);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(int id, PatientModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    con.EditPatientDetails(model,id);
            //    ModelState.Clear();
            //}

            //return RedirectToAction("Display");
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://localhost:51361/api/");
                    var content = await client.PostAsJsonAsync<PatientModel>($"editdata/{id}", model);
                    if (content.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Display");
                    }

                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
            return RedirectToAction("Error");

        }
        

        // GET: Home/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            //if (ModelState.IsValid)
            //{
            //    con.DeletePatientDetails(id);
            //    ModelState.Clear();
            //}
            //if(true)
            //{
            //    ViewBag.Message = "Record Deleted";
            //}
            //return RedirectToAction("Display");

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:51361/api/");

                var content = await client.DeleteAsync($"deletedata/{id}");
                if (content.IsSuccessStatusCode)
                {
                    return RedirectToAction("Display");
                }

                else
                {
                    return RedirectToAction("Error");
                }
            }

        }

        public ActionResult PatientRecord()
        {
            //var result = con.GetPatientAndTestDetails();
            //return View(result);
            using(HttpClient client = new HttpClient())
            {
                List<PatientReportModel> data = new List<PatientReportModel>();
                client.BaseAddress = new Uri("http://localhost:51361/api/GetPatientAndTest");
                var response = client.GetAsync("");
                response.Wait();
                var test = response.Result;
                if (test.IsSuccessStatusCode)
                {
                    var display = test.Content.ReadAsAsync<List<PatientReportModel>>();
                    display.Wait();
                    data = display.Result;
                }
                return View(data);
            }
            
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}

