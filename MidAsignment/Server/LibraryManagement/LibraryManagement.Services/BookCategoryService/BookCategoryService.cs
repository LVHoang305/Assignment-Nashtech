using AutoMapper;
using LibraryManagement.Models;
using LibraryManagement.Models.CreateDTOs;
using LibraryManagement.Repository.BaseRepository;

namespace LibraryManagement.Services
{
    public class BookCategoryService: IBookCategoryService<BookCategoryDTO, BookCategoryCreateDTO>
    {
        private readonly IBaseRepository<BookCategory> _repository;
        private readonly IMapper _mapper;

        public BookCategoryService(IBaseRepository<BookCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void DeleteAsync(int Id)
        {
            _repository.DeleteAsync(Id);
        }

        public void DeleteByFieldAsync(string fieldName, string fieldValue)
        {
            _repository.DeleteByFieldAsync(fieldName, fieldValue);
        }

        public async Task<IEnumerable<BookCategory>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            //var dtos = _mapper.Map<IEnumerable<BookCategoryDTO>>(entities);
            return entities;
        }

        public async Task<BookCategoryDTO> GetByIdAsync(int Id)
        {
            var entity = await _repository.GetByIdAsync(Id);
            var dto = _mapper.Map<BookCategoryDTO>(entity);
            return dto;

        }

        public async Task<BookCategory> CreateAsync(BookCategoryCreateDTO entityDTO)
        {
            var entity = _mapper.Map<BookCategory>(entityDTO);
            _repository.CreateAsync(entity);
            return entity;
        }

        public void UpdateAsync(int id, BookCategoryCreateDTO entityDTO)
        {
            var entity = _mapper.Map<BookCategory>(entityDTO);
            _repository.UpdateAsync(entity);
        }
    }
}

