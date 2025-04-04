namespace HomeworkEFpractic
{
    class Program
    {
        static void Main()
        {
            using (var db = new LibraryDbContext())
            {
                var userRepository = new UserRepository(db);
                var bookRepository = new BookRepository(db);

                var user = new User { Name = "пользователь 1", Email = "user@gmail.com" };
                userRepository.AddUser(user);

                var book = new Book { Title = "Доктор Живаго", Year = 1961 };
                bookRepository.AddBook(book);

                var author = new Author { Name = "George Orwell" };
                db.Authors.Add(author);
                db.SaveChanges();

                var genre = new Genre { Name = "Dystopia" };
                db.Genres.Add(genre);
                db.SaveChanges();

                var users = userRepository.GetAllUsers().ToList();
                foreach(var us in users)
                {
                    Console.WriteLine($"Читатель с Id:{us.Id}, имя {us.Name}, почта {us.Email}");
                    foreach (var b in us.Books)
                    {
                        Console.WriteLine($"Выданная книга: {b.Title}");
                    }
                }

                var books = bookRepository.GetAllBooks().ToList();
                foreach(var b  in books)
                {
                    Console.WriteLine($"Книга {b.Id}: \"{b.Title}\", год {b.Year}");
                    foreach (var u in b.Users)
                    {
                        Console.WriteLine($"Выдана читателю: {u.Name}");
                    }
                }

                // Обновление имени пользователя
                userRepository.UpdateUser(user.Id, "Jane Doe");

                // Обновление года выпуска книги
                bookRepository.UpdateBookYear(book.Id, 1950);

                // Пользователь берет книгу на руки
                userRepository.BorrowBook(user.Id, book.Id);

                // Пользователь возвращает книгу
                userRepository.ReturnBook(user.Id, book.Id);

                // Удаление пользователя
                userRepository.DeleteUser(user.Id);

                // Удаление книги
                bookRepository.DeleteBook(book.Id);
            }
        }
    }
}
