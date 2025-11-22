using System.Collections.Generic;

namespace ScholarshipPortal.Models
{
    // Simple class to hold a State and its corresponding Districts
    public class StateData
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public List<string> Districts { get; set; } = new List<string>();
    }

    // ViewModel to pass the list of states and districts to the registration form
    public class StateDistrictViewModel
    {
        public List<StateData> States { get; set; } = new List<StateData>();

        // Constructor populates the list with major Indian states/districts
        public StateDistrictViewModel()
        {
            States = new List<StateData>
            {
                new StateData { Code = "AN", Name = "Andaman and Nicobar Islands", Districts = new List<string> { "Port Blair", "Nicobar", "South Andaman" } },
                new StateData { Code = "AP", Name = "Andhra Pradesh", Districts = new List<string> { "Visakhapatnam", "Guntur", "Krishna" } },
                new StateData { Code = "AR", Name = "Arunachal Pradesh", Districts = new List<string> { "Itanagar", "Tawang", "West Kameng" } },
                new StateData { Code = "AS", Name = "Assam", Districts = new List<string> { "Guwahati", "Dibrugarh", "Kamrup" } },
                new StateData { Code = "BR", Name = "Bihar", Districts = new List<string> { "Patna", "Gaya", "Muzaffarpur" } },
                new StateData { Code = "CH", Name = "Chandigarh", Districts = new List<string> { "Chandigarh" } },
                new StateData { Code = "CG", Name = "Chhattisgarh", Districts = new List<string> { "Raipur", "Durg", "Bilaspur" } },
                new StateData { Code = "DN", Name = "Dadra and Nagar Haveli and Daman and Diu", Districts = new List<string> { "Daman", "Diu" } },
                new StateData { Code = "DL", Name = "Delhi", Districts = new List<string> { "New Delhi", "Central Delhi", "South West Delhi" } },
                new StateData { Code = "GA", Name = "Goa", Districts = new List<string> { "North Goa", "South Goa" } },
                new StateData { Code = "GJ", Name = "Gujarat", Districts = new List<string> { "Ahmedabad", "Surat", "Vadodara" } },
                new StateData { Code = "HR", Name = "Haryana", Districts = new List<string> { "Gurugram", "Faridabad", "Panipat" } },
                new StateData { Code = "HP", Name = "Himachal Pradesh", Districts = new List<string> { "Shimla", "Kangra", "Mandi" } },
                new StateData { Code = "JK", Name = "Jammu and Kashmir", Districts = new List<string> { "Jammu", "Srinagar", "Udhampur" } },
                new StateData { Code = "JH", Name = "Jharkhand", Districts = new List<string> { "Ranchi", "Dhanbad", "Jamshedpur" } },
                new StateData { Code = "KA", Name = "Karnataka", Districts = new List<string> { "Bengaluru", "Mysuru", "Hubli" } },
                new StateData { Code = "KL", Name = "Kerala", Districts = new List<string> { "Thiruvananthapuram", "Kochi", "Kozhikode" } },
                new StateData { Code = "LA", Name = "Ladakh", Districts = new List<string> { "Leh", "Kargil" } },
                new StateData { Code = "MP", Name = "Madhya Pradesh", Districts = new List<string> { "Bhopal", "Indore", "Jabalpur" } },
                new StateData { Code = "MH", Name = "Maharashtra", Districts = new List<string> { "Mumbai", "Pune", "Nagpur", "Nashik" } },
                new StateData { Code = "MN", Name = "Manipur", Districts = new List<string> { "Imphal", "Thoubal" } },
                new StateData { Code = "ML", Name = "Meghalaya", Districts = new List<string> { "Shillong", "East Khasi Hills" } },
                new StateData { Code = "MZ", Name = "Mizoram", Districts = new List<string> { "Aizawl", "Lunglei" } },
                new StateData { Code = "NL", Name = "Nagaland", Districts = new List<string> { "Kohima", "Dimapur" } },
                new StateData { Code = "OR", Name = "Odisha", Districts = new List<string> { "Bhubaneswar", "Cuttack", "Ganjam" } },
                new StateData { Code = "PB", Name = "Punjab", Districts = new List<string> { "Amritsar", "Ludhiana", "Patiala" } },
                new StateData { Code = "RJ", Name = "Rajasthan", Districts = new List<string> { "Jaipur", "Jodhpur", "Udaipur" } },
                new StateData { Code = "SK", Name = "Sikkim", Districts = new List<string> { "Gangtok", "North Sikkim" } },
                new StateData { Code = "TN", Name = "Tamil Nadu", Districts = new List<string> { "Chennai", "Coimbatore", "Madurai" } },
                new StateData { Code = "TS", Name = "Telangana", Districts = new List<string> { "Hyderabad", "Warangal", "Ranga Reddy" } },
                new StateData { Code = "TR", Name = "Tripura", Districts = new List<string> { "Agartala", "West Tripura" } },
                new StateData { Code = "UP", Name = "Uttar Pradesh", Districts = new List<string> { "Lucknow", "Kanpur", "Varanasi" } },
                new StateData { Code = "UK", Name = "Uttarakhand", Districts = new List<string> { "Dehradun", "Haridwar", "Nainital" } },
                new StateData { Code = "WB", Name = "West Bengal", Districts = new List<string> { "Kolkata", "Howrah", "Bardhaman" } }
            };
        }
    }
}