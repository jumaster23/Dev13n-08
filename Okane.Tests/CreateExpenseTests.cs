using Okane.Application;

namespace Okane.Tests;

public class CreateExpenseTests
{
    [Fact]
    public void Test()
    {
        var expense = new ExpensesService().Create(10, "Food");

        Assert.Equal(10, expense.Amount);
        Assert.Equal("Food", expense.CategoryName);
    }
}