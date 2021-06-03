using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotDatabase.Models.cs
{
    public class Poll : Entity
    {
        public int Id { get; set; }
        public string PollName { get; set; }

        public string choices { get; set; }

    }
}
