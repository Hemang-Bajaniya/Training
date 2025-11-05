# Notes on Collection Library in C# (.NET): Collections

---

## Collections Module
The **Collections** module, primarily in the `System.Collections`, `System.Collections.Generic`, and `System.Collections.Concurrent` namespaces, offers a variety of data structures to store and manage data efficiently. These collections are designed to handle different use cases, from simple lists to thread-safe concurrent collections.

### Key Features
- Provides a range of data structures like lists, arrays, dictionaries, queues, stacks, and sets.
- Supports generic collections for type safety and better performance.
- Includes non-generic collections for backward compatibility (less commonly used in modern .NET).
- Offers thread-safe collections for concurrent programming.
- Provides interfaces like `IEnumerable`, `IList`, and `IDictionary` for standardized collection operations.

### Key Collection Types
- **Generic Collections** (`System.Collections.Generic`):
  - `List<T>`: A dynamic, resizable array that supports adding, removing, and indexing elements.
  - `Dictionary<TKey, TValue>`: A key-value pair collection for fast lookups by key.
  - `Queue<T>`: A first-in, first-out (FIFO) collection.
  - `Stack<T>`: A last-in, first-out (LIFO) collection.
  - `HashSet<T>`: A collection of unique elements with fast lookup and set operations.
  - `SortedList<TKey, TValue>`: A sorted collection of key-value pairs.
  - `SortedDictionary<TKey, TValue>`: A dictionary that maintains keys in sorted order.
- **Non-Generic Collections** (`System.Collections`):
  - `ArrayList`: A non-type-safe, dynamic list (less common in modern .NET).
  - `Hashtable`: A non-type-safe key-value pair collection.
  - `Queue` and `Stack`: Non-generic FIFO and LIFO collections.
- **Concurrent Collections** (`System.Collections.Concurrent`):
  - `ConcurrentDictionary<TKey, TValue>`: A thread-safe dictionary for concurrent access.
  - `ConcurrentQueue<T>`: A thread-safe FIFO queue.
  - `ConcurrentStack<T>`: A thread-safe LIFO stack.
  - `ConcurrentBag<T>`: A thread-safe, unordered collection of items.
  - `BlockingCollection<T>`: A thread-safe collection for producer-consumer scenarios.

### Key Interfaces
- `IEnumerable<T>`: Enables iteration over a collection using `foreach`.
- `IList<T>`: Provides methods for indexed access and manipulation (e.g., `List<T>`).
- `IDictionary<TKey, TValue>`: Supports key-value pair operations (e.g., `Dictionary<TKey, TValue>`).
- `ISet<T>`: Defines set operations like union and intersection (e.g., `HashSet<T>`).
- `ICollection<T>`: Provides basic collection operations like adding, removing, and counting items.

### Common Operations
- **Adding Elements**: Use methods like `Add`, `Enqueue`, or `Push` depending on the collection type.
- **Removing Elements**: Use methods like `Remove`, `Dequeue`, or `Pop`.
- **Iterating**: Use `foreach` with `IEnumerable<T>` or LINQ for querying and filtering.
- **Searching**: Use methods like `Contains`, `Find`, or `TryGetValue` for lookups.
- **Sorting**: Use `Sort` (e.g., in `List<T>`) or rely on sorted collections like `SortedList<TKey, TValue>`.

### Best Practices
- Prefer **generic collections** (`System.Collections.Generic`) over non-generic ones for type safety and performance.
- Use the appropriate collection type based on the use case:
  - `List<T>` for dynamic lists with frequent additions/removals.
  - `Dictionary<TKey, TValue>` for fast key-based lookups.
  - `HashSet<T>` for unique elements and set operations.
  - `Queue<T>` or `Stack<T>` for FIFO/LIFO scenarios.
- Use **concurrent collections** for multi-threaded applications to ensure thread safety.
- Leverage **LINQ** for querying and manipulating collections efficiently.
- Avoid modifying a collection while iterating over it to prevent exceptions (e.g., `InvalidOperationException`).
- Pre-size collections (e.g., set initial capacity for `List<T>`) to minimize resizing overhead.

### Performance Considerations
- **List<T>`: O(1) for indexed access, O(n) for searching or removing elements.
- **Dictionary<TKey, TValue>`: O(1) average case for lookups, additions, and removals.
- **HashSet<T>`: O(1) average case for lookups and set operations.
- **SortedList<TKey, TValue>`: O(log n) for insertions, O(1) for indexed access.
- **Concurrent Collections**: Slightly slower than non-concurrent counterparts due to thread-safety overhead but essential for multi-threaded scenarios.

---

## Summary
The **Collections** module in the .NET BCL provides a versatile set of data structures for managing and manipulating data. Generic collections offer type safety and performance, while concurrent collections support thread-safe operations in multi-threaded applications. Choosing the right collection type based on the use case and following best practices ensures efficient and maintainable code.

**References**:
- [Microsoft Docs: System.Collections.Generic](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic)
- [Microsoft Docs: System.Collections.Concurrent](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent)