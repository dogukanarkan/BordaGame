using System;
using System.Collections.Generic;
using System.Text;

namespace BordaGame
{
    public delegate void SkillDelegate(ICharacter c);

    public interface ICharacter
    {
        string firstSkillName { get; set; }
        string secondSkillName { get; set; }
        int HealthPoint { get; set; }
        int ManaPoint { get; set; }
        int AttackPoint { get; set; }
        int DefensePoint { get; set; }
        int Level { get; set; }
        int Experience { get; set; }
        List<SkillDelegate> Skills { get; set; }

        void BasicAttack(ICharacter c);
        void FirstSkill(ICharacter c);
        void SecondSkill(ICharacter c);
    }
}
