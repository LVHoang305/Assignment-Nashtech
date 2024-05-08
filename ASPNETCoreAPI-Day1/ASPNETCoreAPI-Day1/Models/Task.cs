namespace ASPNETCoreAPI_Day1.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class ViewTask
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
