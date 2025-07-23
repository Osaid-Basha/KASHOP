using KASHOP.DAL.Model;

namespace KASHOP.DAL.Repositortrs
{
    public interface ICategoryRepository
    {
        int Add(Category category);
        IEnumerable<Category> GetAll(bool withTraking=false);
        Category? GetById(int id);
        int Remove(Category category);
        int Update(Category category);
    }
}