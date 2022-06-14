using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace ScheduleProg.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string Course{ get; set; }

        public string Group_Name{ get; set; }
        [ValidateNever]
        public string Name_Of_Group { get { return (Group_Name + Course); } }

        [ValidateNever]

        public List<Subgroup> Subgroups { get; set; }
    }
}
