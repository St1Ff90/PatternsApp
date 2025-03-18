using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsApp
{
    public class GameObserver
    {
        public event EventHandler RatEnters, RatDies;
        public event EventHandler<Rat> NotifyRat;

        public void FireRatEnters(object sender)
        {
            RatEnters?.Invoke(sender, EventArgs.Empty);
        }

        public void FireRatDies(object sender)
        {
            RatDies?.Invoke(sender, EventArgs.Empty);
        }

        public void FireNotifyRat(object sender, Rat whichRat)
        {
            NotifyRat?.Invoke(sender, whichRat);
        }

    }

    public class Rat : IDisposable
    {
        private readonly GameObserver game;
        public int Attack = 1;

        public Rat(GameObserver game)
        {
            this.game = game;
            game.RatEnters += OnRatEnters;
            game.NotifyRat += OnNotifyRat;
            game.RatDies += OnRatDies;
            game.FireRatEnters(this);
        }

        private void OnRatEnters(object sender, EventArgs args)
        {
            if (sender != this)
            {
                ++Attack;
                game.FireNotifyRat(this, (Rat)sender);
            }
        }

        private void OnNotifyRat(object sender, Rat rat)
        {
            if (rat == this)
                ++Attack;
        }

        private void OnRatDies(object sender, EventArgs args)
        {
            --Attack;
        }

        public void Dispose()
        {
            game.FireRatDies(this);
        }
    }
}
