using System.Threading.Tasks;
using System;
using Amazon.Lambda;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.Lambda.Model;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using Newtonsoft.Json;
using Amazon.S3.Model;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace blankCsharp
{
  class Function
  {
    static Function() {
      initialize();
    }

    static void initialize() {
      // AWSSDKHandler.RegisterXRayForAllServices();
    }

    public async static Task<bool> Upload()
    {
      IronPdf.Installation.TempFolderPath = @"/tmp";
      IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
      var PDF = Renderer.RenderHtmlAsPdf("<h1>Hello from DocThread</h1>");
      PDF.SaveAs("/tmp/sample.pdf");

      using (var client = new AmazonS3Client(Amazon.RegionEndpoint.SAEast1))
      {
        var fileTransferUtility = new TransferUtility(client);
        await fileTransferUtility.UploadAsync("/tmp/sample.pdf" , "lambda-test-8127");
        Console.WriteLine("Upload 1 completed");
      }

      return true;
    }

    public async Task<string> FunctionHandler(string input, ILambdaContext context)
    {
      await Upload();
      return "works?";
    }

    public static async Task Main(string[] args)
    {
      await Upload();
      Console.WriteLine("fu?");
    }
  }
}
