﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;

namespace Runtime_Terror
{
    internal class DataHandler
    {
        private string connection = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\bin\Debug\RuntimeTerrorDB.mdf;Integrated Security = True";
        private SqlConnection myConnection;

        public DataHandler() { }

        private void openConnection() 
        { 
            myConnection = new SqlConnection(connection);
            myConnection.Open();

        }

        public void StoreInformation(Student student) // Storing a new student.
        {          
            openConnection();          
            try
            {
                SqlCommand cmd = new SqlCommand("spAddStudent", myConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = student.StdNumber;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = student.Name;
                cmd.Parameters.Add("@Surname", SqlDbType.VarChar).Value = student.Surname;
                cmd.Parameters.Add("@DOB", SqlDbType.VarChar).Value = student.Dob;
                cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = student.Gender;
                cmd.Parameters.Add("@Phone", SqlDbType.Int).Value = student.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = student.Email;
                cmd.Parameters.Add("@ModuleCodes", SqlDbType.VarChar).Value = student.ModuleCodes;
                
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student added successfully");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                myConnection.Close();
            }

        }
        
        public void StoreInformation(Module module) // Storing a new module.
        {
            openConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("spCreateModule", myConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ModuleCode", SqlDbType.VarChar).Value = module.Code;
                cmd.Parameters.Add("@ModuleName", SqlDbType.VarChar).Value = module.Name;
                cmd.Parameters.Add("@ModuleDescription", SqlDbType.VarChar).Value = module.Description;
                cmd.Parameters.Add("@ExternalResources", SqlDbType.VarChar).Value = module.Resources;

                cmd.ExecuteNonQuery();
                MessageBox.Show("Module added successfully");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                myConnection.Close();
            }

        }

        public void Search()
        {
            openConnection();

            try
            {

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                myConnection.Close();

            }
        }

        public void Update(Student student)
        {
            openConnection();

            try
            {
                SqlCommand cmd = new SqlCommand("spUpdate", myConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = student.StdNumber;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = student.Name;
                cmd.Parameters.Add("@Surname", SqlDbType.VarChar).Value = student.Surname;
                cmd.Parameters.Add("@DOB", SqlDbType.VarChar).Value = student.Dob;
                cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = student.Gender;
                cmd.Parameters.Add("@Phone", SqlDbType.Int).Value = student.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = student.Email;
                cmd.Parameters.Add("@ModuleCodes", SqlDbType.VarChar).Value = student.ModuleCodes;


                cmd.ExecuteNonQuery();
                MessageBox.Show("Student updated successfully");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                myConnection.Close();
            }


        }

        public void Delete(Student student)
        {
            openConnection();
            try
            {
             SqlCommand cmd = new SqlCommand("spDelete", myConnection);
             cmd.CommandType = CommandType.StoredProcedure;

             cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = student.StdNumber;

             cmd.ExecuteNonQuery();
             MessageBox.Show("Student deleted successfully");

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                myConnection.Close();
            }

        }
        
        public DataTable Display(string query)
        {
            openConnection();
                
            SqlCommand cmd = new SqlCommand(query, myConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            
            myConnection.Close();
            
            return dt;
            
        }
            
    }

}












    