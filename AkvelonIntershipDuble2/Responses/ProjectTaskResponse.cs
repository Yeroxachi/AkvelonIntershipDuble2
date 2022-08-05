namespace AkvelonIntershipDuble2.Responses
{
    public class ProjectTaskResponse
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }= null!;
        public string ProjectTaskStatus { get; set; } = null!;
        public string TaskDescription { get; set; }= null!;
        public int Priority { get; set; }
        public int ProjectId { get; set; }
    }
}