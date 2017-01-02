using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NetEnumerableExtensions.Test
{
    [TestFixture]
    public class NetEnumerableExtensionsTest
    {
        [Test]
        public void Add_Many_Adds_All_Items()
        {
            var col = CreateEmptyCollection<int>();
            col.AddMany(new[] { 1, 2, 3 });
            Assert.AreEqual(3, col.Count);
        }

        [Test]
        public void To_HashSet_Has_No_Duplicates()
        {
            var set = new[] { 1, 1, 2, 3, 2 }.ToHashSet();
            Assert.AreEqual(set.Count, set.Distinct().Count());
        }

        [Test]
        public void To_SortedSet_Is_Correctly_Sorted()
        {
            var set = new[] { 1, 1, 2, 3, 2 }.ToSortedSet();
            Assert.IsTrue(set.SequenceEqual(new[] { 1, 2, 3 }));
        }

        [Test]
        public void Each_Iterator_Over_Null_Is_Safe()
        {
            // Does not do anything but does not throw NullReferenceException and hence we don't a null check:
            CreateNullCollection<int>().Each(Console.WriteLine);
        }

        [Test]
        public void Each_Iterator_Over_Collection_Handles_Every_Item()
        {
            var collection = new[] {1, 2, 3};
            var counter = 0;

            collection.Each(x =>
            {
                counter++;
            });

            Assert.AreEqual(3, counter);
        }

        [Test]
        public void Filter_Over_Null_Does_Not_Throw_NullReferenceException()
        {
            var it = CreateNullCollection<int>().Filter(x => x > 0);
            Assert.AreEqual(0, it.Count());
        }

        [Test]
        public void When_Dictionary_Has_Key_GetOrDefault_Returns_Value()
        {
            const int key = 1;
            const string value = "Some value";

            var dict = new Dictionary<int, string> { [key] = value };

            Assert.AreEqual(value, dict.GetOrDefault(key));
        }

        [Test]
        public void When_Dictionary_DoesNot_Contain_Key_GetOrDefault_Returns_Default()
        {
            var dict = new Dictionary<int, string>();

            Assert.AreEqual(default(string) /* null */, dict.GetOrDefault(1));
        }

        [Test]
        public void MapToMany_With_Input_Of_Size_5_And_Split_Size_2_Produces_3_Series()
        {
            var collection = new[] { 1, 2, 3, 4, 5 };
            var series = collection.MapToMany(3);
            Assert.AreEqual(2, series.Count());
        }

        private static ICollection<T> CreateEmptyCollection<T>()
        {
            return new List<T>();
        }

        private static ICollection<T> CreateNullCollection<T>()
        {
            return null;
        }
    }
}
