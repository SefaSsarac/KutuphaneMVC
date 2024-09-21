public class Book
{
    // Primary key
    public int Id { get; set; }

    // Title of the book
    public string Title { get; set; }

    // Foreign key referencing the author's ID
    public int AuthorId { get; set; }

    // Navigation property to the Author object
    public Author Author { get; set; }

    // Genre of the book
    public string Genre { get; set; }

    // Date when the book was published
    public DateTime PublishDate { get; set; }

    // International Standard Book Number
    public string ISBN { get; set; }

    // Number of copies available for the book
    public int CopiesAvailable { get; set; }
}
