// No parameters
Func<int> getNumber = () => 42;
Console.WriteLine(getNumber()); // Output: 42

// Single parameter
Func<int, int> square = x => x * x;
Console.WriteLine(square(5)); // Output: 25

// Multiple parameters
Func<int, int, int> add = (x, y) => x + y;
Console.WriteLine(add(3, 4)); // Output: 7

// Statement block
Action<int> printMultiple = x =>
{
    Console.WriteLine($"Number: {x}");
    Console.WriteLine($"Square: {x * x}");
};
printMultiple(3); // Output: Number: 3, Square: 9

List<int> list = new List<int> { 1, 2, 4, 6, 767, 6876, 87, 8 };

var filtered = list.Where(n => n % 2 == 0);

System.Console.WriteLine(string.Join(",", filtered));

var numbers = new List<int> { 1, 2, 3 };
var query = numbers.Where((x => x > 1));
numbers.Add(4);
Console.WriteLine(string.Join(", ", query));

List<Action> actions = new List<Action>();
for (int i = 0; i < 3; i++)
{
    actions.Add(() => Console.WriteLine(i));
}
foreach (var action in actions) action();

System.Console.WriteLine(5.IsEven());