using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Dropbox.Api;
using Dropbox.Api.Common;
using Dropbox.Api.Files;
using Dropbox.Api.Team;
using NUnit.Framework;


namespace my_Api
{
    public class Project
    {


        public static void Main(string[] args)
        {

            Dropbox t = new Dropbox();   
            t.UploadFile("local_file.txt");
            t.GetFileMetadata();
            t.DeleteFile();


        }
    }
    public class Test
    {
        [Test]
        public void Upload()
        {
            Dropbox t = new Dropbox();
            t.UploadFile("test.txt");
        }
        [Test]
        public void GetMetadata()
        {
            Dropbox t = new Dropbox();  
            t.GetFileMetadata();
        }
        [Test]
        public void Delete()
        {
            Dropbox t = new Dropbox();  
            t.DeleteFile();
        }
    }
    public class Dropbox
    {
        public void UploadFile(string file_name)
        {

            Chilkat.Rest chill = new Chilkat.Rest();

            bool success = chill.Connect("content.dropboxapi.com", 443, true, true);
            if (success != true)
            {
                Console.WriteLine(chill.LastErrorText);
                return;
            }

            chill.AddHeader("Content-Type", "application/octet-stream");
            chill.AddHeader("Authorization", "Bearer sl.AoKXn6W5sS_SkZSga4X3fr0TmZH-a8DhPffpzeQz9EgwedN3ZXQoz2G67bWlMbFUTbwy70c-yEQLz_giTRmP0NThhWtDcCN6zmpA2OHZ7UtokTl4uRg1uOnrxbVf9Wul4SOD48i5jJs");



            Chilkat.JsonObject json = new Chilkat.JsonObject();
            json.AppendString("path", "/" + file_name);
            json.AppendString("mode", "add");
            json.AppendBool("autorename", true);
            json.AppendBool("mute", false);
            chill.AddHeader("Dropbox-API-Arg", json.Emit());

            Chilkat.Stream fileStream = new Chilkat.Stream();
            fileStream.SourceFile = file_name;

            string responseStr = chill.FullRequestStream("POST", "/2/files/upload", fileStream);
            if (chill.LastMethodSuccess != true)
            {
                Console.WriteLine(chill.LastErrorText);
                return;
            }

            if (chill.ResponseStatusCode != 200)
            {
                Console.WriteLine("response status code = " + Convert.ToString(chill.ResponseStatusCode));
                Console.WriteLine("response status text = " + chill.ResponseStatusText);
                Console.WriteLine("response header: " + chill.ResponseHeader);
                Console.WriteLine("response body (if any): " + responseStr);
                Console.WriteLine("---");
                Console.WriteLine("LastRequestStartLine: " + chill.LastRequestStartLine);
                Console.WriteLine("LastRequestHeader: " + chill.LastRequestHeader);
                return;
            }

        }
        public void GetFileMetadata()
        {
            Chilkat.Rest chill = new Chilkat.Rest();
            bool succ;

            bool b = true;
            int numb = 443;
            bool bRecon = true;
            succ = chill.Connect("api.dropboxapi.com", numb, b, bRecon);
            if (succ != true)
            {
                System.Diagnostics.Debug.WriteLine("ConnectFailReason: " + Convert.ToString(chill.ConnectFailReason));
                System.Diagnostics.Debug.WriteLine(chill.LastErrorText);
                return;
            }


            Chilkat.JsonObject json = new Chilkat.JsonObject();
            json.UpdateString("path", "/local_file.txt");
            json.UpdateBool("include_media_info", false);
            json.UpdateBool("include_deleted", false);
            json.UpdateBool("include_has_explicit_shared_members", false);

            chill.AddHeader("Authorization", "Bearer sl.AoKXn6W5sS_SkZSga4X3fr0TmZH-a8DhPffpzeQz9EgwedN3ZXQoz2G67bWlMbFUTbwy70c-yEQLz_giTRmP0NThhWtDcCN6zmpA2OHZ7UtokTl4uRg1uOnrxbVf9Wul4SOD48i5jJs");
            chill.AddHeader("Content-Type", "application/json");

            Chilkat.StringBuilder sbRequestBody = new Chilkat.StringBuilder();
            json.EmitSb(sbRequestBody);
            Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
            succ = chill.FullRequestSb("POST", "/2/files/get_metadata", sbRequestBody, sbResponseBody);
            if (succ != true)
            {
                System.Diagnostics.Debug.WriteLine(chill.LastErrorText);
                return;
            }

            int respStatusCode = chill.ResponseStatusCode;
            System.Diagnostics.Debug.WriteLine("response status code = " + Convert.ToString(respStatusCode));
            if (respStatusCode >= 400)
            {
                System.Diagnostics.Debug.WriteLine("Response Status Code = " + Convert.ToString(respStatusCode));
                System.Diagnostics.Debug.WriteLine("Response Header:");
                System.Diagnostics.Debug.WriteLine(chill.ResponseHeader);
                System.Diagnostics.Debug.WriteLine("Response Body:");
                System.Diagnostics.Debug.WriteLine(sbResponseBody.GetAsString());
                return;
            }


        }
        public void DeleteFile()
        {
            Chilkat.Rest chill = new Chilkat.Rest();
            bool succ;

            bool b = true;
            int numb = 443;
            bool bRecon = true;
            succ = chill.Connect("api.dropboxapi.com", numb, b, bRecon);
            if (succ != true)
            {
                System.Diagnostics.Debug.WriteLine("ConnectFailReason: " + Convert.ToString(chill.ConnectFailReason));
                System.Diagnostics.Debug.WriteLine(chill.LastErrorText);
                return;
            }



            Chilkat.JsonObject json = new Chilkat.JsonObject();
            json.UpdateString("path", "/local_file.txt");

            chill.AddHeader("Authorization", "Bearer sl.AoKXn6W5sS_SkZSga4X3fr0TmZH-a8DhPffpzeQz9EgwedN3ZXQoz2G67bWlMbFUTbwy70c-yEQLz_giTRmP0NThhWtDcCN6zmpA2OHZ7UtokTl4uRg1uOnrxbVf9Wul4SOD48i5jJs");
            chill.AddHeader("Content-Type", "application/json");

            Chilkat.StringBuilder sbRequestBody = new Chilkat.StringBuilder();
            json.EmitSb(sbRequestBody);
            Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();
            succ = chill.FullRequestSb("POST", "/2/files/delete_v2", sbRequestBody, sbResponseBody);
            if (succ != true)
            {
                System.Diagnostics.Debug.WriteLine(chill.LastErrorText);
                return;
            }

        }
    }

}
