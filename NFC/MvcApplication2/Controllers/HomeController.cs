// use TinyGPIO.cs
// https://github.com/sample-by-jsakamoto/SignalR-on-RaspberryPi/blob/master/myapp/TinyGPIO.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using MvcApplication2;
using MvcApplication2.SignalR;
using System.Diagnostics;
using MvcApplication2.PInvoke;
using System.Runtime.InteropServices;
using SharpNFC;


namespace MvcApplication2.Controllers
{

    public class HomeController : Controller
    {
        [HttpPut]
        public ActionResult TurnOnLED()
        {
            return null;
        }

        [HttpPut]
        public ActionResult TurnOffLED()
        {
            return null;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OnButton(string parameterName)
        {
            string result = "dummy";

            List<string> deviceNameList = new List<string>();

            NFCContext nfcContext = new NFCContext();

            NFCDevice nfcDevice = nfcContext.OpenDevice(null);

            deviceNameList = nfcContext.ListDeviceNames();

            Console.WriteLine("device count: " + deviceNameList.Count());

            foreach (string deviceName in deviceNameList)
            {
                Console.WriteLine("deviceName: " + deviceName);
            }

            int rtn = nfcDevice.initDevice();
            if (rtn < 0)
            {
                Console.WriteLine("Context init failed");
            }

            nfc_target nfcTarget = new nfc_target();
            List<nfc_modulation> nfc_modulationList = new List<nfc_modulation>();
            nfc_modulation nfcModulation = new nfc_modulation();
            nfcModulation.nbr = nfc_baud_rate.NBR_106;
            nfcModulation.nmt = nfc_modulation_type.NMT_ISO14443A;

            nfc_modulationList.Add(nfcModulation);
            rtn = nfcDevice.Pool(nfc_modulationList, 1, 2, out nfcTarget);

            if (rtn < 0)
            {
                Console.WriteLine("nfc poll targert failed");
            }
            else if (rtn > 0)
            {
                Console.WriteLine("nfc target found!!!");

                string s = nfcDevice.str_target(nfcTarget);

                Console.Write(s);
            }
            else
            {
                Console.WriteLine("no nfc target found");
            }

            Console.WriteLine(result);
            return Json(new { success = true, show = result }, JsonRequestBehavior.AllowGet);

        }
    }
}









