using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DifferentApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
             CloudConfigurationManager.GetSetting("myresourcegroup1diag309_AzureStorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("tempreading32");
            queue.CreateIfNotExists();
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(AddMessage);
            aTimer.Interval = 10000;
            aTimer.Enabled = true;




        }

        public void AddMessage(object source, ElapsedEventArgs e)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
          CloudConfigurationManager.GetSetting("myresourcegroup1diag309_AzureStorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("tempreading32");
            Random rnd = new Random();
            float temp = rnd.Next(40, 100);
            CloudQueueMessage message = new CloudQueueMessage(Convert.ToString(temp));
            queue.AddMessage(message);

        }

    }
}