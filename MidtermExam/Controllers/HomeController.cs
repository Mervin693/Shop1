using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MidtermExam.Models;


namespace MidtermExam.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(string searchString)
    {
        return View();
    }

    public ActionResult CreateShop()
    {
        return View();
    }
    [HttpPost]
    public ActionResult CreateShop(Shop shop)
    {
        DBmanager dBmanager = new DBmanager();
        try
        {
            dBmanager.NewShop(shop);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        return RedirectToAction("ShopList");
    }

    public ActionResult EditShop(int id)
    {

        DBmanager dBmanager = new DBmanager();
        Shop shop = dBmanager.GetShopByID(id);
        return View(shop);
    }
    [HttpPost]
    public ActionResult EditShop(Shop shop)
    {
        DBmanager dBmanager = new DBmanager();
        dBmanager.UpdateShop(shop);
        return RedirectToAction("ShopList");
    }
    public ActionResult DeleteShop(int id)
    {
        DBmanager dBmanager = new DBmanager();
        dBmanager.DeleteShopById(id);
        return RedirectToAction("ShopList");
    }
    public IActionResult Details(int id)
    {
        DBmanager db = new DBmanager();
        var shopList = db.GetShop();
        var shop = shopList.FirstOrDefault(s => s.ID == id);

        if (shop == null)
        {
            return NotFound();
        }

        return View(shop); // pass a one Shop to Details.cshtml
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult ShopList(string searchString)
{
        DBmanager db = new DBmanager();
        var shopList = db.GetShop();

        if (!string.IsNullOrEmpty(searchString))
        {
            shopList = shopList
                .Where(s => s.product_name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        ViewBag.cards = shopList;
        ViewBag.SearchString = searchString;

        return View();
    }
    public IActionResult About()
    {
        return View();
    }
    public IActionResult Portfolio()
    {
        return View();
    }
    public IActionResult BlackWhiteShop()
    {
        return View();
    }
    public IActionResult AboutMe()
    {
        return View();
    }

}
