using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerLog.Services
{
    interface ISendSms
    {
        void Send(string address, string message);
    }
}
