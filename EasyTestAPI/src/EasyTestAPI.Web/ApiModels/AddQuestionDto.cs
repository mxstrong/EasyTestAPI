namespace EasyTestAPI.Web.ApiModels;

public class AddQuestionDto
{
  public string Text { get; set; }
  public string Type { get; set; }
  public List<AddAnswerDto>? Answers { get; set; }
}

public class AddAnswerDto
{
  public string Answer { get; set; }
  public bool IsCorrect { get; set; }
}
