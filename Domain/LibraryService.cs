using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Book_Borrowing_System.Domain
{
    public class LibraryService
    {
        private readonly ILibraryRepository _repo;

        public LibraryService(ILibraryRepository repo)
        {
            _repo = repo;
        }

        public void BorrowBook(Guid userId, string isbn)
        {
            var user = _repo.GetUser(userId);
            var book = _repo.GetBook(isbn);

            book.Borrow();
            user.BorrowedIsbns.Add(isbn);

            _repo.SaveBook(book);
            _repo.SaveUser(user);
        }

        public void ReturnBook(Guid userId, string isbn)
        {
            var user = _repo.GetUser(userId);
            var book = _repo.GetBook(isbn);

            if (!user.BorrowedIsbns.Contains(isbn))
                throw new InvalidOperationException("User did not borrow this book");

            book.Return();
            user.BorrowedIsbns.Remove(isbn);

            _repo.SaveBook(book);
            _repo.SaveUser(user);
        }
    }

}
