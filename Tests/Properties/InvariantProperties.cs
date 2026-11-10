using FsCheck.Xunit;
using Library_Book_Borrowing_System.Domain;
using Library_Book_Borrowing_System.Tests.Arbitraries;
using FsCheck;
using FluentAssertions;

namespace Library_Book_Borrowing_System.Tests.Properties
{
    public class InvariantProperties
    {
        // P2 Інваріант
        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void AvailableCopies_never_below_zero_and_never_exceeds_TotalCopies(Book book)
        {
            // Arrange
            int initialAvailable = book.AvailableCopies;

            // Act  
            for (int i = 0; i < initialAvailable; i++)
            {
                book.Borrow();

                // Assert
                book.AvailableCopies.Should().BeGreaterThanOrEqualTo(0);
                book.AvailableCopies.Should().BeLessThanOrEqualTo(book.TotalCopies);
            }
        }

        // P2 Інваріант
        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void LibraryService_total_books_in_system_is_constant(User user, Book book)
        {
            // Arrange
            var repo = new InMemoryLibraryRepository();
            repo.SaveUser(user);
            repo.SaveBook(book);
            var service = new LibraryService(repo);

            int initialAvailable = book.AvailableCopies;
            string targetIsbn = book.Isbn;

            // Act
            for (int i = 0; i < initialAvailable; i++)
            {
                service.BorrowBook(user.Id, targetIsbn);

                int userCopies = user.BorrowedIsbns.Count(isbn => isbn == targetIsbn);

                // Assert
                (book.AvailableCopies + userCopies).Should().Be(book.TotalCopies);
            }
        }
    
    }

}
