using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA1
{
    public class Email
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public int Id { get; set; }
        public Email() { }

        public Email(string sender,string subject, string content, int id)
        {
            Sender = sender;
            Subject = subject;
            Content = content;
            Id = id;
         }

    }
}
