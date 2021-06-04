using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordBotDatabase.Models.cs
{

    public class Vote : Entity
    {

        public string VoteName { get; set; }
        public int VoteCount { get; set; }

        [Display(Name = "PollId")]
        public virtual int PollId { get; set; }

        [ForeignKey("PollId")]
        public virtual Poll polls { get; set; }

    }
}
