using KutuphaneMVC.Models;
using Microsoft.AspNetCore.Mvc;

public class BookController : Controller
{
    // Static list to hold books for demonstration purposes
    private static List<Book> _books = new List<Book>();

    // GET: Show the list of books
    public IActionResult List()
    {
        var model = new BookViewModel { Books = _books }; // Create view model with books
        return View(model); // Return the view with the model
    }

    // GET: Show details of a specific book
    public IActionResult Details(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id); // Find book by ID
        return View(book); // Return the view with the book's details
    }

    // GET: Show the create book form
    public IActionResult Create()
    {
        return View(new Book()); // Return an empty book model
    }

    // POST: Handle the creation of a new book
    [HttpPost]
    public IActionResult Create(Book book)
    {
        _books.Add(book); // Add the new book to the list
        return RedirectToAction("List"); // Redirect to the book list
    }

    // GET: Show the edit form for a specific book
    public IActionResult Edit(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id); // Find book by ID
        return View(book); // Return the view with the book's details
    }

    // POST: Handle the editing of an existing book
    [HttpPost]
    public IActionResult Edit(Book book)
    {
        var existingBook = _books.FirstOrDefault(b => b.Id == book.Id); // Find existing book by ID
        if (existingBook != null)
        {
            // Update the existing book's details
            existingBook.Title = book.Title;
            existingBook.AuthorId = book.AuthorId;
            existingBook.Genre = book.Genre;
            existingBook.PublishDate = book.PublishDate;
            existingBook.ISBN = book.ISBN;
            existingBook.CopiesAvailable = book.CopiesAvailable;
        }
        return RedirectToAction("List"); // Redirect to the book list
    }

    // GET: Show the delete confirmation for a specific book
    public IActionResult Delete(int id)
    {
        var book = _books.FirstOrDefault(a => a.Id == id); // Find book by ID

        // If the book is found, remove from the list
        if (book != null)
        {
            _books.Remove(book); // Book is removed from the list
        }

        // Redirect to the list after the book is deleted
        return RedirectToAction("List", "Book");
    }

    // POST: Confirm the deletion of a book
    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id); // Find book by ID
        if (book != null)
        {
            _books.Remove(book); // Remove the book from the list
            TempData["Message"] = "Kitap başarıyla silindi."; // The book has been successfully deleted.
        }

        return RedirectToAction("Book"); // Redirect to the book list
    }
}
