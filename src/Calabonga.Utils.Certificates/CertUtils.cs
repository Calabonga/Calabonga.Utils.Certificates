using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Calabonga.Utils.Certificates
{
    /// <summary>
    /// 
    /// </summary>
    public static class Certificates
    {
        /// <summary>
        /// Stores where search
        /// </summary>
        private static readonly StoreName[] _stores =
        {
            StoreName.AddressBook,
            StoreName.AuthRoot,
            StoreName.CertificateAuthority,
            StoreName.Disallowed,
            StoreName.My,
            StoreName.Root,
            StoreName.TrustedPeople,
            StoreName.TrustedPublisher,
        };

        public static X509Certificate2? GetCertificate(CertificateInfo certificate)
        {
            if (certificate.StoreName == null)
            {
                return null;
            }

            Enum.TryParse(certificate.StoreName!, true, out StoreName store);
            var collection = FindInStore(GetStore(store), certificate.SerialNumber, certificate.FindType);
            return collection[0];

        }

        public static List<CertificateInfo> GetAllCertificates()
        {
            var result = new List<CertificateInfo>();
            foreach (var store in _stores)
            {
                var collection = FindInStore(GetStore(store), null, null);
                ProcessCollection(store, collection, result, null);
            }

            return result;
        }

        public static List<CertificateInfo> FindCertificates(string term, X509FindType findType, bool validOnly = false)
        {
            var result = new List<CertificateInfo>();
            foreach (var store in _stores)
            {
                var collection = FindInStore(GetStore(store), term, findType, validOnly);

                ProcessCollection(store, collection, result, findType);
            }

            return result;
        }

        private static void ProcessCollection(StoreName storeName, X509Certificate2Collection collection, List<CertificateInfo> result, X509FindType? x509FindType)
        {
            if (collection.Count <= 0)
            {
                return;
            }

            foreach (var item in collection)
            {
                var info = new CertificateInfo
                {
                    Name = item.GetNameInfo(X509NameType.SimpleName, true),
                    FriendlyName = item.FriendlyName ?? "-",
                    StoreName = storeName.ToString(),
                    IssuedTo = item.Issuer,
                    SerialNumber = item.GetSerialNumberString(),
                    NotAfter = item.NotAfter,
                    NotBefore = item.NotBefore,
                    ExpiredDate = item.GetExpirationDateString(),
                    FindType = x509FindType
                };

                result.Add(info);
            }
        }

        private static X509Store GetStore(StoreName storeName)
        {
            var store = new X509Store(storeName);
            store.Open(OpenFlags.ReadOnly);
            return store;
        }

        private static X509Certificate2Collection FindInStore(X509Store store, string? term, X509FindType? findType, bool validOnly = false)
        {
            if (string.IsNullOrEmpty(term) || findType is null)
            {
                return store.Certificates;
            }

            return store.Certificates.Find(findType.Value, term, validOnly);
        }
    }
}
