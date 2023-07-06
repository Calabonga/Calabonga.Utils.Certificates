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


        public static List<CertificateInfo> GetAllCertificates()
        {
            var result = new List<CertificateInfo>();
            foreach (var store in _stores)
            {
                var collection = FindInStore(GetStore(store), null, null);
                ProcessCollection(store, collection, result);
            }

            return result;
        }


        public static List<CertificateInfo> FindCertificates(string term, X509FindType findType, bool validOnly = false)
        {
            var result = new List<CertificateInfo>();
            foreach (var store in _stores)
            {
                var collection = FindInStore(GetStore(store), term, findType, validOnly);

                ProcessCollection(store, collection, result);
            }

            return result;
        }

        private static void ProcessCollection(StoreName storeName, X509Certificate2Collection collection, List<CertificateInfo> result)
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
                    ExpiredDate = item.GetExpirationDateString()
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
