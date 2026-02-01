using System.Collections.Generic;
using System.Linq;

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

    public Expense? Update(UpdateExpenseRequest request)
    {
        var expense = expenses.FirstOrDefault(e => e.Id == request.Id);
        if (expense == null) return null;
        
        int index = expenses.IndexOf(expense);
        
        var updatedExpense = new Expense(request.Id, request.Amount, request.CategoryName);
        expenses[index] = updatedExpense;

        return updatedExpense;
    }

    public bool Delete(int id)
    {
        var expense = expenses.FirstOrDefault(e => e.Id == id);
        
        if (expense == null)
        {
            return false;
        }
        
        return expenses.Remove(expense);
    }
}