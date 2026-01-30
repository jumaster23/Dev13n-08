namespace Okane.Application.Expenses;

public record CreateExpenseRequest(int Amount, string CategoryName, string? Description = null);