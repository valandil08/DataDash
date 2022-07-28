using DataDash.Models;

namespace DataDash
{
    public static class GlobalData
    {
        public static Dictionary<string, PageInstance> PageData = new Dictionary<string, PageInstance>();
    }

    public class PageInstance
    {
        public DataTransferObject DashboardData = new DataTransferObject();
        public string[] ProxyScreens = new string[0];
        public string? ErrorMessage = null;
    }
}
