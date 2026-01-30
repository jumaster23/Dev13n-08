using Okane.Domain;

namespace Okane.Application.Expenses;

public class ExpenseResponseFactory
{
    public ExpenseResponse Create(Expense expense) =>
        new(
            expense.Id,
            expense.Amount,
            expense.CategoryName,
            expense.Description);
}