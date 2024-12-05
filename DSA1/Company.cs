using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA1
{
    public class Company
    {
        public string CompanyName { get; set; }

        public string CompanyAddress { get; set;}

        public string CompanyPassword { get; set;}

        public string CompanyEmail { get; set;}

        public int CompanyId { get; set;}

        public Company() { }

        public override string ToString()
        {
            return $"{CompanyName} {CompanyAddress} - {CompanyEmail}";
        }

        public Company(string cn,string ca,string cp, string ce,int cid)
        {
            CompanyName = cn;
            CompanyAddress = ca;
            CompanyPassword = cp;
            CompanyEmail = ce;
            CompanyId = cid;
        }

    }
}
