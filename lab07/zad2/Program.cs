namespace Zad2 {
    public class Zad2 {
        public static void Main(string[] args) {
            List<object> myObjects = new List<object>();
            myObjects.Add(new Rectangle());
            myObjects.Add(new Square());
            myObjects.Add(new Circle());
            Rectangle temp = new Rectangle();
            ((IHasInterior) temp).Color = "brown";
            myObjects.Add(temp);
            PrintFigures(myObjects);
        }

        public static void PrintFigures(List<object> figures) {
            foreach (object figure in figures) {
                switch (figure) {
                    case IHasInterior fig: System.Console.WriteLine(fig.Color); break;
                    default: System.Console.WriteLine("no color"); break;
                }
            }
        }
    }

    public interface IFigure {
        void moveTo(double x, double y);
    }

    public interface IHasInterior {
        string Color { get; set; }
    }

    public class Rectangle: IFigure, IHasInterior {

        private double xPos;
        private double yPos;
        string IHasInterior.Color { get; set; } = "blue";

        public void moveTo(double x, double y) {
            xPos = x;
            yPos = y;
        }

        public Rectangle() {
            xPos = 0;
            yPos = 0;
        }
    }

    public class Square: IFigure {
        private double xPos;
        private double yPos;

        public void moveTo(double x, double y) {
            xPos = x;
            yPos = y;
        }

        public Square() {
            xPos = 0;
            yPos = 0;
        }
    }

    public class Circle: IHasInterior {
        string IHasInterior.Color { get; set; } = "green";
    }
}