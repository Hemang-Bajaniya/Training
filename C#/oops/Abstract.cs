namespace Classes;

interface IDrawable
{
    void Draw();
}

abstract class Shape : IDrawable
{
    public abstract double CalculateArea();
    public abstract void Draw();

    public void DisplayInfo()
    {
        Console.WriteLine($"Shape: {GetType().Name}, Area: {CalculateArea()}");
    }
}

class Circle : Shape
{
    private double radius;

    public Circle(double radius)
    {
        this.radius = radius;
    }

    public override double CalculateArea()
    {
        return Math.PI * radius * radius;
    }

    public override void Draw()
    {
        Console.WriteLine("Drawing a circle.");
    }
}