using FsCheck;
using FsCheck.Fluent;
using Library_Book_Borrowing_System.Domain;

namespace Library_Book_Borrowing_System.Tests.Arbitraries
{
    // Зразки з проєкту лекції: FuelTankGen (залежні значення), VehicleGen (генератор з генератора).
    public static class LibraryArbitraries
    {
        // ISBN: рядок з 10 або 13 цифр (Gen.OneOf двох генераторів).
        public static Arbitrary<string> Isbn()
        {
            throw new NotImplementedException();
        }

        // Назва книги: Gen.Elements(...) з кількох назв.
        private static Gen<string> Title()
        {
            throw new NotImplementedException();
        }

        // Book: використайте Isbn().Generator і Title(); кількість копій >= 1
        // (конструктор кидає ArgumentException для 0). Включіть граничний випадок 1.
        public static Arbitrary<Book> Book()
        {
            throw new NotImplementedException();
        }

        // User: Id генерується автоматично (Guid), від генератора потрібне лише ім'я.
        public static Arbitrary<User> User()
        {
            throw new NotImplementedException();
        }
    }
}
