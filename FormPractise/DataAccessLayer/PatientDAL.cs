using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using FormPractise.Models;
using System.Net;
using System.Web.Helpers;

namespace FormPractise.DataAccessLayer
{
    public class PatientDAL
    {
        readonly string ConString = ConfigurationManager.ConnectionStrings["StoredProcConnection"].ToString();

        //Get all patient details from DB

        public List<PatientModel> GetPatientDetails()
        {
            List<PatientModel> details = new List<PatientModel>();
            using (SqlConnection conn = new SqlConnection(ConString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DisplayPatientDetails";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                conn.Close();

                // convert the data to list
                foreach (DataRow dr in dt.Rows)
                {
                    details.Add(new PatientModel
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Address = dr["Address"].ToString(),
                        Contact = dr["Contact"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]),
                        Email = dr["Email"].ToString(),
                        CreatedBy = dr["CreatedBy"].ToString(),
                        CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                        Referal = dr["Referal"].ToString(),
                    });
                }
            }
            return details;
        }
        // Adds the  new record into the database
        public int AddPatient(PatientModel model)
        {
            using (SqlConnection conn = new SqlConnection(ConString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_InsertPatientDetails";
                command.Parameters.AddWithValue("@FirstName", model.FirstName);
                command.Parameters.AddWithValue("@LastName", model.LastName);
                command.Parameters.AddWithValue("@Address", model.Address);
                command.Parameters.AddWithValue("@Contact", model.Contact);
                command.Parameters.AddWithValue("@Gender", model.Gender);
                command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                command.Parameters.AddWithValue("@CreatedDate", model.CreatedDate);
                command.Parameters.AddWithValue("@Referal", model.Referal);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }


            int maxID = 0;
            using (SqlConnection conn = new SqlConnection(ConString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_MaxId";
                conn.Open();
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    maxID = Convert.ToInt32(result);
                }
                conn.Close();
            }
            return maxID;


        }

        //Display the particular record
        public PatientModel GetDetails(int id)
        {
            PatientModel patient = null;
            using (SqlConnection conn = new SqlConnection(ConString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DisplayDetails";
                command.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                conn.Close();

                // convert the data to PatientModel object
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    patient = new PatientModel
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Address = dr["Address"].ToString(),
                        Contact = dr["Contact"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]),
                        Email = dr["Email"].ToString(),
                        CreatedBy = dr["CreatedBy"].ToString(),
                        CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                        Referal = dr["Referal"].ToString(),
                    };
                }
            }
            return patient;
        }
        

        //Updates or Edits the patient record
        public bool EditPatientDetails(PatientModel model, int id)
        {
            using (SqlConnection conn = new SqlConnection(ConString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_EditPatientDetails";
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@FirstName", model.FirstName);
                command.Parameters.AddWithValue("@LastName", model.LastName);
                command.Parameters.AddWithValue("@Address", model.Address);
                command.Parameters.AddWithValue("@Contact", model.Contact);
                command.Parameters.AddWithValue("@Gender", model.Gender);
                command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                command.Parameters.AddWithValue("@CreatedDate", model.CreatedDate);
                command.Parameters.AddWithValue("@Referal", model.Referal);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                return true;
            }

        }
        public bool DeletePatientDetails(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DeletePatientDetails";
                command.Parameters.AddWithValue("@Id", id);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                return true;
            }
        }

        public bool AddTest(PatientAddTestModel model)
        {
            using (SqlConnection conn = new SqlConnection(ConString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_TestEntryDetails";
                command.Parameters.AddWithValue("@TestName", model.TestName);
                command.Parameters.AddWithValue("@GroupName", model.GroupName);
                command.Parameters.AddWithValue("@Price", model.Price);
                conn.Open() ;
                command.ExecuteNonQuery();
                conn.Close();
            }
            return true;
        }

        public List<PatientAddTestModel> GetTestDetails()
        {
            List<PatientAddTestModel> details = new List<PatientAddTestModel>();
            using (SqlConnection conn = new SqlConnection(ConString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DisplayTestDetails";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                conn.Close();

                // convert the data to list
                foreach (DataRow dr in dt.Rows)
                {
                    details.Add(new PatientAddTestModel
                    {
                        TId = Convert.ToInt32(dr["TId"]),
                        TestName = dr["TestName"].ToString(),
                        GroupName = dr["GroupName"].ToString(),
                        Price = Convert.ToDouble(dr["Price"]).ToString(),
                    });
                }
            }
        
            return details;
        }

        public PatientAddTestModel GetIndividualTestDetails(int id)
        {
            PatientAddTestModel details = null;
            using (SqlConnection conn = new SqlConnection(ConString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_IndividualTestDetails";
                command.Parameters.AddWithValue("@TId", id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                conn.Close();

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    details = new PatientAddTestModel
                    {
                        TId = Convert.ToInt32(dr["TId"]),
                        TestName = dr["TestName"].ToString(),
                        GroupName = dr["GroupName"].ToString(),
                        Price = Convert.ToDouble(dr["Price"]).ToString(),
                    };
                }
            }
            return details;
        }

        public void SavePatientTest(int patientId, int[] testIds)
        {
            using (SqlConnection connection = new SqlConnection(ConString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("SP_AddId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                foreach (int testId in testIds)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Id", patientId);
                    command.Parameters.AddWithValue("@TId", testId);
                    command.ExecuteNonQuery();
                }
            }

            
        }

        public List<PatientReportModel> GetPatientAndTestDetails()
        {
            List<PatientReportModel> details = new List<PatientReportModel>();
            using (SqlConnection conn = new SqlConnection(ConString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_DisplayDetailsAndTest";
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                conn.Close();

                // convert the data to list
                foreach (DataRow dr in dt.Rows)
                {
                    details.Add(new PatientReportModel
                    {
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Address = dr["Address"].ToString(),
                        Contact = dr["Contact"].ToString(),
                        TestName = dr["TestName"].ToString(),
                        Price = Convert.ToDouble(dr["Price"]).ToString(),


                    });
                }
            }
            return details;
        }


    }





}