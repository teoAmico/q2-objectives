using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTestApp
{
    internal class BankAccount
    {
        private readonly string customerName;
        private double accountBalance;

        public BankAccount(string name, double balance)
        {
            customerName = name;
            accountBalance = balance;
        }

        public void Debit(double amount)
        {
            if (amount > accountBalance)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            accountBalance -= amount;
        }

        public void Credit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            accountBalance += amount;
        }

        public string CustomerName
        {
            get { return customerName; }
        }

        public double Balance
        {
            get { return accountBalance; }
        }
    }
}
