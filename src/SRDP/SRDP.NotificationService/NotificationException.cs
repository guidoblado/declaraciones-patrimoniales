using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRDP.NotificationService
{
    public class NotificationException : Exception
    {
        internal NotificationException(string bussinessMessage) : base(bussinessMessage)
        {

        }
    }
}
