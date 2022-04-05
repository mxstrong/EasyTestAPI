using EasyTestAPI.Core.TestAggregate;

namespace EasyTestAPI.Web.ApiModels;

public class TestDto
{
  public string TestId { get; set; }
  public string Name { get; set; }
  public string? Description { get; set; }
  public string Code { get; set; }
  public List<QuestionDto> Questions { get; set; }

  public static TestDto FromTest(Test test)
  {
    return new TestDto()
    {
      TestId = test.TestId,
      Name = test.Name,
      Description = test.Description,
      Code = test.Code,
      Questions = test.Questions.Select(q => QuestionDto.FromQuestion(q)).ToList()
    };
  }
}
