using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Exceptions
{
    public class InsufficientBalanceException : Exception
    {
         public InsufficientBalanceException() 
            : base("Insufficient balance for this transaction.") 
        {
        }

        public InsufficientBalanceException(string message) 
            : base(message) 
        {
        }
    }
}