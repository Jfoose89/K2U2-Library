using K2U2Library.Models;
using System.Linq;

namespace K2U2Library
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("K2U2 Library Management System");
                Console.WriteLine("------------------------------");
                Console.WriteLine("1. Register New book");
                Console.WriteLine("2. Register new member");
                Console.WriteLine("3. Register loan");
                Console.WriteLine("4. Register return");
                Console.WriteLine("5. Show active loans");
                Console.WriteLine("6. Search books");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterBook();
                        break;
                    case "2":
                        RegisterMember();
                        break;
                    case "3":
                        RegisterLoan();
                        break;
                    case "4":
                        RegisterReturn();
                        break;
                    case "5":
                        ShowActiveLoans();
                        break;
                    case "6":
                        SearchBooks();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // Menu Methods
        static void RegisterBook()
        {
            Console.Clear();
            Console.WriteLine("Register New Book\n");

            // ISBN
            string isbn;
            while (true)
            {
                Console.Write("ISBN (10 or 13 digits): ");
                isbn = Console.ReadLine()?.Trim() ?? "";

                if (!isbn.All(char.IsDigit))
                {
                    Console.WriteLine("ISBN must contain only digits.");
                    continue;
                }

                if (isbn.Length != 10 && isbn.Length != 13)
                {
                    Console.WriteLine("ISBN must be exactly 10 or 13 digits.");
                    continue;
                }

                break; // valid ISBN
            }

            // Title
            string title;
            while (true)
            {
                Console.Write("Title: ");
                title = Console.ReadLine()?.Trim() ?? "";

                if (!string.IsNullOrWhiteSpace(title))
                    break;

                Console.WriteLine("Title cannot be empty. Please try again.");
            }

            // Author
            string author;
            while (true)
            {
                Console.Write("Author: ");
                author = Console.ReadLine()?.Trim() ?? "";

                if (!string.IsNullOrWhiteSpace(author))
                    break;

                Console.WriteLine("Author cannot be empty. Please try again.");
            }

            // Published Year
            int publishedYear;
            while (true)
            {
                Console.Write("Published Year: ");
                if (int.TryParse(Console.ReadLine(), out publishedYear))
                    break;

                Console.WriteLine("Invalid year. Please enter a number.");
            }

            // Total Copies
            int totalCopies;
            while (true)
            {
                Console.Write("Total Copies: ");
                if (int.TryParse(Console.ReadLine(), out totalCopies))
                    break;

                Console.WriteLine("Invalid number of copies. Please enter a number.");
            }

            try
            {
                using var context = new K2U2LibraryContext();
                // Check if ISBN already exists
                var existingBook = context.Books.FirstOrDefault(b => b.Isbn == isbn);
                if (existingBook != null)
                {
                    Console.WriteLine("A book with this ISBN already exists.");
                    Pause();
                    return;
                }

                var book = new Book
                {
                    Isbn = isbn,
                    Title = title,
                    Author = author,
                    PublishedYear = publishedYear,
                    CopiesTotal = totalCopies,
                    CopiesAvailable = totalCopies
                };

                // Confirmation step
                Console.WriteLine("\nPlease confirm book details:");
                Console.WriteLine($"ISBN: {book.Isbn}");
                Console.WriteLine($"Title: {book.Title}");
                Console.WriteLine($"Author: {book.Author}");
                Console.WriteLine($"Published Year: {book.PublishedYear}");
                Console.WriteLine($"Total Copies: {book.CopiesTotal}");
                Console.Write("\nSave this book? (Y/N): ");

                string? confirm = Console.ReadLine()?.Trim().ToUpper();

                if (confirm != "Y")
                {
                    Console.WriteLine("Book registration cancelled.");
                    Pause();
                    return;
                }

                context.Books.Add(book);
                context.SaveChanges();

                Console.WriteLine("Book registered successfully.");
            }
                catch (Exception ex)
                {
                    Console.WriteLine("Error registering book:");
                    Console.WriteLine(ex.Message);
                }
                Pause();
            }

        static void RegisterMember()
        {
            Console.Clear();

            // First Name
            string firstName;
            while (true)
            {
                Console.Write("First Name: ");
                firstName = Console.ReadLine()?.Trim() ?? "";
                
                if (!string.IsNullOrWhiteSpace(firstName))
                {
                    break;
                }

                Console.WriteLine("First Name cannot be empty. Please try again.");
            }
            // Last Name
            string lastName;
            while (true)
            {
                Console.Write("Last Name: ");
                lastName = Console.ReadLine()?.Trim() ?? "";
                
                if (!string.IsNullOrWhiteSpace(lastName))
                    break;
                Console.WriteLine("Last Name cannot be empty. Please try again.");
            }

            // Email (with validation loop)
            string email;
            while (true)
            {
                Console.Write("Email: ");
                email = Console.ReadLine()?.Trim() ?? "";
                if (!string.IsNullOrWhiteSpace(email) &&
                    email.Contains("@") &&
                    email.Contains("."))
                    break;

                Console.WriteLine("Invalid email format. Please try again.");
            }

            // Phone
            string phone;
            while (true)
            {
                Console.Write("Phone: ");
                phone = Console.ReadLine()?.Trim() ?? "";

                if (!string.IsNullOrWhiteSpace(phone))
                    break;

                Console.WriteLine("Phone number cannot be empty.");
            }

            try
            {
                using var context = new K2U2LibraryContext();

                var member = new Member
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = phone
                };

                // Confirmation step
                Console.WriteLine("\nPlease confirm member details:");
                Console.WriteLine($"Name: {member.FirstName} {member.LastName}");
                Console.WriteLine($"Email: {member.Email}");
                Console.WriteLine($"Phone: {member.Phone}");
                Console.Write("\nSave this member? (Y/N): ");

                string? confirm = Console.ReadLine()?.Trim().ToUpper();

                if (confirm != "Y")
                {
                    Console.WriteLine("Member registration cancelled.");
                    Pause();
                    return;
                }

                context.Members.Add(member);
                context.SaveChanges();

                Console.WriteLine("Member registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error registering member:");
                Console.WriteLine(ex.Message);
            }
            Pause();
        }

        static void RegisterLoan()
        {
            Console.Clear();
            Console.WriteLine("Register Loan\n");

            int memberID;
            while (true)
            {
                Console.Write("Member ID: ");
                if (int.TryParse(Console.ReadLine(), out memberID))
                    break;

                Console.WriteLine("Invalid Member ID: ");
            }

            int bookId;
            while (true)
            {
                Console.Write("Book ID: ");
                if (int.TryParse(Console.ReadLine(), out bookId))
                    break;

                Console.WriteLine("Invalid Book ID: ");
            }

            try
            {
                using var context = new K2U2LibraryContext();

                var member = context.Members.FirstOrDefault(m => m.MemberId == memberID);
                if (member == null)
                {
                    Console.WriteLine("Member not found.");
                    Pause();
                    return;
                }

                var book = context.Books.FirstOrDefault(b => b.BookId == bookId);
                if (book == null)
                {
                    Console.WriteLine("Book not found.");
                    Pause();
                    return;
                }

                if (book.CopiesAvailable <= 0)
                {
                    Console.WriteLine("No copies available for this book.");
                    Pause();
                    return;
                }

                Console.WriteLine("\nConfirm Loan:");
                Console.WriteLine($"Member: {member.FirstName} {member.LastName}");
                Console.WriteLine($"Book: {book.Title}");
                Console.Write("Proceed? (Y/N): ");

                if (Console.ReadLine()?.Trim().ToUpper() != "Y")
                {
                    Console.WriteLine("Loan cancelled.");
                    Pause();
                    return;
                }

                var loan = new Loan
                {
                    MemberId = memberID,
                    BookId = bookId,
                    LoanDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(14)
                };

                context.Loans.Add(loan);
                context.SaveChanges();

                Console.WriteLine("Loan registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error registering loan:");
                Console.WriteLine(ex.Message);
            }

            Pause();
        }

        static void RegisterReturn()
        {
            Console.Clear();
            Console.WriteLine("Register Return\n");

            int loanID;
            while (true)
            {
                Console.Write("Loan ID: ");
                if (int.TryParse(Console.ReadLine(), out loanID))
                    break;

                Console.WriteLine("Invalid Loan ID: ");
            }

            try
            {
                using var context = new K2U2LibraryContext();

                var loan = context.Loans
                    .FirstOrDefault(l => l.LoanId == loanID && l.ReturnDate == null);

                if (loan == null)
                {
                    Console.WriteLine("Active loan not found.");
                    Pause();
                    return;
                }

                Console.WriteLine("\nConfirm Return:");
                Console.WriteLine($"Loan ID: {loan.LoanId}");
                Console.WriteLine($"Loan Date: {loan.LoanDate:yyyy-MM-dd}");
                Console.Write("Proceed? (Y/N): ");

                if (Console.ReadLine()?.Trim().ToUpper() != "Y")
                {
                    Console.WriteLine("Return cancelled.");
                    Pause();
                    return;
                }

                loan.ReturnDate = DateTime.Now;
                context.SaveChanges();

                Console.WriteLine("Book returned successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error registering return:");
                Console.WriteLine(ex.Message);
            }

            Pause();
        }

        static void ShowActiveLoans()
        {
            Console.Clear();
            Console.WriteLine("ActiveLoans:\n");

            try
            {
                using var context = new K2U2LibraryContext();

                // Query the view
                var loans = context.VwActiveLoans
                    .OrderBy(l => l.LoanDate)
                    .ToList();

                if (loans.Count == 0)
                {
                    Console.WriteLine("No active loans found.");
                }
                else
                {
                    Console.WriteLine("{0,-5} {1, -30} {2,-25} {3,-15} {4,-10}", "ID", "Book Title", "Member Name", "Loan Date", "Due Date");

                    foreach (var loan in loans)
                    {
                        Console.WriteLine("{0,-5} {1, -30} {2,-25} {3,-15:yyyy-MM-dd} {4,-10:yyyy-MM-dd}",
                            loan.LoanId,
                            loan.BookTitle,
                            $"{loan.FirstName} {loan.LastName}", // concatenate,
                            loan.LoanDate,
                            loan.DueDate);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching active loans:");
                Console.WriteLine(ex.Message);
            }
            Pause();
        }

        static void SearchBooks()
        {
            Console.Clear();
            Console.WriteLine("Search Books\n");

            using var context = new K2U2LibraryContext();

            while (true)
            {
                Console.Write("Enter Title, Author, or ISBN to search (or leave empty to go back): ");
                string query = Console.ReadLine()?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(query) || query == "0")
                {
                    Console.WriteLine("Returning to main menu.");
                    Pause();
                    return;
                }

                // Search in Books table (case-insensitive)
                var results = context.Books
                    .Where(b => b.Title.Contains(query) ||
                                b.Author.Contains(query) ||
                                b.Isbn.Contains(query))
                    .OrderBy(b => b.Title)
                    .ToList();

                if (results.Count == 0)
                {
                    Console.WriteLine("No books found matching your query.\n");
                }
                else
                {
                    Console.WriteLine("\nSearch Results:");
                    Console.WriteLine("{0,-5} {1,-30} {2,-25} {3,-15} {4,-5}", "ID", "Title", "Author", "ISBN", "Available");

                    foreach (var book in results)
                    {
                        Console.WriteLine("{0,-5} {1,-30} {2,-25} {3,-15} {4,-5}",
                            book.BookId,
                            book.Title,
                            book.Author,
                            book.Isbn,
                            book.CopiesAvailable);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
