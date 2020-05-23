# CollectionsBenchmark

Small list: 100 elements
Large list: 5000 elements


### Lists

- Sized lists are slighter faster than unplanned lists
- Unplanned lists allocate more memory

```
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.836 (1909/November2018Update/19H2)
AMD Ryzen 5 1600X, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.100-preview.4.20258.7
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.25106, CoreFX 5.0.20.25106), X64 RyuJIT
  DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.25106, CoreFX 5.0.20.25106), X64 RyuJIT


|                          Method |        Mean |       Error |      StdDev | Ratio | RatioSD | Rank |   Gen 0 |   Gen 1 | Gen 2 | Allocated |
|-------------------------------- |------------:|------------:|------------:|------:|--------:|-----:|--------:|--------:|------:|----------:|
|      Small_List_PlannedCapacity |    829.8 ns |    15.40 ns |    20.03 ns |  0.69 |    0.02 |    1 |  2.1172 |       - |     - |   8.65 KB |
|      Small_List_DynamicCapacity |  1,217.2 ns |     5.58 ns |     4.95 ns |  1.00 |    0.00 |    2 |  2.4357 |       - |     - |   9.95 KB |
|      Large_List_PlannedCapacity | 60,526.5 ns | 1,190.68 ns | 1,274.01 ns | 49.68 |    1.25 |    3 | 79.3457 | 27.5879 |     - | 429.74 KB |
| Large_List_BelowPlannedCapacity | 87,090.9 ns |   978.17 ns |   816.82 ns | 71.57 |    0.63 |    4 | 93.5059 | 40.8936 |     - | 490.04 KB |
|      Large_List_DynamicCapacity | 92,385.2 ns |   792.93 ns |   662.14 ns | 75.92 |    0.54 |    5 | 95.0928 | 47.4854 |     - | 518.91 KB |
```

### Dictionaries

- Sized dictionaries are faster than unplanned dictionaries
- Unplanned dictionaries allocates more memory

```
|                                Method |       Mean |     Error |    StdDev |  Ratio | RatioSD | Rank |    Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|-------------------------------------- |-----------:|----------:|----------:|-------:|--------:|-----:|---------:|---------:|---------:|----------:|
|      Small_Dictionary_PlannedCapacity |   1.500 us | 0.0299 us | 0.0492 us |   0.61 |    0.02 |    1 |   2.6588 |        - |        - |  10.87 KB |
|      Small_Dictionary_DynamicCapacity |   2.520 us | 0.0333 us | 0.0295 us |   1.00 |    0.00 |    2 |   4.3449 |        - |        - |  17.77 KB |
|      Large_Dictionary_PlannedCapacity | 318.795 us | 6.2616 us | 5.8571 us | 126.64 |    2.74 |    3 |  86.9141 |  43.4570 |  43.4570 | 550.41 KB |
| Large_Dictionary_BelowPlannedCapacity | 334.271 us | 5.0805 us | 4.2425 us | 132.62 |    1.66 |    4 | 166.5039 | 110.8398 | 110.8398 | 917.89 KB |
|      Large_Dictionary_DynamicCapacity | 354.500 us | 7.0372 us | 7.8218 us | 140.04 |    3.22 |    5 | 151.3672 |  90.8203 |  90.8203 | 831.47 KB |
```

## Arrays

- Allocate arrays with ArrayPool shared buffer is faster than in regular arrays
- Allocate small (or large) primitive (or reference) type arrays with ArrayPool shared buffer are fast and have similar performance
- Allocate larger arrays with ArrayPool are faster than regular array
- Allocate small (or large) primitive type arrays with StackAlloc or Span are faster

```
|                              Method |        Mean |      Error |     StdDev |      Median |  Ratio | RatioSD | Rank |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------------ |------------:|-----------:|-----------:|------------:|-------:|--------:|-----:|-------:|------:|------:|----------:|
|    Small_Primitive_UnsafeStackAlloc |   0.0001 ns |  0.0005 ns |  0.0004 ns |   0.0000 ns |  0.000 |    0.00 |    1 |      - |     - |     - |         - |
|    Large_Primitive_UnsafeStackAlloc |   0.0008 ns |  0.0020 ns |  0.0016 ns |   0.0000 ns |  0.000 |    0.00 |    1 |      - |     - |     - |         - |
|      Small_Primitive_SpanStackAlloc |  12.9849 ns |  0.0264 ns |  0.0220 ns |  12.9946 ns |  0.793 |    0.00 |    2 |      - |     - |     - |         - |
|               Small_Primitive_Array |  16.3722 ns |  0.0751 ns |  0.0666 ns |  16.3944 ns |  1.000 |    0.00 |    3 | 0.1014 |     - |     - |     424 B |
|     Small_Primitive_SharedArrayPool |  21.1541 ns |  0.0402 ns |  0.0356 ns |  21.1599 ns |  1.292 |    0.00 |    4 |      - |     - |     - |         - |
|     Large_Primitive_SharedArrayPool |  21.2913 ns |  0.1885 ns |  0.1472 ns |  21.2548 ns |  1.301 |    0.01 |    4 |      - |     - |     - |         - |
| Large_ReferenceType_SharedArrayPool |  24.0799 ns |  0.1087 ns |  0.0908 ns |  24.0814 ns |  1.471 |    0.01 |    5 |      - |     - |     - |         - |
| Small_ReferenceType_SharedArrayPool |  24.3184 ns |  0.4524 ns |  0.4232 ns |  24.0522 ns |  1.486 |    0.02 |    5 |      - |     - |     - |         - |
|           Small_ReferenceType_Array |  29.8450 ns |  0.3949 ns |  0.3501 ns |  29.7395 ns |  1.823 |    0.03 |    6 | 0.1970 |     - |     - |     824 B |
|               Large_Primitive_Array | 530.2019 ns |  3.3555 ns |  2.9746 ns | 529.7078 ns | 32.385 |    0.20 |    7 | 4.7617 |     - |     - |   20024 B |
|      Large_Primitive_SpanStackAlloc | 679.9406 ns |  1.8343 ns |  1.6261 ns | 680.4270 ns | 41.531 |    0.18 |    8 |      - |     - |     - |         - |
|           Large_ReferenceType_Array | 990.5360 ns | 13.2352 ns | 11.0520 ns | 991.8528 ns | 60.523 |    0.83 |    9 | 9.5234 |     - |     - |   40024 B |
```