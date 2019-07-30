namespace Xlsx2Vcf.Services.Domain
{
    public class Contact
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Mail { get; private set; }

        public Contact(string firstName, string lastName, string phone, string mail)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Mail = mail;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 13;
                hash = (hash * 7) + FirstName.GetHashCode();
                hash = (hash * 7) + Phone.GetHashCode();
                hash = (hash * 7) + Mail.GetHashCode();
                return hash;
            }
        }
    }
}