namespace DataDash.Models
{
    public class DataTransferObject
    {
        public int NumberOfColumns { get; set; } = 0;
        public int NumberOfRows { get; set; } = 0;
        public string? ErrorMessage { get; set; } = null;

        public string BackgroundColour { get; set; } = "rgb(40,40,40)";
        public string BorderColour { get; set; } = "rgb(128,128,128)";
        public string TextColour { get; set; } = "rgb(170,170,170)";
        public string TitleRowBackgroundColour { get; set; } = "Black";

        public List<TitleRow> TitleRows { get; set; } = new List<TitleRow>();
        public List<SingleReadOutRow> SingleReadouts { get; set; } = new List<SingleReadOutRow>();
        public List<DisplayTable> DisplayTables { get; set; } = new List<DisplayTable>();
        public HostingInfoFooter? HostingInfoFooter { get; set; } = null;
    }

    public class HostingInfoFooter
    {
        public int TopLeftRowNumber { get; set; }
        public int BottomRightRowNumber { get; set; }

        public HostingInfoFooter(int topLeftRowNumber, int bottomRightRowNumber)
        {
            TopLeftRowNumber = topLeftRowNumber;
            BottomRightRowNumber = bottomRightRowNumber;
        }
    }

    public class TitleRow
    {
        public int TopLeftColumnNumber { get; set; }
        public int TopLeftRowNumber { get; set; }
        public int BottomRightColumnNumber { get; set; }
        public int BottomRightRowNumber { get; set; }
        public TextElement? Title { get; set; }
        public string Align { get; set; } = "center";
    }

    public class SingleReadOutRow
    {
        public int TopLeftColumnNumber { get; set; }
        public int TopLeftRowNumber { get; set; }
        public int BottomRightColumnNumber { get; set; }
        public int BottomRightRowNumber { get; set; }

        public List<SingleReadOutEntry> ReadOuts { get; set; } = new List<SingleReadOutEntry>();
    }

    public class SingleReadOutEntry
    {
        public TextElement? Title { get; set; }
        public TextElement? Value { get; set; }
    }

    public class DisplayTable
    {
        public int TopLeftColumnNumber { get; set; }
        public int TopLeftRowNumber { get; set; }
        public int BottomRightColumnNumber { get; set; }
        public int BottomRightRowNumber { get; set; }

        public TextElement[] headers { get; set; } = new TextElement[0];

        public List<TextElement[]> dataRows { get; set; } = new List<TextElement[]>();

    }

    public class TextElement
    {
        public string? Text { get; set; }
        public string? Colour { get; set; }

        public TextElement()
        {
            Text = "";
            Colour = "default";
        }

        public TextElement(string text)
        {
            Text = text;
            Colour = "default";
        }

        public TextElement(string text, string colour)
        {
            Text = text;
            Colour = colour;
        }
    }
}
