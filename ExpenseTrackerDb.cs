using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace PersonalExpenseTracker
{
    public static class ExpenseTrackerDb
    {
        // Create tables if they don't exist
        public static void CreateTables(SQLiteConnection conn)
        {
            string createUsersTableQuery = @"
                CREATE TABLE IF NOT EXISTS Users (
                    UserId INTEGER PRIMARY KEY,
                    UserName TEXT,
                    Budget REAL
                );";
            string createTransactionsTableQuery = @"
                CREATE TABLE IF NOT EXISTS Transactions (
                    TransactionId INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserId INTEGER,
                    Amount REAL,
                    Category TEXT,
                    Date TEXT,
                    FOREIGN KEY (UserId) REFERENCES Users (UserId)
                );";

            using (SQLiteCommand cmd = new SQLiteCommand(createUsersTableQuery, conn))
            {
                cmd.ExecuteNonQuery();
            }

            using (SQLiteCommand cmd = new SQLiteCommand(createTransactionsTableQuery, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        // Add a new user to the database
        public static void AddUser(SQLiteConnection conn, User user)
        {
            // Check if the user already exists
            string checkUserQuery = "SELECT COUNT(1) FROM Users WHERE UserId = @UserId";
            SQLiteCommand checkCmd = new SQLiteCommand(checkUserQuery, conn);
            checkCmd.Parameters.AddWithValue("@UserId", user.UserId);

            int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (userExists == 0)
            {
                string insertUserQuery = "INSERT INTO Users (UserId, UserName, Budget) VALUES (@UserId, @UserName, @Budget)";
                SQLiteCommand cmd = new SQLiteCommand(insertUserQuery, conn);
                cmd.Parameters.AddWithValue("@UserId", user.UserId);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Budget", user.UserBudget.TotalBudget);
                cmd.ExecuteNonQuery();
            }
            else
            {
                Console.WriteLine($"User with ID {user.UserId} already exists.");
            }
        }

        // Add a new transaction for a user (auto-increment TransactionId)
        public static void AddTransaction(SQLiteConnection conn, Transaction transaction)
        {
            string insertTransactionQuery = "INSERT INTO Transactions (UserId, Amount, Category, Date) " +
                                            "VALUES (@UserId, @Amount, @Category, @Date)";
            SQLiteCommand cmd = new SQLiteCommand(insertTransactionQuery, conn);
            cmd.Parameters.AddWithValue("@UserId", transaction.UserId);
            cmd.Parameters.AddWithValue("@Amount", transaction.Amount);
            cmd.Parameters.AddWithValue("@Category", transaction.Category);
            cmd.Parameters.AddWithValue("@Date", transaction.Date.ToString("yyyy-MM-dd"));
            cmd.ExecuteNonQuery();
        }

        // Get all transactions for a specific user
        public static List<Transaction> GetTransactionsForUser(SQLiteConnection conn, int userId)
        {
            List<Transaction> transactions = new List<Transaction>();
            string getTransactionsQuery = "SELECT * FROM Transactions WHERE UserId = @UserId";
            SQLiteCommand cmd = new SQLiteCommand(getTransactionsQuery, conn);
            cmd.Parameters.AddWithValue("@UserId", userId);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                transactions.Add(new Transaction
                {
                    TransactionId = reader.GetInt32(0),
                    UserId = reader.GetInt32(1),
                    Amount = reader.GetDecimal(2),
                    Category = reader.GetString(3),
                    Date = DateTime.Parse(reader.GetString(4))
                });
            }

            return transactions;
        }

        // Update a user's information
        public static void UpdateUser(SQLiteConnection conn, User user)
        {
            string updateUserQuery = "UPDATE Users SET UserName = @UserName, Budget = @Budget WHERE UserId = @UserId";
            SQLiteCommand cmd = new SQLiteCommand(updateUserQuery, conn);
            cmd.Parameters.AddWithValue("@UserId", user.UserId);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Budget", user.UserBudget.TotalBudget);
            cmd.ExecuteNonQuery();
        }

        // Delete a user from the database
        public static void DeleteUser(SQLiteConnection conn, int userId)
        {
            string deleteUserQuery = "DELETE FROM Users WHERE UserId = @UserId";
            SQLiteCommand cmd = new SQLiteCommand(deleteUserQuery, conn);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.ExecuteNonQuery();
        }

        // Delete a transaction from the database
        public static void DeleteTransaction(SQLiteConnection conn, int transactionId)
        {
            string deleteTransactionQuery = "DELETE FROM Transactions WHERE TransactionId = @TransactionId";
            SQLiteCommand cmd = new SQLiteCommand(deleteTransactionQuery, conn);
            cmd.Parameters.AddWithValue("@TransactionId", transactionId);
            cmd.ExecuteNonQuery();
        }
    }
}
