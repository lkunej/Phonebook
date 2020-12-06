using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookNamespace.DAL.Models
{
    public class PhoneNumber
    {
        public PhoneNumber()
        {

        }

        public Guid Id { get; set; }
        public Guid ContactId { get; set; }
        public string Number { get; set; }
    }
}