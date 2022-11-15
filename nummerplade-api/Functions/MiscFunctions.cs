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

    }
}
