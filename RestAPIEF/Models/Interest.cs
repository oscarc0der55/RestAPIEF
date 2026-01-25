namespace RestAPIEF.Models
{
    public class Interest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? Websitelink { get; set; }

        public int PersonId { get; set; }

        public List<Link> Links { get; set; } = new();
    }
}
