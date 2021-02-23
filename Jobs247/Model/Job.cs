

namespace Jobs247.Model
{
    public class Job
    {
        public int JobId { get; set; }
        public string Description { get; set; }
        public Position Position { get; set; }
        public Company Company { get; set; }  


        public string SearchAllText()
        {
            return (Description + Position.name + Company.name).ToLower();
        }
    }
}
