namespace EasyTestAPI.Web.ApiModels;

public class AddQuestionDto
{
  public string Text { get; set; }
  public string Type { get; set; }
  public List<string>? Answers { get; set; }
}
