using System;

namespace PersonalExpenseTracker
{
    public class ExpenseTracker
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public BudgetManager BudgetManager { get; set; }

        public ExpenseTracker(int userId, string userName)
        {
            UserId = userId;
            UserName = userName;
            BudgetManager = new BudgetManager();
        }

        public void AddTransaction(Transaction transaction)
        {
            // Code to add the transaction to the database
            // Example: Database.Save(transaction);
        }

        public string GenerateReport(DateRange range)
        {
            // Generate a report based on a given date range
            return $"Report for {UserName} from {range.StartDate} to {range.EndDate}";
        }

        public override string ToString()
        {
            return $"{UserName}'s Expense Tracker";
        }
    }
}
