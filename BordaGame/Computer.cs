using System;
using System.Collections.Generic;
using System.Text;

namespace BordaGame
{
    public class Computer : Player
    {
        public Computer() : base() { }

        public void SelectCharacter()
        {
            int rand = new Random().Next(1, 11);
            Character = Deck[rand];
        }

        public void CreateDeck()
        {
            foreach (string item in _characterList)
            {
                Deck.Add(CharacterFactory.Build(item));
            }
        }
    }
}
