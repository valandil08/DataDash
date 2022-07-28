using DataDash.Models;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        [HttpGet(Name = "GetTest")]
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


            dashboardData.TitleRows.Add(new TitleRow()
            {
                TopLeftColumnNumber = 0,
                TopLeftRowNumber = 3,
                BottomRightColumnNumber = 7,
                BottomRightRowNumber = 3,
                Title = new TextElement("Title Text")
            });

            dashboardData.DisplayTables.Add(new DisplayTable()
            {
                TopLeftColumnNumber = 0,
                TopLeftRowNumber = 4,
                BottomRightColumnNumber = 7,
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


            dashboardData.TitleRows.Add(new TitleRow()
            {
                TopLeftColumnNumber = 8,
                TopLeftRowNumber = 3,
                BottomRightColumnNumber = 15,
                BottomRightRowNumber = 3,
                Title = new TextElement("Title Text")
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