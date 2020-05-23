# CollectionsBenchmark

Small list: 100 elements
Large list: 5000 elements


### Lists

- Sized lists are slighter faster than unplanned lists
- Unplanned lists allocate more memory

```
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.836 (1909/November2018Update/19H2)
AMD Ryzen 5 1600X, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.102
  [Host]     : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT
  DefaultJob : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT


|                          Method |        Mean |       Error |      StdDev |      Median | Ratio | RatioSD | Rank |   Gen 0 |   Gen 1 | Gen 2 | Allocated |
|-------------------------------- |------------:|------------:|------------:|------------:|------:|--------:|-----:|--------:|--------:|------:|----------:|
|      Small_List_PlannedCapacity |    914.6 ns |    26.97 ns |    79.51 ns |    951.6 ns |  0.72 |    0.07 |    1 |  2.1172 |       - |     - |   8.65 KB |
|      Small_List_DynamicCapacity |  1,248.9 ns |    24.84 ns |    50.74 ns |  1,229.4 ns |  1.00 |    0.00 |    2 |  2.4357 |       - |     - |   9.95 KB |
|      Large_List_PlannedCapacity | 60,065.9 ns | 1,193.73 ns | 3,227.32 ns | 58,534.9 ns | 48.48 |    2.94 |    3 | 79.2847 | 27.5879 |     - | 429.74 KB |
| Large_List_BelowPlannedCapacity | 88,145.4 ns | 1,757.32 ns | 4,504.68 ns | 86,381.6 ns | 71.49 |    5.27 |    4 | 93.5059 | 40.8936 |     - | 490.04 KB |
|      Large_List_DynamicCapacity | 90,367.1 ns | 1,113.91 ns | 1,041.95 ns | 90,385.1 ns | 72.68 |    2.34 |    4 | 94.7266 | 47.2412 |     - | 518.91 KB |

```

### Dictionaries

- Sized dictionaries are faster than unplanned dictionaries
- Unplanned dictionaries allocates more memory

```
|                                Method |       Mean |     Error |     StdDev |     Median |  Ratio | RatioSD | Rank |    Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|-------------------------------------- |-----------:|----------:|-----------:|-----------:|-------:|--------:|-----:|---------:|---------:|---------:|----------:|
|      Small_Dictionary_PlannedCapacity |   1.424 us | 0.0220 us |  0.0205 us |   1.418 us |   0.50 |    0.01 |    1 |   2.6569 |        - |        - |  10.86 KB |
|      Small_Dictionary_DynamicCapacity |   2.864 us | 0.0550 us |  0.0734 us |   2.894 us |   1.00 |    0.00 |    2 |   4.3449 |        - |        - |  17.76 KB |
|      Large_Dictionary_DynamicCapacity | 306.047 us | 5.4974 us | 10.9788 us | 301.235 us | 108.60 |    5.64 |    3 | 121.5820 |  90.8203 |  90.8203 | 831.46 KB |
|      Large_Dictionary_PlannedCapacity | 308.570 us | 4.7542 us |  4.4471 us | 306.242 us | 107.66 |    3.38 |    4 |  86.9141 |  43.4570 |  43.4570 | 550.41 KB |
| Large_Dictionary_BelowPlannedCapacity | 337.269 us | 6.1279 us | 11.0498 us | 332.123 us | 119.04 |    5.19 |    5 | 166.5039 | 110.8398 | 110.8398 | 917.88 KB |

```

## Arrays

- Allocate arrays with ArrayPool shared buffer is faster than in regular arrays
- Allocate small (or large) primitive (or reference) type arrays with ArrayPool shared buffer are fast and have similar performance
- Allocate larger arrays with ArrayPool are faster than regular array
- Allocate small (or large) primitive type arrays with StackAlloc or Span are faster

```
|                              Method |          Mean |      Error |     StdDev |        Median |  Ratio | RatioSD | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------ |--------------:|-----------:|-----------:|--------------:|-------:|--------:|-----:|-------:|------:|------:|----------:|
|    Small_Primitive_UnsafeStackAlloc |     0.0011 ns |  0.0015 ns |  0.0014 ns |     0.0003 ns |  0.000 |    0.00 |    1 |      - |     - |     - |         - |
|    Large_Primitive_UnsafeStackAlloc |     0.0029 ns |  0.0041 ns |  0.0036 ns |     0.0010 ns |  0.000 |    0.00 |    1 |      - |     - |     - |         - |
|      Small_Primitive_SpanStackAlloc |    14.7070 ns |  0.0493 ns |  0.0461 ns |    14.6956 ns |  0.948 |    0.01 |    2 |      - |     - |     - |         - |
|               Small_Primitive_Array |    15.4964 ns |  0.1528 ns |  0.1276 ns |    15.4537 ns |  1.000 |    0.00 |    3 | 0.1014 |     - |     - |     424 B |
|     Large_Primitive_SharedArrayPool |    25.7820 ns |  0.0623 ns |  0.0583 ns |    25.7669 ns |  1.664 |    0.01 |    4 |      - |     - |     - |         - |
|     Small_Primitive_SharedArrayPool |    25.8728 ns |  0.0845 ns |  0.0749 ns |    25.8648 ns |  1.670 |    0.02 |    4 |      - |     - |     - |         - |
|           Small_ReferenceType_Array |    28.6989 ns |  0.4877 ns |  0.4562 ns |    28.5090 ns |  1.851 |    0.04 |    5 | 0.1970 |     - |     - |     824 B |
| Large_ReferenceType_SharedArrayPool |    29.6188 ns |  0.1101 ns |  0.0920 ns |    29.6278 ns |  1.911 |    0.02 |    6 |      - |     - |     - |         - |
| Small_ReferenceType_SharedArrayPool |    32.3660 ns |  0.0911 ns |  0.0761 ns |    32.3489 ns |  2.089 |    0.02 |    7 |      - |     - |     - |         - |
|               Large_Primitive_Array |   523.9022 ns |  4.1193 ns |  3.4398 ns |   523.3133 ns | 33.811 |    0.44 |    8 | 4.7617 |     - |     - |   20024 B |
|      Large_Primitive_SpanStackAlloc |   680.8935 ns |  1.0936 ns |  1.0230 ns |   680.7808 ns | 43.945 |    0.36 |    9 |      - |     - |     - |         - |
|           Large_ReferenceType_Array | 1,007.4567 ns | 16.2304 ns | 14.3878 ns | 1,003.7539 ns | 65.020 |    1.19 |   10 | 9.5234 |     - |     - |   40024 B |

```