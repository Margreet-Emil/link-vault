namespace LinkVault.Models
{
    public class Note : Baseentity
    {
        public string Content { get; set; }
        public string Title { get; set; }

        public bool IsPinned  { get; set; }


        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
