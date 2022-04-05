using EasyTestAPI.Core.TestAggregate;

namespace EasyTestAPI.Web.ApiModels;

public class QuestionDto
{
  public string QuestionId { get; set; }
  public string Text { get; set; }
  public string QuestionType { get; set; }
  public List<AnswerDto> Answers { get; set; }

  public static QuestionDto FromQuestion(Question question)
  {
    return new QuestionDto()
    {
      QuestionId = question.QuestionId,
      Text = question.Text,
      QuestionType = question.QuestionType.Name,
      Answers = question.Answers.Select(a => AnswerDto.FromAnswer(a)).ToList()
    };
  }
}
