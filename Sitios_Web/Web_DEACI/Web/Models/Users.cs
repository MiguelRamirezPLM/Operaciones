//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.ActivitySessions = new HashSet<ActivitySessions>();
            this.ApplicationUsers = new HashSet<ApplicationUsers>();
            this.UserCountries = new HashSet<UserCountries>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        [Required]
        [Display(Name = "usuario")]
        public string NickName { get; set; }
        [Required]
        [Display(Name = "contraseña")]
        public string Password { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public bool Active { get; set; }
        public int CountryId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivitySessions> ActivitySessions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationUsers> ApplicationUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual Countries_Users Countries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCountries> UserCountries { get; set; }


        public ApplicationUsers ApplicationsUsers = new ApplicationUsers();
        public Roles RolesUser = new Roles();
        public Applications Applications = new Applications();
        public Countries_Users Country = new Countries_Users();
        public UserCountries UserCountries1 = new UserCountries();


        public PLMCryptographyComponent.CryptographyComponent cryptography = new PLMCryptographyComponent.CryptographyComponent();
        PLMUsers db = new PLMUsers();

        public bool Validación(string NickName, string Password)
        {
            Password = cryptography.encrypt(Password);
            var login = db.Users.Where(x => x.NickName == NickName && x.Password == Password).ToList();
            if (login.LongCount() > 0)
            {
                foreach (Users User in login)
                {
                    User.NickName = NickName;
                    User.Password = Password;
                    UserId = User.UserId;
                    Name = User.Name;
                    LastName = User.LastName;
                    SecondLastName = User.SecondLastName;
                    Email = User.Email;
                    Active = User.Active;
                    CountryId = User.CountryId;

                    var Hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];
                    var Applic = from Aplications in db.Applications
                                 where Aplications.HashKey == Hashkey
                                 select Aplications;
                    if (Applic.LongCount() > 0)
                    {
                        foreach (Applications Ap in Applic)
                        {
                            Applications.Active = Ap.Active;
                            Applications.ApplicationId = Ap.ApplicationId;
                            Applications.ApplicationUsers = Ap.ApplicationUsers;
                            Applications.Description = Ap.Description;
                            Applications.DocumentFile = Ap.DocumentFile;
                            Applications.RootUrl = Ap.RootUrl;
                            Applications.URL = Ap.URL;
                            Applications.Version = Ap.Version;
                            var AppUser = from ApplicationUser in db.ApplicationUsers
                                          where ApplicationUser.UserId == UserId
                                          && ApplicationUser.ApplicationId == Applications.ApplicationId
                                          select ApplicationUser;
                            if (AppUser.LongCount() > 0)
                            {
                                foreach (ApplicationUsers App in AppUser)
                                {
                                    ApplicationsUsers.ApplicationId = App.ApplicationId;
                                    ApplicationsUsers.RoleId = App.RoleId;
                                    ApplicationsUsers.UserId = App.UserId;

                                    var Roles = from Rol in db.Roles
                                                where Rol.RoleId == App.RoleId
                                                select Rol;
                                    if (Roles.LongCount() > 0)
                                    {
                                        foreach (Roles Rol in Roles)
                                        {
                                            RolesUser.Active = Rol.Active;
                                            RolesUser.Description = Rol.Description;
                                            RolesUser.RoleId = Rol.RoleId;
                                            var Count = from CountryU in db.Countries_Users
                                                        where CountryU.CountryId == User.CountryId
                                                        select CountryU;
                                            if (Count.LongCount() > 0)
                                            {
                                                foreach (Countries_Users C in Count)
                                                {
                                                    Country.CountryId = C.CountryId;
                                                    Country.CountryName = C.CountryName;
                                                    Country.ID = C.ID;

                                                    var UserC = from UserCountries in db.UserCountriesSet
                                                                where UserCountries.UserId == UserId
                                                                //&& UserCountries.UserId == UserId
                                                                select UserCountries;
                                                    if (UserC.LongCount() > 0)
                                                    {
                                                        foreach (UserCountries UC in UserC)
                                                        {
                                                            UserCountries1.CountryId = UC.CountryId;
                                                            UserCountries1.UserId = UC.UserId;
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }

                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}