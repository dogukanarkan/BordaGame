﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BordaGame
{
    public class Computer : Player
    {
        public Computer() : base() { }

        public override void Turn(Player person)
        {
            Console.WriteLine("\nComputer's Turn\n");
            int rand = new Random().Next(3);
            Character.Skills[rand].Invoke(person.Character);
            CheckBattle(person);
        }

        public override void Win()
        {
            Console.WriteLine("Computer won!");
            base.Win();
        }

        public void SelectCharacter()
        {
            int rand = new Random().Next(10);
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
