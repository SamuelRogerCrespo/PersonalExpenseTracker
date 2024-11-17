namespace PersonalExpenseTracker
{
    public class BudgetManager
    {
        public decimal TotalBudget { get; set; }
        public decimal CurrentSpending { get; set; }

        public BudgetManager()
        {
            TotalBudget = 0;
            CurrentSpending = 0;
        }

        public void SetBudget(decimal budget)
        {
            TotalBudget = budget;
        }

        public void TrackSpending(decimal amount)
        {
            CurrentSpending += amount;
            if (CurrentSpending > TotalBudget)
            {
                Console.WriteLine("You have exceeded your budget!");
            }
        }
    }
}
