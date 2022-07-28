using DataDash.Models;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        [HttpGet(Name = "GetWeatherForecast")]
        public DataTransferObject Get()
        {
            DataTransferObject dashboardData = new DataTransferObject();
            dashboardData.NumberOfColumns = 16;
            dashboardData.NumberOfRows = 18;

            dashboardData.TitleRows.Add(new TitleRow()
            {
                TopLeftColumnNumber = 0,
                TopLeftRowNumber = 0,
                BottomRightColumnNumber = 15,
                BottomRightRowNumber = 0,
                Title = new TextElement("Title Text")
            });

            dashboardData.SingleReadouts.Add(new SingleReadOutRow()
            {
                TopLeftColumnNumber = 8,
                TopLeftRowNumber = 15,
                BottomRightColumnNumber = 15,
                BottomRightRowNumber = 16,
                ReadOuts = new List<SingleReadOutEntry>()
                {
                  new SingleReadOutEntry()
                  {
                     Title = new TextElement("Title 1"),
                     Value = new TextElement("value 1")
                  },
                  new SingleReadOutEntry()
                  {
                     Title = new TextElement("Title 2"),
                     Value = new TextElement("value 2")
                  }
                }
            });

            dashboardData.DisplayTables.Add(new DisplayTable()
            {
                TopLeftColumnNumber = 8,
                TopLeftRowNumber = 4,
                BottomRightColumnNumber = 15,
                BottomRightRowNumber = 13,
                headers = new TextElement[] {
                      new TextElement("Col 1"),
                      new TextElement("Col 2"),
                      new TextElement("Col 3")
                   },
                dataRows = new List<TextElement[]>()
                {
                  new TextElement[]{ new TextElement("col 1"), new TextElement("col 2"), new TextElement("col 3") },
                  new TextElement[]{ new TextElement("col 1"), new TextElement("col 2"), new TextElement("col 3") },
                  new TextElement[]{ new TextElement("col 1"), new TextElement("col 2"), new TextElement("col 3") }
                }
            });

            dashboardData.HostingInfoFooter = new HostingInfoFooter(17, 17);

            return dashboardData;
        }
    }
}