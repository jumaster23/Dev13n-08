namespace Okane.Application;

public class ExpensesService(List<Expense> expenses)
{
    private static int _lastId = 1;

    public Result<Expense> Create(CreateExpenseRequest request)
    {
        var (amount, categoryName) = request;

        if (amount < 1)
            return new ErrorResult<Expense>(
                $"{nameof(request.Amount)} must be greater than 1.");

        var id = _lastId++;
        var expense = new Expense(id, amount, categoryName);
        expenses.Add(expense);

        return new OkResult<Expense>(expense);
    }

    public Result<Expense> Retrieve(int expenseId)
    {
        var expense = expenses.FirstOrDefault(e => e.Id == expenseId);

        if (expense is null)
            return new NotFoundResult<Expense>(
                $"Expense with id {expenseId} was not found.");

        return new OkResult<Expense>(expense);
    }

    public Result<IEnumerable<Expense>> All()
        => new OkResult<IEnumerable<Expense>>(expenses);

    public Result<Expense> Update(int id, UpdateExpenseRequest request)
    {
        if (request.Amount < 1)
            return new ErrorResult<Expense>(
                $"{nameof(request.Amount)} must be greater than 1.");

        var existing = expenses.FirstOrDefault(e => e.Id == id);

        if (existing is null)
            return new NotFoundResult<Expense>(
                $"Expense with id {id} was not found.");

        expenses.Remove(existing);

        var updated = new Expense(
            id,
            request.Amount,
            request.CategoryName);

        expenses.Add(updated);

        return new OkResult<Expense>(updated);
    }

    public Result Delete(int id)
    {
        var expense = expenses.FirstOrDefault(e => e.Id == id);

        if (expense is null)
            return new NotFoundResult(
                $"Expense with id {id} was not found.");

        expenses.Remove(expense);

        return new OkResult();
    }
}
