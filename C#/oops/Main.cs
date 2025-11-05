using System.Runtime.InteropServices;

namespace Classes;

public class MyClass
{
    public static void Main()
    {
        // BankAccount bankAccount = new("Alex", 30);

        // bankAccount.MakeDeposit(103.3m, DateTime.Now, "New add");
        // System.Console.WriteLine(bankAccount.Balance);
        // bankAccount.MakeWithdrawal(100, DateTime.Now, "rent");
        // System.Console.WriteLine(bankAccount.Balance);

        // System.Console.WriteLine(bankAccount.GetAccountHistory());

        // UpiAccount upiAccount = new("Alex", 100, "12383j849", "Axis");
        // System.Console.WriteLine(upiAccount.UpiHandle);
        // System.Console.WriteLine(upiAccount.Balance);
        // upiAccount.MakeDeposit(103.3m, DateTime.Now, "New add");
        // System.Console.WriteLine(upiAccount.Balance);
        // upiAccount.MakeWithdrawal(100, DateTime.Now, "rent");
        // System.Console.WriteLine(upiAccount.Balance);

        // System.Console.WriteLine(upiAccount.GetAccountHistory());

        // Shape circle = new Circle(5.0);
        // circle.DisplayInfo();
        // circle.Draw();

        // IMouseHandler mouseHandler = new WindowMouseHandler();
        // mouseHandler.SetPointer(10, 20);
        // System.Console.WriteLine(mouseHandler.GetPointer());

        MySingletonClass mySingletonClass = MySingletonClass.GetInstance();

        mySingletonClass = MySingletonClass.GetInstance();
    }
}