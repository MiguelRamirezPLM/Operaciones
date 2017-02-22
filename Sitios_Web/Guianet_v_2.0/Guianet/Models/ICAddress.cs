using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class ICAddress
    {
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public bool? Active { get; set; }
        public byte? CountryId { get; set; }
        public int? AddressId { get; set; }
        public string Suburb { get; set; }
        public int? StateId { get; set; }
        public string Location { get; set; }
        public string CountryName { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string CompanyName { get; set; }
        public string StateName { get; set; }
        public int? Branchs { get; set; }
        public int? Brands { get; set; }
        public int? Adverts { get; set; }
        public int? Products { get; set; }

        public string Phone { get; set; }
        public string Fax { get; set; }
        public string PhoneFax { get; set; }
        public string Lada { get; set; }
        public string ShortName { get; set; }
        public string Adviser { get; set; }

    }
}