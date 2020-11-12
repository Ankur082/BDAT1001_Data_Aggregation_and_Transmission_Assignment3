using System;

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

            List<string> students = new List<student>();
            List<int> ages = new List<int>;
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
                bool exists = FTP.FileExists(Constants.FTP.BaseUrl + "/<your folder>/myimage.jpg");

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


                var FileBytes = FTP.DownloadFileBytes(Constant.FTP.BaseUrl + "/" + directory + "/info.csv");
                string infoCsvData = Encoding.UTF8.GetString(FileBytes, 0, FileBytes.Length);
                try
                {
                    string[] lines = infoCsvData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

                    student.FromCsv(lines[1]);

                    Console.WriteLine(student.HeaderRow);
                    Console.WriteLine(student.ToCsv());


                    ages.Add(student.Age);
                    students.Add(student);


                }
                catch (Exception e)
                {

                }


                int count = students.Count();
                Console.WriteLine($"The list contain {count} students");

                int highestMax = students.Max(x => x.Age);
                Console.WriteLine($"The highest Age in the list is {highestMax}");

                int lowestMax = students.Min(x => x.Age);
                Console.WriteLine($"The lowest Age in the list is {lowestMax}");

                double averageAge = students.Average(x => x.Age);
                Console.WriteLine($"The average age is => {averageAge.ToString("0")}");


                string containCheck = "Ank";
                bool listContains = students.Contains(containCheck);

                if (listContains == true)
                {
                    Console.WriteLine($"Found {containCheck}");
                }
                else
                {
                    Console.WriteLine($"Could not find {containCheck}");
                }


                //Starts With

                string name = "2004";
                Console.WriteLine("No of Students:: " + FindStudentCount1(name, students));

                //Starts With Method
                public static int StudentCount(string name, List<Student> students)
                {
                    List<Student> startswith = new List<Student>();
                    foreach (Student student in students)
                    {
                        //Comparing alphabet with firstname and lastname of student.
                        if (student.StudentId.StartWith(name))
                        {
                            filteredStudents.Add(student);
                            Console.WriteLine(student.ToString());
                        }
                    }
                    return startswith.Count;
                }


                string myID = "200454993"
                Student student = students.Find(x => x.StudentId == myID);

                if (student != null)
                {
                    student.MyRecord = true;
                    Console.WriteLine($"Found Student {student}");
                }
                else
                {
                    Console.WriteLine($"Could not find Student {student}");
                }


                / Making csv file
                string CSVPath = @"C:\Users\Ankur\Desktop\FTP_files\students.csv";
                using (StreamWriter fs1 = new StreamWriter(CSVPath))
                {
                    fs1.WriteLine("StudentId,FirstName,LastName,Age,DateOfBirth,MyRecord,Image");
                    foreach (Student st1 in students)
                    {
                        fs1.WriteLine(st1.ToCSV());
                    }
                }

                //creating and writing students.json file
                string jsonFile = JsonConvert.SerializeObject(students);
                System.IO.File.WriteAllText(@"C:\Users\Ankur\Desktop\FTP_files\students.json", jsonFile);
                Console.WriteLine("student.json file created");

                //creating and writing students.xml file
                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(students.GetType());
                TextWriter writer = new StreamWriter(@"C:\Users\Ankur\Desktop\FTP_files\students.xml");
                x.Serialize(writer, students);
                writer.Close();
                Console.WriteLine("students.xml file created");



                string csvlocalUploadFilePath = @"C:\Users\Ankur\Desktop\FTP_files\students.csv";
                string csvremoteUploadFileDestination = "/200454993 Ankur Singh/student.csv";
                Console.WriteLine(FTP.UploadFile(csvlocalUploadFilePath, Constants.FTP.BaseUrl + csvremoteUploadFileDestination));


                string jsonlocalUploadFilePath = @"C:\Users\Ankur\Desktop\FTP_files\students.json";
                string csvremoteUploadFileDestination = "/200454993 Ankur Singh/student.json";
                Console.WriteLine(FTP.UploadFile(jsonlocalUploadFilePath, Constants.FTP.BaseUrl + csvremoteUploadFileDestination));


                string xmllocalUploadFilePath = @"C:\Users\Ankur\Desktop\FTP_files\students.xml";
                string xmlremoteUploadFileDestination = "/200454993 Ankur Singh/student.xml";
                Console.WriteLine(FTP.UploadFile(xmllocalUploadFilePath, Constants.FTP.BaseUrl + xmlremoteUploadFileDestination));
            }

        }
    }
}
