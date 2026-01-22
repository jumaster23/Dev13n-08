using Okane.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOpenApi()
    .AddTransient<ExpensesService>()
    .AddSingleton<List<Expense>>();

var app = builder.Build();
    
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/expenses", 
    (ExpensesService service, CreateExpenseRequest request) => 
        Results.Created("/expenses", service.Create(request)));

app.MapGet("/expenses/{id}", 
    (ExpensesService service, int id) =>
    {
        var response = service.Retrieve(id);
        
        // TODO: Remove this logic
        if  (response == null)
            return Results.NotFound();
        
        return Results.Ok(response);
    });

app.Run();