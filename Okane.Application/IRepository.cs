namespace Okane.Application;

public interface IRepository<T>
{
    void Add(T entity);
    T? ById(int id);
    IEnumerable<T> All();
    void Remove(int id);
    bool Exists(int id);
    Expense Update(int id, UpdateExpenseRequest request);
}