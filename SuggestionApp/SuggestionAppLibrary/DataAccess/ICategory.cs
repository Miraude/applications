
namespace SuggestionAppLibrary.DataAccess;

public interface ICategory
{
   Task CreateCategory(Category category);
   Task<List<Category>> GetAllCategories();
   Task UpdateCategory(Category category);
}