using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Collections;

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

        public void StoreInformation(Student student) // Student Create.
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
        
        public void StoreInformation(Module module) // Module Create.
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

        public void UpdateStudent(Student student) // Student Update
        {
            openConnection();

            try
            {
                SqlCommand cmd = new SqlCommand("spUpdateStudentInfo", myConnection);
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

        public void DeleteStudent(Student student) // Student Delete
        {
            openConnection();
            try
            {
             SqlCommand cmd = new SqlCommand("spDeleteStudentInfo", myConnection);
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
        public void Update(Module updatedModule) // Module Update
        {
            openConnection();

            try
            {
                SqlCommand cmd = new SqlCommand("spUpdateModule", myConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ModuleCode", SqlDbType.VarChar).Value = updatedModule.Code;
                cmd.Parameters.Add("@ModuleName", SqlDbType.VarChar).Value = updatedModule.Name;
                cmd.Parameters.Add("@ModuleDescription", SqlDbType.VarChar).Value = updatedModule.Description;
                cmd.Parameters.Add("@ExternalResources", SqlDbType.VarChar).Value = updatedModule.Resources;

                cmd.ExecuteNonQuery();
                
                MessageBox.Show("Module updated successfully");
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
        public void Delete(string moduleCode) // Module Delete
        {
            openConnection();
            try
            {
                SqlCommand cmd = new SqlCommand("spDeleteModule", myConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ModuleCode", SqlDbType.VarChar).Value = moduleCode;

                cmd.ExecuteNonQuery();
                MessageBox.Show("Module deleted successfully");

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
        
        public DataTable Search(string formInfo)
        {
            openConnection();

            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from Student where StudentId ='" + formInfo + "'", myConnection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                return dataTable;

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                myConnection.Close();

            }
        }
        
        public DataTable searchModule(string formInfo)
        {
            openConnection();

            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from Modules where ModuleCode ='" + formInfo + "'", myConnection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                return dataTable;

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                myConnection.Close();

            }
        }

        public Boolean EntryExists(string id, string table) // Checking if data entry exists before update or delete.
        {
            openConnection();
            try
            {
                if (table == "Student")
                {
                    SqlCommand cmd = new SqlCommand("SELECT StudentId FROM Student WHERE StudentId = @StudentId", myConnection);
                    cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = id;
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    return reader.HasRows ? true : false;
                }
                else if(table == "Modules")
                {
                    SqlCommand cmd = new SqlCommand("SELECT ModuleCode FROM Modules WHERE ModuleCode = @ModuleCode", myConnection);
                    cmd.Parameters.Add("@ModuleCode", SqlDbType.VarChar).Value = id;
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    return reader.HasRows ? true : false;
                }
                else
                {
                    return false;
                }
                
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                
                myConnection.Close();
            }
            
        }

        public DataTable getModules()
        {
            DataTable dataTable = new DataTable();

            try
            {
                openConnection();

                string selectQ = "SELECT ModuleName FROM Modules";

                SqlCommand cmd = new SqlCommand(selectQ, myConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                                               
                sqlDataAdapter.Fill(dataTable);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }

            return dataTable;
        }
    }

}












    