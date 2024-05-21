namespace Application.DTOs
{
    public class GetGroupDTOs
    {
        public string Name { get; set; } = string.Empty;
        public string CreatedOn { get; set; } = string.Empty;

    }
    public class AddGroupDTO
    {
        public string Name { get; set; } = string.Empty;
    }
    
    public class UpdateGroupDTO
    {
        public string Name { get; set; } = string.Empty;
    }
}
