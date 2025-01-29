using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApplication4.Models;

namespace companyProject
{
    public class ShiftDAL
    {

        private readonly string _connection = ConfigurationManager.ConnectionStrings["MyString"].ConnectionString;

        public List<ShiftModel> GetShift()
        {
            {

                List<ShiftModel> list = new List<ShiftModel>();
                using (SqlConnection con = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetAllShift", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ShiftModel shift = new ShiftModel();

                        shift.Id = Convert.ToInt32(reader["shift_id"]);
                        shift.Name = reader["shift_name"].ToString();
                        shift.start_time = Convert.ToDateTime(reader["start_time"]);
                        shift.end_time = Convert.ToDateTime(reader["end_time"]);
                        shift.companyName = reader["c_name"].ToString();
                        shift.imagepath = reader["imagePath"].ToString();

                        //employee.shiftname = reader["shiftName"].ToString();

                        list.Add(shift);

                    }
                }
                return list;
            }
        }


        public int AddShifts(ShiftModel shift)
        {
            int newShiftId = 0;
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_AddTheShift", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameters for the shift
                cmd.Parameters.AddWithValue("@name", shift.Name);
                cmd.Parameters.AddWithValue("@start_Time", shift.start_time);
                cmd.Parameters.AddWithValue("@end_Time", shift.end_time);
                cmd.Parameters.AddWithValue("@compId", shift.Company);
                cmd.Parameters.AddWithValue("@imagePath", shift.imagepath ?? (object)DBNull.Value);

                // Output parameter for the generated ShiftId
                SqlParameter outputId = new SqlParameter
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputId);

                // Execute the command
                con.Open();
                cmd.ExecuteNonQuery();
                newShiftId = Convert.ToInt32(outputId.Value);
            }
            return newShiftId;
        }





        public ShiftModel getShiftById(int id)
        {
            ShiftModel shift = new ShiftModel();
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_getShiftById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {

                    shift.Id = Convert.ToInt32(reader["shift_id"]);
                    shift.Name = reader["shift_name"].ToString();
                    shift.start_time = Convert.ToDateTime(reader["start_time"]);
                    shift.end_time = Convert.ToDateTime(reader["end_time"]);
                    shift.Company = Convert.ToInt32(reader["fk_company_id"]);
                    shift.imagepath = reader["imagePath"].ToString();

                }
            }
            return shift;
        }

        public void UpdateShift(ShiftModel shift)
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateShift", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", shift.Id);
                cmd.Parameters.AddWithValue("@name", shift.Name);
                cmd.Parameters.AddWithValue("@startTime", shift.start_time);
                cmd.Parameters.AddWithValue("@endTime", shift.end_time);
                cmd.Parameters.AddWithValue ("@fk_compId", shift.Company);
                cmd.Parameters.AddWithValue("@imagePath", shift.imagepath);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }



        public void DeleteShift(int id)
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteShift", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }























    }
            }

//        public List<ShiftModel> getAllShift()
//        {
//            List<ShiftModel> list = new List<ShiftModel>();

//            using (SqlConnection con = new SqlConnection(_connection))
//            {
//                SqlCommand cmd = new SqlCommand("sp_GetAllShift", con);
//                cmd.CommandType = CommandType.StoredProcedure;
//                con.Open();
//                SqlDataReader reader = cmd.ExecuteReader();

//                while (reader.Read())
//                {
//                    ShiftModel shift = new ShiftModel();
//                    shift.id = Convert.ToInt32(reader["shift_id"]);
//                    shift.Name = reader["shift_name"].ToString();
//                    shift.start_time = (TimeSpan)(reader["start_time"]);
//                    shift.end_time = (TimeSpan)(reader["end_time"]);

//                    list.Add(shift);

//                }
//            }
//            return list;
//        }

//        public void AddShift(ShiftModel shift)
//        {
//            using (SqlConnection con = new SqlConnection(_connection))
//            {
//                SqlCommand cmd = new SqlCommand("sp_AddShift", con);
//                cmd.CommandType = CommandType.StoredProcedure;
//                cmd.Parameters.AddWithValue("@name", shift.Name);
//                cmd.Parameters.AddWithValue("@start_Time", shift.start_time);
//                cmd.Parameters.AddWithValue("@end_Time", shift.end_time);
//                con.Open();
//                cmd.ExecuteNonQuery();


//            }
//        }
//    }
//}