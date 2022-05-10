using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace BibliotekaWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public IEnumerable<IDictionary<string, string>> Pozycje { get; private set; }

        [BindProperty]
        public string Search { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;

            Search = "";
        }

        private async Task<IActionResult> PobierzPozycje()
        {
            var httpClient = _httpClientFactory?.CreateClient("Biblioteka");
            var response = await httpClient.GetAsync(String.IsNullOrEmpty(Search) ? "/pozycje" : $"/pozycje?s={Search.ToLower()}");

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var pozycje = await JsonSerializer.DeserializeAsync<IEnumerable<IDictionary<string, string>>>(contentStream);

                Pozycje = pozycje;

                return Page();
            }

            return RedirectToPage("./Error");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return await PobierzPozycje();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return await PobierzPozycje();
        }
    }
}