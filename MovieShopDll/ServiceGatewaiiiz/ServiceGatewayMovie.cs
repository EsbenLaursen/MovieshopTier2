using MovieShopDll.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieShopDll.ServiceGatewaiiiz
{
    public class ServiceGatewayMovie : IServiceGateway<Movie>
    {
        public Movie Create(Movie t)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56052/");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("api/movies", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Movie>().Result;
                }
                return null;
            }
        }

        public bool Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56052/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.DeleteAsync("/api/movies/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public List<Movie> Read()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56052/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("api/movies").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<List<Movie>>().Result;
                }
            }
            return new List<Movie>();
        }

        public Movie Read(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56052/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("/api/movies/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Movie>().Result;
                }
            }
            return null;
        }

        public Movie Update(Movie t)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56052/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PutAsJsonAsync("api/movies/" + t.Id, t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Movie>().Result;
                }
            }
            return new Movie();
        }
    }
}
