using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICapchaService
    {
        Task<bool> IsValidAsync(string token);
    }
    public class CapchaService: ICapchaService
    {
        private readonly HttpClient _httpClient;

        public CapchaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> IsValidAsync(string token)
        {
            var response = await _httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret=6LfnbwYqAAAAAASgl1-wooXXcWVZwSNrj2Pm_Ry6\r\n&response={token}");
            var content = await response.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(content);
            return json.success;
        }
    }

}
