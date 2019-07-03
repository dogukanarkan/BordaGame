using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using BordaGame.Characters;

namespace BordaGame
{
    public class Game
    {
        private const int CHARACTERCOUNT = 4;
        private Person _person = new Person();
        private Computer _computer = new Computer();
        private List<string> _characterList = AppDomain.CurrentDomain
                            .GetAssemblies()
                            .SelectMany(x => x.GetTypes())
                            .Where(x => typeof(ICharacter)
                            .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                            .Select(x => x.Name)
                            .ToList();
        private string _playersNameFile = "players.txt";
        private bool _hasPlayer = false;
        private string PlayerName { get; set; }

        public void InitGame()
        {
            _person.ThresholdReached += Person_ThresholdReached;
            _computer.ThresholdReached += Computer_ThresholdReached;
            Console.WriteLine("Welcome to BordaGame!");
            Console.Write("Your nickname: ");
            PlayerName = Console.ReadLine();
            _hasPlayer = CheckNickname(PlayerName);
            File.AppendAllText(_playersNameFile, PlayerName + Environment.NewLine);

            Menu();
        }

        private void Computer_ThresholdReached(object sender, EventArgs e)
        {
            _computer.LevelUp(this);
        }

        private void Person_ThresholdReached(object sender, EventArgs e)
        {
            _person.LevelUp(this);
        }

        private void Menu()
        {
            Console.WriteLine("1. New Game");
            if (_hasPlayer)
            {
                Console.WriteLine("2. Continue");
            }

            string input = Console.ReadLine();
            if (input.Equals("1"))
            {
                NewGame();
            }
            else if (input.Equals("2"))
            {
                if (_hasPlayer)
                {
                    ContinueGame();
                }
            }

            Console.Clear();
            Console.WriteLine("Please select one of the below options.");
            Menu();
        }

        private bool CheckNickname(string nickname)
        {
            try
            {
                string[] lines = File.ReadAllLines(_playersNameFile);

                foreach (string line in lines)
                {
                    if (line.Equals(nickname))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private void NewGame()
        {
            Console.WriteLine($"Hello {PlayerName},");
            Console.WriteLine("Now, you should choose 5 characters for game. These are whole characters:");
            ListCharacters();
            Console.WriteLine("You need to enter the name of characters that you want to choose.");

            CreatePersonDeck(_person.Level);
            CreateComputerDeck();
            ListDeck();

            SelectPersonCharacter();
            SelectComputerCharacter();

            Console.WriteLine("\n");
            GameCycle();
        }

        private void ContinueGame()
        {
            GameCycle();
        }

        private void GameCycle()
        {
            CountDown(5);
            Console.Clear();


        }

        public void ListCharacters()
        {
            int count = 1;
            foreach (string item in _characterList)
            {
                Console.WriteLine($"{count++}. {item.ToString()}");
            }
        }

        public void CreatePersonDeck(int level)
        {
            do
            {
                string input = Console.ReadLine();
                if (CheckCharacterName(input) && !CheckDeck(input))
                {
                    _person.Deck.Add(CharacterFactory.Build(input));
                    Console.WriteLine($"{input} added successfully.");
                }
                else
                {
                    Console.WriteLine("Please enter the character name again.");
                }
            } while (_person.Deck.Count < (CHARACTERCOUNT + level));
        }

        private void CreateComputerDeck()
        {
            foreach (string item in _characterList)
            {
                _computer.Deck.Add(CharacterFactory.Build(item));
            }
        }

        private bool CheckCharacterName(string name)
        {
            return _characterList.Any(s => name.Contains(s));
        }

        public bool CheckDeck(string name)
        {
            return _person.Deck.Any(s => name.Contains(s.GetType().Name));
        }

        public void ListDeck()
        {
            Console.WriteLine("This is your deck:");
            foreach (ICharacter item in _person.Deck)
            {
                Console.Write($"{item.GetType().Name} ");
            }
            Console.WriteLine();
        }

        private void SelectPersonCharacter()
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

            IEnumerable<ICharacter> selected = from character in _person.Deck
                                               where character.GetType().Name.Equals(inputCharacter)
                                               select character;
            _person.Character = selected.Single();
        }

        private void SelectComputerCharacter()
        {
            int rand = new Random().Next(1, 11);
            _computer.Character = _computer.Deck[rand];
        }

        public void CountDown(int second)
        {
            for (int i = second; i > 0; i--)
            {
                Console.WriteLine($"New game will start in {i} seconds...");
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                Thread.Sleep(995);
            }
        }
    }
}
