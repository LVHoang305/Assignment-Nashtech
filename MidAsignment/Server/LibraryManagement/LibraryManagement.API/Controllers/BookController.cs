using LibraryManagement.Services;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.Models.CreateDTOs;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController: ControllerBase
    {
        private readonly IBookService<BookDTO, BookCreateDTO> _service;

        public BookController(IBookService<BookDTO, BookCreateDTO> service)
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
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Policy = "SuperUserPolicy")]
        public async Task<IActionResult> Create(BookCreateDTO dto)
        {
            var newBook = _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "SuperUserPolicy")]
        public async Task<IActionResult> Update(int id, BookCreateDTO dto)
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

