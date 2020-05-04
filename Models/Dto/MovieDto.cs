namespace SFF.Models
{
    public class MovieDto : BaseEntity
    {
        public string Title { get; set; }
        public int AmountInStock { get; set; }
    }
}