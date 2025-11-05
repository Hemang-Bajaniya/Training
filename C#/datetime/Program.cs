// DateTime now = DateTime.Now;
// Console.WriteLine(now);

// DateTime today = DateTime.Today;
// Console.WriteLine(today);

// DateTime dob = new DateTime(1995, 12, 15);
// Console.WriteLine(dob);

// System.Console.WriteLine(now.Year);
// System.Console.WriteLine(now.Month);

// System.Console.WriteLine(now.AddDays(10));
// System.Console.WriteLine(now.AddHours(4));

// Console.WriteLine($"{now:dd-MM-yyyy}");
// Console.WriteLine($"{now:MMMM dd, yyyy}");
// Console.WriteLine($"{now:HH:mm:ss}");

// (year, month, day, h, m, s)
var d = new DateTime(2010, 12, 01, 21, 2, 3);
System.Console.WriteLine(d);

// hh -hour
// HH - 24 format
// mm - mins
// MM - month
System.Console.WriteLine($"{d:hh:MM:yyyy}");

System.Console.WriteLine(d.ToLongTimeString());
System.Console.WriteLine(d.ToShortTimeString());
System.Console.WriteLine(d.ToShortDateString());
System.Console.WriteLine(d.ToLongDateString());