using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BordaGame
{
    public abstract class Player
    {
        protected List<string> _characterList = AppDomain.CurrentDomain
                            .GetAssemblies()
                            .SelectMany(x => x.GetTypes())
                            .Where(x => typeof(ICharacter)
                            .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                            .Select(x => x.Name)
                            .ToList();
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

        public virtual void Turn(Player player) { }
        public void LevelUp()
        {
            Threshold += 100;
            Level++;
            Experience %= 100;

            foreach (ICharacter character in Deck)
            {
                character.HealthPoint += Level * 20;
                character.ManaPoint += Level * 10;
                character.AttackPoint += Level * 4;
                character.DefensePoint += Level * 2;
            }
        }

        public void Win()
        {
            Experience += 100;
            CheckLevel();
        }

        public void Lose()
        {
            Experience += 50;
            CheckLevel();
        }

        private void CheckLevel()
        {
            if (Experience >= Threshold)
            {
                OnThresholdReached(EventArgs.Empty);
            }
        }

        private void CheckBattle()
        {
            if (Character.HealthPoint <= 0)
            {
                OnBattleFinish(EventArgs.Empty);
            }
        }

        public event EventHandler ThresholdReached;
        protected virtual void OnThresholdReached(EventArgs e)
        {
            ThresholdReached?.Invoke(this, e);
        }

        public event EventHandler BattleFinish;
        protected virtual void OnBattleFinish(EventArgs e)
        {
            BattleFinish?.Invoke(this, e);
        }
    }
}
