using System;

namespace WebServer.Db.Models
{
    public class User
    {
        private User(){}
        
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}