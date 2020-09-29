using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.ViewModels
{
    public class UserInfoViewModel
    {
        public string userId { get; set; }

        public string displayName { get; set; }

        public string pictureUrl { get; set; }

        public string statusMessage { get; set; }
    }
}
