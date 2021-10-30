using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TestBasedFileSystem_Assessment
{
    [Serializable]
    class Teacher
    {
        public ArrayList ids = new ArrayList();
        public ArrayList names = new ArrayList();
        public ArrayList classes = new ArrayList();
        public ArrayList sections = new ArrayList();
    }
    class Solution
    {
        public static void addTeacherName()
        {
            Stream stream;
            IFormatter formatter;
            if (!File.Exists("C:\\data\\teacher.txt"))
            {
                Teacher newTeacher = new Teacher();
                formatter= new BinaryFormatter();
                stream = new FileStream("C:\\data\\teacher.txt", FileMode.Create, FileAccess.Write);
                Console.Write("Enter Teacher Id:");
                newTeacher.ids.Add(int.Parse(Console.ReadLine()));
                Console.Write("Enter Teacher Name:");
                newTeacher.names.Add(Console.ReadLine());
                Console.Write("Enter Teacher Class:");
                newTeacher.classes.Add(Console.ReadLine());
                Console.Write("Enter Teacher Section:");
                newTeacher.sections.Add(Console.ReadLine());
                formatter.Serialize(stream, newTeacher);
                Console.WriteLine("Teacher added to the file successfully.");
                stream.Close();
            }
            else
            {
                //stream = new FileStream("C:\\data\\teacher.txt", FileMode.Append, FileAccess.Write);
                formatter = new BinaryFormatter();
                stream = new FileStream("C:\\data\\teacher.txt", FileMode.Open, FileAccess.Read);
                Teacher existingTeacher = (Teacher)formatter.Deserialize(stream);
                stream.Close();
                Console.Write("Enter Teacher Id:");
                existingTeacher.ids.Add(int.Parse(Console.ReadLine()));
                Console.Write("Enter Teacher Name:");
                existingTeacher.names.Add(Console.ReadLine());
                Console.Write("Enter Teacher Class:");
                existingTeacher.classes.Add(Console.ReadLine());
                Console.Write("Enter Teacher Section:");
                existingTeacher.sections.Add(Console.ReadLine());
                stream = new FileStream("C:\\data\\teacher.txt", FileMode.Create, FileAccess.Write);
                formatter.Serialize(stream, existingTeacher);
                Console.WriteLine("Teacher added to the file successfully.");
                stream.Close();

            }



            //Stream stream = new FileStream("C:\\data\\teacher.txt", FileMode.Create, FileAccess.Write);

            
        } //endof of Add Teacher Functions


        public static void displayTeacherName()
        {
            //Check if File Exist
            if (!File.Exists("C:\\data\\teacher.txt"))
            {
                Console.WriteLine("File does not exist. Please add some records");
                return;
            }
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("C:\\data\\teacher.txt", FileMode.Open, FileAccess.Read);
            Teacher teacher = (Teacher)formatter.Deserialize(stream);
            Console.WriteLine("capacity is", teacher.ids.Count);
            for (int i = 0; i < teacher.ids.Count; i++)
            {
                Console.WriteLine("{0}, {1}, {2},{3}", teacher.ids[i], teacher.names[i], teacher.classes[i], teacher.sections[i]);

            }
            stream.Close();
        }//end of display function

        public static void updateTeacherName()
        {
            if (!File.Exists("C:\\data\\teacher.txt"))
            {
                Console.WriteLine("File does not exist. Please add some records");
                return;
            }
            Console.WriteLine("Add id of teacher to update:");
            int id = int.Parse(Console.ReadLine());
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("C:\\data\\teacher.txt", FileMode.Open, FileAccess.Read);
            Teacher teacher = (Teacher)formatter.Deserialize(stream);
            stream.Close();
            int index = teacher.ids.IndexOf(id);
            if (index == -1)
            {
                Console.WriteLine("Id does not exist");
                return;
            }
            Console.Write("Enter the updated Teacher Name:");
            teacher.names[index] = Console.ReadLine();
            Console.Write("Enter the updated Class:");
            teacher.classes[index] = Console.ReadLine();
            Console.Write("Enter the updated Section:");
            teacher.sections[index] = Console.ReadLine();

            formatter = new BinaryFormatter();
            stream = new FileStream("C:\\data\\teacher.txt", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, teacher);
            Console.WriteLine("Teacher Updated to the file successfully.");
            stream.Close();


        }//end of update function
        static void Main(string[] args)
        {            
            string answer="yes",choice;
            do
            {
                Console.WriteLine("Enter Choice: 1. to Add Teacher Info 2. to Update Teacher Info 3.Display the teacher records");
                choice = Console.ReadLine();
                if (choice != "1" && choice != "2" && choice != "3")
                    Console.WriteLine("Incorrect Input! Please enter 1 or 2 or 3");
                else
                {
                    switch (choice)
                    {

                        case "1":
                            Console.WriteLine("Adding new teacher");
                            Solution.addTeacherName();
                            break;

                        case "2":
                            Console.WriteLine("Updating existing teacher");                            
                            Solution.updateTeacherName();
                            break;

                        case "3":
                            Console.WriteLine("***Current List Of Teachers*******");
                            Solution.displayTeacherName();
                            break;

                        default:
                            Console.WriteLine("Default Case");
                            break;
                    }                    
                }

                Console.WriteLine("Do you wish to continue?");
                answer = Console.ReadLine();
            } while (answer == "yes");

            //Console.ReadKey();
        }
    }
}
