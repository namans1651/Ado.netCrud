using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using WebApplication4.Models;

namespace WebApplication4
{
    public class CountryDAL
    {
        private readonly string _connection = ConfigurationManager.ConnectionStrings["MyString"].ConnectionString;

        public List<CountryModel> GetCountry()
        {
            List<CountryModel> list = new List<CountryModel>();

            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_GetCountry", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {

                    CountryModel country = new CountryModel();

                    country.CountryId = Convert.ToInt32(Reader["PK_Country_Id"]);
                    country.CountryName = Reader["Country_Name"].ToString();
                    //country.IsActive = true;
                    //country.IsDeleted = false;

                    list.Add(country);

                }
            }
            return list;

        }

        public List<StateModel> GetStateByCountry(int Id)
        {
            List<StateModel> list = new List<StateModel>();
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_GetStateBYCountry", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fk_CountryId", Id);
                con.Open();
                SqlDataReader Reader = cmd.ExecuteReader();
                while (Reader.Read())
                {

                    StateModel state = new StateModel();

                    state.StateId = Convert.ToInt32(Reader["PK_State_Id"]);
                    state.StateName = Reader["StateName"].ToString();
                    state.CountryId = Convert.ToInt32(Reader["FK_Country_Id"]);

                    list.Add(state);

                }
            }
            return list;

        }

        public void AddCountry(CountryModel country)
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_AddCountry", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", country.CountryName);
                con.Open();
                cmd.ExecuteNonQuery();


            }

        }


    }
}