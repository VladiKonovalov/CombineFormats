using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public static class EmployeeValidator
    {
        public static bool IsValid(Employee emp)
        {
            return IsValidId(emp.Id) && IsValidEmail(emp.Email);
        }

        private static bool IsValidId(string id)
        {
            return !string.IsNullOrEmpty(id) && Regex.IsMatch(id, @"^[A-Za-z0-9]+$");
        }

        private static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
