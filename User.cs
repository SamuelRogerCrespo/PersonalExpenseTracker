// User.cs
using System;
using System.Collections.Generic;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public List<ITransaction> Transactions { get; set; }
    public Budget UserBudget { get; set; }

    public User(int userId, string userName, decimal initialBudget)
    {
        UserId = userId;
        UserName = userName;
        Transactions = new List<ITransaction>();
        UserBudget = new Budget(initialBudget);
    }

    // Method to add a transaction
    public void AddTransaction(ITransaction transaction)
    {
        Transactions.Add(transaction);
        UserBudget.TrackSpending(transaction.Amount);
    }

    public void DisplayTransactions()
    {
        Console.WriteLine($"{UserName}'s Transactions:");
        foreach (var transaction in Transactions)
        {
            Console.WriteLine(transaction.ToString());
        }
    }
}
