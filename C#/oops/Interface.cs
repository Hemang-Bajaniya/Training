namespace Classes;

interface IMouseHandler
{
    public void SetPointer(int x, int y);

    static int X, Y;

    public string GetPointer()
    {
        return $"X:{X}, Y:{Y}";
    }
}

public class WindowMouseHandler : IMouseHandler
{
    public void SetPointer(int a, int b)
    {
        IMouseHandler.X = a;
        IMouseHandler.Y = b;
    }
}

