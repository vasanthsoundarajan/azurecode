using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using System.Linq;
using Microsoft.Azure.WebJobs.Host;

namespace cosdbselect
{
    public static class Function1
    {
        //Get server status
        [FunctionName("getServerstats")]
        public static IActionResult Run(
             [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post",
                 Route = null)]HttpRequest req,
             [CosmosDB("SrvMon", "serverstatus",
                 ConnectionStringSetting = "CosmosDBConnection",
                 SqlQuery = "select * from serverstatus")] 
         IEnumerable<ServerStatus> sstat,
             ILogger log)
         {
             log.LogInformation("C# HTTP trigger function processed a request.");

            /*
            //if we need to apply any filter on the returned records
            var result = sstat.Where(s => s.id == "1");*/

            //return the result object
            return new OkObjectResult(sstat);
         }

        //ServerStatus class to get the values from IEnumerable
        public class ServerStatus
         {
            public string id;
            public string servername;
            public string serverstatus;
            public string lastcheck;
            public string lesatchecktime;
        }

    }
}
