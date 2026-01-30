using Okane.Domain;

namespace Okane.Application.Expenses;

public class InMemoryExpensesRepository : InMemoryRepository<Expense>, IExpensesRepository
{
    public Expense Update(int id, UpdateExpenseRequest request, Category category)
    {
        var existing = Entities.First(e => e.Id == id);
        
        existing.Amount = request.Amount;
        existing.Category = category;

        return existing;
    }

    public IEnumerable<Expense> ByCategoryName(string categoryName) => 
        Entities.Where(e => e.Category.Name == categoryName);
}