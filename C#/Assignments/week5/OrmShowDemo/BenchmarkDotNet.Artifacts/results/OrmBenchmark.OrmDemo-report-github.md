```

BenchmarkDotNet v0.15.5, Windows 11 (10.0.26100.6899/24H2/2024Update/HudsonValley)
Intel Core i5-1035G1 CPU 1.00GHz (Max: 1.19GHz), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 9.0.1 (9.0.1, 9.0.124.61010), X64 RyuJIT x86-64-v4
  DefaultJob : .NET 9.0.1 (9.0.1, 9.0.124.61010), X64 RyuJIT x86-64-v4


```
| Method       | Mean       | Error     | StdDev    |
|------------- |-----------:|----------:|----------:|
| EfCoreQuery  | 6,928.9 μs | 132.76 μs | 214.38 μs |
| DapperQuery  |   687.8 μs |  12.89 μs |  22.91 μs |
| AdoNet_Query |   722.5 μs |  14.41 μs |  31.32 μs |
