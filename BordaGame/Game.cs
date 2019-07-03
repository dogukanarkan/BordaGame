﻿using System;
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
            Console.WriteLine("Welcome to BordaGame!");
            Console.Write("Your nickname: ");
            PlayerName = Console.ReadLine();
            _hasPlayer = CheckNickname(PlayerName);
            File.AppendAllText(_playersNameFile, PlayerName + Environment.NewLine);

            Menu();
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

        }

        private void ContinueGame()
        {

        }

        public void ListCharacters()
        {
            int count = 1;
            foreach (string item in _characterList)
            {
                Console.WriteLine($"{count++}. {item.ToString()}");
            }
        }
    }
}
