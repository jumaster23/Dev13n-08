using Okane.Domain;

namespace Okane.Application.Expenses;

public interface IExpensesRepository : IRepository<Expense>
{
    Expense Update(int id, UpdateExpenseRequest request, Category category);
    IEnumerable<Expense> ByCategoryName(string categoryName);
}