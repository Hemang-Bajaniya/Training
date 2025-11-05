namespace Classes;

class X
{
    public virtual void M1()
    {
        System.Console.WriteLine("M1");
    }
}

class Y : X
{
    public sealed override void M1()
    {
        base.M1();
    }
}

class Z : Y
{
    // public override void M1() // 'Z.M1()': cannot override inherited member 'Y.M1()' because it is sealed
    // {
    // }
}

