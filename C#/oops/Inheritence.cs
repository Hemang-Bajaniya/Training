
namespace Classes;

public class UpiAccount : BankAccount
{
    private string _upiid { get; init; }
    public string UpiHandle
    {
        get
        {
            return _upiid + "@" + BankName;
        }
    }
    public string BankName { get; set; }

    public UpiAccount(string name, decimal balance, string upiid, string bank) : base(name, balance)
    {
        _upiid = upiid;
        BankName = bank;
    }

    public new void MakeDeposit(decimal amount, DateTime date, string note)
    {
        base.MakeDeposit(amount, date, note + " with upi1");
    }
    public override void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        base.MakeWithdrawal(amount, date, note + " with upi");
    }
}