using Okane.Application;

namespace Okane.Tests;

public class ExpensesServiceTests
{
    private readonly ExpensesService _service;
    private List<Expense> _expenses;

    public ExpensesServiceTests()
    {
        _expenses = new List<Expense>();
        _service = new ExpensesService(_expenses);
    }

    [Fact]
    public void Create_Response()
    {
        var expense = _service.Create(new(10, "Food"));

        Assert.Equal(10, expense.Amount);
        Assert.Equal("Food", expense.CategoryName);
    }
    
    [Fact]
    public void Create_AddsExpense()
    {
        _expenses = new List<Expense>();
        
        
        var expense = _service.Create(new(10, "Food"));

        var retrieved = _service.Retrieve(expense.Id);
        
        Assert.NotNull(retrieved);
        Assert.Equal(10, retrieved.Amount);
        Assert.Equal("Food", retrieved.CategoryName);
    }
    
    [Fact]
    public void Retrieve_NotFound()
    {
        var retrieved = _service.Retrieve(1);
        
        Assert.Null(retrieved);
    }

    [Fact]
    public void All()
    {
        _service.Create(new(10, "Food"));
        _service.Create(new(20, "Drinks"));
        
        var response = _service.All().ToArray();
        
        Assert.Equal(2, response.Count());
        
        var firstExpense = response.First();
        Assert.Equal(10, firstExpense.Amount);
        Assert.Equal("Food", firstExpense.CategoryName);
    }
}