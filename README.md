# NetEnumerableExtensions

A set of enumerable extensions which are handy when working with enumerables and collections to make code more fluent, less boilerplate.

## Fluent conversions

One line conversions into more collection types than just Array or List
```
var set = items.ToHashSet();
```

## Fluent conversions to thread-safe equivalent
```
var concurrentQueue = queue.AsConcurrent();
```

## Dictionaries: one line safe retrieval
No need to call of .Contains(key) to prevent KeyNotFoundException. No need to do TryGetValue either - it takes a few lines. Get back NULL or any other default(TValue) if the key is missing in a single call - and in most cases that's exactly what you need
```
var value = dictionary.GetOrDefault(key); // returns value | default(TValue)
```

## Execute action on every item 
Equivalent of List.ForEach, but accepts any enumerable
```
        collection.Each(x =>
        {
            // Do something with x
        });

```

## Finally, the extensions are null safe

The .NET Framework implementation does not like NULLs. Most likely, you'll get an exception. Hence, checking the input becomes your routine  responsibility.

While having unitialized variables is a BAD idea, and NULL is a billion dollar design mistake, the typical real world situation where the input comes from anywhere and could be anything requires writing defensive code like this:
```
// 1000s null checks litter the code but are necessary to survive on unpredictable input
if (something == null) 
{
   // bail
}
else
{
  // do something useful
}
// Could we do any better?
```
All methods in this repository are null-safe and accept nulls as legit input, e.g. there is no need to write checks to prevent null reference exceptions.

The behavior on input nulls is same as on empty sets - no action executed or empty enumerator is being returned.
```
ICollection<int> bugger = null;

bugger.Each(x =>
{
    // This is safe 
});


bugger.Filter(x => x > 5);  // This is safe 

```
