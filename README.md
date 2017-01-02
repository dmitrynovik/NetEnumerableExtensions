# NetEnumerableExtensions

This is a small set of enumerable extensions which, in my opinion, are handy when working with enumerables and collections to make your code more fluent and less boilerplate.

## Fluent conversions

Convert your enumerable into more types than just Array or List
```
var set = items.ToHashSet();
```

## Fluent conversions to thread-safe equivalent
```
var concurrentQueue = queue.AsConcurrent();
```

## Dictionaries: one line retrieval
Avoid redundant call of .Contains(key) to prevent exception
```
var value = dictionary.GetOrDefault(key); // returns value | default(TValue)
```

## Each iterator on any enumerable (not just List<T>)

```
        collection.Each(x =>
        {
            // Do something
        });

```

## Avoid tedious NULL checks in your code
All methods are null-safe and accept null as input if called from code where non-initialized collections are possible, 
e.g. there is no need to write checks to avoid crash on nulls.
```
if (something == null) // litter!
```
The behavior on input nulls is same as on empty sets - no action executed or empty enumerator is being returned.
```
ICollection<int> bugger = null;
bugger.Each(x =>
{
    // This is safe 
});

```
