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
        public double X { get; set; }
        public double Y { get; set; }
    }
}

