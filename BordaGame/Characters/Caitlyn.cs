using System;
using System.Collections.Generic;
using System.Text;

namespace BordaGame.Characters
{
    class Caitlyn : ICharacter
    {
        public Caitlyn()
        {
            firstSkillName = "Caitlyn1";
            secondSkillName = "Caitlyn2";
            HealthPoint = 100;
            ManaPoint = 100;
            AttackPoint = 10;
            DefensePoint = 10;
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
        public string firstSkillName { get; set; }
        public string secondSkillName { get; set; }
        public List<SkillDelegate> Skills { get;  set; }

        public void BasicAttack(ICharacter c)
        {
            Console.WriteLine("Basic attack from Caitlyn.");
            int damage = AttackPoint - c.DefensePoint;
            if (damage < 0)
            {
                damage = 0;
            }
            c.HealthPoint -= damage;
        }

        public void FirstSkill(ICharacter c)
        {
            Console.WriteLine($"First skill from Caitlyn and this skill name is {firstSkillName}");
            int damage = 20 - c.DefensePoint;
            if (damage < 0)
            {
                damage = 0;
            }
            c.HealthPoint -= damage;
        }

        public void SecondSkill(ICharacter c)
        {
            Console.WriteLine($"Second skill from Caitlyn and this skill name is {secondSkillName}");
            int damage = (int)(c.HealthPoint * 0.3) - c.DefensePoint;
            if (damage < 0)
            {
                damage = 0;
            }
            c.HealthPoint -= damage;
        }
    }
}
