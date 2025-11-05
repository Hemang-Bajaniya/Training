using System.Numerics;

namespace GenericDemo;

class RandomItems<T> : IRandom<T> where T : INumber<T>
{
    private List<T> _list = new();
    private Random _random = new();

    public void ShowList()
    {
        foreach (T item in _list)
        {
            System.Console.WriteLine(item);
        }
    }
    public T GetRandomItem()
    {
        int no = _random.Next(0, _list.Count());

        return _list[no];
    }

    public void AddItem(T val)
    {
        _list.Add(val);
    }
}