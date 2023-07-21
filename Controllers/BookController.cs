using BookManagement.BLL.Services;
using BookManagement.Entity.ModelDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        IBookServices _bookServices;

        public BookController(IBookServices bookServices)
        {
            _bookServices = bookServices;
        }

        [HttpGet("getBooks")]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookServices.GetAll();
            
            return Ok(books);
        }

        [HttpGet("getBooks/{id}")]
        public async Task<IActionResult> GetById( Guid id)
        {
            var book = await _bookServices.GetById(id);

            return Ok(book);
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook(BookDTO book)
        {
            await _bookServices.Add(book);
            return Ok("Book added successfully !!");
        }

        [HttpPost("deleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            await _bookServices.Delete(id);
            return Ok("Book deleted successfully!!");
        }

        [HttpPost("updateBook/{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, BookDTO book)
        {
            await _bookServices.Update(id, book);
            return Ok("Book updated successfully!!");
        }

        [HttpGet("getBooksByRecords/{records}")]
        public async Task <IActionResult> GetBooksByRecords(int records)
        {
           var books= _bookServices.GetBooksByRecords(records);
            return Ok(books);
        }


        

    }
}
