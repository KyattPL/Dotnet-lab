// See https://aka.ms/new-console-template for more information
namespace Zad1
{
    class Zad1
    {
        public static void Main(string[] args)
        {
            decimal a = Convert.ToDecimal(Console.ReadLine());
            decimal b = Convert.ToDecimal(Console.ReadLine());
            decimal c = Convert.ToDecimal(Console.ReadLine());

            (decimal? x1, decimal? x2) = Quadratic(a, b, c);
            if (x2 == null)
            {
                String.Format("{0:G5}", x1);
                System.Console.WriteLine("Answer: x = {0}", String.Format("{0:G5}", x1));
            }
            else
            {
                System.Console.WriteLine($"Answer: x1 = {String.Format("{0:G5}", x1)}, x2 = {String.Format("{0:G5}", x2)}");
            }
        }

        public static (decimal?, decimal?) Quadratic(decimal a, decimal b, decimal c)
        {
            decimal x1 = (b - (decimal)Math.Sqrt((double)(b * b - 4 * a * c))) / 2 * a;
            decimal x2 = (b + (decimal)Math.Sqrt((double)(b * b - 4 * a * c))) / 2 * a;

            if (b * b - 4 * a * c < 0 || (a == 0 && b == 0))
            {
                return (null, null);
            }
            else if (a == 0 && b != 0)
            {
                return (-c / b, null);
            }

            if (x1 == x2)
            {
                return (x1, null);
            }
            else
            {
                return (x1, x2);
            }
        }
    }
}