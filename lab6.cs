using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace lab6
{
    public interface IPerson
    {
        string surname { get; set; }
        string initials { get; set; }
        short year { get; set; }
        double salary { get; set; }
    }

    class Person : IPerson, IComparable<Person>
    {
        public string surname { get; set; }
        public string initials { get; set; }
        public short year { get; set; }
        public double salary { get; set; }
        string IPerson.surname { get; set; }
        string IPerson.initials { get; set; }
        short IPerson.year { get; set; }
        double IPerson.salary { get; set; }

        public Person(string surname, string initials, short year, double salary)
        {
            this.surname = surname;
            this.initials = initials;
            this.year = year;
            this.salary = salary;
        }

        public string Info
        {
            get { return $"{surname} {initials}"; }
        }

        public int CompareTo(Person other)
        {
            return string.Compare(other.Info, Info, StringComparison.InvariantCultureIgnoreCase);
        }

        public override string ToString()
        {
            return String.Format("{0,20}|{1,20}|{2,20}|{3,20}", surname, initials, year, salary);
        }
    }

    class CollectionType<T> : IEnumerable<T> where T : Person
    {
        List<T> list = new List<T>();

        public CollectionType()
        {
            list = new List<T>();
        }

        public int Count
        {
            get { return list.Count; }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return list[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }
                list[index] = value;
            }
        }

        public void Add(T person)
        {
            list.Add(person);
        }

        public T Remove(T person)
        {
            var element = list.FirstOrDefault(h => h == person);
            if (element != null)
            {
                list.Remove(element);
                return element;
            }
            throw new NullReferenceException();
        }

        public void Sort()
        {
            list.Sort();
        }

        public T GetByName(string surname)
        {
            return
            list.FirstOrDefault(
            h => string.Compare(h.Info, surname, StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class CollectionType
    {
        List<CollectionType> link = new List<CollectionType>();
        string surname { get; set; }
        string initials { get; set; }
        short year { get; set; }
        double salary { get; set; }

        public CollectionType() { }

        public CollectionType(string surname, string initials, short year, double salary)
        {
            this.surname = surname;
            this.initials = initials;
            this.year = year;
            this.salary = salary;
        }

        public void Add(CollectionType[] coll)
        {
            for (int i = 0; i < coll.Length; i++)
            {
                link.Add(coll[i]);
            }
        }

        public void Output()
        {
            Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", "Прiзвище ", "Iнiцiали ", "Рiк народження ", "Оклад"));
            foreach (CollectionType s in link)
            {
                Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", s.surname, s.initials, s.year, s.salary));
            }
        }

        public void Select()
        {
            Console.WriteLine("\n                Запит 1:");
            var where = link.Where(h => (h.year >= 80 && h.salary > 1000));
            Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", "Прiзвище ", "Iнiцiали ", "Рiк народження ", "Оклад"));
            foreach (var c in where)
            {
                Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", c.surname, c.initials, c.year, c.salary));
            }
            Console.WriteLine("\n                Запит 2:");
            var min = link.Min(h => h.salary);
            Console.WriteLine($"                {min}");
            Console.WriteLine("\n                Запит 3:");
            var max = link.Max(h => h.salary);
            Console.WriteLine($"                {max}");
            Console.WriteLine("\n                Запит 4:");
            var count = link.Count();
            Console.WriteLine($"                {count}");
            Console.WriteLine("\n                Запит 5:");
            var order = link.OrderBy(h => h.year).ThenByDescending(h => h.surname);
            Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", "Прiзвище ", "Iнiцiали ", "Рiк народження ", "Оклад"));
            foreach (var c in order)
            {
                Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", c.surname, c.initials, c.year, c.salary));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person sgn1 = new Person("Iванов", "I.I.", 1975, 517.50);
            Person sgn2 = new Person("Петренко", "П.П.", 1956, 219.10);
            Person sgn3 = new Person("Панiковський", "М.С.", 1967, 300.00);

            CollectionType<Person> collection = new CollectionType<Person>();
            collection.Add(sgn1);
            collection.Add(sgn2);
            collection.Add(sgn3);

            Console.WriteLine(String.Format("{0,20}|{1,20}|{2,20}|{3,20}", "Прiзвище ", "Iнiцiали ", "Рiк народження ", "Оклад"));
            foreach (Person s in collection)
            {
                Console.WriteLine(s.ToString());
            }

            Person sgn5 = new Person("Iванов", "I.I.", 1975, 517.50);
            Person sgn6 = new Person("Петренко", "П.П.", 1956, 219.10);
            Person sgn7 = new Person("Панiковський", "М.С.", 1967, 300.00);

            CollectionType<Person> collection2 = new CollectionType<Person>();
            collection.Add(sgn5);
            collection.Add(sgn6);
            collection.Add(sgn7);

            var list = new List<CollectionType<Person>>();
            list.Add(collection);
            list.Add(collection2);

            Console.WriteLine("\n                OrderBy:");
            var order = collection.OrderBy(h => h.year).ThenBy(h => h.surname);
            foreach (var signal in order)
            {
                Console.WriteLine(signal);
            }
            Console.WriteLine("\n                where:");
            var where = collection.Where(h => (h.year >= 100 && h.salary > 1450) || h.Info.StartsWith("L"));
            foreach (var signal in where)
            {
                Console.WriteLine(signal.ToString());
            }
            Console.WriteLine("\n                Select:");
            var select = collection.Select((h, i) => new { Index = i + 1, h.Info });
            foreach (var s in select)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("\n                Skip:");
            var skip = collection.Skip(3);
            foreach (var signal in skip)
            {
                Console.WriteLine(signal);
            }
            Console.WriteLine("\n                Take:");
            var take = collection.Take(3);
            foreach (var signal in take)
            {
                Console.WriteLine(signal);
            }
            Console.WriteLine("\n                Concat:");
            var concat = collection.Concat(collection2);
            foreach (var signal in concat)
            {
                Console.WriteLine(signal);
            }
            Console.WriteLine("\n                First:");
            var first = collection.First(h => h.Info.Length > 5);
            Console.WriteLine(first);
            Console.Write("\n                Min: ");
            var min = collection.Min(h => h.salary);
            Console.WriteLine(min);
            Console.Write("\n                Max: ");
            var max = collection.Max(h => h.salary);
            Console.WriteLine(max);
            Console.WriteLine("\nAll and Any:");
            var allAny = list.First(c => c.All(h => h.year >= 14) && c.Any(h => h is Person)).Select(h => h.Info).OrderByDescending(s => s);
            foreach (var str in allAny)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine("\nContains:");
            var contains = list.Where(c => c.Contains(sgn1)).SelectMany(c => c.SelectMany(h => h.Info.Split(' '))).Distinct().OrderBy(s => s).ToList();
            foreach (var str in contains)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine();

            CollectionType ct = new CollectionType();

            CollectionType[] collections = {
                new CollectionType("Iванов", "I.I.", 1975, 517.50),
                new CollectionType("Петренко", "П.П.", 1956, 219.10),
                new CollectionType("Панiковський", "М.С.", 1967, 300.00)
            };

            ct.Add(collections);
            ct.Output();
            ct.Select();

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}