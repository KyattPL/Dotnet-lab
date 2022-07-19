namespace Zad3 {
    public class Zad3 {
        public static void Main(string[] args) {
            System.Console.WriteLine("hello".WavyText());
            System.Console.WriteLine("why was 6 scared of 7? --- cause 7 8 9".WavyText());
        }
    }

    public static class StringExtension {
        public static string WavyText(this string str) {
            
            string newStr = "";
            bool isEven = false;

            foreach (char c in str) {
                if (Char.IsLetter(c)) {
                    if (isEven) {
                        newStr += c.ToString().ToUpper();
                    } else {
                        newStr += c.ToString().ToLower();
                    }
                    isEven = !isEven;
                } else {
                    newStr += '.';
                }
            }

            return newStr;
        }
    }
}