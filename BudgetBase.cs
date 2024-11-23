//test before submitting
// BudgetBase.cs
public abstract class BudgetBase
{
    public decimal TotalBudget { get; set; }
    public decimal CurrentSpending { get; set; }

    public BudgetBase(decimal totalBudget)
    {
        TotalBudget = totalBudget;
        CurrentSpending = 0;
    }

    // Abstract method to track spending, implemented in derived class
    public abstract void TrackSpending(decimal amount);
}
