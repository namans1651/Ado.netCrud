using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApplication4.Models;

namespace companyProject
{
    public class EmployeeDAL
    {
        private readonly string _connection = ConfigurationManager.ConnectionStrings["MyString"].ConnectionString;

        public List<EmployeeModel> GetEmployees()
        {
            {

                List<EmployeeModel> list = new List<EmployeeModel>();


                using (SqlConnection con = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        EmployeeModel employee = new EmployeeModel();

                        employee.Id = Convert.ToInt32(reader["Id"]);
                        employee.Name = reader["name"].ToString();
                        employee.MobileNumber = reader["mobileNo"].ToString();
                        employee.email = reader["email"].ToString();
                        employee.Address = reader["address"].ToString();
                        //employee.fk_ShiftId = Convert.ToInt32(reader["FK_shift_id"]);
                        //employee.ShiftName = reader["shift_name"].ToString();
                        //employee.fk_CompanyID = Convert.ToInt32(reader["FK_company_id"]);
                        employee.CompanyName = reader["c_name"].ToString();
                        employee.ShiftName = reader["shift_name"].ToString();
                        employee.Country = reader["CountryName"].ToString();
                        employee.State = reader["StateName"].ToString();
                        employee.City = reader["CityName"].ToString();
                        employee.Reason = reader["Reason"].ToString();


                        list.Add(employee);

                    }
                }
                return list;
            }
        }


        public List<EmployeeModel> GetShiftEndReasons()
        {
            List<EmployeeModel> reasons = new List<EmployeeModel>();

            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_getShiftEndReason", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EmployeeModel reason = new EmployeeModel();
                    reason.FK_ShiftEndReasonId = Convert.ToInt32(reader["ShiftEndReasonId"]);
                    reason.Reason = reader["Reason"].ToString();
                    reasons.Add(reason);
                }
            }

            return reasons;
        }






        public void AddEmployee(EmployeeModel employee)
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_AddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", employee.Name);
                cmd.Parameters.AddWithValue("@mobile", employee.MobileNumber);
                cmd.Parameters.AddWithValue("@email", employee.email);
                cmd.Parameters.AddWithValue("@address", employee.Address);
                cmd.Parameters.AddWithValue("@shiftid", employee.fk_ShiftId);
                cmd.Parameters.AddWithValue("@fkcompId", employee.fk_CompanyID);
                cmd.Parameters.AddWithValue("@fkCountryid", employee.FK_Country_Id);
                cmd.Parameters.AddWithValue("@fkStateid", employee.FK_State_Id);
                cmd.Parameters.AddWithValue("@fkCityid", employee.FK_City_Id);
                cmd.Parameters.AddWithValue("@shiftendreasonid", employee.FK_ShiftEndReasonId);
                cmd.Parameters.AddWithValue("@shiftEndTime", employee.ShiftEndTime);

                con.Open();
                cmd.ExecuteNonQuery();


            }

        }

        //public void AddEmployee2(EmployeeModel employee)
        //{
        //    using (SqlConnection con = new SqlConnection(_connection))
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_AddEmployee2", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@name", employee.Name);
        //        cmd.Parameters.AddWithValue("@mobile", employee.MobileNumber);
        //        cmd.Parameters.AddWithValue("@address", employee.Address);

        //        con.Open();
        //        cmd.ExecuteNonQuery();


        //    }

        //}
        public void AddEmployee2(string jsonData)
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_AddEmployee2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@jsonData", jsonData);

                con.Open();
                cmd.ExecuteNonQuery();
          

            }

        }

        public EmployeeModel getEmployeeById(int id)
        {
            EmployeeModel employee = new EmployeeModel();
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_getEmployeeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    employee.Id = Convert.ToInt32(reader["id"]);
                    employee.Name = reader["name"].ToString();
                    employee.MobileNumber = reader["MobileNo"].ToString();
                    employee.email = reader["email"].ToString();
                    employee.Address = reader["address"].ToString();
                    employee.fk_ShiftId = Convert.ToInt32(reader["FK_shift_id"]);
                    employee.fk_CompanyID = Convert.ToInt32(reader["FK_company_id"]);
                    employee.FK_Country_Id = Convert.ToInt32(reader["FK_Country_Id"]);
                    employee.FK_State_Id = Convert.ToInt32(reader["FK_State_Id"]);
                    employee.FK_City_Id = Convert.ToInt32(reader["FK_City_Id"]);

                }
            }
            return employee;
        }

        public void UpdateEmployee(EmployeeModel employee)
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", employee.Id);
                cmd.Parameters.AddWithValue("@name", employee.Name);
                cmd.Parameters.AddWithValue("@mob_number", employee.MobileNumber);
                cmd.Parameters.AddWithValue("@email", employee.email);
                cmd.Parameters.AddWithValue("@address", employee.Address);
                cmd.Parameters.AddWithValue("@shiftId", employee.fk_ShiftId);
                cmd.Parameters.AddWithValue("@fkcompId", employee.fk_CompanyID);
                cmd.Parameters.AddWithValue("@fkCountryId", employee.FK_Country_Id);
                cmd.Parameters.AddWithValue("@fkStateId", employee.FK_State_Id);
                cmd.Parameters.AddWithValue("@fkCityId", employee.FK_City_Id);
                cmd.Parameters.AddWithValue("@fkShiftReasonId", employee.FK_ShiftEndReasonId);
             //   cmd.Parameters.AddWithValue("@shiftendtime", employee.ShiftEndTime);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int id)
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //public void SaveEmployeeCountryState(int employeeId, int countryId, int stateId)
        //{
        //    using (SqlConnection con = new SqlConnection(_connection))
        //    {
        //        SqlCommand cmd = new SqlCommand("sp_EmployeeCountryState", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
        //        cmd.Parameters.AddWithValue("@CountryId", countryId);
        //        cmd.Parameters.AddWithValue("@StateId", stateId);

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}
        public List<CountryModel> GetCountries()
        {
            List<CountryModel> countries = new List<CountryModel>();

            using (SqlConnection con = new SqlConnection(_connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spGetCountries", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CountryModel country = new CountryModel();

                    country.CountryId = Convert.ToInt32(reader["CountryId"]);
                    country.CountryName = reader["CountryName"].ToString();
                    countries.Add(country);
                }
            }

            return countries;
        }



        public List<StateModel> GetStateByCountry(int countryId)
        {
            List<StateModel> list = new List<StateModel>();

            using (SqlConnection con = new SqlConnection(_connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spGetStates", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CountryId", countryId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    StateModel state = new StateModel();

                    state.StateId = Convert.ToInt32(reader["StateID"]);
                    state.StateName = reader["StateName"].ToString();
                    state.CountryId = Convert.ToInt32(reader["FK_CountryId"]);
                    list.Add(state);
                }
            }

            return list;
        }

        public List<CityModel> GetCityByState(int stateId)
        {
            List<CityModel> list = new List<CityModel>();

            using (SqlConnection con = new SqlConnection(_connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spGetCity", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StateId", stateId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CityModel city = new CityModel();

                    city.Id = Convert.ToInt32(reader["CityID"]);
                    city.Name = reader["CityName"].ToString();
                    city.StateId = Convert.ToInt32(reader["FK_StateID"]);
                    list.Add(city);
                }
            }

            return list;
        }
    }
}
    