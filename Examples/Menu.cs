using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using TwizoAPI;

namespace Examples
{
    public class Menu
    {
        //Set API key and select server
        private string apiKey = "";
        private string apiHost = "";

        private Verification verification;
        private SMS sms;
        private NumberLookup lookup;
        private BackupCode backup;

        public static string MyResultsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public void RunMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Please select a function.");
            Console.WriteLine("0 - Help");
            Console.WriteLine("1 - Send verification");
            Console.WriteLine("2 - Verify verification by messageId");
            Console.WriteLine("3 - Send SMS");
            Console.WriteLine("4 - Send number Lookup");
            Console.WriteLine("5 - Backup codes");
            Console.WriteLine("6 - Wallet balance");
            Console.WriteLine("7 - Export status");
            Console.WriteLine("8 - Export results"); 
            Console.WriteLine("9 - Exit");
            Console.WriteLine();

            string input = Console.ReadLine();

            switch (input)
            {
                case ("0"):
                    Help(); break;
                case ("1"):
                    Verification(); break;
                case ("2"):
                    VerifyByMessageId(); break;
                case ("3"):
                    Sms(); break;
                case ("4"):
                    NumberLookup(); break;
                case ("5"):
                    Backup(); break;
                case ("6"):
                    Balance(); break;
                case ("7"):
                    Status(); break;
                case ("8"):
                    Results(); break;
                case ("9"):
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    RunMenu(); break;
            }
        }

