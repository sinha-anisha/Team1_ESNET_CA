using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1_ESNET_CA.Controllers
{
    public class PurchaseController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ItemImage"] = ItemImage;
            ViewData["ItemName"] = ItemName;
            ViewData["ItemDesc"] = ItemDesc;
            ViewData["ItemDL"] = ItemDownload;
            ViewData["PurDate"] = PurchaseDate;
            ViewData["PurQuantity"] = PurchaseQuantity;
            ViewData["PurActCode"] = ActivationCode;
            return View();
        }
    }
}
