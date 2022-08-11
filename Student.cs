using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace GetStudentListAPI
{
    public class Student
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int AdmissionYear { get; set; }


        public List<Student> studentList = new List<Student>();

        public Student()
        {
            //nothing here
        }
        public Student(string studentName, string studentAddress, int admissionYear)
        {
            Name = studentName;
            Address = studentAddress;
            AdmissionYear = admissionYear;
        }
        public List<Student> StudentGenerator(string[] nameArr, string[] addressArr)
        {
            Random rnd = new Random();

            for (int i = 0; i < nameArr.Length; i++)
            {
                studentList.Add(new Student(nameArr[i], addressArr[i], rnd.Next(2017, 2022)));
            }

            return studentList;
        }

        public string GetStudents()
        {
            SqlConnection con = new SqlConnection(@"Data Source=CMDLHRDB01;Initial Catalog=StudentTbl;Persist Security Info=True;User ID=sa;Password=CureMD2022");
            con.Open();
            string query = "SELECT * FROM Students;";
            SqlDataAdapter SDA = new SqlDataAdapter(query, con);
            DataTable table = new DataTable();
            SDA.Fill(table);
            if (table.Rows.Count > 0)
            {
                con.Close();
                return JsonConvert.SerializeObject(table);
            }

            //Error handling
            else
            {
                con.Close();
                return "No student records found in the database";
            }
        }
    }

}