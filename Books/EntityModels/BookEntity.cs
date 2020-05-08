using System;

namespace Books.EntityModels
{
    public class BookEntity
    {
        public int Id { get; set; }
        public string Title{ get; set; }
        public DateTime PublishedOn{ get; set; }
        public int AuthorId { get; set; }
        public AuthorEntity AuthorEntity { get; set; }
    }
}