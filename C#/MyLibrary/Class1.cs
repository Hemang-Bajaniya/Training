// using System;
// using static System.Console;
namespace MyLibrary;


/// <summary>
/// The Helper class demonstrates the use of various access modifiers on its static methods.
/// - internal: within same assembly
/// - public: any assembly
/// - protected internal: either in same assembly or diff assmb derived class
/// </summary>
public class Helper
{
    internal static double FindSquareRoot(int num)
    {
        return Math.Sqrt(num);
    }

    public static string Greet()
    {
        return "Hello! from class library";
    }

    protected internal static string GetDateTime()
    {
        return DateTime.Now.ToString();
    }
}
