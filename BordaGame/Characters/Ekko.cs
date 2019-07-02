using System;
using System.Collections.Generic;
using System.Text;

namespace BordaGame.Characters
{
    class Ekko : ICharacter
    {
        public Ekko()
        {
            firstSkillName = "Ekko1";
            secondSkillName = "Ekko2";
            HealthPoint = 100;
            ManaPoint = 100;
            AttackPoint = 10;
            DefensePoint = 10;
            Level = 1;
            Experience = 0;
            SkillDelegate s1 = new SkillDelegate(BasicAttack);
            SkillDelegate s2 = new SkillDelegate(FirstSkill);
            SkillDelegate s3 = new SkillDelegate(SecondSkill);
            Skills = new List<SkillDelegate>();
            Skills.Add(s1);
            Skills.Add(s2);
            Skills.Add(s3);
        }

        public int HealthPoint { get; set; }
        public int ManaPoint { get; set; }
        public int AttackPoint { get; set; }
        public int DefensePoint { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public string firstSkillName { get; set; }
        public string secondSkillName { get; set; }
        public List<SkillDelegate> Skills { get; set; }

        public void BasicAttack(ICharacter c)
        {
            Console.WriteLine("Basic attack from Ekko.");
            c.HealthPoint = c.HealthPoint + c.DefensePoint - AttackPoint;
        }
        public void FirstSkill(ICharacter c)
        {
            Console.WriteLine($"First skill from Ekko and this skill name is {firstSkillName}");
            c.HealthPoint = c.HealthPoint + c.DefensePoint - 20;
        }

        public void SecondSkill(ICharacter c)
        {
            Console.WriteLine($"Second skill from Ekko and this skill name is {secondSkillName}");
            c.HealthPoint = c.HealthPoint + c.DefensePoint - (int)(c.HealthPoint * 0.3);
        }
    }
}
