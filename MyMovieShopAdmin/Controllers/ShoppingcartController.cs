using MovieShopDll;
using MovieShopDll.Entities;
using MyMovieShopAdmin.DAL;
using MyMovieShopAdmin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MyMovieShopAdmin.Controllers
{
    public class ShoppingcartController : ApplicationController
    {
        IServiceGateway<Movie> mm = new DllFacade().GetServiceGatewayMovies();
        IServiceGateway<Customer> cm = new DllFacade().GetServiceGatewayCustomer();
        IServiceGateway<Order> om = new DllFacade().GetServiceGatewayOrder();

        public ActionResult Index()
        {
            HomeShoppingCartViewModel viewModel = new HomeShoppingCartViewModel()
            {
                Movies = CartItems.Movies
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            Movie m = CartItems.Movies.Find(x => x.Id == id);
            if (id.HasValue)
            {
                CartItems.Movies.Remove(m);
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Checkout()
        {
            int id = 1;
            try
            {
                id = (int)Session["Id"];
            }
            catch (Exception) { }
            Customer c = cm.Read(id);
            if (c != null)
            {
                Order o = new Order() { Date = DateTime.Now, Movies = CartItems.Movies, Customer = c };
                om.Create(o);
            }

            CartItems.Movies = new List<Movie>(); //Resets the list
            return View();
        }

    }
}