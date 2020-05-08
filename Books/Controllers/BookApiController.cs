using System.Linq;
using Books.Data;
using Books.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.Controllers
{
    [ApiController]
    [Route("books")]
    public class BookApiController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        // GET
        public BookApiController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult GetAll(int page = 1, int pageSize = 25)
        {
            var query = _dbContext
                .BookEntities
                .Include(x => x.AuthorEntity)
                .Skip((page - 1) * pageSize).Take(pageSize);
            var data = query.Select(x => new BookViewModel
            {
                BookId = x.Id,
                Title = x.Title,
                PublishedOn = x.PublishedOn.ToLongDateString(),
                AuthorName = $"{x.AuthorEntity.FirstName} {x.AuthorEntity.LastName}"
            });
            return Ok(data);
        }
    }
}