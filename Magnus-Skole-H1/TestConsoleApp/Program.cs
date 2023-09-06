using System;
using System.Net;
using System.IO;
using System.Text.Json;
//using System.Web.Script.Serialization;


class create_API_object
{
    public static string API_URL = "https://intempus.dk/web/v1";
    public static string API_KEY = "13fe9b81eb93d475c5fb57d79ec401543459610b";
    public static string USERNAME = "royeko5223@horsgit.com";
    public static string ENDPOINT = "customer";
    public static string DATA; // = new JavaScriptSerializer().Serialize(new
    //{
    //    city = "Copenhagen",
    //    company = "/web/v1/company/1/",
    //    contact = "John Doe",
    //    country = "Denmark ",
    //    email = "sample@gmail.com",
    //    name = "Mary Doe",
    //    notes = "Just another guy",
    //    phone = "639xxxxxxxxx",
    //    street_address = "Staunings Pl. 3, 1607 Copenhagen V, Denmark",
    //    zip_code = "1607"
    //});


    // https://www.intempus.dk/api-doc/
    // https://intempus.dk/api_documentation/#api-Accepted_Terms

    static void Main(string[] args)
    {

        var data = new
        {
            city = "Copenhagen",
            company = "/web/v1/company/1/",
            contact = "John Doe",
            country = "Denmark",
            email = "sample@gmail.com",
            name = "Mary Doe",
            notes = "Just another guy",
            phone = "639xxxxxxxxx",
            street_address = "Staunings Pl. 3, 1607 Copenhagen V, Denmark",
            zip_code = "1607"
        };

        DATA = JsonSerializer.Serialize(data);
        string response = CreateAPIObject();
        Console.WriteLine(response);

    }

    public static string CreateAPIObject()
    {
        string url = string.Format("{0}/{1}/", API_URL, ENDPOINT);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.ContentType = "application/json"; //set the content type to JSON.
        request.Method = "POST";

        // add the Authorization header to authenticate on our endpoint.
        string authorizationheader = string.Format("Authorization: ApiKey {0}:{1}", USERNAME, API_KEY);
        request.Headers.Add(authorizationheader);

        //initiate the request.
        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            streamWriter.Write(DATA);
            streamWriter.Flush();
            streamWriter.Close();
        }

        // Get the response.
        var httpResponse = (HttpWebResponse)request.GetResponse();
        var statusCode = httpResponse.StatusCode;
        StreamReader response = new StreamReader(httpResponse.GetResponseStream());
        return string.Format("Response Code: {0} - {1}\nData: {2}",
            (int)statusCode, statusCode.ToString(), response.ReadToEnd());
    }
}