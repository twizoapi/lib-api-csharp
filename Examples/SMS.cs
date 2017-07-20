using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TwizoAPI;

namespace Examples
{
    public class SMS
    {
        private string messageId;
        private string apiKey;
        private string apiHost;

        //Define variables
        string[] recipients = new string[] { "601151174973" };
        string body = "This is a test sms.";
        string sender = "Twizo";
        int senderTon = -1;
        int senderNpi = -1;
        int pid = -1;
        //DateTime scheduledDelivery = DateTime.Now.AddMinutes(1);
        string tag = "";
        int validity = 259200;
        int resultType = 0;
        string callbackUrl = "";

        //Define advanced variables
        int dcs = -1;
        string udh = "";

        public SMS(string apiKey, string apiHost)
        {
            this.apiKey = apiKey;
            this.apiHost = apiHost;
        }

        public void Send()
        {
            //Create entity
            Twizo twizo = new Twizo(this.apiKey, this.apiHost);
            var sms = twizo.CreateSms(this.recipients, this.body, this.sender);
            sms.resultType = 2;

            //Set variables
            if (this.senderTon >= 0)
                sms.senderTon = this.senderTon;
            if (this.senderNpi >= 0)
                sms.senderNpi = this.senderNpi;
            if (this.pid >= 0)
                sms.pid = this.pid;
            //sms.scheduledDelivery = this.scheduledDelivery.ToString("yyyy-MM-ddTHH:mm:ssZ");
            if (this.tag != "")
                sms.tag = this.tag;
            sms.validity = this.validity;
            if (this.resultType >= 0)
                sms.resultType = this.resultType;
            if (this.callbackUrl != "")
                sms.callbackUrl = this.callbackUrl;

            if (this.dcs >= 0)
                sms.dcs = this.dcs;
            if (this.udh != "")
                sms.udh = this.udh;

            //Send
            sms.Send();

            //Log
            LogResponse(sms);
            this.messageId = sms.messageId;
        }

        public TwizoAPI.Entity.Sms Refresh()
        {
            Twizo twizo = new Twizo(this.apiKey, this.apiHost);
            return twizo.GetSms(this.messageId);
        }

        private void LogResponse(TwizoAPI.Entity.Sms sms)
        {
            string file = Menu.MyResultsFolder + @"\TwizoTestLogResponse.txt";
            File.Delete(file);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("-------------SMS-------------");

            Type type = sms.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                sb.AppendLine(String.Format("{0, -25} : {1}", property.Name, property.GetValue(sms, null)));
            }
            sb.AppendLine(Environment.NewLine);

            File.AppendAllText(file, sb.ToString());
        }
    }
}
