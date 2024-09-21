using KutuphaneMVC.Models;
using Microsoft.AspNetCore.Mvc;

public class AuthorController : Controller
{
    // Static list to hold authors for demonstration purposes
    private static List<Author> _authors = new List<Author>();

    // GET: Show the list of authors
    public IActionResult List()
    {
        var model = new AuthorViewModel { Authors = _authors }; // Create view model with authors
        return View(model); // Return the view with the model
    }

    // GET: Show details of a specific author
    public IActionResult Details(int id)
    {
        var author = _authors.FirstOrDefault(a => a.Id == id); // Find author by ID
        return View(author); // Return the view with the author's details
    }

    // GET: Show the create author form
    public IActionResult Create()
    {
        return View(new Author()); // Return an empty author model
    }

    // POST: Handle the creation of a new author
    [HttpPost]
    public IActionResult Create(Author author)
    {
        _authors.Add(author); // Add the new author to the list
        return RedirectToAction("List"); // Redirect to the author list
    }

    // GET: Show the edit form for a specific author
    public IActionResult Edit(int id)
    {
        var author = _authors.FirstOrDefault(a => a.Id == id); // Find author by ID
        return View(author); // Return the view with the author's details
    }

    // POST: Handle the editing of an existing author
    [HttpPost]
    public IActionResult Edit(Author author)
    {
        var existingAuthor = _authors.FirstOrDefault(a => a.Id == author.Id); // Find existing author by ID
        if (existingAuthor != null)
        {
            // Update the existing author's details
            existingAuthor.FirstName = author.FirstName;
            existingAuthor.LastName = author.LastName;
            existingAuthor.DateOfBirth = author.DateOfBirth;
        }
        return RedirectToAction("List"); // Redirect to the author list
    }

    // GET: Show the delete confirmation for a specific author
    public IActionResult Delete(int id)
    {
        // Find the author to be deleted from the author list
        var author = _authors.FirstOrDefault(a => a.Id == id);

        // If the author is found, remove from the list
        if (author != null)
        {
            _authors.Remove(author); // Author is removed from the list
        }

        // Redirect to the list after the author is deleted
        return RedirectToAction("List", "Author");
    }

    // POST: Confirm the deletion of an author
    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        var author = _authors.FirstOrDefault(a => a.Id == id); // Find author by ID
        if (author != null)
        {
            _authors.Remove(author); // Remove the author from the list
        }
        return RedirectToAction("List"); // Redirect to the author list
    }
}
