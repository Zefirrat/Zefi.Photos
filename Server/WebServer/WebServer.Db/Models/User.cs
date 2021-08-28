using System;
using System.Security.Claims;

namespace WebServer.Db.Models
{
    public class User : ClaimsIdentity
    {
        private User(){}
        
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}