using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using lab09.Models;

namespace lab09.Controllers;

public class ToolController : Controller {

    [Route("Tool/Solve/{a}/{b}/{c}")]
    public IActionResult Solve(Decimal a, Decimal b, Decimal c) {
        (Decimal? sol1, Decimal? sol2) = SolveQuadratic(a, b, c);

        List<Decimal?> solutions = new List<Decimal?>();
        if (sol1 != null) {
            solutions.Add(sol1);
        }

        if (sol2 != null) {
            solutions.Add(sol2);
        }

        ViewBag.a = a;
        ViewBag.b = b;
        ViewBag.c = c;
        ViewBag.sols = solutions;

        if (sol1 != null && sol2 != null) {
            ViewBag.color = "green";
        } else if (sol1 != null && sol2 == null) {
            ViewBag.color = "orange";
        } else if (sol1 == null && sol2 == null && c != 0) {
            ViewBag.color = "red";
            ViewBag.solText = "No solution!";
        } else {
            ViewBag.color = "blue";
            ViewBag.solText = "Infinite number of solutions";
        }

        return View("Solve");
    }

    public (Decimal?, Decimal?) SolveQuadratic(Decimal a, Decimal b, Decimal c) {

        if (b * b - 4 * a * c < 0 || (a == 0 && b == 0)) {
            return (null, null);
        } else if (a == 0 && b != 0) {
            return (-c / b, null);
        }

        Decimal x1 = (-b - (decimal)Math.Sqrt((double)(b * b - 4 * a * c))) / 2 * a;
        Decimal x2 = (-b + (decimal)Math.Sqrt((double)(b * b - 4 * a * c))) / 2 * a;

        if (x1 == x2) {
            return (x1, null);
        } else {
            return (x1, x2);
        }
    }
}