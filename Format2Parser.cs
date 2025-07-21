using System.Text.Json;

namespace ConsoleApp1
{
    public class Format2Parser : IEmployeeParser
    {
        private class Format2Model
        {
            public required string ID { get; set; }
            public required string Name { get; set; }
            public required string Mail { get; set; }
            public required string Start { get; set; }
        }

        public List<Employee> Parse(string jsonContent)
        {
            var list = JsonSerializer.Deserialize<List<Format2Model>>(jsonContent);
            return list?.Select(x => new Employee
            {
                Id = x.ID,
                FullName = x.Name,
                Email = x.Mail,
                StartDate = DateTime.Parse(x.Start)
            }).ToList() ?? new List<Employee>();
        }
    }
}
