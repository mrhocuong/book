using System.Collections.Generic;

namespace Books.EntityModels
{
    public class AuthorEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<BookEntity> BookEntities { get; set; }
    }
}