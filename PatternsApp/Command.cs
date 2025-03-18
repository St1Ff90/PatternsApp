using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsApp
{
    public class Command
    {
        public enum Action
        {
            Deposit,
            Withdraw
        }

        public Action TheAction;
        public int Amount;
        public bool Success;
    }

    public class Account
    {
        public int Balance { get; set; }

        public void Process(Command c)
        {
            c.Success = true;
            if (c.Amount <= 0) return;
            switch (c.TheAction)
            {
                case Command.Action.Deposit:
                    Balance += c.Amount;
                    break;
                case Command.Action.Withdraw:
                    if (Balance < c.Amount)
                    {
                        c.Success = false;
                        break;
                    }
                    else
                    {
                        Balance -= c.Amount;
                    }
                    break;
            }
        }
    }
}
