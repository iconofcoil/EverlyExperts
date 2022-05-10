using System.ComponentModel.DataAnnotations.Schema;

namespace EverlyExperts.Models
{
    [Table("Members")]
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WebsiteShortUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public string WebsiteTopics { get; set; }

        public ICollection<Friend>? Friends { get; set; }
    }
}
