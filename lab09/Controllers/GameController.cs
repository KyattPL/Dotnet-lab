using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using lab09.Models;

namespace lab09.Controllers;

public class GameController : Controller {

    [Route("Set,{n}")]
    public IActionResult Set(int n) {

        HttpContext.Session.SetInt32("maxRange", n - 1);
        ViewBag.maxRange = HttpContext.Session.GetInt32("maxRange");
        
        return View("Set");
    }

    [Route("Draw")]
    public IActionResult Draw() {

        Random RNG = new Random();
        int? range = HttpContext.Session.GetInt32("maxRange");

        if (range != null) {
            HttpContext.Session.SetInt32("randomizedNum", RNG.Next(range.Value));
            HttpContext.Session.SetInt32("guesses", 0);
        }

        return View("Draw");
    }

    [Route("Guess,{n}")]
    public IActionResult Guess(int n) {

        int? randomizedNum = HttpContext.Session.GetInt32("randomizedNum");
        int? guesses = HttpContext.Session.GetInt32("guesses");

        if (randomizedNum != null) {

            HttpContext.Session.SetInt32("guesses", guesses.Value + 1);

            if (n == randomizedNum) {
                ViewBag.info = "You've guessed the number!";
                ViewBag.cardTheme = "bg-success";
            } else if (n < randomizedNum) {
                ViewBag.info = "Too low!";
                ViewBag.cardTheme = "bg-primary";
            } else {
                ViewBag.info = "Too high!";
                ViewBag.cardTheme = "bg-danger";
            }

            ViewBag.noGuesses = guesses.Value + 1;
        }

        return View("Guess");        
    }
}