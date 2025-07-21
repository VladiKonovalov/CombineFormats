using System.Text.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***************we begin the test:");

            var inputFiles = new[] { "input.json", "input2.json", "input3.csv" };
            var allEmployees = new List<Employee>();

            foreach (var file in inputFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                var fileContent = File.ReadAllText(file);

                var parser = FormatDetector.Detect(fileContent, file);
                var employees = parser.Parse(fileContent);
                var validEmployees = employees.Where(EmployeeValidator.IsValid).ToList();

                allEmployees.AddRange(validEmployees);
            }

            Console.WriteLine("Normalized Valid Employees (JSON):");
            string output = JsonSerializer.Serialize(allEmployees, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(output);
        }
    }
}
