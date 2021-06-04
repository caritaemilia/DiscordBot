using System.ComponentModel.DataAnnotations;

namespace DiscordBotDatabase
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
