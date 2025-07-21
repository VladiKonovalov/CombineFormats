using System.Text.Json;

namespace ConsoleApp1
{
    public static class FormatDetector
    {
        public static IEmployeeParser Detect(string content, string filePath = "")
        {
            // Check if it's a CSV file
            if (filePath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase) || 
                content.TrimStart().StartsWith("StaffNumber,"))
            {
                return new Format3Parser();
            }
            
            // Try to parse as JSON
            try
            {
                using JsonDocument doc = JsonDocument.Parse(content);
                var first = doc.RootElement[0];

                if (first.TryGetProperty("EmployeeId", out _))
                    return new Format1Parser();
                if (first.TryGetProperty("ID", out _))
                    return new Format2Parser();
            }
            catch (JsonException)
            {
                // Not valid JSON, might be CSV
                if (content.Contains(",") && content.Contains("\n"))
                {
                    return new Format3Parser();
                }
            }

            throw new Exception("Unknown format");
        }
    }
}
