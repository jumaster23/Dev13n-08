namespace Okane.Application;

public class ExpensesService(List<Expense> expenses)
{
    private static int _lastId = 1;

    public Expense Create(CreateExpenseRequest request)
    {
        var (amount, categoryName) = request;
        var id = _lastId++;
        var expense = new Expense(id, amount, categoryName);
        expenses.Add(expense);
        return expense;
    }

    public Expense? Retrieve(int expenseId)
    {
        var expense = expenses.FirstOrDefault(e => e.Id == expenseId);
        return expense;
    }

    public IEnumerable<Expense> All() => expenses;
}