using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        //private string firstName;
        //private string lastName;
        private string allPhone;
        private string allEmail;
        private string allProfile;

        public ContactData()
        {
        }
        public ContactData(string firstName)
        {
            FirstName = firstName;
        }
        public ContactData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return (FirstName == other.FirstName)&&(LastName == other.LastName);
        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode() + LastName.GetHashCode();
        }

        public override string ToString()
        {
            return "Firstname= " + FirstName + " Lastname= " + LastName;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Object.ReferenceEquals(other.LastName, this.LastName))
            {
                return FirstName.CompareTo(other.FirstName);
            }
            else
            {
                return LastName.CompareTo(other.LastName);
            }


        }

        private string CleanUpPhone(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
        }

        private string CleanUpPhone(string liter, string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return liter + ": "+ phone + "\r\n";
        }

        private string CleanUpEmail(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return email.Replace(" ", "").Replace("(", "").Replace(")", "") + "\r\n";
        }

        private string CleanUpAddress(string address)
        {
            if (address == null || address == "")
            {
                return "";
            }
            return address + "\r\n";
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string HomePhome { get; set; }
        public string MobilePhome { get; set; }
        public string WorkPhome { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }

        public string AllPhone {
            get
            {
                if(allPhone != null)
                {
                    return allPhone;
                }
                else
                {
                    return (CleanUpPhone(HomePhome) + CleanUpPhone(MobilePhome) + CleanUpPhone(WorkPhome)).Trim();
                }
            }
            set
            {
                allPhone = value;
            }
        }
        
        public string AllEmail
        {
            get
            {
                if (allEmail != null)
                {
                    return allEmail;
                }
                else
                {
                    return (CleanUpEmail(Email) + CleanUpEmail(Email2) + CleanUpEmail(Email3)).Trim();
                }
            }
            set
            {
                allEmail = value;
            }
        }

        public string AllProfile
        {
            get
            {
                if(allProfile != null)
                {
                    return allProfile;
                }
                else
                {
                    return (FirstName + " " + LastName + "\r\n" +
                           CleanUpAddress(Address) + "\r\n" + 
                           CleanUpPhone("H", HomePhome) + CleanUpPhone("M", MobilePhome) + CleanUpPhone("W", WorkPhome)+ "\r\n" +
                           CleanUpEmail(Email) + CleanUpEmail(Email2) + CleanUpEmail(Email3)).Trim();
                }
            }
            set
            {
                allProfile = value;
            }
        }
    }
}
