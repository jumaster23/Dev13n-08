namespace Okane.Application;

public interface ICategoriesRepository : IRepository<Category>
{
    Category? ByName(string name);
}