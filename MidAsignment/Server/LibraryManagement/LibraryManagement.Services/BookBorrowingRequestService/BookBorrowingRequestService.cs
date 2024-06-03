using AutoMapper;
using LibraryManagement.Models;
using LibraryManagement.Models.CreateDTOs;
using LibraryManagement.Repository.BaseRepository;

namespace LibraryManagement.Services
{
    public class BookBorrowingRequestService: IBookBorrowingRequestService<BookBorrowingRequestDTO, BookBorrowingRequestCreateDTO>
    {
        private readonly IBaseRepository<BookBorrowingRequest> _repository;
        private readonly IMapper _mapper;

        public BookBorrowingRequestService(IBaseRepository<BookBorrowingRequest> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void DeleteAsync(int Id)
        {
            _repository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<BookBorrowingRequestDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<BookBorrowingRequestDTO>>(entities);
            return dtos;
        }

        public async Task<BookBorrowingRequestDTO> GetByIdAsync(int Id)
        {
            var entity = await _repository.GetByIdAsync(Id);
            var dto = _mapper.Map<BookBorrowingRequestDTO>(entity);
            return dto;

        }

        public void CreateAsync(BookBorrowingRequestCreateDTO entityDTO)
        {
            var entity = _mapper.Map<BookBorrowingRequest>(entityDTO);
            _repository.CreateAsync(entity);
        }

        public void UpdateAsync(int id, BookBorrowingRequestDTO entityDTO)
        {
            var entity = _mapper.Map<BookBorrowingRequest>(entityDTO);
            _repository.UpdateAsync(entity);
        }
    }
}

