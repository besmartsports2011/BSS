using System;
using System.Collections.Generic;

namespace BSS_WebAPI.Models
{
    /// <summary>
    /// Book Entity
    /// </summary>
    public class Login
    {
        public string username { get; set; }

        public string password { get; set; }

        public int userId { get; set; }

        public string email { get; set; }

        public string role { get; set; }


        public Guid userGuid { get; set; }

        public List<string> errors { get; set; }

        public Login()
        {
            errors = new List<string>();
        }
    }
}