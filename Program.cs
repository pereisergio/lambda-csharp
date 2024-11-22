using ExerciceSection15.Entities;
using System.Globalization;
using System.Collections.Generic;
using System.IO;



namespace ExerciceSection15
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"in.txt";

            List<Employee> employees = new List<Employee>();
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        string name = line[0];
                        string email = line[1];
                        double salary = double.Parse(line[2], CultureInfo.InvariantCulture);

                        employees.Add(new Employee(name, email, salary));
                    }
                }

                Console.Write("Enter salary: ");
                double searchSalary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                var searchOne =
                    from emp in employees
                    where emp.Salary > searchSalary
                    orderby emp.Name
                    select emp.Email;

                Console.WriteLine($"Email of people whose salary is more than {searchSalary.ToString("F2", CultureInfo.InvariantCulture)}:");
                foreach (var email in searchOne)
                {
                    Console.WriteLine(email);
                }

                var searchTwo =
                    from emp in employees
                    where emp.Name[0] == 'M'
                    select emp;

                Console.WriteLine($"Sum of salary of people whose name starts with 'M': {searchTwo.Sum(emp => emp.Salary).ToString("F2", CultureInfo.InvariantCulture)}");

            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }
    }
}