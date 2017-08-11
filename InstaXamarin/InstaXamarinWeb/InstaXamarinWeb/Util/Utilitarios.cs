using InstaXamarinWeb.Models;
using Newtonsoft.Json.Linq;
using PushSharp.Apple;
using PushSharp.Core;
using PushSharp.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace InstaXamarinWeb.Util
{
    public class Utilitarios
    {
        public static String URLBase { get { return System.Configuration.ConfigurationManager.AppSettings["URLBase"]; } }

        public static String GET_URL(String URLComplemento)
        {
            String URL = URLBase + (!URLBase.EndsWith("/") ? "/" : ""); // URL Fechando com "/"

            if (URLComplemento.StartsWith("/"))
                URLComplemento = URLComplemento.Remove(0, 1);

            URL = URL + URLComplemento;

            return URL;
        }


        public static String HashPassword(String Password)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(Password)))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public static AccessToken GetToken(HttpRequestMessage Request)
        {
            if (Request.Headers.Any(hh => hh.Key == "token"))
            {
                String Token = Request.Headers.First(hh => hh.Key == "token").Value.First();

                AccessToken AT = null;
                using (DB Banco = new DB())
                    AT = Banco.AccessTokens.Find(Token);

                if (AT != null)
                    return AT;
            }

            return null;
        }

        public static int GetTokenUsuarioLogado(HttpRequestMessage Request)
        {
            AccessToken AT = GetToken(Request);

            if (AT != null)
                return AT.UsuarioId;

            return -1;
        }

        public static int GetLarguraTela(HttpRequestMessage Request)
        {
            if (Request.Headers.Any(hh => hh.Key.ToLower() == "larguratela"))
            {
                String Token = Request.Headers.First(hh => hh.Key.ToLower() == "larguratela").Value.First();
                int RET = -1;

                int.TryParse(Token, out RET);

                return RET;
            }

            return -1;
        }

        public static void EnviaNotificacaoIOS(String Titulo, String Mensagem, int PostID, String Token)
        {
            var config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox,
                System.Web.Hosting.HostingEnvironment.MapPath("~/Certificados/InstaXamarin.p12"),
                "instaxamarin");

            // Create a new broker
            var apnsBroker = new ApnsServiceBroker(config);

            // Wire up events
            apnsBroker.OnNotificationFailed += (notification, aggregateEx) => {

                aggregateEx.Handle(ex => {

                    // See what kind of exception it was to further diagnose
                    if (ex is ApnsNotificationException)
                    {
                        var notificationException = (ApnsNotificationException)ex;

                        // Deal with the failed notification
                        var apnsNotification = notificationException.Notification;
                        var statusCode = notificationException.ErrorStatusCode;

                        Console.WriteLine($"Apple Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}");

                    }
                    else
                    {
                        // Inner exception might hold more useful information like an ApnsConnectionException			
                        Console.WriteLine($"Apple Notification Failed for some unknown reason : {ex.InnerException}");
                    }

                    // Mark it as handled
                    return true;
                });
            };

            apnsBroker.OnNotificationSucceeded += (notification) => {
                Console.WriteLine("Apple Notification Sent!");
            };

            apnsBroker.Start();

            apnsBroker.QueueNotification(new ApnsNotification
            {
                DeviceToken = Token,
                Payload = JObject.Parse("{\"aps\":{\"badge\":1, \"alert\":{\"title\":\"" + Titulo + "\", \"body\":\"" + Mensagem + "\"},\"sound\":\"default\", \"IDPost\" : \"" + PostID + "\"}}")
            });

            apnsBroker.Stop();
        }

        public static void EnviaNotificacaoAndroid(String Titulo, String Mensagem, int PostID, String Token)
        {
            // Configuration
            var config = new GcmConfiguration("1038368474519", "AIzaSyBRjWr1jCMf6gPOs7cpGZ36Ts2F-fgwHWQ", null);

            // Create a new broker
            var gcmBroker = new GcmServiceBroker(config);

            // Falha
            gcmBroker.OnNotificationFailed += (notification, aggregateEx) => {

                aggregateEx.Handle(ex => {

                    // See what kind of exception it was to further diagnose
                    if (ex is GcmNotificationException)
                    {
                        var notificationException = (GcmNotificationException)ex;

                        // Deal with the failed notification
                        var gcmNotification = notificationException.Notification;
                        var description = notificationException.Description;

                        Console.WriteLine($"GCM Notification Failed: ID={gcmNotification.MessageId}, Desc={description}");
                    }
                    else if (ex is GcmMulticastResultException)
                    {
                        var multicastException = (GcmMulticastResultException)ex;

                        foreach (var succeededNotification in multicastException.Succeeded)
                        {
                            Console.WriteLine($"GCM Notification Succeeded: ID={succeededNotification.MessageId}");
                        }

                        foreach (var failedKvp in multicastException.Failed)
                        {
                            var n = failedKvp.Key;
                            var e = failedKvp.Value;

                            Console.WriteLine($"GCM Notification Failed: ID={n.MessageId}, Desc={ex.Message}");
                        }

                    }
                    else if (ex is DeviceSubscriptionExpiredException)
                    {
                        var expiredException = (DeviceSubscriptionExpiredException)ex;

                        var oldId = expiredException.OldSubscriptionId;
                        var newId = expiredException.NewSubscriptionId;

                        Console.WriteLine($"Device RegistrationId Expired: {oldId}");

                        if (!string.IsNullOrWhiteSpace(newId))
                        {
                            // If this value isn't null, our subscription changed and we should update our database
                            Console.WriteLine($"Device RegistrationId Changed To: {newId}");
                        }
                    }
                    else if (ex is RetryAfterException)
                    {
                        var retryException = (RetryAfterException)ex;
                        // If you get rate limited, you should stop sending messages until after the RetryAfterUtc date
                        Console.WriteLine($"GCM Rate Limited, don't send more until after {retryException.RetryAfterUtc}");
                    }
                    else
                    {
                        Console.WriteLine("GCM Notification Failed for some unknown reason");
                    }

                    // Mark it as handled
                    return true;
                });
            };

            // Enviado
            gcmBroker.OnNotificationSucceeded += (notification) => {

            };

            // Start the broker
            gcmBroker.Start();

            // Queue a notification to send
            gcmBroker.QueueNotification(new GcmNotification
            {
                RegistrationIds = new List<string> { Token },
                Data = JObject.Parse("{ \"title\" : \"" + Titulo + "\", \"message\" : \"" + Mensagem + "\", \"IDPost\" : \"" + PostID + "\" }")
            });

            gcmBroker.Stop();
        }
    }
}