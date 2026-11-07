using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Book_Borrowing_System.Domain
{
    public class User
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public List<string> BorrowedIsbns { get; } = new();

        public User(string name)
        {
            Name = name;
        }
    }

}
