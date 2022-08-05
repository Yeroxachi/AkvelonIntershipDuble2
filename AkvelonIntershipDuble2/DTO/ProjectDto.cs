using System;
using AkvelonIntershipDuble2.Entities;

namespace AkvelonIntershipDuble2.DTO
{
    public class ProjectDto
    {
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProjectStatus { get; set; }
        public int Priority { get; set; }
    }
}