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
            var gen10 = Gen.Choose(0, int.MaxValue).Select(x => x.ToString("D10"));
            var gen13 = Gen.Choose(0, int.MaxValue).Select(x => x.ToString("D13"));
            
            return Arb.From(Gen.OneOf(gen10, gen13));
        }

        // Назва книги: Gen.Elements(...) з кількох назв.
        private static Gen<string> Title()
        {
            return Gen.Elements(
                "Clean Architecture",
                "Кобзар",
                "Полліанна",
                "Патерни проєктування",
                "CLR via C#"
            );
        }

        // Book: використайте Isbn().Generator і Title(); кількість копій >= 1
        // (конструктор кидає ArgumentException для 0). Включіть граничний випадок 1.
        public static Arbitrary<Book> Book()
        {
            var gen = from isbn in Isbn().Generator
                      from title in Title()
                      from copies in Gen.Choose(1, 50) 
                      select new Book(isbn, title, copies);

            return Arb.From(gen);
        }

        // User: Id генерується автоматично (Guid), від генератора потрібне лише ім'я.
        public static Arbitrary<User> User()
        {
            var gen = from id in Gen.Choose(1, 10000)
              select new User($"User-{id}");

            return Arb.From(gen);
        }
    }
}
