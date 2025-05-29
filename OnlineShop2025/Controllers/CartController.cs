using System;
using System.Security.Claims;
using Asp2025_DataAccess.Data;
using Asp2025_DataAccess.Repository;
using Asp2025_DataAccess.Repository.IRepository;
using Asp2025_Models;
using Asp2025_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using OnlineShop.Utility;

namespace OnlineShop.Controllers;

public class CartController : Controller
{
    private IProductRepository productRepository;
    private IInquiryDetailRepository inquiryDetailRepository;
    private IInquiryHeaderRepository inquiryHeaderRepository;

    public CartController(ApplicationDbContext context, IProductRepository productRepository, IInquiryDetailRepository inquiryDetailRepository, IInquiryHeaderRepository inquiryHeaderRepository)
    {
        this.productRepository = productRepository;
        this.inquiryDetailRepository = inquiryDetailRepository;
        this.inquiryHeaderRepository = inquiryHeaderRepository;

    }

    [HttpPost]
    [ActionName("Index")]
    public IActionResult IndexPost(IEnumerable<Product> products)
    {
        List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();

        foreach (var prod in products)
        {
            shoppingCarts.Add(new ShoppingCart()
            {
                ProductId = prod.Id,
                Count = prod.TempCount
            });
        }

        HttpContext.Session.Set(WC.SessionCart, shoppingCarts);

        return RedirectToAction("Summary");
    }

    public IActionResult Summary()
    {
        var claimIdentity = (ClaimsIdentity)User.Identity;

        // если пользователь зашел в систему он будет точно определен
        var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

        // достаем инфу и вбрасываем ее в поля

        return null;
    }

    public IActionResult Index()
    {
        List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
        if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null &&
       HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
        {
            shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
        }
        else
        {
            return View();
        }
        // DetailsVM detailsVM = new DetailsVM()
        // {
        //     Product = context.Product.Include(p => p.Category).Include(p => p.Company).Where(p => p.Id == id).FirstOrDefault(),
        //     ExistsInCart = false
        // };
        List<Product> productsInCart = new List<Product>();
        var products = productRepository.GetAll(includeProperties: "Category,Company");

        foreach (var item in shoppingCarts)
        {
            var prod = products.FirstOrDefault(x => x.Id == item.ProductId);

            prod.TempCount = item.Count;    // ???

            productsInCart.Add(prod);
        }
        return View(productsInCart);
    }

    [HttpPost]
    public IActionResult UpdateCart(List<Product> products)
    {
        List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();

        foreach (var prod in products)
        {
            shoppingCarts.Add(new ShoppingCart()
            {
                ProductId = prod.Id,
                Count = prod.TempCount
            });
        }

        HttpContext.Session.Set(WC.SessionCart, shoppingCarts);

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Authorize]
    public IActionResult InquiryAdd(string fullName, string phone, string email)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


        InquiryHeader inquiryHeader = new InquiryHeader()
        {

            FullName = fullName,
            PhoneNumber = phone,
            Email = email,
            InquiryDate = DateTime.Now,
            ApplicationUserId = claim.Value,
        };
        inquiryHeaderRepository.Add(inquiryHeader);
        inquiryHeaderRepository.Save();

        List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
        if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null &&
       HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
        {
            shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
        }
        else
        {
            TempData[WC.Error] = "No products in cart";
            return View();
        }

        foreach (var item in shoppingCarts)
        {
            InquiryDetail inquiryDetail = new InquiryDetail()
            {
                ProductId = item.ProductId,
                InquiryHeaderId = inquiryHeader.Id,
                Count = item.Count // Assuming Count is the quantity of the product
            };
            inquiryDetailRepository.Add(inquiryDetail);
        }
        inquiryDetailRepository.Save();
        TempData[WC.Success] = "Inquiry created successfully";
        return RedirectToAction("Index", "Home");
    }

}

