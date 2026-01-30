using Okane.Domain;

namespace Okane.Application.Categories;

public interface ICategoriesRepository : IRepository<Category>
{
    public Category? ByName(string name);
    void Update(Category category);
}