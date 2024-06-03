using LibraryManagement.Models.CreateDTOs;
using LibraryManagement.Repository.BaseRepository;
using LibraryManagement.Models;
using AutoMapper;

namespace LibraryManagement.Services
{
    public class CategoryService: ICategoryService<CategoryDTO, CategoryCreateDTO>
    {
        private readonly IBaseRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IBaseRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void DeleteAsync(int Id)
        {
            _repository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<CategoryDTO>>(entities);
            return dtos;
        }

        public async Task<CategoryDTO> GetByIdAsync(int Id)
        {
            var entity = await _repository.GetByIdAsync(Id);
            var dto = _mapper.Map<CategoryDTO>(entity);
            return dto;

        }

        public void CreateAsync(CategoryCreateDTO entityDTO)
        {
            var entity = _mapper.Map<Category>(entityDTO);
            _repository.CreateAsync(entity);
        }

        public void UpdateAsync(int id, CategoryDTO entityDTO)
        {
            var entity = _mapper.Map<Category>(entityDTO);
            _repository.UpdateAsync(entity);
        }
    }
}

