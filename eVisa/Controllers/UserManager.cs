using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eVisa.Function;
using eVisa.Models;
using eVisa.ViewModel;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;

namespace eVisa.Controllers
{
    public class UserManager
    {
        public bool IsValid(string UserId, string Password)
        {
            using ( var db = new eVisaContext() )
            {
                var hash = Crypto.Hash(Password);
                return db.ContactInformation.Any(u => u.UserId == UserId && u.Password == hash);
            }
        }

    }
}