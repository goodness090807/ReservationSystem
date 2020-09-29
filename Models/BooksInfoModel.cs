using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Models
{
    public class BooksInfoModel
    {
        public string UID { get; set; }

        public string UserName { get; set; }

        public string Service { get; set; }

        public DateTime ToStoreDateTime { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
