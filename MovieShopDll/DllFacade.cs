using MovieShopDll.Entities;
using MovieShopDll.Managers;
using MovieShopDll.ServiceGatewaiiiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieShopDll
{
    public class DllFacade
    {
        public IServiceGateway<Movie> GetServiceGatewayMovies()
        {
            return new ServiceGatewayMovie();
        }
        public IServiceGateway<Customer> GetServiceGatewayCustomer()
        {
            return new ServiceGatewayCustomer();
        }
        public IServiceGateway<Order> GetServiceGatewayOrder()
        {
            return new ServiceGatewayOrder();
        }
        public IServiceGateway<Genre> GetServiceGatewayGenre()
        {
            return new ServiceGatewayGenre();
        }
    }
}
