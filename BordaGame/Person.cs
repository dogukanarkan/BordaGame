using System;
using System.Collections.Generic;
using System.Text;

namespace BordaGame
{
    public class Person : Player
    {
        public Person() : base() { }

        public void LevelUp(Game game)
        {
            UnlockNewCharacter(game);
            LevelUp();
        }

        private void UnlockNewCharacter(Game game)
        {
            Console.WriteLine("Your deck:");
            game.ListDeck();
            Console.WriteLine("Whole characters:");
            game.ListCharacters();
            Console.Write("You can choose new character. " +
                "You should enter the character name that you want to choose: ");
            string input = Console.ReadLine();
            game.CreatePersonDeck(Level);
            Console.WriteLine("\n");
            Console.WriteLine("This is yours new deck:");
            game.ListDeck();
            game.CountDown(5);
        }
    }
}
