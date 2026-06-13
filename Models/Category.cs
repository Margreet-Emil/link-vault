namespace LinkVault.Models
{
    public class Category: Baseentity
    {
        public string Name { get; set; }
        public string ? Description { get; set; } 


        public ICollection<Bookmark> Bookmark { get; set; }
            public ICollection<Note> Notes { get; set; }
    }
}
