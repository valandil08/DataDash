using DataDash;
using DataDash.Models;
using Newtonsoft.Json;
using System;
using System.Threading;

#if DEBUG

Environment.SetEnvironmentVariable("UrlToPoll", "http://localhost:5013/WeatherForecast");
Environment.SetEnvironmentVariable("IntervalBetweenUpdatesInSeconds", "2");

// if these values are set
Environment.SetEnvironmentVariable("HttpAuthHeaderScheme", "bearer");
Environment.SetEnvironmentVariable("HttpAuthHeaderValue", "sfghwrewfrgrtt2w");



#endif


int intervalBetweenUpdatesInSeconds = GetIntervalBetweenUpdatesInSeconds();

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
            if(DateTime.Now > lastCheck.AddSeconds(intervalBetweenUpdatesInSeconds))
            {
                FetchData(client);
            }

            // this sleep value determines the accuracy of the check.
            // the delay between update triggers will up to this value more than intervalBetweenUpdatesInSeconds 
            Thread.Sleep(100);
        }
    }

}).Start();



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


int GetIntervalBetweenUpdatesInSeconds()
{
    string? intervalBetweenUpdatesInSecondsAsString = Environment.GetEnvironmentVariable("IntervalBetweenUpdatesInSeconds");

    if (intervalBetweenUpdatesInSecondsAsString != null)
    {
        try
        {
            return int.Parse(intervalBetweenUpdatesInSecondsAsString);
        }
        catch
        {
            Console.WriteLine("intervalBetweenUpdatesInSeconds is set but not a valid int, defaulting to 3 second interval");
        }
    }
    else
    {
        Console.WriteLine("intervalBetweenUpdatesInSeconds not set, defaulting to 3 second interval");
    }

    return 3;
}

void FetchData(HttpClient client)
{
    string? url = Environment.GetEnvironmentVariable("UrlToPoll");

    if (url == null)
    {
        GlobalData.ErrorMessage = "UrlToPoll environmental variable not set";
        return;
    }
    else
    {
        string? httpAuthHeaderScheme = Environment.GetEnvironmentVariable("HttpAuthHeaderScheme");
        string? httpAuthHeaderValue = Environment.GetEnvironmentVariable("HttpAuthHeaderValue");

        if (httpAuthHeaderValue == null && httpAuthHeaderScheme != null)
        {
            GlobalData.ErrorMessage = "HttpAuthHeaderScheme environmental variable provided but HttpAuthHeaderValue missing";
        }
        else if (httpAuthHeaderValue != null && httpAuthHeaderScheme == null)
        {
            GlobalData.ErrorMessage = "HttpAuthHeaderValue environmental variable provided but HttpAuthHeaderScheme missing";
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
                    GlobalData.ErrorMessage = "API call returned status code " + (int)response.StatusCode;
                }
                else
                {
                    string json = response.Content.ReadAsStringAsync().Result;

                    if (json == null || json == "")
                    {
                        GlobalData.ErrorMessage = "Unable to parse data returned from api";
                    }
                    else
                    {
                        GlobalData.DashboardData = JsonConvert.DeserializeObject<DataTransferObject>(json) ?? new DataTransferObject();
                        GlobalData.ErrorMessage = null;
                    }
                }

            }
            catch (Exception ex)
            {
                GlobalData.ErrorMessage = ex.GetBaseException().Message;
            }
        }
    }
}