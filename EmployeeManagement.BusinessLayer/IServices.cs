namespace EmployeeManagement.BusinessLayer
{
    public interface IServices<out T>
    {
        T Service { get; }
    }
}
