using System.ComponentModel.DataAnnotations.Schema;

namespace EverlyExperts.Models
{
    [Table("Friends")]
    public class Friend
    {
        public int MemberId { get; set; }
        public int FriendId { get; set; }

        [ForeignKey("FriendId")]
        public virtual Member? FriendMember { get; set; }
    }
}
