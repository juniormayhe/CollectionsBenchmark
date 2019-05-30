# CollectionsBenchmark

Small list: 100 elements
Large list: 5000 elements


### Lists

- Sized lists are slighter faster than unplanned lists
- Unplanned lists allocate more memory

```
BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.503 (1809/October2018Update/Redstone5)
Intel Core i7-3632QM CPU 2.20GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview3-010431
  [Host]     : .NET Core 2.2.3 (CoreCLR 4.6.27414.05, CoreFX 4.6.27414.05), 64bit RyuJIT  [AttachedDebugger]
  DefaultJob : .NET Core 2.2.3 (CoreCLR 4.6.27414.05, CoreFX 4.6.27414.05), 64bit RyuJIT

|                                Method |       Mean |      Error |     StdDev |  Ratio | RatioSD | Rank |    Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|-------------------------------------- |-----------:|-----------:|-----------:|-------:|--------:|-----:|---------:|---------:|---------:|----------:|
|      Small_Dictionary_PlannedCapacity |   2.846 us |  0.0552 us |  0.0489 us |   0.58 |    0.01 |    1 |   3.5362 |        - |        - |  10.87 KB |
|      Small_Dictionary_DynamicCapacity |   4.914 us |  0.0585 us |  0.0519 us |   1.00 |    0.00 |    2 |   5.7755 |        - |        - |  17.77 KB |
|      Large_Dictionary_PlannedCapacity | 503.861 us | 10.6161 us | 25.2304 us | 100.04 |    2.79 |    3 |  95.7031 |  52.2461 |  43.4570 | 550.41 KB |
| Large_Dictionary_BelowPlannedCapacity | 522.801 us |  6.6485 us |  5.8937 us | 106.40 |    1.84 |    4 | 166.0156 | 110.3516 | 110.3516 | 917.89 KB |
|      Large_Dictionary_DynamicCapacity | 562.753 us | 10.7677 us | 10.5753 us | 114.55 |    2.51 |    5 | 151.3672 |  90.8203 |  90.8203 | 831.47 KB |
```

### Dictionaries

- Sized dictionaries are faster than unplanned dictionaries
- Unplanned dictionaries allocates more memory

```
|                                Method |       Mean |      Error |     StdDev |  Ratio | RatioSD | Rank |    Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|-------------------------------------- |-----------:|-----------:|-----------:|-------:|--------:|-----:|---------:|---------:|---------:|----------:|
|      Small_Dictionary_PlannedCapacity |   2.846 us |  0.0552 us |  0.0489 us |   0.58 |    0.01 |    1 |   3.5362 |        - |        - |  10.87 KB |
|      Small_Dictionary_DynamicCapacity |   4.914 us |  0.0585 us |  0.0519 us |   1.00 |    0.00 |    2 |   5.7755 |        - |        - |  17.77 KB |
|      Large_Dictionary_PlannedCapacity | 503.861 us | 10.6161 us | 25.2304 us | 100.04 |    2.79 |    3 |  95.7031 |  52.2461 |  43.4570 | 550.41 KB |
| Large_Dictionary_BelowPlannedCapacity | 522.801 us |  6.6485 us |  5.8937 us | 106.40 |    1.84 |    4 | 166.0156 | 110.3516 | 110.3516 | 917.89 KB |
|      Large_Dictionary_DynamicCapacity | 562.753 us | 10.7677 us | 10.5753 us | 114.55 |    2.51 |    5 | 151.3672 |  90.8203 |  90.8203 | 831.47 KB |
```

## Arrays

- Allocate arrays with ArrayPool shared buffer is faster than in regular arrays
- Allocate small (or large) primitive (or reference) type arrays with ArrayPool shared buffer are fast and have similar performance
- Allocate larger arrays with ArrayPool are faster than regular array
- Allocate small (or large) primitive type arrays with StackAlloc or Span are faster

```
|                              Method |          Mean |      Error |     StdDev |        Median |  Ratio | RatioSD | Rank |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------ |--------------:|-----------:|-----------:|--------------:|-------:|--------:|-----:|--------:|------:|------:|----------:|
|    Large_Primitive_UnsafeStackAlloc |     0.0000 ns |  0.0000 ns |  0.0000 ns |     0.0000 ns |  0.000 |    0.00 |    1 |       - |     - |     - |         - |
|    Small_Primitive_UnsafeStackAlloc |     0.0055 ns |  0.0065 ns |  0.0058 ns |     0.0042 ns |  0.000 |    0.00 |    2 |       - |     - |     - |         - |
|      Small_Primitive_SpanStackAlloc |     1.3331 ns |  0.0397 ns |  0.0371 ns |     1.3331 ns |  0.035 |    0.00 |    3 |       - |     - |     - |         - |
|      Large_Primitive_SpanStackAlloc |     6.3109 ns |  0.0575 ns |  0.0510 ns |     6.3217 ns |  0.167 |    0.00 |    4 |       - |     - |     - |         - |
|               Small_Primitive_Array |    37.7743 ns |  0.3208 ns |  0.2679 ns |    37.7637 ns |  1.000 |    0.00 |    5 |  0.1348 |     - |     - |     424 B |
|     Large_Primitive_SharedArrayPool |    47.5122 ns |  0.3284 ns |  0.2911 ns |    47.5025 ns |  1.256 |    0.01 |    6 |       - |     - |     - |         - |
| Small_ReferenceType_SharedArrayPool |    52.2805 ns |  0.7781 ns |  0.6498 ns |    52.1534 ns |  1.384 |    0.02 |    7 |       - |     - |     - |         - |
| Large_ReferenceType_SharedArrayPool |    53.0677 ns |  0.4761 ns |  0.4221 ns |    52.9884 ns |  1.406 |    0.02 |    7 |       - |     - |     - |         - |
|     Small_Primitive_SharedArrayPool |    58.9349 ns |  1.1926 ns |  1.1155 ns |    58.7923 ns |  1.562 |    0.03 |    8 |       - |     - |     - |         - |
|           Small_ReferenceType_Array |    80.7801 ns |  1.7554 ns |  1.8027 ns |    80.2140 ns |  2.142 |    0.06 |    9 |  0.2619 |     - |     - |     824 B |
|               Large_Primitive_Array | 1,581.4661 ns | 12.9802 ns | 11.5066 ns | 1,576.3062 ns | 41.847 |    0.39 |   10 |  6.3686 |     - |     - |   20024 B |
|           Large_ReferenceType_Array | 3,248.0881 ns | 32.1773 ns | 30.0987 ns | 3,244.3077 ns | 86.084 |    0.84 |   11 | 12.6572 |     - |     - |   40024 B |
```