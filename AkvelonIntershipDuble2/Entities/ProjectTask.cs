using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AkvelonIntershipDuble2.Entities
{
    public class ProjectTask
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        [MaxLength(100)]
        public string TaskName { get; set; }
        public ProjectTaskStatus ProjectTaskStatus { get; set; }
        [MaxLength(100)]
        public string TaskDescription { get; set; }
        public int Priority { get; set; }
        public Project Project { get; set; }
    }
}