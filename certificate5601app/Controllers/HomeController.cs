using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace certificate5601app.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            string certThumbprint = "9FE3B4D6515152CEE52433CD12A77A344BC817F3";
            bool validOnly = false;

            using (X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                certStore.Open(OpenFlags.ReadOnly);

                X509Certificate2Collection certCollection = certStore.Certificates.Find(
                                            X509FindType.FindByThumbprint,
                                            // Replace below with your certificate's thumbprint
                                            certThumbprint,
                                            validOnly);
                // Get the first cert with the thumbprint
                X509Certificate2 cert = (X509Certificate2)certCollection.OfType<X509Certificate>().FirstOrDefault();

                if (cert is null)
                    throw new Exception($"Certificate with thumbprint {certThumbprint} was not found");

                // Use certificate
                Console.WriteLine(cert.FriendlyName);

                // Consider to call Dispose() on the certificate after it's being used, avaliable in .NET 4.6 and later

                ViewBag.Message = "Certificate Subject: " + cert.Subject;

            }

            

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}