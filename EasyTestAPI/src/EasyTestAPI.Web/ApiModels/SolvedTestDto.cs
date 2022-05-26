namespace EasyTestAPI.Web.ApiModels;

public class SolvedTestDto : AnsweredTestDto
{
  public int TotalScore { get; set; }
  public int QuestionCount { get; set; }
  public List<TestAnswerDto>? Answers { get; set; }
  public List<QuestionDto> Questions { get; set; }
}
