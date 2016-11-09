using MovieShopDll.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieShopDll
{
    public class ServiceGatewayGenre : IServiceGateway<Genre>
    {
        public Genre Create(Genre t)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56052/");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("api/genres", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Genre>().Result;
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

                var response = client.DeleteAsync("/api/genres/"+id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public List<Genre> Read()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56052/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("api/genres").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<List<Genre>>().Result;
                }
            }
            return new List<Genre>();
        }

        public Genre Read(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56052/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("/api/genres/"+id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Genre>().Result;
                }
            }
            return null;
        }

        public Genre Update(Genre t)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56052/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PutAsJsonAsync("api/genres/"+t.Id, t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Genre>().Result;
                }
            }
            return new Genre();
        }
    }
}
