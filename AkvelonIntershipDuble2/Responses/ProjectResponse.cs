using System;
using System.Collections.Generic;

namespace AkvelonIntershipDuble2.Responses
{
    public class ProjectResponse
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProjectStatus { get; set; } = null!;
        public int Priority { get; set; }
        public ICollection<ProjectTaskResponse> Tasks { get; set; }

        public ProjectResponse()
        {
            Tasks = new List<ProjectTaskResponse>();
        }
    }
}