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
    public class BackupCode
    {
        private string apiKey;
        private string apiHost;

        //Set variables
        private string identifier = "myIdentifier";

        public BackupCode(string apiKey, string apiHost)
        {
            this.apiKey = apiKey;
            this.apiHost = apiHost;
        }

        public void Create()
        {
            Twizo twizo = new Twizo(this.apiKey, this.apiHost);
            var backup = twizo.CreateBackupCode(this.identifier);

            backup.Create();
            LogResponse(backup);
        }

        public void Delete()
        {
            Twizo twizo = new Twizo(this.apiKey, this.apiHost);
            var backup = twizo.CreateBackupCode(this.identifier);

            backup.Delete();
            LogResponse(backup);
        }

        public void Update()
        {
            Twizo twizo = new Twizo(this.apiKey, this.apiHost);
            var backup = twizo.CreateBackupCode(this.identifier);

            backup.Update();
            LogResponse(backup);
        }

        public void GetStatus()
        {
            Twizo twizo = new Twizo(this.apiKey, this.apiHost);
            var backup = twizo.GetBackupCode(this.identifier);

            LogResponse(backup);
        }

        public void Verify(string token)
        {
            Twizo twizo = new Twizo(this.apiKey, this.apiHost);
            var backup = twizo.CreateBackupCode(this.identifier);

            backup.Verify(token);
            LogResponse(backup);
            
        }

        private void LogResponse(TwizoAPI.Entity.BackupCode backup)
        {
            string file = Menu.MyResultsFolder + @"\TwizoTestLogResponse.txt";
            File.Delete(file);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----------BACKUP CODE----------");

            Type type = backup.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "codes")
                {
                    Array a = (Array)property.GetValue(backup);
                    if (a != null)
                    {
                        foreach (object code in a)
                        {
                            sb.AppendLine(String.Format("{0, -25} : {1}", property.Name, code.ToString()));
                        }
                    }
                    else
                    {
                        sb.AppendLine(String.Format("{0, -25} : {1}", property.Name, "null"));
                    }
                }
                else
                {
                    object value = property.GetValue(backup);
                    sb.AppendLine(String.Format("{0, -25} : {1}", property.Name, property.GetValue(backup)));
                }
                
            }
            sb.AppendLine(Environment.NewLine);

            File.AppendAllText(file, sb.ToString());
        }
    }
}
