namespace StudentManagerApp
{
    internal partial class Program
    {
        public static void Menu()
        {
            string menuText =
                $@"

            ---------MENU----------
            1- Show Student
            2- Show Students
            3- Add Student 
            4- Update Student
            5- Delete Student
            6- Exit Program 
            ------------------------
            ";
            //while loop
            while (true)
            {
                //show menu
                Console.WriteLine(menuText);
                Console.WriteLine("Enter your Choice ");
                string userInput = Console.ReadLine() ?? "0";

                //switch
                switch (userInput)
                {
                    case "1":
                        studentController.ShowStudent();
                        //Console.WriteLine("needs debug");
                        break;
                    case "2":
                        studentController.ShowStudents();
                        break;
                    case "3":
                        studentController.AddStudent();
                        break;
                    case "4":
                        studentController.UpdateStudent();
                        //Console.WriteLine("need debug");
                        break;
                    case "5":
                        studentController.DeleteStudent();

                        break;
                    case "6":
                        Console.WriteLine("Exiting Program..");
                        Thread.Sleep(3000);
                        Console.Clear();

                        return;
                    default:
                        Console.WriteLine("invalid option try again");
                        break;
                }
            }
        }
    }
}
