using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectChallengeDomain.IService;
using ProjectChallengeDomain.Models;
using ProjectChallengeDomain.Models.Requests;

namespace ProjectChallengeAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _serviceBook;
        public BookController(IBookService serviceBook)
        {
            _serviceBook = serviceBook;

        }
        /// <summary>
        /// Get all books
        /// </summary>
        /// <response code="200">List of all books</response>
        /// <response code="404">No books found</response>
        /// <response code="500">Error to get all books</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _serviceBook.Get();

            if (books == null || books.Count() == 0)
                return NotFound(new { message = "Books not found." });
            return Ok(books);
        }
        /// <summary>
        /// Get a book by Id
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <response code="200">The book from the id</response>
        /// <response code="404">No book found</response>
        /// <response code="500">Error to get the book</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var book = await _serviceBook.GetById(id);

            if (book == null)
                return NotFound(new { message = "Book not found." });

            return Ok(book);
        }

        /// <summary>
        /// Creates a book
        /// </summary>
        /// <param name="Book">Book data</param>
        /// <returns>Book successfully created</returns>
        /// <response code="200">Book object</response>
        /// <response code="400">Book invalid data</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        public IActionResult CreateBook(BookRequestPost Book)
        {
            var response = _serviceBook.Post(Book);
            return Ok(response);
        }
        /// <summary>
        /// Update a book
        /// </summary> 
        /// <param name="id">Book id to be updated</param>
        /// <param name="Book">Book data</param>
        /// <response code="200">Book successfully updated</response>
        /// <response code="400">Book invalid data</response>
        /// <response code="404">Book not found</response>
        /// <response code="500">Error to update book</response>
        [HttpPut]
        public async Task<IActionResult> UpdateBook(BookRequestPut Book)
        {
            var response =await _serviceBook.Put(Book);

            if (response == null)
                return NotFound(new { message = "Book not found." });

            return Ok(response);
        }
        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="id">Book id</param>
        /// <response code="200">Book successfully deleted</response>
        /// <response code="404">Book not found</response>
        /// <response code="500">Error to delete book</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var response = await _serviceBook.Delete(id);
            if (!response)
                return NotFound(new { message = "Book not found." });
            return Ok(true);
        }
    }
}
