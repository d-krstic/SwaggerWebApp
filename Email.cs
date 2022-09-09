using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerWebApp
{
    public class Email
    {
        public int Id { get; set; }
        public string Mail { get; set; }

        public Email(int i, string m) {
            Id = i;
            Mail = m;
        }

        public Email() { }
    }
}
