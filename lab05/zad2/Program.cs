namespace Zad2
{
    class Zad2
    {
        public static void Main(string[] args)
        {
            int n = Convert.ToInt32(System.Console.ReadLine());

            while (n < 1)
            {
                System.Console.WriteLine("n musi być większe od 0:");
                n = Convert.ToInt32(System.Console.ReadLine());
            }

            string numbers = "";
            int numsInString = 0;
            string temp = "";

            do
            {
                numbers += System.Console.ReadLine() + '\n' ?? "";
                foreach (char c in numbers)
                {
                    if (c == ' ' || c == '\t' || c == '\n')
                    {
                        if (temp != "")
                        {
                            numsInString += 1;
                        }
                        temp = "";
                    }
                    else
                    {
                        temp += c;
                    }
                }
            } while (numsInString < n);

            int max = int.MinValue, sndMax = int.MinValue;

            temp = "";
            int numCounter = 0;

            foreach (char c in numbers)
            {
                if (c == ' ' || c == '\t' || c == '\n')
                {
                    if (temp != "")
                    {
                        numCounter += 1;
                        int casted = Convert.ToInt32(temp);
                        if (casted > max)
                        {
                            sndMax = max;
                            max = casted;
                        }
                    }
                    temp = "";
                }
                else
                {
                    temp += c;
                }

                if (numCounter == n)
                {
                    break;
                }
            }

            if (temp != "")
            {
                int casted = Convert.ToInt32(temp);
                if (casted > max)
                {
                    sndMax = max;
                    max = casted;
                }
            }

            if (sndMax == max || sndMax == int.MinValue)
            {
                System.Console.WriteLine("brak rozwiązania");
            }
            else
            {
                System.Console.WriteLine($"odpowiedź: {sndMax}");
            }
        }
    }
}