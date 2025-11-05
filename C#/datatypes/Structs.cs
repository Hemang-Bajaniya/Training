namespace DataTypes;

readonly struct Person
{
    readonly public string Name;
    readonly public int Age;

    readonly public double Salary { get; init; }

    public Person(string name, int age, double sal)
    {
        Name = name;
        Age = age;
        Salary = sal;
    }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public override string ToString()
    {
        return $"{Name} is {Age} years old and earns {Salary}";
    }
}

class StructDemo
{
    public static void Main()
    {
        Person p = new(name: "Alex", age: 10);

        // p.Age = 20;
        // p.Salary = 200;
        System.Console.WriteLine(p);
    }
}