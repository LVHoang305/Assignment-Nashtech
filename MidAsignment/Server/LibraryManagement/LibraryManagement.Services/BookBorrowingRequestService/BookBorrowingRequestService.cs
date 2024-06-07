using AutoMapper;
using Azure.Core;
using LibraryManagement.Models;
using LibraryManagement.Models.CreateDTOs;
using LibraryManagement.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<BookBorrowingRequest>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            //var dtos = _mapper.Map<IEnumerable<BookBorrowingRequestDTO>>(entities);
            return entities;
        }

        public async Task<IEnumerable<BookBorrowingRequest>> GetByFieldAsync(string json)
        {
            var entities = await _repository.GetByFieldsAsync(json);
            //var dtos = _mapper.Map<IEnumerable<BookBorrowingRequestDTO>>(entities);
            return entities;
        }

        public async Task<BookBorrowingRequestDTO> GetByIdAsync(int Id)
        {
            var entity = await _repository.GetByIdAsync(Id);
            var dto = _mapper.Map<BookBorrowingRequestDTO>(entity);
            return dto;

        }

        public async Task<List<BookBorrowingRequestDTO>> GetRequestsForCurrentMonthAsync(int userId)
    {
        var currentYear = DateTime.Now.Year;
        var currentMonth = DateTime.Now.Month;

        var requests = await _repository.GetAllAsync();
        var filteredRequests = requests
            .Where(r => r.DateRequested.Year == currentYear && r.DateRequested.Month == currentMonth && r.RequestorId == userId)
            .ToList();

        var dtoList = _mapper.Map<List<BookBorrowingRequestDTO>>(filteredRequests);
        return dtoList;
    }

        public async Task<BookBorrowingRequest> CreateAsync(BookBorrowingRequestCreateDTO entityDTO)
        {
            var requestInThisMonth = GetRequestsForCurrentMonthAsync(entityDTO.RequestorId);
            var entity = _mapper.Map<BookBorrowingRequest>(entityDTO);
            if (requestInThisMonth.Result.Count < 3)
            {             
                _repository.CreateAsync(entity);
            }
            return entity;
        }

        public void UpdateAsync(int id, BookBorrowingRequestCreateDTO entityDTO)
        {
            var entity = _mapper.Map<BookBorrowingRequest>(entityDTO);
            entity.Id = id;
            _repository.UpdateAsync(entity);
        }
    }
}

