using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebApplication4.Models;

namespace companyProject
{
    public class CompanyDAL
    {
        private readonly string _connection = ConfigurationManager.ConnectionStrings["MyString"].ConnectionString;

        public List<CompanyModel> getAllCompany()
        {
            List<CompanyModel> list = new List<CompanyModel>();

            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_GetCompany", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    CompanyModel company = new CompanyModel();

                    company.Id = Convert.ToInt32(reader["company_id"]);
                    company.Name = reader["c_name"].ToString();
                    company.Address = reader["c_address"].ToString();
                    company.PinCode = reader["c_pincode"].ToString();
                    company.State = reader["c_state"].ToString();
                    company.City = reader["c_city"].ToString();
                    company.ContactNumber = reader["c_contactNo"].ToString();


                    list.Add(company);

                }

            }

            return list;
        }


        public void AddCompany(string companyJson)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_jsonAdd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CompanyJson", companyJson);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding company to the database.", ex);
            }
        }


        //public void AddCompany(string companyJson)
        //{
        //    try
        //    {

        //        CompanyModel company = JsonConvert.DeserializeObject<CompanyModel>(companyJson);
        //        using (SqlConnection con = new SqlConnection(_connection))
        //        {
        //            SqlCommand cmd = new SqlCommand("sp_Addcompany", con);
        //                cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@name", company.Name);
        //            cmd.Parameters.AddWithValue("@address", company.Address);
        //            cmd.Parameters.AddWithValue("@pincode", company.PinCode);
        //            cmd.Parameters.AddWithValue("@state", company.State);
        //            cmd.Parameters.AddWithValue("@city", company.City);
        //            cmd.Parameters.AddWithValue("@contact", company.ContactNumber);

        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error while adding company to the database.", ex);
        //    }
        //}

        //public void AddCompany(CompanyModel company)
        //{
        //    using (SqlConnection con = new SqlConnection(_connection))
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_Addcompany", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@name", company.Name);
        //        cmd.Parameters.AddWithValue("@address", company.Address);
        //        cmd.Parameters.AddWithValue("@pincode", company.PinCode);
        //        cmd.Parameters.AddWithValue("@state", company.State);
        //        cmd.Parameters.AddWithValue("@city", company.City);
        //        cmd.Parameters.AddWithValue("@contact", company.ContactNumber);
        //        con.Open();
        //        cmd.ExecuteNonQuery();


        //    }

        //}


        public CompanyModel getCompanyById(int id)
        {
            CompanyModel company = new CompanyModel();
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_getCompanyById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    company.Id = Convert.ToInt32(reader["company_id"]);
                    company.Name = reader["c_name"].ToString();
                    company.Address = reader["c_address"].ToString();
                    company.PinCode = reader["c_pincode"].ToString();
                    company.State = reader["c_state"].ToString();
                    company.City = reader["c_city"].ToString();
                    company.ContactNumber = reader["c_contactNo"].ToString();

                }

            }

            return company;
        }

        public void UpdateCompany(CompanyModel company)
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_updateCompany", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", company.Id);
                cmd.Parameters.AddWithValue("@name", company.Name);
                cmd.Parameters.AddWithValue("@address", company.Address);
                cmd.Parameters.AddWithValue("@pincode", company.PinCode);
                cmd.Parameters.AddWithValue("@state", company.State);
                cmd.Parameters.AddWithValue("@city", company.City);
                cmd.Parameters.AddWithValue("@contact", company.ContactNumber);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void DeleteCompany(int id)
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteCompany", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}