namespace EasyTestAPI.Web.ApiModels;

public class UserTestsDto
{
  public List<AnsweredTestDto> SolvedTests { get; set; }
  public List<TestWithSolutionDto> CreatedTests { get; set; }
}

public class AnsweredTestDto
{
  public string AnsweredTestId { get; set; }
  public string TestName { get; set; }
  public string Description { get; set; }
  public string SolvedAt { get; set; }
}

public class TestWithSolutionDto: TestDto
{
  public List<TestSolutionDto> Solutions { get; set; }
}

public class TestSolutionDto
{
  public string AnsweredTestId { get; set; }
  public UserDto? Solver { get; set; }
  public string SolvedAt { get; set; }
}
