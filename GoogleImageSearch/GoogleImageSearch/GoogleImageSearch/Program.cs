using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Google.Apis.Customsearch.v1;
using Google.Apis.Discovery.v1;
using Google.Apis.Discovery.v1.Data;
using Google.Apis.Services;

namespace GoogleImageSearch
{
    /// <summary>
    /// This example uses the discovery API to list all APIs in the discovery repository.
    /// https://developers.google.com/discovery/v1/using.
    /// <summary>
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Discovery API Sample");
            Console.WriteLine("====================");
            try
            {
                new Program().Run().Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                }
            }
            /*Console.WriteLine("Press any key to continue...");
            Console.ReadKey();*/
        }

        private async Task Run()
        {
            /*
            // Create the service.
            var service = new DiscoveryService(new BaseClientService.Initializer
            {
                ApplicationName = "Discovery Sample",
                ApiKey = "AIzaSyBQdB5nHRipY4iCLCcLv1JRVfZ8ORXvdSM",
            });

            // Run the request.
            Console.WriteLine("Executing a list request...");
            var result = await service.Apis.List().ExecuteAsync();

            // Display the results.
            if (result.Items != null)
            {
                foreach (DirectoryList.ItemsData api in result.Items)
                {
                    Console.WriteLine(api.Id + " - " + api.Title);
                }
            }*/

            var css = new CustomsearchService(new BaseClientService.Initializer
            {
                ApplicationName = "Discovery Sample",
                ApiKey = "AIzaSyBQdB5nHRipY4iCLCcLv1JRVfZ8ORXvdSM",
            });

            //key=AIzaSyBQdB5nHRipY4iCLCcLv1JRVfZ8ORXvdSM
            //cx =002750842850285730046:zc4edzjucig
            //q =megalodon%20ccr
            
            CseResource.ListRequest listRequest = css.Cse.List("volvo v90");
            listRequest.Cx = "002750842850285730046:zc4edzjucig";
            listRequest.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
            listRequest.FileType = "jpg";
            listRequest.ImgSize = CseResource.ListRequest.ImgSizeEnum.Xlarge;

            int setNum = 10;
            int setSize = 10;

            for (int i = 0; i < setNum; i++)
            {
                Console.Out.WriteLine("**** Downloading set {0} of {1} of {2} images", i+1, setNum, setSize);
                listRequest.Start = i*setSize + 1;
                var results = listRequest.Execute();
                foreach (var result in results.Items)
                {
                    string localFilename = $"c:\\tmp\\volvo\\teach\\V90\\{Guid.NewGuid()}.{"jpg"}";
                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            Console.Out.WriteLine("{0} : {1}", localFilename, result.Title);
                            client.DownloadFile(result.Link, localFilename);
                        }
                        catch (Exception ex)
                        {
                            Console.Out.WriteLine("\t*** EXCEPTION : {0}", ex);
                        }
                        
                    }
                }
            }
        }
    }
}