﻿using Microsoft.AspNetCore.Identity;

namespace BuildingExample.Models
{
    public class ApplicationUser : IdentityUser
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
    }
}
