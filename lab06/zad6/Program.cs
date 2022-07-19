namespace Zad6 {
    public class Zad6 {
        public static void Main(string[] args) {
            (int a, int b, int c, int d) = CountMyTypes(10, 3.4, 1, "fae", "fafik", -3.5, 9, 8, 7.43, true);
            System.Console.WriteLine($"Ints = {a}, Reals = {b}, Strings = {c}, Others = {d}");
        }

        public static (int, int, int, int) CountMyTypes(params object[] args) {
            int evenInts = 0;
            int positiveReals = 0;
            int longStrings = 0;
            int others = 0;
            foreach (object argument in args) {
                switch (argument) {
                    case int t when t % 2 == 0:
                        evenInts += 1;
                        break;
                    case float f when f > 0:
                    case double d when d > 0:
                        positiveReals += 1;
                        break;
                    case string str when str.Length >= 5:
                        longStrings += 1;
                        break;
                    default:
                        others += 1;
                        break; 
                }
            }

            return (evenInts, positiveReals, longStrings, others);
        }
    }
}