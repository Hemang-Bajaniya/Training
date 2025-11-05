# OOP Concepts in C# — Notes

This file summarizes core Object-Oriented Programming (OOP) concepts in C# with short explanations and compact examples.

---

## 1. Class & Object
- Class: blueprint / type that defines data (fields/properties) and behavior (methods).
- Object: instance of a class.

Example:
```csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}
var p = new Person { Name = "A", Age = 30 }; // object
```

---

## 2. Encapsulation
- Hide internal state and expose controlled access via properties or methods.
- Use access modifiers (public, private, protected, internal).

Example:
```csharp
public class BankAccount
{
    private decimal balance;
    public decimal Balance => balance; // read-only property

    public void Deposit(decimal amount)
    {
        if (amount > 0) balance += amount;
    }
}
```

---

## 3. Abstraction
- Present a simplified interface and hide implementation details.
- Achieved using abstract classes and interfaces.

Abstract class example:
```csharp
public abstract class Shape
{
    public abstract double Area();
}
public class Circle : Shape
{
    public double Radius { get; set; }
    public override double Area() => Math.PI * Radius * Radius;
}
```

Interface example:
```csharp
public interface ILogger
{
    void Log(string message);
}
public class ConsoleLogger : ILogger
{
    public void Log(string message) => Console.WriteLine(message);
}
```

---

## 4. Inheritance
- Derived class reuses/extends base class members.
- Single class inheritance; multiple inheritance via interfaces.

Example:
```csharp
public class Animal { public void Eat() { } }
public class Dog : Animal { public void Bark() { } }
```

---

## 5. Polymorphism
- Same operation behaves differently on different types.

Compile-time (overloading):
```csharp
public class Calc
{
    public int Add(int a, int b) => a + b;
    public double Add(double a, double b) => a + b;
}
```

Runtime (overriding):
```csharp
public class Animal { public virtual string Speak() => "..." ; }
public class Dog : Animal { public override string Speak() => "Woof"; }

Animal a = new Dog();
Console.WriteLine(a.Speak()); // prints "Woof"
```

---

## 6. Abstract Classes vs Interfaces
- Abstract class: can contain implementation, fields, constructors; use for closely related types.
- Interface: only contracts (C# 8+ allows default implementations); supports multiple inheritance.

---

## 7. Constructors, Destructors (Finalizers) & Static
- Constructors initialize state. Can be parameterless or parameterized.
- Finalizer (~ClassName) for unmanaged cleanup — prefer IDisposable.
- static members belong to the type, not instance.

Example:
```csharp
public class Resource : IDisposable
{
    public Resource() { /* init */ }
    public void Dispose() { /* cleanup */ }
}
```

---

## 8. Properties, Indexers, and Auto-properties
- Properties encapsulate getters/setters.
- Indexers let objects be indexed like arrays.

Example:
```csharp
public class Sample
{
    public int X { get; set; } // auto-property
    private int[] arr = new int[10];
    public int this[int i] { get => arr[i]; set => arr[i] = value; } // indexer
}
```

---

## 9. Access Modifiers (quick)
- public, private, protected, internal, protected internal, private protected.
- Control visibility across classes, derived types, and assemblies.

---

## 10. Sealed, Virtual, Override, Abstract, Partial
- virtual: allow overriding.
- override: provide new behavior.
- sealed: prevent inheritance (on class) or prevent further override (on method).
- partial: split class across files (same assembly).

Example:
```csharp
public sealed class FinalClass { } // cannot inherit
public class Base { public virtual void M() {} }
public class Derived : Base { public sealed override void M() {} } // can't override further
```

---

## 11. Interfaces + Dependency Injection (DI) — short note
- Program to interface, not implementation.
- DI uses interfaces for loose coupling and testability.

---

## 12. Delegates & Events (behavioral)
- Delegates are type-safe function pointers.
- Events use delegates to implement the observer pattern.

Example:
```csharp
public delegate void Notify(string message);
public class Broadcaster
{
    public event Notify OnNotify;
    public void Raise() => OnNotify?.Invoke("hi");
}
```

---

## 13. Generics (type-safe reusable code)
- Create classes/methods that work with any type.

Example:
```csharp
public class Repository<T>
{
    private List<T> items = new();
    public void Add(T item) => items.Add(item);
}
```

---

## 14. Best Practices
- Favor interfaces for extensibility and testability.
- Keep classes focused (Single Responsibility).
- Encapsulate state; prefer properties over public fields.
- Use virtual/override sparingly and document virtual methods.
- Avoid exposing mutable internal collections directly.
- Prefer IDisposable + using for unmanaged resources.

---

## 15. Further Reading / Keywords
- SOLID principles, Design Patterns, Composition over Inheritance, Dependency Injection, Unit Testing with mock interfaces.

--- 

