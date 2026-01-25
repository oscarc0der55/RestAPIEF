using Microsoft.Identity.Client;

namespace RestAPIEF.Models
{
    public class Link
    {

        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public int InterestId { get; set; }
        
    }
}
