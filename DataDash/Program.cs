using DataDash;
using DataDash.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Threading;

#if DEBUG

Environment.SetEnvironmentVariable("default", "UrlToPoll=http://localhost:5013/WeatherForecast;IntervalBetweenUpdatesInSeconds=2;HttpAuthHeaderScheme=bearer;HttpAuthHeaderValue=sfghwrewfrgrtt2w");
Environment.SetEnvironmentVariable("data", "UrlToPoll=http://localhost:5013/Test;IntervalBetweenUpdatesInSeconds=2;HttpAuthHeaderScheme=bearer;HttpAuthHeaderValue=sfghwrewfrgrtt2w");

#endif

var variables = Environment.GetEnvironmentVariables();

foreach(string key in variables.Keys)
{
    string? value = Environment.GetEnvironmentVariable(key);

    if (value != null)
    {
        try
        {
            string? urlToPoll = null;
            string? intervalBetweenUpdatesInSeconds = null;
            string? httpAuthHeaderScheme = null;
            string? httpAuthHeaderValue = null;

            string[] parts = value.Split(';');

            foreach (string part in parts)
            {
                string name = part.Split("=")[0];
                string evValue = part.Split("=")[1];

                switch(name.ToLower())
                {
                    case "urltopoll":
                        urlToPoll = evValue;
                        break;

                    case "intervalbetweenupdatesinseconds":
                        intervalBetweenUpdatesInSeconds = evValue;
                        break;

                    case "httpauthheaderscheme":
                        httpAuthHeaderScheme = evValue;
                        break;

                    case "httpauthheadervalue":
                        httpAuthHeaderValue = evValue;
                        break;
                }
            }

            if (urlToPoll != null && intervalBetweenUpdatesInSeconds != null)
            {
                // start the background thread that polls for the data
                new Thread(() =>
                {

                    // thread will close when application is closed if IsBackground = true
                    Thread.CurrentThread.IsBackground = true;

                    DateTime lastCheck = DateTime.Now;

                    // thread will automatically be closed when the application is closed
                    // using while(true) will not prevent the application from closing
                    // or cause it to keep running when the application is closed
                    using (HttpClient client = new HttpClient())
                    {
                        while (true)
                        {
                            if (DateTime.Now > lastCheck.AddSeconds(int.Parse(intervalBetweenUpdatesInSeconds)))
                            {
                                FetchData(client, key, urlToPoll, httpAuthHeaderScheme, httpAuthHeaderValue);
                            }

                            // this sleep value determines the accuracy of the check.
                            // the delay between update triggers will up to this value more than intervalBetweenUpdatesInSeconds 
                            Thread.Sleep(100);
                        }
                    }

                }).Start();
            }

            GlobalData.PageData.Add(key.ToLower(), new PageInstance());

        }
        catch
        {
            // unable to parse
        }
    }
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


void FetchData(HttpClient client, string key, string urlToPoll, string? httpAuthHeaderScheme = null, string? httpAuthHeaderValue = null)
{
    string? url = urlToPoll;

    if (url == null)
    {
        GlobalData.PageData[key].ErrorMessage = "UrlToPoll environmental variable not set";
        return;
    }
    else
    {

        if (httpAuthHeaderValue == null && httpAuthHeaderScheme != null)
        {
            GlobalData.PageData[key].ErrorMessage = "HttpAuthHeaderScheme environmental variable provided but HttpAuthHeaderValue missing";
        }
        else if (httpAuthHeaderValue != null && httpAuthHeaderScheme == null)
        {
            GlobalData.PageData[key].ErrorMessage = "HttpAuthHeaderValue environmental variable provided but HttpAuthHeaderScheme missing";
        }
        else
        {
            if (httpAuthHeaderValue != null && httpAuthHeaderScheme != null)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(httpAuthHeaderScheme, httpAuthHeaderValue);
            }

            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                {
                    GlobalData.PageData[key].ErrorMessage = "API call returned status code " + (int)response.StatusCode;
                }
                else
                {
                    string json = response.Content.ReadAsStringAsync().Result;

                    if (json == null || json == "")
                    {
                        GlobalData.PageData[key].ErrorMessage = "Unable to parse data returned from api";
                    }
                    else
                    {
                        GlobalData.PageData[key].DashboardData = JsonConvert.DeserializeObject<DataTransferObject>(json) ?? new DataTransferObject();
                        GlobalData.PageData[key].ErrorMessage = null;
                    }
                }

            }
            catch (Exception ex)
            {

                GlobalData.PageData[key].ErrorMessage = ex.GetBaseException().Message;
            }
        }
    }

}