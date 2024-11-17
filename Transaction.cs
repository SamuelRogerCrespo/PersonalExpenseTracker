// Transaction.cs
public class Transaction : ITransaction
{
    public int TransactionId { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public required string Category { get; set; }  // Add 'required' modifier here
    public DateTime Date { get; set; }

    // Implementing the ProcessTransaction method from ITransaction interface
    public void ProcessTransaction()
    {
        Console.WriteLine($"Processing transaction of {Amount:C} for {Category} on {Date.ToShortDateString()}.");
    }

    public override string ToString()
    {
        return $"{Amount:C} in {Category} on {Date.ToShortDateString()}";
    }
}
