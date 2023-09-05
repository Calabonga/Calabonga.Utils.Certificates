# Calabonga.Utils.Certificates

How to use? It's very simple!

```csharp
﻿// See https://aka.ms/new-console-template for more information

using Calabonga.Utils.Certificates;
using Calabonga.Utils.Certificates.Demo;
using System.Security.Cryptography.X509Certificates;

Printer.PrintTitle("Searching '04d54dc0a2016b263eeeb255d321056e' as serial number..");
Printer.PrintCollection(Certificates.FindCertificates("04d54dc0a2016b263eeeb255d321056e", X509FindType.FindBySerialNumber));

Printer.PrintTitle("Searching 'localhost' by issuer name..");
Printer.PrintCollection(Certificates.FindCertificates("Localhost", X509FindType.FindByIssuerName));

Printer.PrintTitle("Getting all in all stores...");
Printer.PrintCollection(Certificates.GetAllCertificates());

```
