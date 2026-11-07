using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Book_Borrowing_System.Domain
{
    public class Book
    {
        public string Isbn { get; }
        public string Title { get; }
        public int TotalCopies { get; }
        public int AvailableCopies { get; private set; }

        public Book(string isbn, string title, int copies)
        {
            if (copies < 1)
                throw new ArgumentException("Copies must be > 0");

            Isbn = isbn;
            Title = title;
            TotalCopies = copies;
            AvailableCopies = copies;
        }

        public void Borrow()
        {
            if (AvailableCopies == 0)
                throw new InvalidOperationException("No copies available");

            AvailableCopies--;
        }

        public void Return()
        {
            if (AvailableCopies == TotalCopies)
                throw new InvalidOperationException("All copies already returned");

            AvailableCopies++;
        }
    }

}
