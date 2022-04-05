namespace EasyTestAPI.Web.ApiModels;

public class AddTestDto
{
  public string Name { get; set; }
  public string? Description { get; set; }
  public List<AddQuestionDto> Questions { get; set; }

}
