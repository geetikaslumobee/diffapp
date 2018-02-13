using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace DifferentApp.Controllers
{
    public class QueuesController : Controller
    {
        // GET: Queues
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddMessage()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
          CloudConfigurationManager.GetSetting("myresourcegroup1diag309_AzureStorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("tempreading32");
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(10);
            var timer = new System.Threading.Timer((e) =>
            {
                Random rnd = new Random();
                float temp = rnd.Next(40, 100);
                CloudQueueMessage message = new CloudQueueMessage(Convert.ToString(temp));
                queue.AddMessage(message);
                ViewBag.QueueName = queue.Name;
                ViewBag.Message = message.AsString;

            }, null, startTimeSpan, periodTimeSpan);

            return View();
        }
    }
}
