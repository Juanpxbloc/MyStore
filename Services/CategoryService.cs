using MyStore.Entities;
using MyStore.Models;
using MyStore.Repositories;

namespace MyStore.Services
{
    public class CategoryService(GenericRepository<Category> _categoryRepository) // We are sending category service to the repository
    {
        public async Task<IEnumerable<CategoryVM>> GetAllAsync() // Method to get all categories
        {
            var categories = await _categoryRepository.GetAllAsync(); // We call the generic repository to get all categories

            var CategoryVM = categories.Select(item =>
            new CategoryVM
            {
                CategoryId = item.CategoryId, // construct CategoryVM objects
                Name = item.Name
            }
            ).ToList();

            return CategoryVM;
        }

        public async Task AddAsync(CategoryVM viewModel) // Method to add a new category
        {
            var entity = new Category
            {
                Name = viewModel.Name
            };
            await _categoryRepository.AddAsync(entity); // Call the generic repository to add the new category
        }

        public async Task<CategoryVM?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            var CategoryVM = new CategoryVM();

            if (category != null)
            {
                CategoryVM.Name = category.Name;
                CategoryVM.CategoryId = category.CategoryId;
            }
            return CategoryVM;
        }

        public async Task EditAsync(CategoryVM viewModel)
        {
            var entity = new Category
            {
                CategoryId = viewModel.CategoryId,
                Name = viewModel.Name
            };
            await _categoryRepository.EditAsync(entity);

        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return;
            }
            await _categoryRepository.DeleteAsync(category);
        }

    }
}
