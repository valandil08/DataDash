# Data Dash
 
Data Dash is designed to minimimise the impact of a common problem faced with expanding software systems, the need for more monitoring dashboards and the additional work required to maintain then.

By making the UI layout part of the data transfer object the dashboard receives from the api it calls, the data for the ui config and the data it is displaying is store in code of the system being monitor and thus will throw error if a merge or refactor would break the code instead of finding out after deployment that the dashboard no longer works.

Common senarios that can cause this are are: database refactors, code restructuring, changing how a system works or large code merges from multiple branches.
All of these risk breaking the dashboards monitoring them if the code generating the data for the dashboard is external to the system being monitored.

Setting up the data dash is just 5 steps
- Learning how the DataTransferObject class and its subclasses work
- Implement a web api endpoint returning a DataTransferObject
- Learn how to config the data dash website (4 environmental variable, thats it)
- Setup the data dash website
- Deploy it pointing at the web api endpoint created earlier

# Learning how the DataTransferObject class and its subclasses work

```C#

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
   dataRows = new List<TextElement[]>
   {
      new TextElement[]{ new TextElement("col 1"), new TextElement("col 2"), new TextElement("col 3") },
      new TextElement[]{ new TextElement("col 1"), new TextElement("col 2"), new TextElement("col 3") }
      new TextElement[]{ new TextElement("col 1"), new TextElement("col 2"), new TextElement("col 3") }
   })
});

dashboardData.HostingInfoFooter = new HostingInfoFooter(17, 17);
```        
# Implement a web api endpoint returning a DataTransferObject

to add

# Learn how to config the data dash website

to add

# Setup the data dash website

to add

# Deploy it pointing at the web api endpoint created earlier

to add
