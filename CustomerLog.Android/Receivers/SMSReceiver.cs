using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Telephony;
using System.Text;
using CustomerLog.Services;
using Android.Widget;


namespace CustomerLog.Droid.Receivers
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" }, Priority = (int)IntentFilterPriority.HighPriority)]
    public class SMSReceiver : BroadcastReceiver
    {
        private string SMSMessage { get; set; }
        private string SenderAddress { get; set; }
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action.Equals("android.provider.Telephony.SMS_RECEIVED"))
            {
                Bundle bundle = intent.Extras;
                if (bundle != null)
                {
                    try
                    {
                        var smsArray = (Java.Lang.Object[])bundle.Get("pdus");

                        foreach (var item in smsArray)
                        {
#pragma warning disable CS0618
                            var sms = SmsMessage.CreateFromPdu((byte[])item);
#pragma warning restore CS0618
                            SenderAddress = sms.OriginatingAddress;
                            SMSMessage = sms.MessageBody;
                            Toast.MakeText(context,$"From {SenderAddress} : {SMSMessage}", ToastLength.Long).Show();
                        }
                        
                        ///if (PhoneNumberUtils.Compare("your number", sender))
                        if (SenderAddress == "AirtelMoney" || SenderAddress == "MTNMoney" || SenderAddress == "0956307462")
                        {
                            CustomerServices.ProcessMessage(SMSMessage, SenderAddress);
                        }
                        
                    }
                    catch (Exception)
                    {
                        //Something went wrong.
                    }
  
                }
            }
        }
    }
}