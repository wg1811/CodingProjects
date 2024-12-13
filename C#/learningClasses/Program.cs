using System;
using learningClasses;
// Person person = new Person()
// {
//     Name = "Ted",
//     Age = 24,
//     City = "London",
//     HasPet = true,
// };

// person.PersonInfo();

// Car newCar = new Car()
// {
//     Make = "Porsche",
//     Model = "911",
//     Year = 1970,
//     Color = "Yellow",
//     FourWheelDrive = false,
// };

// newCar.ShowCarInfo();

// List<int> numList = new List<int>();
// {

// }
// for (int i = 0; i < 5; i++) {
//     numList.Add(i*27);
// }
// int counter = 1;
// foreach (int num in numList)
// {
//     Console.WriteLine($"{counter}: {num}");
//     counter++; 
// }

// Making Students and Classrooms
List<Student> studentList = [];
string[] nameList = ["Bob", "Javier", "Olga", "Robin", "Nasir"];
int[] ageList = [23, 43, 54, 23, 65];
for (int i = 0; i < nameList.Length; i++)
{

    studentList.Add(new Student());
    studentList[i].Name = nameList[i];
    studentList[i].Age = ageList[i];
    Console.WriteLine(studentList[i].Name + " is this old: " + studentList[i].Age);
}
// foreach (Student student in classroom) {

//     student.DisplayStudent();
// }
List<Classroom> classOffering = [];
string[] classNames = ["Algebra", "Calculus", "History", "Philosophy", "Economics"];
for (int i = 0; i < classNames.Length; i++)
{
    classOffering.Add(new Classroom());
    classOffering[i].Name = classNames[i];
    classOffering[i].Semester = "Fall";
    classOffering[i].StudentList = studentList;
    classOffering[i].DisplayClassInfo();
}
