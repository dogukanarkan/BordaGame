using System;
using System.Collections.Generic;
using System.Text;

namespace BordaGame
{
    public abstract class Player
    {
        public Player()
        {
            Threshold = 100;
            Level = 1;
            Experience = 0;
            Deck = new List<ICharacter>();
            Character = null;
        }

        public int Threshold { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public List<ICharacter> Deck { get; set; }
        public ICharacter Character { get; set; }
    }
}
