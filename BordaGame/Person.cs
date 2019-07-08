using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BordaGame
{
    public class Person : Player
    {
        private const int CHARACTERCOUNT = 4;
        public Person() : base() { }

        public override void Turn(Player computer)
        {
            int count = 0;
            Console.WriteLine("\nYour Turn\n");
            Console.WriteLine("What do you want to do?");
            Character.Skills.ForEach(n => Console.WriteLine($"{++count}. {n.Method.Name}"));
            Console.Write("Your choose: ");
            int input = Convert.ToInt32(Console.ReadLine());
            Character.Skills[input - 1].Invoke(computer.Character);
            CheckBattle(computer);
        }

        public void LevelUp(Game game)
        {
            Console.WriteLine("Leveled up!");
            LevelUp();
            UnlockNewCharacter(game);
        }

        public override void Win()
        {
            Console.WriteLine("You won!");
            base.Win();
        }

        private void UnlockNewCharacter(Game game)
        {
            ListDeck();
            Console.WriteLine("Whole characters:");
            game.ListCharacters();
            Console.Write("You can choose new character. " +
                "You should enter the character name that you want to choose: ");
            CreateDeck();
            Console.WriteLine("\n");
            Console.WriteLine("This is yours new deck:");
            ListDeck();
            game.CountDown(5);
        }

        public void SelectCharacter()
        {
            Console.Write("Which character do you want to play this game?: ");
            string inputCharacter = Console.ReadLine();

            while (!CheckDeck(inputCharacter))
            {
                Console.WriteLine("You don't have this character in your deck." +
                    "Please enter the character that you have.");
                ListDeck();

                Console.Write("Which character do you want to play this game?: ");
                inputCharacter = Console.ReadLine();
            }

            IEnumerable<ICharacter> selected = from character in Deck
                                               where character.GetType().Name.Equals(inputCharacter)
                                               select character;
            Character = selected.Single();
        }

        public void CreateDeck()
        {
            do
            {
                string input = Console.ReadLine();
                if (CheckCharacterName(input) && !CheckDeck(input))
                {
                    Deck.Add(CharacterFactory.Build(input));
                    Console.WriteLine($"{input} added successfully.");
                }
                else
                {
                    Console.WriteLine("Please enter the character name again.");
                }
            } while (Deck.Count < (CHARACTERCOUNT + Level));
        }

        public void ListDeck()
        {
            Console.WriteLine("This is your deck:");
            foreach (ICharacter item in Deck)
            {
                Console.Write($"{item.GetType().Name} ");
            }
            Console.WriteLine();
        }

        public bool CheckDeck(string name)
        {
            return Deck.Any(s => name.Contains(s.GetType().Name));
        }

        private bool CheckCharacterName(string name)
        {
            return _characterList.Any(s => name.Contains(s));
        }
    }
}