        private void Help()
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("The purpose of this program is to test the C# library of Twizo.");
            Console.WriteLine("Parameters for functions should be set in the code of the classes.");
            Console.WriteLine("By default, all results will be written to your Documents folder. This can be changed in the Menu-class.");
            Console.WriteLine();
            Console.WriteLine("Function details:");
            Console.WriteLine();
            Console.WriteLine("1 - Send a verification or check the validity of a token. Write response to TwizoTestLogResponse.txt.");
            Console.WriteLine("2 - Verify a verification by manually providing the token and message id.");
            Console.WriteLine("3 - Send an SMS. Write response to TwizoTestLogResponse.txt.");
            Console.WriteLine("4 - Send a number lookup. Write reponse to TwizoTestLogResponse.txt.");
            Console.WriteLine("5 - Create, delete, update or verify backup codes.");
            Console.WriteLine("6 - Retrieve information about your wallet.");
            Console.WriteLine("7 - Request the status of available instances of all functions. Write the data to TwizoTestLogStatus.txt.");
            Console.WriteLine("8 - Request the sms delivery reports and number lookup results. Write the data to TwizoTestLogResults.txt.");
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey(true);
            RunMenu();
        }

        private void Verification()
        {
            verification = new Verification(apiKey, apiHost);
            verification.Send();

            Console.WriteLine();
            Console.WriteLine("Verification was succesfully sent and server response was written to TwizoTestLogResponse.txt.");
            Console.WriteLine("Enter token to verify or press 0 to print messageId and return to main menu.");
            Console.WriteLine();
            string token = Console.ReadLine();
            Console.WriteLine();

            if (!(token == "0"))
            {
                if (verification.Verify(token))
                {
                    Console.WriteLine("Verification was succesfull! Server reponse was written to TwizoTestLogResponse.txt.");
                    Console.WriteLine("Press any key to return to main menu.");
                }
                else
                {
                    Console.WriteLine("Invalid token. Server response was written to TwizoTestLogResponse.txt.");
                    Console.WriteLine("Press any key to return to main menu.");
                }
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine(verification.getMessageId());
            }
            RunMenu();
        }

        private void VerifyByMessageId()
        {
            Console.WriteLine();
            Console.WriteLine("Enter token and messageId, separated by a colon.");
            Console.WriteLine();
            string input = Console.ReadLine();
            string token = input.Split(':')[0];
            string messageId = input.Split(':')[1];
            verification = new Verification(apiKey, apiHost, messageId);
            Console.WriteLine();
            if (verification.Verify(token))
            {
                Console.WriteLine("Verification was succesfull! Server reponse was written to TwizoTestLogResponse.txt.");
                Console.WriteLine("Press any key to return to main menu.");
            }
            else
            {
                Console.WriteLine("Invalid token. Server response was written to TwizoTestLogResponse.txt.");
                Console.WriteLine("Press any key to return to main menu.");
            }
            Console.ReadKey(true);
            RunMenu();
        }

        private void Sms()
        {
            sms = new SMS(apiKey, apiHost);
            sms.Send();

            Console.WriteLine();
            Console.WriteLine("Sms was succesfully sent! Server response was written to TwizoTestLogResponse.txt.");
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey(true);
            RunMenu();
        }

        private void NumberLookup()
        {
            lookup = new NumberLookup(apiKey, apiHost);
            lookup.Send();

            Console.WriteLine();
            Console.WriteLine("Number lookup was sucesfully sent! Server response was written to TwizoTestLogResponse.txt.");
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey(true);
            RunMenu();
        }

        private void Backup()
        {
            backup = new BackupCode(apiKey, apiHost);
            Console.WriteLine();
            Console.WriteLine("Choose a function");
            Console.WriteLine("1 - Create backup codes");
            Console.WriteLine("2 - Get backup code status");
            Console.WriteLine("3 - Delete backup codes");
            Console.WriteLine("4 - Update backup codes");
            Console.WriteLine("5 - Verify backup code");
            Console.WriteLine();
            string function = Console.ReadLine();

            if (function == "1")
            {
                backup.Create();
            }
            else if (function == "2")
            {
                backup.GetStatus();
            }
            else if (function == "3")
            {
                backup.Delete();
            }
            else if (function == "4")
            {
                backup.Update();
            }
            else if (function == "5")
            {
                Console.WriteLine("Enter your backup token");
                string token = Console.ReadLine();
                backup.Verify(token);
            }
            else
                Backup();

            Console.WriteLine();
            Console.WriteLine("Backupcode operation succesfull! Server reponse was written to TwizoTestLogResponse.txt.");
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey(true);
            RunMenu();
        }

        private void Balance()
        {
            Twizo twizo = new Twizo(apiKey, apiHost);
            var balance = twizo.CreateBalance();
            balance.LoadData();
            Console.WriteLine();
            Console.WriteLine("Wallet name: " + balance.wallet);
            Console.WriteLine("Currency code: " + balance.currencyCode);
            Console.WriteLine("Balance: " + balance.credit.ToString());
            Console.WriteLine("Free verifications: " + balance.freeVerification.ToString());
            Console.WriteLine();
            RunMenu();
        }

        private void Status()
        {
            string file = Menu.MyResultsFolder + @"\TwizoTestLogStatus.txt";
            File.Delete(file);

            if (this.verification != null)
            {
                var myVerification = verification.Refresh();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("----------VERIFICATION----------");

                Type type = myVerification.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    sb.AppendLine(String.Format("{0, -25} : {1}", property.Name, property.GetValue(myVerification, null)));
                }
                sb.AppendLine(Environment.NewLine);

                File.AppendAllText(file, sb.ToString());
            }

            if (this.sms != null)
            {
                var mySMS = sms.Refresh();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("-------------SMS-------------");

                Type type = mySMS.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    sb.AppendLine(String.Format("{0, -25} : {1}", property.Name, property.GetValue(mySMS, null)));
                }
                sb.AppendLine(Environment.NewLine);

                File.AppendAllText(file, sb.ToString());
            }

            if (this.lookup != null)
            {
                var myLookup = lookup.Refresh();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("---------NUMBER LOOKUP---------");

                Type type = myLookup.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    sb.AppendLine(String.Format("{0, -25} : {1}", property.Name, property.GetValue(myLookup, null)));
                }
                sb.AppendLine(Environment.NewLine);

                File.AppendAllText(file, sb.ToString());
            }

            Process.Start(file);
            Console.WriteLine("Status was succesfully requested for the existing instances and written to TwizoTestLogStatus.txt");
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey(true);
            RunMenu();
        }

        private void Results()
        {
            string file = Menu.MyResultsFolder + @"\TwizoTestLogResults.txt";
            File.Delete(file);

            Twizo twizo = new Twizo(apiKey, apiHost);
            var SmsReports = twizo.GetSmsResults();
            var LookupResults = twizo.GetNumberLookupResults();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("---------------SMS REPORTS---------------");
            foreach (var sms in SmsReports)
            {
                Type type = sms.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    sb.AppendLine(String.Format("{0, -25} : {1}", property.Name, property.GetValue(sms, null)));
                }
                sb.AppendLine(Environment.NewLine);
                sb.AppendLine("----------------------");
            }

            sb.AppendLine("----------NUMBER LOOKUP RESULTS----------");
            foreach (var lookup in LookupResults)
            {
                Type type = lookup.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    sb.AppendLine(String.Format("{0, -25} : {1}", property.Name, property.GetValue(lookup, null)));
                }
                sb.AppendLine(Environment.NewLine);
                sb.AppendLine("----------------------");
            }

            File.AppendAllText(file, sb.ToString());

            Process.Start(file);
            Console.WriteLine("Results were succesfully requested and written to TwizoTestLogResults.txt");
            Console.WriteLine("Press any key to return to main menu.");
            Console.ReadKey(true);
            RunMenu();
        }
    }
}
