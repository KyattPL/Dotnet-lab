namespace Zad3
{
    public class Zad3 
    {
        public static void Main(string[] args) 
        {
            int[] tab = {1, 2, 3, 4, 5};
            System.Console.WriteLine(System.Array.Exists(tab, x => x == 4));
            int[] evens = System.Array.FindAll(tab, x => x % 2 == 0);

            foreach (int num in evens) {
                System.Console.Write(num + " ");
            }
            System.Console.WriteLine();

            System.Console.WriteLine(System.Array.IndexOf(tab, 2));
            System.Array.Reverse(tab);
            
            foreach (int num in tab) {
                System.Console.Write(num + " ");
            }
            System.Console.WriteLine();

            System.Array.ForEach(tab, x => Console.Write($"{x} "));
            System.Console.WriteLine();
        }
    }
}