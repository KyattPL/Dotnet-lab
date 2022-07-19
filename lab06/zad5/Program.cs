namespace Zad5 {
    public class Zad5 {
        public static void Main(string[] args) {
            DrawCard("Ryszard", "Rys", 'X', 2, 20);
            System.Console.WriteLine();
            DrawCard("Tomasz");
            System.Console.WriteLine();
            DrawCard(szerRamki: 1, imie: "Lucjan", nazwisko: "Trybak");
            System.Console.WriteLine();
            DrawCard("Hieronim", "Brzęczyszczykiewicz", '@', 4, 40);
            System.Console.WriteLine();
            DrawCard("Alicja", "Sierodzka-Stańczyk", '/', 3, 14);
        }

        public static string FixedLengthPad(string content, char znakRamki, int szerRamki, int minSzerWiz) {
            string result = "";

            for (int j = 0; j < szerRamki; j++) {
                result += znakRamki;
            }

            for (int j = 0; j < (minSzerWiz - 2 * szerRamki - content.Length) / 2; j++) {
                result += ' ';
            }
            result += content;

            for (int j = 0; j < (minSzerWiz - 2 * szerRamki - content.Length) / 2; j++) {
                result += ' ';
            }

            if (content.Length % 2 == 1) {
                result += ' ';
            }

            for (int j = 0; j < szerRamki; j++) {
                result += znakRamki;
            }

            return result;
        }

        public static void DrawCard(string imie, string nazwisko="Anonim", char znakRamki='+', int szerRamki=2, int minSzerWiz=18) {
            
            int imieRzad = (2 + (2 * szerRamki)) / 2;
            int nazwRzad = imieRzad + 1;

            if (imie.Length > minSzerWiz - 2 * szerRamki || nazwisko.Length > minSzerWiz - 2 * szerRamki) {
                System.Console.WriteLine("Imie lub nazwisko za długie dla danej szerokości wizytówki!");
                return;
            }

            for (int i = 0; i < 2 + (2 * szerRamki); i++) {
                if (i == imieRzad - 1) {
                    System.Console.Write(FixedLengthPad(imie, znakRamki, szerRamki, minSzerWiz));
                } else if (i == nazwRzad - 1) {
                    System.Console.Write(FixedLengthPad(nazwisko, znakRamki, szerRamki, minSzerWiz));
                } else {
                    for (int j = 0; j < minSzerWiz; j++) {
                        System.Console.Write(znakRamki);
                    }                    
                }
                System.Console.WriteLine();
            }
        }
    }
}