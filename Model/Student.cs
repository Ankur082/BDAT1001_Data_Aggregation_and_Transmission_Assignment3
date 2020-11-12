using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FTPApp.Model
{
    public class Student
    {
        public string HeaderRow = $"{nameof(StudentId)},{nameof(FirstName)},{nameof(LastName)},{nameof(DateOfBirth)},{nameof(MyRecord)},{nameof(ImageData)}";
        public string StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }



        private string _DateOfBirthString;
        public string DateOfBirthString
        {
            get { return _DateOfBirthString; }
            set
            {
                _DateOfBirthString = value;



                DateTime datetime;
                if (DateTime.TryParse(_DateOfBirthString, out datetime) == true)
                {
                    DateOfBirth = datetime;
                }
            }
        }

        public string ImageData { get; set; }
        public DateTime DateOfBirth { get; set; }

        public bool MyRecord { get; set; }
        public virtual int Age
        {
            get
            {
                DateTime Now = DateTime.Now;
                int Years = new DateTime(DateTime.Now.Subtract(DateOfBirth).Ticks).Year - 1;
                DateTime PastYearDate = DateOfBirth.AddYears(Years);
                int Months = 0;
                for (int i = 1; i <= 12; i++)
                {
                    if (PastYearDate.AddMonths(i) == Now)
                    {
                        Months = i;
                        break;
                    }
                    else if (PastYearDate.AddMonths(i) >= Now)
                    {
                        Months = i - 1;
                        break;
                    }
                }
                int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
                int Hours = Now.Subtract(PastYearDate).Hours;
                int Minutes = Now.Subtract(PastYearDate).Minutes;
                int Seconds = Now.Subtract(PastYearDate).Seconds;
                return Years;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDirectory"></param>
        public void FromDirectory(string myDirectory)
        {
            try
            {
                string[] directoryPart = myDirectory.Split(" ", StringSplitOptions.None);

                StudentId = directoryPart[0];
                FirstName = directoryPart[1];
                LastName = directoryPart[2];
            }

            catch (Exception e)
            {

            }


        }

        public override string ToString()
        {
            return $"{StudentId}:{FirstName} {LastName}";
        }


        public void FromCsv(string csvDataLine)
        {
            try
            {
                string[] csvDataLineParts = csvDataLine.Split(",", StringSplitOptions.None);


                StudentId = csvDataLineParts[0];
                FirstName = csvDataLineParts[1];
                LastName = csvDataLineParts[2];
                DateOfBirthString = csvDataLineParts[3];
                ImageData = csvDataLineParts[4];


            }

            catch (Exception e)
            {

            }


        }

        public string ToCSV()
        {
            string result = $"{StudentId},{FirstName},{LastName},{DateOfBirthString},{MyRecord},{ImageData}";
            return result;

        }


    }



}

