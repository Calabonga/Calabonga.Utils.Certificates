using System;
using System.Security.Cryptography.X509Certificates;

namespace Calabonga.Utils.Certificates
{
    public class CertificateInfo
    {
        public string? Name { get; set; }

        public string? FriendlyName { get; set; }

        public string? StoreName { get; set; }

        public string? IssuedTo { get; set; }

        public string? SerialNumber { get; set; }

        public DateTime NotAfter { get; set; }

        public DateTime NotBefore { get; set; }

        public string? ExpiredDate { get; set; }

        public X509FindType? FindType { get; set; }

        public override string ToString() => $"{Name} | {FriendlyName} | {StoreName} | {IssuedTo} | {SerialNumber} | {ExpiredDate}";
    }
}