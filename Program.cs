using System;

using FTPApp.Model;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FTPApp.Model.Utilities;
using Newtonsoft.Json;

namespace FTPApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> directories = FTP.GetDirectory(Constants.FTP.BaseUrl);

            foreach (var directory in directories)
            {
                Console.WriteLine(directory);
            }

            //List<string> students = new List<student>();

            List<Student> Students = new List<Student>();

            List<int> agesList = new List<int>();

            foreach (var directory in directories)
            {
                Student student = new Student();


                student.FromDirectory(directory);

                Console.WriteLine(student.ToString());

                //Path to the remote file you will download
                string remoteDownloadFilePath = "/200454993 Ankur Singh/info.csv";


                //Path to a valid folder and the new file to be saved
                string localDownloadFileDestination = @"C:\Users\Ankur\Desktop\FTP_files\info.csv";

                //Search for a file named myimage.jpg
                bool exists = FTP.FileExists(Constants.FTP.BaseUrl + directory + "/myimage.jpg");

                //Does the file exist?
                if (exists == true)
                {
                    Console.WriteLine("File exists");
                }
                else
                {
                    Console.WriteLine("File does not exist");
                }

                Console.WriteLine(FTP.DownloadFile(Constants.FTP.BaseUrl + remoteDownloadFilePath, localDownloadFileDestination));


                var FileBytes = FTP.DownloadFileBytes(Constants.FTP.BaseUrl + "/" + directory + "/info.csv");
                string infoCsvData = Encoding.UTF8.GetString(FileBytes, 0, FileBytes.Length);
                try
                {
                    string[] lines = infoCsvData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

                    student.FromCsv(lines[1]);

                    Console.WriteLine(student.HeaderRow);
                    Console.WriteLine(student.ToCSV());


                    agesList.Add(student.Age);
                    Students.Add(student);


                }
                catch (Exception e)
                {

                }

            }


                int count = Students.Count();
                Console.WriteLine($"The list contain {count} students");

                int highestMax = Students.Max(x => x.Age);
                Console.WriteLine($"The highest Age in the list is {highestMax}");

                int lowestMax = Students.Min(x => x.Age);
                Console.WriteLine($"The lowest Age in the list is {lowestMax}");

                double averageAge = Students.Average(x => x.Age);
                Console.WriteLine($"The average age is => {averageAge.ToString("0")}");


                Student me = new Student
                {
                    StudentId = "200454993",
                    FirstName = "Ankur",
                    LastName = "Singh",
                   
                };



                bool listContains = Students.Contains(me);

                if (listContains == true)
                {
                    Console.WriteLine($"Found {me}");
                }
                else
                {
                    Console.WriteLine($"Could not find {me}");
                }


                //Starts With

                string name = "2004";
                Console.WriteLine("No of Students:: " + StudentCount(name, Students));

                //Starts With Method
                int StudentCount(string name, List<Student> students)
                {
                    List<Student> startswith = new List<Student>();
                    foreach (Student student in students)
                    {
                        //Comparing alphabet with firstname and lastname of student.
                        if (student.StudentId.StartsWith(name))
                        {
                            startswith.Add(student);
                            Console.WriteLine(student.ToString());
                        }
                    }
                    return startswith.Count;
                }

                
                string myID = "200454993";
                Student studentMyRecord = Students.Find(x => x.StudentId == myID);

                if (studentMyRecord != null)
                {
                    Student student = new Student();
                    student.MyRecord = true;
                    Console.WriteLine($"Found Student with {myID}");
                }
                else
                {
                    Console.WriteLine($"Could not find Student with {myID}");
                }


                // creating csv file
                string CSVPath = @"C:\Users\Ankur\Desktop\FTP_files\students.csv";
                using (StreamWriter fs1 = new StreamWriter(CSVPath))
                {
                    fs1.WriteLine("StudentId,FirstName,LastName,Age,DateOfBirth,MyRecord,Image");
                    foreach (Student st1 in Students)
                    {
                        fs1.WriteLine(st1.ToCSV());
                    }
                }

                //creating json file
                string jsonStudents = JsonConvert.SerializeObject(Students);
                System.IO.File.WriteAllText(@"C:\Users\Ankur\Desktop\FTP_files\students.json", jsonStudents);
                Console.WriteLine("student.json file created");

                //creating xml file
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(Students.GetType());
                TextWriter writer = new StreamWriter(@"C:\Users\Ankur\Desktop\FTP_files\students.xml");
                x.Serialize(writer, Students);
                writer.Close();
                Console.WriteLine("students.xml file created");


            // CSV Method
            string csvlocalUploadFilePath = @"C:\Users\Ankur\Desktop\FTP_files\students.csv";
            string csvremoteUploadFileDestination = "/200454993 Ankur Singh/student.csv";
            Console.WriteLine(FTP.UploadFile(csvlocalUploadFilePath, Constants.FTP.BaseUrl + csvremoteUploadFileDestination));

            // JSON Method
            string jsonlocalUploadFilePath = @"C:\Users\Ankur\Desktop\FTP_files\students.json";
            string jsonremoteUploadFileDestination = "/200454993 Ankur Singh/student.json";
            Console.WriteLine(FTP.UploadFile(jsonlocalUploadFilePath, Constants.FTP.BaseUrl + jsonremoteUploadFileDestination));

            // XML Method
            string xmllocalUploadFilePath = @"C:\Users\Ankur\Desktop\FTP_files\students.xml";
            string xmlremoteUploadFileDestination = "/200454993 Ankur Singh/student.xml";
            Console.WriteLine(FTP.UploadFile(xmllocalUploadFilePath, Constants.FTP.BaseUrl + xmlremoteUploadFileDestination));
        }

        }
    }

