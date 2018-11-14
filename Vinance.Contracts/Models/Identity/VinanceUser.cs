using System;
using System.Collections.Generic;
using System.Text;

namespace Vinance.Contracts.Models.Identity
{
    public class VinanceUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
