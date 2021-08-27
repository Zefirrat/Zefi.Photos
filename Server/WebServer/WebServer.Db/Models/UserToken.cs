using System;

namespace WebServer.Db.Models
{
    public class UserToken
    {
        private UserToken(){}
        
        public Guid UserTokenId { get; set; }
        public User User { get; set; }
        public string TokenHash { get; set; }
    }
}