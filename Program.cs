using System;
using System.Data.SQLite;  // Using System.Data.SQLite for SQLite interactions
using PersonalExpenseTracker;

class Program
{
    static void Main(string[] args)
    {
        // Open SQLite connection using System.Data.SQLite
        using (SQLiteConnection conn = new SQLiteConnection("Data Source=ExpenseTracker.db;Version=3;"))
        {
            conn.Open();

            // Create necessary tables
            ExpenseTrackerDb.CreateTables(conn);

            // Create a new user
            var user = new User(1, "John Doe", 1000);
            ExpenseTrackerDb.AddUser(conn, user);

            // Create some transactions for the user
            var transaction1 = new Transaction
            {
                TransactionId = 1,
                UserId = user.UserId,
                Amount = 100,
                Category = "Food",
                Date = DateTime.Now
            };
            var transaction2 = new Transaction
            {
                TransactionId = 2,
                UserId = user.UserId,
                Amount = 50,
                Category = "Entertainment",
                Date = DateTime.Now
            };

            // Insert transactions into the database
            ExpenseTrackerDb.AddTransaction(conn, transaction1);
            ExpenseTrackerDb.AddTransaction(conn, transaction2);

            // Fetch and display transactions for the user
            var transactions = ExpenseTrackerDb.GetTransactionsForUser(conn, user.UserId);
            foreach (var trans in transactions)
            {
                Console.WriteLine(trans.ToString());
            }

            // Close connection
            conn.Close();
        }

        Console.WriteLine("User and transactions have been added to the database.");
    }
}
