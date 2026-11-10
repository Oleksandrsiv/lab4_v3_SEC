using FsCheck.Xunit;
using Library_Book_Borrowing_System.Domain;
using Library_Book_Borrowing_System.Tests.Arbitraries;
using FsCheck;
using FsCheck.Fluent;
using FluentAssertions;

namespace Library_Book_Borrowing_System.Tests.Properties
{
    public class UserProperties
    {
        // P1 Туди й назад
        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public Property Service_Borrow_then_Return_leaves_user_list_unchanged(User user, Book book)
        {
            // Arrange
            bool hasAvailableCopies = book.AvailableCopies > 0;
            bool ok = true;

            if (hasAvailableCopies)
            {
                var repo = new InMemoryLibraryRepository();
                repo.SaveUser(user);
                repo.SaveBook(book);
                var service = new LibraryService(repo);

                var initialBorrowedIsbns = user.BorrowedIsbns.ToList();

                // Act
                service.BorrowBook(user.Id, book.Isbn);
                service.ReturnBook(user.Id, book.Isbn);

                // Assert
                ok = user.BorrowedIsbns.SequenceEqual(initialBorrowedIsbns);
            }
            return ok.When(hasAvailableCopies);
        }

        // P7 Легко перевірити
        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void Service_Borrow_adds_isbn_to_user_history(User user, Book book)
        {
            // Arrange
            var repo = new InMemoryLibraryRepository();
            repo.SaveUser(user);
            repo.SaveBook(book);
            var service = new LibraryService(repo);

            // Act
            service.BorrowBook(user.Id, book.Isbn);

            // Assert
            user.BorrowedIsbns.Should().HaveCount(1);
            user.BorrowedIsbns.Should().Contain(book.Isbn);
        }

        // P5 Оракул
        [Property(Arbitrary = new[] { typeof(LibraryArbitraries) })]
        public void Direct_Book_Borrow_equals_Service_BorrowBook(User user, Book book)
        {
            // Arrange
            var oracleBook = new Book(book.Isbn, book.Title, book.TotalCopies);
            var serviceBook = new Book(book.Isbn, book.Title, book.TotalCopies);

            var repo = new InMemoryLibraryRepository();
            repo.SaveUser(user);
            repo.SaveBook(serviceBook); // Сервіс буде працювати зі своєю копією
            var service = new LibraryService(repo);

            // Act
            oracleBook.Borrow();

            service.BorrowBook(user.Id, serviceBook.Isbn);

            // Assert
            serviceBook.AvailableCopies.Should().Be(oracleBook.AvailableCopies);
        }
    }

}
