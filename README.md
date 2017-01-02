# NetEnumerableExtensions

This is a small set of enumerable extensions which, in my opinion, are very handy when working with enumerables and collections.

## Quick conversions

Convert your enumerable into Queue / Stack / Set (hash or sorted)
```
var set = items.ToHashSet();
```

## Quickly convert to thread-safe equivalent
```
var concurrentQueue = queue.AsConcurrent();
```

## No need to check if key is contained in the dictionary
```
var value = dictionary.GetOrDefault(key); // returns value | default(TValue)
```

## Avoid tedious NULL checks in your code
All methods are null-safe and accept null as input if called from code where non-initialized collections are possible, 
e.g. there is no need to write checks to avoid crash on nulls
```
if (something == null)
```
