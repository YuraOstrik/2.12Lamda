using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace _2._12LAMDA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //  --- --  1 Task //
            //string Text = "We use both first and third-party cookies to personalise web content";
            //string A = "use";
            //static bool Word(string Text, string A) => Text.Contains(A);
            //Console.WriteLine(Word(Text, A));

            var items = new List<Item>();
            var capacity = new Capacity(100, items, 0);
            var backpack = new Backpack("Black", "nike", "Nylon", 2, capacity);
            backpack.Event += (sender, e) =>
            {
                Console.WriteLine($"{e.item.Name} {e.Message} {e.capacity}");
            };

            var item1 = new Item("Laptop", 30);
            var item2 = new Item("Book", 40);
            var item3 = new Item("Shoes", 50);

            backpack.Put(item1);
            backpack.Put(item2);
            backpack.Put(item3);

            backpack.Take(item1);
            Console.WriteLine(backpack);

            // 3 Task
            //int[] mass = { 1, 2, 3, 4, -5 };
            //int num = 0;
            //Func<int[], int> Pos = arr =>
            //{
            //    for (int i = 0; i < arr.Length; i++)
            //    {
            //        if (arr[i] > 0) num++;
            //    }
            //    return num;
            //};
            //int pos = Pos(mass);
            //Console.WriteLine(pos);
        }
    }
    public class Item
    {
        public string Name { get; set; }
        public int Valume { get; set; }
        public Item(string name, int valume)
        {
            Name = name;
            Valume = valume;
        }
        public override string ToString()
        {
            return $"{Name} | {Valume}";
        }
    }

    public class Capacity
    {
        public List<Item> L_item;
        public int MaxVal { get; set; }
        public int Valume { get; set; }
        public Capacity(int maxVal, List<Item> l_item, int valume)
        {
            MaxVal = maxVal;
            L_item = l_item;
            Valume = valume;
        }
        public bool Add(Item item)
        {
            if(Valume + item.Valume > MaxVal)
            {
                return false;
            }
            L_item.Add(item);
            Valume += item.Valume;
            return true;
        }
        public bool Remove(Item item)
        {
            if (!(L_item.Contains(item)))
            {
                return false;
            }
            L_item.Remove(item);
            Valume -= item.Valume;
            return true;
        }
        public override string ToString()
        {
            var Description = string.Join(", ", L_item.Select(i => i.ToString()));
            return $"Capacity: {Valume}/{MaxVal}, Items: [{Description}]";
        }
    }
    public class Backpack
    {
        public event EventHandler<BackpackEventAct>? Event;
        public string Colour {  get; set; }
        public string Firm { get; set; }
        public string Textile {  get; set; }
        public int Weight {  get; set; }
        public Capacity capacity{  get; set; }
        public Backpack(string colour, string firm,string textile, int weight, Capacity capacity)
        {
            Colour = colour;
            Firm = firm;
            Textile = textile;
            Weight = weight;
            this.capacity = capacity;
        }

        public void Put(Item item)
        {
            if(capacity.Add(item))
            {
                Event?.Invoke(this, new BackpackEventAct(item," - был добавлен в рюкзак, сейчас он выглядит так: ", capacity));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No---no---no---no---non");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public void Take(Item item)
        {
            if (capacity.Remove(item))
            {
                Event?.Invoke(this, new BackpackEventAct(item, " - был удален с рюкзака, сейчас он выглядит так: ", capacity));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[[][][[][][][][][][][][][]");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public override string ToString()
        {
            return $"Backpack: Colour: {Colour}, Firm: {Firm}, Textile: {Textile}, Weight: {Weight}kg, {capacity}";
        }
    }
    public class BackpackEventAct : EventArgs
    {
        public string? Message { get; set; }
        public Item item { get; set; }
        public Capacity capacity {  get; set; }
        public BackpackEventAct(Item item, string? message, Capacity capacity)
        {
            this.item = item;
            Message = message;
            this.capacity = capacity;
        }
    }


}
