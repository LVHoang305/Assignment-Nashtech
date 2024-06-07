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

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            //var dtos = _mapper.Map<IEnumerable<CategoryDTO>>(entities);
            return entities;
        }

        public async Task<CategoryDTO> GetByIdAsync(int Id)
        {
            var entity = await _repository.GetByIdAsync(Id);
            var dto = _mapper.Map<CategoryDTO>(entity);
            return dto;

        }

        public async Task<Category> CreateAsync(CategoryCreateDTO entityDTO)
        {
            var entity = _mapper.Map<Category>(entityDTO);
            _repository.CreateAsync(entity);
            return entity;
        }

        public void UpdateAsync(int id, CategoryCreateDTO entityDTO)
        {
            var entity = _mapper.Map<Category>(entityDTO);
            entity.Id = id;
            _repository.UpdateAsync(entity);
        }
    }
}

