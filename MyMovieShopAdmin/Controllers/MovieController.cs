using MovieShopDll;
using MovieShopDll.Entities;
using MyMovieShopAdmin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMovieShopAdmin.Controllers
{
    public class MovieController : ApplicationController
    {
        IServiceGateway<Movie> gm = new DllFacade().GetServiceGatewayMovies();
        // GET: Movie
        [HttpGet]
        public ActionResult Index(int id)
        {
            MoviesIndexViewModel viewModel = new MoviesIndexViewModel() {
                Movie = gm.Read(id)
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Added(int id)
        {
            CartItems.Movies.Add(gm.Read(id));
            return View();
        }
    }
}