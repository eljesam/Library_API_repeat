namespace Library_API_repeat.Api.Models
{
    public class Author
    {
        public int it {  get; set; }
        public string name { get; set; } = string.Empty;
        public string? Country { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
