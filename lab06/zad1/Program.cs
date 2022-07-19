namespace Zad1 {
    public class Zad1 {

        public static void Main(string[] args) {
            var info = (Imie: "Tomasz", Nazwisko: "Nowak", Wiek: 43, Placa: 4850.0);
            PrintInfo(info);
            System.Console.WriteLine(info);
            System.Console.WriteLine(info.Imie + " " + info.Nazwisko);
            System.Console.WriteLine(info.Item1 + " " + info.Item2);
            (string imie, string nazwisko, int wiek, double placa) = info;
            System.Console.WriteLine($"{imie} {nazwisko} - {wiek} - {placa}zł miesięcznie");
        }

        public static void PrintInfo((string imie, string nazwisko, int wiek, double placa) t) {
            System.Console.WriteLine($"{t.imie} {t.nazwisko} - {t.wiek} lat - {t.placa}zł miesięcznie");
        }
    }
}