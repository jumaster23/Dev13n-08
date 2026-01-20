namespace Okane.Application;

public class ExpensesService
{
    public Expense Create(int amount, string category) => 
        new(amount, category);
}