using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace AliceSRV
{
    public static class WhaitAllData
    {
        public static bool ping_1 { get; set; }
        public static bool OK { get; set; }

        public static IPEndPoint Ip { get; set; }

        public static bool startWatch { get; set; }

        public static bool ForPreyConnect { get; set; }

        public static bool BAs { get; set; }
        public static int BAsNum { get; set; }
        public static bool stopWatch { get; set; }
        public static bool restartWatch { get; set; }
        public static bool Updatedata { get; set; }
        public static  bool NoError { get; set; }
    }
}
