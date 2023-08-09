using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirPortWebApi.Models.DataLayer
{
    public class LoginDbOperations
    {
        public Login GetUser(string email)
        {
            AirportManagementEntities1 entities = new AirportManagementEntities1();
            return entities.Logins.FirstOrDefault(x => x.Email == email);
        }
    }
}