using System;
using System.Data.SQLite;

namespace PersonalExpenseTracker
{
    public class BudgetManager
    {
        public decimal TotalBudget { get; set; }
        public decimal CurrentSpending { get; set; }

        public BudgetManager(decimal totalBudget)
        {
            TotalBudget = totalBudget;
            CurrentSpending = 0;
        }

        public void SetBudget(decimal budget)
        {
            TotalBudget = budget;
            // Save budget to the database
            SaveBudgetToDatabase();
        }

        public void TrackSpending(decimal amount)
        {
            CurrentSpending += amount;
            if (CurrentSpending > TotalBudget)
            {
                Console.WriteLine("You have exceeded your budget!");
            }
            // Save spending to the database
            UpdateBudgetInDatabase();
        }

        private void SaveBudgetToDatabase()
        {
            using (var conn = new SQLiteConnection("Data Source=ExpenseTracker.db;Version=3;"))
            {
                conn.Open();
                string sql = "INSERT INTO Budgets (TotalBudget) VALUES (@TotalBudget)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TotalBudget", TotalBudget);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateBudgetInDatabase()
        {
            using (var conn = new SQLiteConnection("Data Source=ExpenseTracker.db;Version=3;"))
            {
                conn.Open();
                string sql = "UPDATE Budgets SET CurrentSpending = @CurrentSpending WHERE TotalBudget = @TotalBudget";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CurrentSpending", CurrentSpending);
                    cmd.Parameters.AddWithValue("@TotalBudget", TotalBudget);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
