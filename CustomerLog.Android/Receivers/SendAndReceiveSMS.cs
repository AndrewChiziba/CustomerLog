//using System;
//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Telephony;
//using System.Text;
//using CustomerLog.Services;
//using Android.Widget;

//namespace CustomerLog.Droid.Receivers
//{
//    [BroadcastReceiver(Enabled = true, Exported = true)]
//    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" }, Priority = (int)IntentFilterPriority.HighPriority)]
//    public class SendAndReceiveSMS : BroadcastReceiver
//    {
//        protected string message, address = string.Empty;

//        public override void OnReceive(Context context, Intent intent)
//        {
//            if (intent.Action.Equals("android.provider.Telephony.SMS_RECEIVED"))
//            {
//                Bundle bundle = intent.Extras;
//                if (bundle != null)
//                {
//                    try
//                    {
//                        var smsArray = (Java.Lang.Object[])bundle.Get("pdus");

//                        foreach (var item in smsArray)
//                        {
//#pragma warning disable CS0618
//                            var sms = SmsMessage.CreateFromPdu((byte[])item);
//#pragma warning restore CS0618
//                            address = sms.OriginatingAddress;
//                            message = sms.MessageBody;

//                            GlobalEvents.OnSMSReceived_Event(this, new SMSEventArgs() { PhoneNumber = address, Message = message });
//                            Toast.MakeText(context, message, ToastLength.Long).Show();
//                        }
//                    }
//                    catch (Exception)
//                    {
//                        //Something went wrong.
//                    }
//                }
//            }
//        }
//    }
//}