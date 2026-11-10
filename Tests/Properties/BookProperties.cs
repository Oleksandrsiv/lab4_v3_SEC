using FsCheck.Xunit;
using Library_Book_Borrowing_System.Domain;
using Library_Book_Borrowing_System.Tests.Arbitraries;
using FsCheck;
using FsCheck.Fluent;
using FluentAssertions;

namespace Library_Book_Borrowing_System.Tests.Properties
{
    public class BookProperties
    {
        // P1 Туди й назад
        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public Property Borrow_then_Return_restores_available_copies(Book book)
        {
            // Arrange
            bool hasAvailableCopies = book.AvailableCopies > 0;
            bool ok = true;

            if (hasAvailableCopies)
            {
                int before = book.AvailableCopies;
                
                // Act
                book.Borrow();
                book.Return();
                
                // Assert
                ok = book.AvailableCopies == before;
            }

            return ok.When(hasAvailableCopies);
        }

        // P8 Без винятків
        [Property]
        public void Creating_valid_book_does_not_throw(NonEmptyString isbn, NonEmptyString title, PositiveInt copies)
        {
            // Arrange (дані генеруються автоматично через параметри)

            // Act
            Action act = () => new Book(isbn.Get, title.Get, copies.Get);

            // Assert
            act.Should().NotThrow();
        }
    }

}
