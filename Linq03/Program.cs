using Linq03.Entitites;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Linq03 {
    class Program {
        static void Main(string[] args) {
            //in.txt
            Console.Write("Enter the full file path: ");
            string path = Console.ReadLine();

            List<Employee> employees = new List<Employee>();

            try {
                using (StreamReader sr = File.OpenText(path)) {
                    while (!sr.EndOfStream) {
                        string[] fields = sr.ReadLine().Split(',');
                        string name = fields[0];
                        string email = fields[1];
                        double salary = double.Parse(fields[2], CultureInfo.InvariantCulture);
                        employees.Add(new Employee(name, email, salary));
                    }
                }

                Console.Write("Enter salary: R$ ");
                double value = double.Parse(Console.ReadLine());

                var emails = employees.Where(e => e.Salary > value).OrderBy(e => e.Email).Select(e => e.Email);
                Console.WriteLine("Email of people whose salary is more than R$ " + value);
                foreach (string email in emails) {
                    Console.WriteLine(email);
                }

                var sum = employees.Where(e => e.Name[0] == 'M').Sum(e => e.Salary);
                Console.WriteLine("Sum of salary of people whose name starts with 'M': R$ " + sum.ToString("F2"));

            }
            catch (IOException e) {
                Console.WriteLine("An error occured");
                Console.WriteLine(e.Message);
            }
            catch (FormatException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
