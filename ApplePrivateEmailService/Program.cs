using Amazon.SimpleEmail.Model;
using Amazon.SimpleEmail;
using Amazon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ApplePrivateEmailService
{
    class Program
    {
        static void Main(string[] args)
        {

            Body emailreqbody = new Body();
            if (true)
            {
                emailreqbody.Html = new Content
                {
                    Charset = "UTF-8",
                    Data = "<h1>Test Email from Apple Relay Service with different settings</h1>",
                };
            }
            SendEmailRequest sendRequest = null;
            SendEmailResponse response = null;
            string FromAddress = "no-reply@sportsadda.com";
            List<string> to = new List<string>();
            //to.Add("5dqtjg5vyq@privaterelay.appleid.com");
            to.Add("qmyr5bksa4@privaterelay.appleid.com");

            try
            {
                //emailreqbody = new Body();

                using (var client = SESClient(false))
                {
                    sendRequest = new SendEmailRequest
                    {
                        Source = FromAddress,
                        Destination = new Destination
                        {
                            ToAddresses = to,
                            //CcAddresses = cc,
                            //BccAddresses = bcc
                        },
                        Message = new Amazon.SimpleEmail.Model.Message
                        {
                            Subject = new Content("Test Apple with different settings"),
                            Body = emailreqbody
                        }
                    };

                    response = client.SendEmail(sendRequest);

                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine("Email Sent");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Response Not OK");
                        Console.ReadLine();
                    }

                }
            }


            
            catch (Exception ex)
            {
                Console.WriteLine("Failed");
                Console.WriteLine("exception - " + Convert.ToString(ex));
                Console.ReadLine();
            }

        }

        private static AmazonSimpleEmailServiceClient SESClient(bool useProfile)

        {
            if (useProfile)
            {
                AmazonSimpleEmailServiceConfig config = new AmazonSimpleEmailServiceConfig();
                config.RegionEndpoint = RegionEndpoint.USEast1;
                Amazon.Runtime.AWSCredentials credentials = new Amazon.Runtime.StoredProfileAWSCredentials("default");
                return new AmazonSimpleEmailServiceClient(credentials, config);

            }
            else
                return new AmazonSimpleEmailServiceClient(RegionEndpoint.USEast1);


        }
    }
}
