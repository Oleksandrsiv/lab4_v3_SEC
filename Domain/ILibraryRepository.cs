using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Book_Borrowing_System.Domain
{
    public interface ILibraryRepository
    {
        Book GetBook(string isbn);
        User GetUser(Guid id);
        void SaveBook(Book book);
        void SaveUser(User user);
    }

}
