using System;
using System.Collections.Generic;
using System.Text;
using BordaGame.Characters;

namespace BordaGame
{
    public static class CharacterFactory
    {
        public static ICharacter Build(string name)
        {
            switch (name)
            {
                case "Ahri":
                    return new Ahri();
                case "Caitlyn":
                    return new Caitlyn();
                case "Darius":
                    return new Darius();
                case "Draven":
                    return new Draven();
                case "Ekko":
                    return new Ekko();
                case "Gangplank":
                    return new Gangplank();
                case "Malphite":
                    return new Malphite();
                case "Morgana":
                    return new Morgana();
                case "Nautilus":
                    return new Nautilus();
                case "Shen":
                    return new Shen();
                default:
                    return null;
            }
        }
    }
}
