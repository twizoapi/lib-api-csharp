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
    public class Verification
    {
        private string messageId;
        private string apiKey;
        private string apiHost;

        //Define variables
        string recipient = "601151174973";
        string type = "sms";
        int tokenLength = 6;
        string tokenType = "numeric";
        string tag = "";
        string sessionId = "";
        int validity = 60;
        bool call = false;
        string bodyTemplate = "Your verification token is: %token%";
        string sender = "Twizo";
        int senderTon = -1;
        int senderNpi = -1;
        int dcs = 0;

        public Verification(string apiKey, string apiHost)
        {
            this.apiKey = apiKey;
            this.apiHost = apiHost;
        }

        public Verification(string apiKey, string apiHost, string messageId)
        {
            this.apiKey = apiKey;
            this.apiHost = apiHost;
            this.messageId = messageId;
        }

        public void Send()
        {
            //Create entity
            Twizo twizo = new Twizo(this.apiKey, this.apiHost);
            TwizoAPI.Entity.Verification verification;
            if (call)
                verification = twizo.CreateVerificationCall(this.recipient);
            else
                verification = twizo.CreateVerification(this.recipient);

            //Set variables
            verification.type = this.type;
            verification.tokenLength = this.tokenLength;
            verification.tokenType = this.tokenType;
            if (this.tag != "")
                verification.tag = this.tag;
            if (this.sessionId != "")
                verification.sessionId = this.sessionId;
            verification.validity = this.validity;
            verification.bodyTemplate = this.bodyTemplate;
            verification.sender = this.sender;
            if (this.senderTon >= 0)
                verification.senderTon = this.senderTon;
            if (this.senderTon >= 0)
                verification.senderNpi = this.senderNpi;
            verification.dcs = this.dcs;

            //Send
            verification.Send();

            //Log
            LogResponse(verification);
            this.messageId = verification.messageId;
        }

        public Boolean Verify(string token)
        {
            Twizo twizo = new Twizo(this.apiKey, this.apiHost);
            var verification = twizo.GetTokenResult(token, this.messageId);
            LogResponse(verification);
            return (verification.statusCode == (int)TwizoAPI.Entity.VerificationStatusCode.SUCCESS);
        }

        public TwizoAPI.Entity.Verification Refresh()
        {
            Twizo twizo = new Twizo(this.apiKey, this.apiHost);
            return twizo.GetVerification(this.messageId);
        }

        private void LogResponse(TwizoAPI.Entity.Verification verification)
        {
            string file = Menu.MyResultsFolder + @"\TwizoTestLogResponse.txt";
            File.Delete(file);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----------VERIFICATION----------");

            Type type = verification.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                sb.AppendLine(String.Format("{0, -25} : {1}", property.Name, property.GetValue(verification, null)));
            }
            sb.AppendLine(Environment.NewLine);

            File.AppendAllText(file, sb.ToString());
        }

        public string getMessageId()
        {
            return this.messageId;
        }
    }
}
