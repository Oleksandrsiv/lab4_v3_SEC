using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Book_Borrowing_System.Domain
{
    public class InMemoryLibraryRepository : ILibraryRepository
    {
        private readonly Dictionary<string, Book> _books = new();
        private readonly Dictionary<Guid, User> _users = new();

        public Book GetBook(string isbn)
            => _books[isbn];

        public User GetUser(Guid id)
            => _users[id];

        public void SaveBook(Book book)
            => _books[book.Isbn] = book;

        public void SaveUser(User user)
            => _users[user.Id] = user;

        public void AddBook(Book book)
            => _books[book.Isbn] = book;

        public void AddUser(User user)
            => _users[user.Id] = user;
    }

}
