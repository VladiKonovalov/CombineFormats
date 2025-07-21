namespace ConsoleApp1
{
    public interface IEmployeeParser
    {
        List<Employee> Parse(string jsonContent);
    }
}
