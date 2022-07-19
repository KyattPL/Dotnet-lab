using System.Reflection;

namespace Zad4
{
    public class Zad4
    {
        public static void Main(string[] args)
        {
            MixedNumber num = new MixedNumber(true, 3, 5, 7);
            Assembly assem = typeof(MixedNumber).Assembly;
            object? num1 = assem.CreateInstance("Zad4.MixedNumber");
            object? num2 =  assem.CreateInstance("Zad4.MixedNumber");
            if (num1 != null && num2 != null) {
                MethodInfo? mf = num.GetType().GetMethod("op_Addition");
                object? num3 = mf?.Invoke(num1, new object[] {num1, num2});
                System.Console.WriteLine(num3);
            }
        }
    }

    public class MixedNumber
    {
        private int whole;
        private int numerator;
        private int denominator;
        private static int noFixes = 0;

        public bool Sign { get; set; }

        public int Whole
        {
            get { return whole; }
            set { if (value >= 0) whole = value; else whole = 0; }
        }

        public int Numerator
        {
            get { return numerator; }
            set
            {
                if (value >= 0 && value < denominator)
                {
                    numerator = value;
                }
                else if (value >= 0 && value >= denominator)
                {
                    CheckWholes(value, denominator);
                }
                else
                {
                    numerator = 0;
                }
                LowestDenominator(numerator, denominator);
            }
        }

        public int Denominator
        {
            get { return denominator; }
            set
            {
                if (value > 0 && value > numerator)
                {
                    denominator = value;
                }
                else if (value >= 0 && value <= numerator)
                {
                    CheckWholes(numerator, value);
                }
                else
                {
                    denominator = 1;
                }
                LowestDenominator(numerator, denominator);
            }
        }

        public double Fraction
        {
            get
            {
                int sign = Sign == true ? 1 : -1;
                return sign * (Whole + Numerator / (double)Denominator);
            }
        }

        public static int FixesCount
        {
            get
            {
                return noFixes;
            }
        }

        public MixedNumber(bool isPos, int wholes, int numer, int denom)
        {
            Sign = isPos;
            whole = Math.Abs(wholes);
            numerator = Math.Abs(numer);
            denominator = Math.Abs(denom);
            CheckWholes(numer, denom);
            LowestDenominator(numerator, denominator);
        }

        public MixedNumber(int sign, int wholes, int numer, int denom)
        {
            if (sign < 0)
            {
                Sign = false;
            }
            else
            {
                Sign = true;
            }
            whole = Math.Abs(wholes);
            numerator = Math.Abs(numer);
            denominator = Math.Abs(denom);
            CheckWholes(numer, denom);
            LowestDenominator(numerator, denominator);
        }

        public MixedNumber(int wholes, int numer, int denom) : this(true, wholes, numer, denom) {

        }

        public MixedNumber() : this(true, 1, 0, 1)
        {

        }

        private void LowestDenominator(int currNumer, int currDenom)
        {
            List<int> numerDistribution = new List<int>();
            List<int> denomDistribution = new List<int>();

            int divisor = 2;
            int dividedNumerator = currNumer;
            while (divisor <= dividedNumerator)
            {
                if (dividedNumerator % divisor == 0)
                {
                    dividedNumerator /= divisor;
                    numerDistribution.Add(divisor);
                    divisor = 2;
                }
                else
                {
                    divisor += 1;
                }
            }

            divisor = 2;
            int dividedDenominator = currDenom;
            while (divisor <= dividedDenominator)
            {
                if (dividedDenominator % divisor == 0)
                {
                    dividedDenominator /= divisor;
                    denomDistribution.Add(divisor);
                    divisor = 2;
                }
                else
                {
                    divisor += 1;
                }
            }

            if (denomDistribution.Count() == 0)
            {
                denomDistribution.Add(currDenom);
            }

            if (numerDistribution.Count() == 0)
            {
                numerDistribution.Add(currNumer);
            }

            int denomDistrLen = denomDistribution.Count();
            int i = 0;
            while (i < denomDistrLen)
            {
                if (numerDistribution.Remove(denomDistribution[i]))
                {
                    denomDistribution.RemoveAt(i);
                    denomDistrLen -= 1;
                }
                else
                {
                    i += 1;
                }
            }

            int newNumerator = numerator == 0 ? 0 : 1;
            int newDenominator = 1;

            foreach (int prime in numerDistribution)
            {
                newNumerator *= prime;
            }

            foreach (int prime in denomDistribution)
            {
                newDenominator *= prime;
            }

            if (newNumerator != Numerator || newDenominator != Denominator)
            {
                noFixes += 1;
            }

            numerator = newNumerator;
            denominator = newDenominator;
        }

        private void CheckWholes(int currNum, int currDen)
        {
            if (currNum >= currDen)
            {
                int wholesToAdd = currNum / currDen;
                int leftNumerator = currNum - wholesToAdd * currDen;

                whole += wholesToAdd;
                numerator = leftNumerator;
                noFixes += 1;
            }
        }

        public static MixedNumber operator +(MixedNumber a, MixedNumber b)
        {
            int aSign = a.Sign == true ? 1 : -1;
            int bSign = b.Sign == true ? 1 : -1;

            int wholes = aSign * a.Whole + bSign * b.Whole;
            int newDenom = a.Denominator * b.Denominator;
            int newNumer = aSign * a.Numerator * b.Denominator + bSign * b.Numerator * a.Denominator;

            if (wholes < 0 || (wholes == 0 && newNumer < 0))
            {
                return new MixedNumber(false, wholes, newNumer, newDenom);
            }
            else
            {
                return new MixedNumber(true, wholes, newNumer, newDenom);
            }
        }

        public override string ToString()
        {
            if (Sign == true)
            {
                return $"{Whole*Denominator + Numerator}/{Denominator}";
            }
            else
            {
                return $"-{Whole*Denominator + Numerator}/{Denominator}";
            }
        }

        public void Deconstruct(out bool sign, out int wholes, out int numer, out int denom)
        {
            (sign, wholes, numer, denom) = (Sign, Whole, Numerator, Denominator);
        }
    }
}