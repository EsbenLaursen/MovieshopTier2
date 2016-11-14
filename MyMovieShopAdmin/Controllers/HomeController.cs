using MovieShopDll;
using MovieShopDll.Entities;
using MyMovieShopAdmin.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System;
using System.Web.Routing;

namespace MyMovieShopAdmin.Controllers
{
  

    public class HomeController : ApplicationController
    {
        

        IServiceGateway<Movie> mm = new DllFacade().GetServiceGatewayMovies();
        IServiceGateway<Customer> cm = new DllFacade().GetServiceGatewayCustomer();
        IServiceGateway<Order> om = new DllFacade().GetServiceGatewayOrder();
        IServiceGateway<Genre> gm = new DllFacade().GetServiceGatewayGenre();

        public ActionResult Index()
        {
            //ViewModel
            HomeIndexViewModel viewModel = new HomeIndexViewModel()
            {
                MoviesInCart = mm.Read(),
                Movies = mm.Read(),
                Genres = gm.Read()
            };
            return View(viewModel);
        }
        [Authorize]
        public ActionResult About()
        {
            int id;
            // Using session then
            try
            {
                id = (int)Session["Id"];
                CurrentUser.Customer = cm.Read(id);
            }
            catch (Exception)
            {
                id = 1;
            }
            CurrentUser.Customer = cm.Read(id);
            //ViewModel
            HomeIndexViewModel viewModel = new HomeIndexViewModel()
            {
                Customer = CurrentUser.Customer,
                Orders = om.Read().Where(x => x.Customer.Id == CurrentUser.Customer.Id).ToList() //Fetches the customer, his address and his orders
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult About(Customer c)
        {
            if (c != null)
            {
                if (ModelState.IsValid)
                {
                    Customer updated = cm.Update(c); //Updates the database
                    CurrentUser.Customer = updated;
                }
            }
            //ViewModel
            HomeIndexViewModel viewModel = new HomeIndexViewModel()
            {
                Customer = CurrentUser.Customer,
                Orders = om.Read().Where(x => x.Customer.Id == CurrentUser.Customer.Id).ToList() //Fetches the customer, his address and his orders
            };
            return View(viewModel);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Session.Clear();
        }

    }
}