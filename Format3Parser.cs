using System.Globalization;

namespace ConsoleApp1
{
    public class Format3Parser : IEmployeeParser
    {
        public List<Employee> Parse(string csvContent)
        {
            var employees = new List<Employee>();
            var lines = csvContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            // Skip header line
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue;
                
                var columns = ParseCsvLine(line);
                
                if (columns.Length >= 4)
                {
                    try
                    {
                        var employee = new Employee
                        {
                            Id = columns[0].Trim(), // StaffNumber
                            FullName = columns[1].Trim(), // PersonName
                            Email = columns[2].Trim(), // ContactEmail
                            StartDate = DateTime.ParseExact(columns[3].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture) // EmploymentDate
                        };
                        employees.Add(employee);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error parsing CSV line {i + 1}: {ex.Message}");
                    }
                }
            }
            
            return employees;
        }
        
        private string[] ParseCsvLine(string line)
        {
            var result = new List<string>();
            var current = "";
            bool inQuotes = false;
            
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(current.Trim());
                    current = "";
                }
                else
                {
                    current += c;
                }
            }
            
            result.Add(current.Trim());
            return result.ToArray();
        }
    }
} 