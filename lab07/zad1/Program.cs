namespace Zad1
{
    public class Zad1
    {
        public static void Main(string[] args)
        {
            MixedNumber pierwsza = new MixedNumber();
            MixedNumber druga = new MixedNumber(false, -3, 7, 13);
            MixedNumber trzecia = new MixedNumber(1, 1, 4, 7);

            System.Console.WriteLine($"Pierwsza: {pierwsza}");
            System.Console.WriteLine($"Druga: {druga}");
            System.Console.WriteLine($"Trzecia: {trzecia}");

            System.Console.WriteLine($"Poprawki: {MixedNumber.FixesCount}");
            trzecia.Numerator = 8;
            System.Console.WriteLine($"Poprawki: {MixedNumber.FixesCount}");

            System.Console.WriteLine($"Trzecia: {trzecia}");

            MixedNumber wynik = druga + trzecia;
            System.Console.WriteLine($"Druga + trzecia = {wynik}");
        }
    }

    public class MixedNumber
    {

        private bool isPositive = true;
        private int whole;
        private int numerator;
        private int denominator;
        private static int noFixes = 0;

        public bool Sign
        {
            get { return isPositive; }
            set { isPositive = value; }
        }

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
            isPositive = isPos;
            whole = Math.Abs(wholes);
            numerator = Math.Abs(numer);
            denominator = Math.Abs(denom);
            CheckWholes(numer, denom);
            LowestDenominator(numer, denom);
        }

        public MixedNumber(int sign, int wholes, int numer, int denom)
        {
            if (sign < 0)
            {
                isPositive = false;
            }
            else
            {
                isPositive = true;
            }
            whole = Math.Abs(wholes);
            numerator = Math.Abs(numer);
            denominator = Math.Abs(denom);
            CheckWholes(numer, denom);
            LowestDenominator(numer, denom);
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
                return new MixedNumber(false, Math.Abs(wholes), Math.Abs(newNumer), newDenom);
            }
            else
            {
                return new MixedNumber(true, Math.Abs(wholes), Math.Abs(newNumer), newDenom);
            }
        }

        public override string ToString()
        {
            if (Sign == true)
            {
                return $"{Numerator + Denominator * Whole}/{Denominator}";
            }
            else
            {
                return $"-{Numerator + Denominator * Whole}/{Denominator}";
            }
        }

        public void Deconstruct(out bool sign, out int wholes, out int numer, out int denom)
        {
            (sign, wholes, numer, denom) = (Sign, Whole, Numerator, Denominator);
        }
    }
}