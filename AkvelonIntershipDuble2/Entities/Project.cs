using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AkvelonIntershipDuble2.Entities
{
    public class Project
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        [MaxLength(100)] public string ProjectName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public int Priority { get; set; }
        public ICollection<ProjectTask> Tasks { get; set; }

        public Project()
        {
            Tasks = new List<ProjectTask>();
        }
    }
}