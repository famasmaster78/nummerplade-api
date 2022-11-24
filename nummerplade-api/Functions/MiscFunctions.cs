using System.Net.Http;
using System.Text.RegularExpressions;
using nummerplade_api.Classes;
using nummerplade_api.Model;

namespace nummerplade_api.Functions
{
    public class MiscFunctions
    {

        public async Task<string> nummerpladeNetUrl(string nrplade)
        {
            // Return
            return $"https://www.nummerplade.net/nummerplade/{nrplade}.html";

        }

        public async Task<bool> CarExists(string nrplade)
        {

            // Init HTTPClient
            var httpClient = new HttpClient();

            bool carStatus = false;

            if ((await httpClient.GetAsync($"https://www.tjekbil.dk/api/v3/dmr/regnr/{nrplade}")).StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Car exists
                carStatus = true;
            }

            return carStatus;
        }

        public async Task<Extended> GetCarInformation(string nrplade)
        {
            // First check in local database if this is a registered policecar
            // Init HTTPClient
            var httpClient = new HttpClient();

            // Download
            string carId = (await httpClient.GetFromJsonAsync<dmr_data>($"https://www.tjekbil.dk/api/v3/dmr/regnr/{nrplade}")).debtData.carId;

            // Get insurance
            var response = (await httpClient.GetFromJsonAsync<Extended>($"https://www.tjekbil.dk/api/v3/dmr/kid/{carId}/extendednew"));

            // Return
            return response;

        }

        public bool validateEmail(string email)
        {
            // Regex pattern
            string pattern = @"^((\w[^\W]+)[\.\-]?){1,}\@(([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";
            string substitution = @"";

            // Regex options
            RegexOptions options = RegexOptions.Multiline;

            // Create regex
            Regex regex = new Regex(pattern, options);

            // Check if match
            return regex.IsMatch(email);
        }

    }
}
