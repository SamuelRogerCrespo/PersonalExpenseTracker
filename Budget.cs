// Budget.cs
public class Budget : BudgetBase
{
    public Budget(decimal totalBudget) : base(totalBudget) { }

    public override void TrackSpending(decimal amount)
    {
        CurrentSpending += amount;
        if (CurrentSpending > TotalBudget)
        {
            Console.WriteLine("Warning: Budget exceeded!");
        }
        else
        {
            Console.WriteLine($"Spending tracked: {amount:C}. Total spending: {CurrentSpending:C}");
        }
    }
}
