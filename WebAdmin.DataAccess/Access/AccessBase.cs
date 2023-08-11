using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAdmin.DataAccess
{
    public class AccessBase
    {
        public static Access FireStore { get { return new Access("genesis-93f18"); } }
        public static string Buket { get { return "genesis-93f18.appspot.com"; } }
    }
}
