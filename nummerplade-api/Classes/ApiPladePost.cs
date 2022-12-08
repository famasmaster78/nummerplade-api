using System;
namespace nummerplade_api.Classes
{
    public class ApiPladePost
    {
        public Location Location { get; set; }
        public string Email { get; set; }
    }

    public class Location
    {
        public string X { get; set; }
        public string Y { get; set; }
    }
}

