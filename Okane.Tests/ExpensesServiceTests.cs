using Okane.Application;

namespace Okane.Tests;

public class ExpensesServiceTests
{
    [Fact]
    public void Create_Response()
    {
        var service = new ExpensesService([]);
        var expense = service.Create(new(10, "Food"));

        Assert.Equal(10, expense.Amount);
        Assert.Equal("Food", expense.CategoryName);
    }
    
    [Fact]
    public void Create_AddsExpense()
    {
        var expenses = new List<Expense>();
        var service = new ExpensesService(expenses);
        
        var expense = service.Create(new(10, "Food"));

        var retrieved = service.Retrieve(expense.Id);
        
        Assert.NotNull(retrieved);
        Assert.Equal(10, retrieved.Amount);
        Assert.Equal("Food", retrieved.CategoryName);
    }
    
    [Fact]
    public void Retrieve_NotFound()
    {
        var expenses = new List<Expense>();
        var service = new ExpensesService(expenses);

        var retrieved = service.Retrieve(1);
        
        Assert.Null(retrieved);
    }

    [Fact]
    public void All()
    {
        var expenses = new List<Expense>();
        var service = new ExpensesService(expenses);

        service.Create(new(10, "Food"));
        service.Create(new(20, "Drinks"));
        
        var response = service.All().ToArray();
        
        Assert.Equal(2, response.Count());
        
        var firstExpense = response.First();
        Assert.Equal(10, firstExpense.Amount);
        Assert.Equal("Food", firstExpense.CategoryName);
    }
}