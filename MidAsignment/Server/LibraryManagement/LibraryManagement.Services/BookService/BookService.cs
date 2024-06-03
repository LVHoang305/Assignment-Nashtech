using AutoMapper;
using LibraryManagement.Models;
using LibraryManagement.Models.CreateDTOs;
using LibraryManagement.Repository.BaseRepository;

namespace LibraryManagement.Services
{
    public class BookService: IBookService<BookDTO, BookCreateDTO>
    {
        private readonly IBaseRepository<Book> _repository;
        private readonly IMapper _mapper;

        public BookService(IBaseRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void DeleteAsync(int Id)
        {
            _repository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            //var dtos = _mapper.Map<IEnumerable<BookDTO>>(entities);
            return entities;
        }

        public async Task<BookDTO> GetByIdAsync(int Id)
        {
            var entity = await _repository.GetByIdAsync(Id);
            var dto = _mapper.Map<BookDTO>(entity);
            return dto;

        }

        public void CreateAsync(BookCreateDTO entityDTO)
        {
            var entity = _mapper.Map<Book>(entityDTO);
            _repository.CreateAsync(entity);
        }

        public void UpdateAsync(int id, BookDTO entityDTO)
        {
            var entity = _mapper.Map<Book>(entityDTO);
            _repository.UpdateAsync(entity);
        }

    }
}

