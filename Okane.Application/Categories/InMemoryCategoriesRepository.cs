using Okane.Application.Expenses;
using Okane.Domain;

namespace Okane.Application.Categories;

public class InMemoryCategoriesRepository : InMemoryRepository<Category>, ICategoriesRepository
{
    public Category? ByName(string name) => 
        Entities.FirstOrDefault(category => category.Name == name);

    public void Update(Category category)
    {
        Entities.Remove(category);
        Entities.Add(category);
    }
}