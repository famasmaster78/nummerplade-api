namespace nummerplade_api.Functions
{
    public class MiscFunctions
    {

        public async Task<string> nummerpladeNetUrl(string nrplade)
        {
            // Return
            return $"https://www.nummerplade.net/nummerplade/{nrplade}.html";

        }

    }
}
