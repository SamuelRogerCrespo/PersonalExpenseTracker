// ITransaction.cs
public interface ITransaction
{
    decimal Amount { get; set; }
    string Category { get; set; }
    DateTime Date { get; set; }
    void ProcessTransaction();
}
