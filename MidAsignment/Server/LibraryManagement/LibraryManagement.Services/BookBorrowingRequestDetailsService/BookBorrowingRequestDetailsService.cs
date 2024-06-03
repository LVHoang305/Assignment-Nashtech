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

        public async Task<IEnumerable<BookBorrowingRequestDetailsDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<BookBorrowingRequestDetailsDTO>>(entities);
            return dtos;
        }

        public async Task<BookBorrowingRequestDetailsDTO> GetByIdAsync(int Id)
        {
            var entity = await _repository.GetByIdAsync(Id);
            var dto = _mapper.Map<BookBorrowingRequestDetailsDTO>(entity);
            return dto;

        }

        public void CreateAsync(BookBorrowingRequestDetailsCreateDTO entityDTO)
        {
            var entity = _mapper.Map<BookBorrowingRequestDetails>(entityDTO);
            _repository.CreateAsync(entity);
        }

        public void UpdateAsync(int id, BookBorrowingRequestDetailsDTO entityDTO)
        {
            var entity = _mapper.Map<BookBorrowingRequestDetails>(entityDTO);
            _repository.UpdateAsync(entity);
        }
    }
}

