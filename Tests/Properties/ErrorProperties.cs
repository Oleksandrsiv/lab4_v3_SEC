using FsCheck.Xunit;
using Library_Book_Borrowing_System.Domain;
using Library_Book_Borrowing_System.Tests.Arbitraries;
using FsCheck;
using FluentAssertions;


namespace Library_Book_Borrowing_System.Tests.Properties
{
    public class ErrorProperties
    {
        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void Borrow_when_no_copies_available_throws_InvalidOperationException(Book book)
        {
            // Arrange
            while (book.AvailableCopies > 0)
            {
                book.Borrow();
            }

            // Act
            Action act = () => book.Borrow();

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("No copies available");
        }

        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void Return_when_all_copies_returned_throws_InvalidOperationException(Book book)
        {
            // Arrange
            // Згенерована книга одразу має AvailableCopies == TotalCopies, 
            // тому додаткової підготовки не потрібно.

            // Act
            Action act = () => book.Return();

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("All copies already returned");
        }

        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void ReturnBook_for_unborrowed_book_throws_InvalidOperationException(User user, Book book)
        {
            // Arrange
            var repo = new InMemoryLibraryRepository();
            repo.SaveUser(user);
            repo.SaveBook(book);
            
            var service = new LibraryService(repo);

            // Act
            Action act = () => service.ReturnBook(user.Id, book.Isbn);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("User did not borrow this book");
        }

        
    }

}
