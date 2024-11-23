//Adding simple scenario to test implementation.  Add more later
// Program.cs
using System;

class Program
{
    static void Main(string[] args)
    {
        // Create a new user with an initial budget of $1000
        var user = new User(1, "John Doe", 1000);
        
        // Create some transactions
        var transaction1 = new Transaction
        {
            TransactionId = 1,
            UserId = 1,
            Amount = 100,
            Category = "Food",
            Date = DateTime.Now
        };

        var transaction2 = new Transaction
        {
            TransactionId = 2,
            UserId = 1,
            Amount = 50,
            Category = "Entertainment",
            Date = DateTime.Now
        };

        // Add transactions and track spending
        user.AddTransaction(transaction1);
        user.AddTransaction(transaction2);

        // Display transactions
        user.DisplayTransactions();
    }
}
