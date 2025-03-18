using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // Игровой класс, содержащий список существ.
    public class Game
    {
        public IList<Creature> Creatures = new List<Creature>();
    }

    public enum Statistic
    {
        Attack,
        Defense
    }

    // Класс запроса, который передается по цепочке для модификации статистик.
    public class StatQuery
    {
        public Statistic Statistic;
        public int Value;

        public StatQuery(Statistic statistic, int value)
        {
            Statistic = statistic;
            Value = value;
        }
    }

    // Базовый класс существа.
    public class Creature
    {
        protected Game game;
        public string Name;
        private int baseAttack, baseDefense;

        public Creature(Game game, string name, int baseAttack, int baseDefense)
        {
            this.game = game;
            Name = name;
            this.baseAttack = baseAttack;
            this.baseDefense = baseDefense;
        }

        // Свойства, вычисляемые через запросы (chain of responsibility).
        public virtual int Attack
        {
            get
            {
                var q = new StatQuery(Statistic.Attack, baseAttack);
                foreach (var creature in game.Creatures)
                {
                    creature.Query(this, q);
                }
                return q.Value;
            }
        }

        public virtual int Defense
        {
            get
            {
                var q = new StatQuery(Statistic.Defense, baseDefense);
                foreach (var creature in game.Creatures)
                {
                    creature.Query(this, q);
                }
                return q.Value;
            }
        }

        // Метод, через который другие существа могут модифицировать статистику.
        public virtual void Query(object source, StatQuery query)
        {
            // Базовая реализация ничего не делает.
        }

        public override string ToString() => $"{Name}: A = {Attack}, D = {Defense}";
    }

    // Обычный гоблин: базовые статы 1/1. При запросе защиты от других гоблинов добавляет +1.
    public class Goblin : Creature
    {
        public Goblin(Game game)
            : base(game, "Goblin", 1, 1)
        {
        }

        public override void Query(object source, StatQuery query)
        {
            // Если это не тот же объект и запрашивается защита,
            // то обычный гоблин увеличивает защиту на 1.
            if (source != this && query.Statistic == Statistic.Defense)
                query.Value++;
        }
    }

    // Гоблин-король наследуется от гоблина, но:
    // – его имя изменяется на "Goblin King",
    // – при запросе атаки он добавляет +1 к атаке других гоблинов.
    public class GoblinKing : Goblin
    {
        public GoblinKing(Game game)
            : base(game)
        {
            Name = "Goblin King";
        }

        public override void Query(object source, StatQuery query)
        {
            if (source != this && query.Statistic == Statistic.Attack)
                query.Value++;
            base.Query(source, query);
        }
    }

}
