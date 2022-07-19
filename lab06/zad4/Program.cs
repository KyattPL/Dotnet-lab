namespace Zad4 {
    public class Zad4 {

        public static void Main(string[] args) {
            var info = new {Imie = "Tomasz", Nazwisko = "Nowak", Wiek = 43, Placa = 4850.0};
            PrintInfo(info);
            System.Console.WriteLine(info);
        }

        public static void PrintInfo(dynamic t) {
            System.Console.WriteLine($"{t.Imie} {t.Nazwisko} - {t.Wiek} lat - {t.Placa}zł miesięcznie");
        }
    }
}