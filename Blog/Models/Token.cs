using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Token: User
    {
        public string TokenString { get; set; }
        private Random rnd = new Random();
        public int TimeLife { get; set; }
        public Token(User user)
        {
            this.Id = user.Id;
            this.Email = user.Email;
            this.Password = user.Password;
            this.Name = user.Name;
            this.TokenString = rnd.Next(1,10000).ToString() + Id.ToString() + Name.Length.ToString();
            this.TimeLife = 1620;
        }
    }
}
