using System.Text.Json;

namespace ConsoleApp1
{
    public class Format1Parser : IEmployeeParser
    {
        private class Format1Model
        {
            public required string EmployeeId { get; set; }
            public required string FullName { get; set; }
            public required string Email { get; set; }
            public required string StartDate { get; set; }
        }

        public List<Employee> Parse(string jsonContent)
        {
            var list = JsonSerializer.Deserialize<List<Format1Model>>(jsonContent);
            return list?.Select(x => new Employee
            {
                Id = x.EmployeeId,
                FullName = x.FullName,
                Email = x.Email,
                StartDate = DateTime.Parse(x.StartDate)
            }).ToList() ?? new List<Employee>();
        }
    }
}
