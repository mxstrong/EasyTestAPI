using EasyTestAPI.Core.TestAggregate;

namespace EasyTestAPI.Web.ApiModels;

public class AnswerDto
{
  public string AnswerId { get; set; }
  public string Answer { get; set; }
  public bool IsCorrect { get; set; }

  public static AnswerDto FromAnswer(Answer answer)
  {
    return new AnswerDto()
    {

      AnswerId = answer.AnswerId,
      Answer = answer.AnswerText,
      IsCorrect = answer.IsCorrect
    };
  }
}
