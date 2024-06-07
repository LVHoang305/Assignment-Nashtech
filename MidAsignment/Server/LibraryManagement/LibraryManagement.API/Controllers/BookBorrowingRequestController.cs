using LibraryManagement.Services;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Models.CreateDTOs;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookBorrowingRequestController : ControllerBase
    {
        private readonly IBookBorrowingRequestService<BookBorrowingRequestDTO, BookBorrowingRequestCreateDTO> _service;

        public BookBorrowingRequestController(IBookBorrowingRequestService<BookBorrowingRequestDTO, BookBorrowingRequestCreateDTO> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var dtos = await _service.GetAllAsync();
            return Ok(dtos);
        }

        [HttpGet("ByField")]
        public async Task<IActionResult> GetByFieldAsync(string json)
        {
            var dtos = await _service.GetByFieldAsync(json);
            return Ok(dtos);
        }

        [HttpGet("InThisMonth/{id}")]
        public async Task<IActionResult> GetRequestThisMonthAsync(int id)
        {
            var dtos = await _service.GetRequestsForCurrentMonthAsync(id);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            return Ok(dto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(BookBorrowingRequestCreateDTO dto)
        {
            var newBookBorrowingRequest = _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = newBookBorrowingRequest.Id }, newBookBorrowingRequest);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "SuperUserPolicy")]
        public async Task<IActionResult> Update(int id, BookBorrowingRequestCreateDTO dto)
        {
            _service.UpdateAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "SuperUserPolicy")]
        public async Task<IActionResult> Delete(int id)
        {
            _service.DeleteAsync(id);
            return Ok();
        }
    }
}

