using LibraryManagement.Models.CreateDTOs;
using LibraryManagement.Models;
using LibraryManagement.Repository.BaseRepository;
using AutoMapper;

namespace LibraryManagement.Services
{
    public class BookBorrowingRequestDetailsService: IBookBorrowingRequestDetailsService<BookBorrowingRequestDetailsDTO, BookBorrowingRequestDetailsCreateDTO>
    {
        private readonly IBaseRepository<BookBorrowingRequestDetails> _repository;
        private readonly IMapper _mapper;

        public BookBorrowingRequestDetailsService(IBaseRepository<BookBorrowingRequestDetails> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void DeleteAsync(int Id)
        {
            _repository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<BookBorrowingRequestDetails>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            //var dtos = _mapper.Map<IEnumerable<BookBorrowingRequestDetailsDTO>>(entities);
            return entities;
        }

        public async Task<BookBorrowingRequestDetailsDTO> GetByIdAsync(int Id)
        {
            var entity = await _repository.GetByIdAsync(Id);
            var dto = _mapper.Map<BookBorrowingRequestDetailsDTO>(entity);
            return dto;

        }

        public async Task<BookBorrowingRequestDetails> CreateAsync(BookBorrowingRequestDetailsCreateDTO entityDTO)
        {
            var entity = _mapper.Map<BookBorrowingRequestDetails>(entityDTO);
            _repository.CreateAsync(entity);
            return entity;
        }

        public void UpdateAsync(int id, BookBorrowingRequestDetailsCreateDTO entityDTO)
        {
            var entity = _mapper.Map<BookBorrowingRequestDetails>(entityDTO);
            entity.Id = id;
            _repository.UpdateAsync(entity);
        }
    }
}

