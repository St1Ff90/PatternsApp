using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsApp
{
    public class CreatureTemplate
    {
        public int Attack, Health;

        public CreatureTemplate(int attack, int health)
        {
            Attack = attack;
            Health = health;
        }
    }

    public abstract class CardGame
    {
        public CreatureTemplate[] Creatures;

        public CardGame(CreatureTemplate[] creatures)
        {
            Creatures = creatures;
        }

        // returns -1 if no clear winner (both alive or both dead)
        public int Combat(int creature1, int creature2)
        {
            CreatureTemplate first = Creatures[creature1];
            CreatureTemplate second = Creatures[creature2];
            Hit(first, second);
            Hit(second, first);
            bool firstAlive = first.Health > 0;
            bool secondAlive = second.Health > 0;
            if (firstAlive == secondAlive) return -1;
            return firstAlive ? creature1 : creature2;
        }

        // attacker hits other creature
        protected abstract void Hit(CreatureTemplate attacker, CreatureTemplate other);
    }

    public class TemporaryCardDamageGame : CardGame
    {
        private int cretureHelth;

        public TemporaryCardDamageGame(CreatureTemplate[] creatures) : base(creatures)
        {
            Console.WriteLine("total is " + creatures.Length);
            int i = 1;
            foreach (CreatureTemplate creature in creatures)
            {
                Console.WriteLine("creature " + i + " with health " + creature.Health.ToString() + " and atack " + creature.Health.ToString());
            }
        }

        protected override void Hit(CreatureTemplate attacker, CreatureTemplate other)
        {
            Console.WriteLine("Try Hit");
        }
    }

    public class PermanentCardDamage : CardGame
    {
        public PermanentCardDamage(CreatureTemplate[] creatures) : base(creatures)
        {
        }

        // todo
        protected override void Hit(CreatureTemplate attacker, CreatureTemplate other)
        {
            throw new NotImplementedException();
        }
    }
}
