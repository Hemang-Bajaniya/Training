namespace Classes;

public class BankAccount
{
    private static int s_accountno = 1242224242;
    private List<Transaction> _allTransactions = [];
    private string _name;
    public string Number
    // { get; set; } // automatically implemented property
    { get; init; } // init from constructor only
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value.Trim();
        }
    }
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var t in _allTransactions)
            {
                balance += t.Amount;
            }
            return balance;
        }
        private set;
    }

    public BankAccount(string name, decimal balance)
    {
        Name = name;
        Number = s_accountno.ToString();
        MakeDeposit(balance, DateTime.Now, "Initial balance");
        s_accountno++;
        this.MakeDeposit(100, DateTime.Now, "ok");
    }

    public virtual void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
        }
        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);
    }

    public virtual void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
        }
        if (Balance - amount < 0)
        {
            throw new InvalidOperationException("Not sufficient funds for this withdrawal");
        }
        var withdrawal = new Transaction(-amount, date, note);
        _allTransactions.Add(withdrawal);
    }

    public string GetAccountHistory()
    {
        var report = new System.Text.StringBuilder();

        decimal balance = 0;
        report.AppendLine("Date\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
        }

        return report.ToString();
    }
}