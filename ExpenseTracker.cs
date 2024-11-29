using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace PersonalExpenseTracker
{
    public class ExpenseTracker
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public BudgetManager BudgetManager { get; set; }

        public ExpenseTracker(int userId, string userName, decimal totalBudget)
        {
            UserId = userId;
            UserName = userName;
            BudgetManager = new BudgetManager(totalBudget);
        }

        public void AddTransaction(Transaction transaction)
        {
            // Code to add the transaction to the SQLite database
            using (var conn = new SQLiteConnection("Data Source=ExpenseTracker.db;Version=3;"))
            {
                conn.Open();
                string sql = "INSERT INTO Transactions (UserId, Amount, Category, Date) VALUES (@UserId, @Amount, @Category, @Date)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", transaction.UserId);
                    cmd.Parameters.AddWithValue("@Amount", transaction.Amount);
                    cmd.Parameters.AddWithValue("@Category", transaction.Category);
                    cmd.Parameters.AddWithValue("@Date", transaction.Date);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Transaction> GetTransactionsForUser(int userId)
        {
            List<Transaction> transactions = new List<Transaction>();

            using (var conn = new SQLiteConnection("Data Source=ExpenseTracker.db;Version=3;"))
            {
                conn.Open();
                string sql = "SELECT * FROM Transactions WHERE UserId = @UserId";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var transaction = new Transaction
                        {
                            TransactionId = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            Amount = reader.GetDecimal(2),
                            Category = reader.GetString(3),
                            Date = reader.GetDateTime(4)
                        };
                        transactions.Add(transaction);
                    }
                }
            }

            return transactions;
        }
    }
}
