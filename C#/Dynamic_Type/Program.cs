namespace Dynamic_Usage
{
    class Util
    {
        public string GetTime()
        {
            return TimeOnly.FromDateTime(DateTime.Now).ToString();
        }
    }
    class Program
    {
        public static void Main()
        {
            dynamic util = new Util(); // no static type check
            System.Console.WriteLine(util.GetDate()); // no intellisense

            // dynamic for clr
            // common interoperatability

            dynamic d = 1;
            var testSum = d + 3;
            // Rest the mouse pointer over testSum in the following statement.
            System.Console.WriteLine(testSum);
        }
    }
}