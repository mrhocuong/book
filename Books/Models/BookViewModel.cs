using System;

namespace Books.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string PublishedOn { get; set; }
        public string AuthorName { get; set; }
    }
}