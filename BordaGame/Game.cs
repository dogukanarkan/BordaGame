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
        private static bool _isFinish = false;
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
        public string PlayerName { get; set; }

        public void InitGame()
        {
            _person.ThresholdReached += _person_ThresholdReached;
            _person.BattleFinish += _person_BattleFinish;
            _computer.ThresholdReached += _computer_ThresholdReached;
            _computer.BattleFinish += _computer_BattleFinish;
            Console.WriteLine("Welcome to BordaGame!");
            Console.Write("Your nickname: ");
            PlayerName = Console.ReadLine();
            _hasPlayer = CheckNickname(PlayerName);
            File.AppendAllText(_playersNameFile, PlayerName + Environment.NewLine);

            Menu();
        }

        private void _computer_BattleFinish(object sender, EventArgs e)
        {
            _isFinish = true;
            _computer.Win();
            _person.Lose();
        }

        private void _person_BattleFinish(object sender, EventArgs e)
        {
            _isFinish = true;
            _person.Win();
            _computer.Lose();
        }

        private void _computer_ThresholdReached(object sender, EventArgs e)
        {
            _computer.LevelUp();
        }

        private void _person_ThresholdReached(object sender, EventArgs e)
        {
            _person.LevelUp(this);
        }

        private void Menu()
        {
            int count = 0;
            Console.WriteLine($"{++count}. New Game");
            if (_hasPlayer)
            {
                Console.WriteLine($"{++count}. Continue");
            }
            Console.WriteLine($"{++count}. Exit");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    NewGame();
                    break;
                case "2":
                    if (_hasPlayer)
                    {
                        ContinueGame();
                    }
                    Environment.Exit(0);
                    break;
                case "3":
                    if (count == 3)
                    {
                        Environment.Exit(0);
                    }
                    goto default;
                default:
                    Console.Clear();
                    Console.WriteLine("Please select one of the below options.");
                    Menu();
                    break;
            }

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

            _person.CreateDeck();
            _computer.CreateDeck();
            _person.ListDeck();

            _person.SelectCharacter();
            _computer.SelectCharacter();

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

            Player temp;
            Queue<Player> turn = new Queue<Player>();
            turn.Enqueue(_person);
            turn.Enqueue(_computer);

            while (!_isFinish)
            {
                Console.WriteLine($"Computer health: {_computer.Character.HealthPoint}");
                Console.WriteLine($"Person health: {_person.Character.HealthPoint}");
                temp = turn.Dequeue();
                turn.Enqueue(temp);
                temp.Turn(turn.Peek());
            }
        }

        public void ListCharacters()
        {
            int count = 1;
            foreach (string item in _characterList)
            {
                Console.WriteLine($"{count++}. {item.ToString()}");
            }
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
