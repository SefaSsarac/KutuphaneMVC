namespace KutuphaneMVC.Models
{
    public class BookViewModel
    {
        // Collection of books for the view
        public IEnumerable<Book> Books { get; set; }

        // Single book object for details or editing
        public Book Book { get; set; }
    }
}
