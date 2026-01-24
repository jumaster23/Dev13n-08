namespace Okane.Application;

public class ExpensesService(List<Expense> expenses)
{
    private static int _lastId = 1;
    
    public Result<ExpenseResponse> Create(CreateExpenseRequest request)
    {
        var (amount, categoryName) = request;

        if (amount < 1)
            return new ErrorResult<ExpenseResponse>(
                $"{nameof(request.Amount)} must be greater than 1.");

        var id = _lastId++;
        var expense = new Expense(id, amount, categoryName);
        expenses.Add(expense);
        
        var response = new ExpenseResponse(expense.Id, expense.Amount, expense.CategoryName);
        return new OkResult<ExpenseResponse>(response);
    }

    public Result<ExpenseResponse> Retrieve(int expenseId)
    {
        var expense = expenses.FirstOrDefault(e => e.Id == expenseId);
        
        if (expense is null)
            return new NotFoundResult<ExpenseResponse>(
                $"Expense with id {expenseId} was not found.");
        
        var response = new ExpenseResponse(expense.Id, expense.Amount, expense.CategoryName);

        return new OkResult<ExpenseResponse>(response);
    }

    public Result<IEnumerable<ExpenseResponse>> All()
    {
        var response = expenses
            .Select(expense => new ExpenseResponse(expense.Id, expense.Amount, expense.CategoryName));
        
        return new OkResult<IEnumerable<ExpenseResponse>>(response);
    }

    public Result<ExpenseResponse> Update(int id, UpdateExpenseRequest request)
    {
        if (request.Amount < 1)
            return new ErrorResult<ExpenseResponse>(
                $"{nameof(request.Amount)} must be greater than 1.");

        var existing = expenses.FirstOrDefault(e => e.Id == id);

        if (existing is null)
            return new NotFoundResult<ExpenseResponse>(
                $"Expense with id {id} was not found.");

        expenses.Remove(existing);

        var updated = new Expense(
            id,
            request.Amount,
            request.CategoryName);

        expenses.Add(updated);
        
        var response = new ExpenseResponse(updated.Id, updated.Amount, updated.CategoryName);
        return new OkResult<ExpenseResponse>(response);
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