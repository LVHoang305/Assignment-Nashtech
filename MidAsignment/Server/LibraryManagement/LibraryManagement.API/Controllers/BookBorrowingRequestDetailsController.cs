using LibraryManagement.Services;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Models.CreateDTOs;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookBorrowingRequestDetailsController : ControllerBase
    {
        private readonly IBookBorrowingRequestDetailsService<BookBorrowingRequestDetailsDTO, BookBorrowingRequestDetailsCreateDTO> _service;

        public BookBorrowingRequestDetailsController(IBookBorrowingRequestDetailsService<BookBorrowingRequestDetailsDTO, BookBorrowingRequestDetailsCreateDTO> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var dtos = await _service.GetAllAsync();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookBorrowingRequestDetailsCreateDTO dto)
        {
            _service.CreateAsync(dto);
            return Ok("");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookBorrowingRequestDetailsDTO dto)
        {
            _service.UpdateAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _service.DeleteAsync(id);
            return Ok();
        }
    }
}

