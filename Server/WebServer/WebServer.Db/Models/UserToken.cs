using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer.Db.Models
{
    public class UserToken
    {
        private UserToken(){}
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserTokenId { get; set; }
        
        public User User { get; set; }
        public string TokenHash { get; set; }
    }
}