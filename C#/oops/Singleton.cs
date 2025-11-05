namespace Classes;

sealed class MySingletonClass
{

    private static MySingletonClass _ref = null;
    private MySingletonClass()
    { }

    public static MySingletonClass GetInstance()
    {
        if (_ref == null)
        {
            System.Console.WriteLine("Creating instance at first time");
            _ref = new();
        }
        else
        {
            System.Console.WriteLine("Returning existing instance");
        }

        return _ref;
    }
}