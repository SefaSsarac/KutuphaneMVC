namespace KutuphaneMVC.Models
{
    public class AuthorViewModel
    {
        // Collection of authors to be displayed
        public IEnumerable<Author> Authors { get; set; }

        // Single author details for editing or displaying
        public Author Author { get; set; }
    }
}
