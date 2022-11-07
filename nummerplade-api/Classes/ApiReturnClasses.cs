using System;
namespace nummerplade_api.Classes
{
    public class InsuranceReturn
    {
        public bool success = false;
        public string? status = "";
        public bool is_police_vehicle = false;
        public Insurance Insurance;
        public General? General;
    }
}

