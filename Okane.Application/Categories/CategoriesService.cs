using Okane.Application.Expenses;
using Okane.Domain;

namespace Okane.Application.Categories;

public class CategoriesService(ICategoriesRepository categories, IExpensesRepository expenses)
{
    public Result<CategoryResponse> Create(CreateCategoryRequest request)
    {
        if (categories.ByName(request.Name) != null)
            return new ErrorResult<CategoryResponse>(
                $"{nameof(Category)} with name {request.Name} already exists.");
        
        var category = new Category
        {
            Name = request.Name
        };
        categories.Add(category);
        
        return new OkResult<CategoryResponse>(new CategoryResponse(category.Id, category.Name));
    }

    public Result<CategoryResponse> Retrieve(int id)
    {
        var category = categories.ById(id);

        if (category == null)
            return new NotFoundResult<CategoryResponse>($"{nameof(Category)} with id {id} was not found.");
        
        return new OkResult<CategoryResponse>(new CategoryResponse(category.Id, category.Name));
    }

    public Result<IEnumerable<CategoryResponse>> All()
    {
        var response = categories
            .All()
            .Select(c => new CategoryResponse(c.Id, c.Name));
        
        return new OkResult<IEnumerable<CategoryResponse>>(response);
    }

    public Result Remove(int id)
    {
        var category = categories.ById(id);

        if (category == null)
            return new NotFoundResult($"{nameof(Category)} with id {id} was not found.");

        var categoryExpenses = expenses.ByCategoryName(category.Name);
        
        if (categoryExpenses.Any())
            return new ErrorResult("Can not delete category with existing expenses");
        
        categories.Remove(id);
        return new OkResult();
    }

    public Result<CategoryResponse> Update(UpdateCategoryRequest request)
    {
        var withName = categories.ByName(request.Name);
        if (withName != null)
            return  new ErrorResult<CategoryResponse>(
                $"{nameof(Category)} with name {request.Name} already exists.");
        
        var existing = categories.ById(request.Id);

        existing.Name = request.Name;

        categories.Update(existing);
        
        return  new OkResult<CategoryResponse>(new CategoryResponse(existing.Id, existing.Name));
    }
}