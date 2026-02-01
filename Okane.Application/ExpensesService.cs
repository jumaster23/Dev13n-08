namespace Okane.Application;

public class ExpensesService(IRepository<Expense> expenses)
{
    public Result<ExpenseResponse> Create(CreateExpenseRequest request)
    {
        var (amount, categoryName) = request;

        if (amount < 1)
            return new ErrorResult<ExpenseResponse>(
                $"{nameof(request.Amount)} must be greater than 1.");
        
        var expense = new Expense
        {
            
            Amount = request.Amount,
            CategoryName = request.CategoryName
        };
        expenses.Add(expense);
        
        var response = new ExpenseResponse(expense.Id, expense.Amount, expense.CategoryName);
        return new OkResult<ExpenseResponse>(response);
    }

    public Result<ExpenseResponse> Retrieve(int expenseId)
    {
        var expense = expenses.ById(expenseId);
        
        if (expense is null)
            return new NotFoundResult<ExpenseResponse>(
                $"Expense with id {expenseId} was not found.");
        
        var response = new ExpenseResponse(expense.Id, expense.Amount, expense.CategoryName);

        return new OkResult<ExpenseResponse>(response);
    }

    public Result<IEnumerable<ExpenseResponse>> All()
    {
        var response = expenses.All()
            .Select(expense => new ExpenseResponse(expense.Id, expense.Amount, expense.CategoryName));
        
        return new OkResult<IEnumerable<ExpenseResponse>>(response);
    }

    public Result<ExpenseResponse> Update(int id, UpdateExpenseRequest request)
    {
        if (request.Amount < 1)
            return new ErrorResult<ExpenseResponse>(
                $"{nameof(request.Amount)} must be greater than 1.");

        if (!expenses.Exists(id))
            return new NotFoundResult<ExpenseResponse>(
                $"Expense with id {id} was not found.");

        var updated = expenses.Update(id, request);
        
        var response = new ExpenseResponse(updated.Id, updated.Amount, updated.CategoryName);
        return new OkResult<ExpenseResponse>(response);
    }

    public Result Delete(int id)
    {
        if (!expenses.Exists(id))
            return new NotFoundResult(
                $"Expense with id {id} was not found.");

        expenses.Remove(id);

        return new OkResult();
    }
}