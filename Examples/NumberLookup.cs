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
    public class NumberLookup
    {
        private string messageId;
        private string apiKey;
        private string apiHost;

        //Set variables
        string[] numbers = {"60151174973"};
        string tag = "";
        int validity = 259200;
        int resultType = 2;
        string callbackUrl = "";

        public NumberLookup(string apiKey, string apiHost)
        {
            this.apiKey = apiKey;
            this.apiHost = apiHost;
        }

        public void Send()
        {
            Twizo twizo = new Twizo(this.apiKey, this.apiHost);
            var lookup = twizo.CreateNumberLookup(numbers);

            if (this.tag != "")
                lookup.tag = this.tag;
            lookup.validity = this.validity;
            lookup.resultType = this.resultType;
            if (this.callbackUrl != "")
                lookup.callbackUrl = this.callbackUrl;

            lookup.Send();
            LogResponse(lookup);
            this.messageId = lookup.messageId;
        }

        public TwizoAPI.Entity.NumberLookup Refresh()
        {
            Twizo twizo = new Twizo(this.apiKey, this.apiHost);
            return twizo.GetNumberLookup(this.messageId);
        }

        private void LogResponse(TwizoAPI.Entity.NumberLookup lookup)
        {
            string file = Menu.MyResultsFolder + @"\TwizoTestLogResponse.txt";
            File.Delete(file);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----------NUMBER LOOKUP----------");

            Type type = lookup.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                sb.AppendLine(String.Format("{0, -25} : {1}", property.Name, property.GetValue(lookup, null)));
            }
            sb.AppendLine(Environment.NewLine);

            File.AppendAllText(file, sb.ToString());
        }
    }
}
