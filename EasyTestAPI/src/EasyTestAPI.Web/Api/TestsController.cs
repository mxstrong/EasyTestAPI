using EasyTestAPI.Core.TestAggregate;
using EasyTestAPI.Core.TestAggregate.Specifications;
using EasyTestAPI.Infrastructure.Data;
using EasyTestAPI.SharedKernel.Interfaces;
using EasyTestAPI.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;
namespace EasyTestAPI.Web.Api;

public class TestsController : BaseApiController
{
  private readonly IRepository<Test> _repository;
  private readonly IReadRepository<QuestionType> _questionTypesRepo;
  private readonly AppDbContext _context;
  public TestsController(IRepository<Test> repository, IReadRepository<QuestionType> questionTypesRepo, AppDbContext context)
  {
    _repository = repository;
    _questionTypesRepo = questionTypesRepo;
    _context = context;
  }
  [HttpGet]
  public async Task<ActionResult<List<TestDto>>> GetTests()
  {
    var tests = await _repository.ListAsync(new TestWithQuestionsAndAnswersSpec());
    var testDtos = tests.Count > 0 ? tests.Select(t => TestDto.FromTest(t)).ToList() : new List<TestDto>();
    return Ok(tests);
  }
  [HttpPost]
  public async Task<ActionResult<TestDto>> AddTest([FromBody] AddTestDto test)
  {
    var code = Guid.NewGuid().ToString().Split('-').Take(2).Aggregate((firstPart, secondPart) => firstPart + secondPart);
    // TODO: check for duplicate codes
    var testId = Guid.NewGuid().ToString();
    var testToCreate = new Test()
    {
      TestId = testId,
      Name = test.Name,
      Description = test.Description,
      Code = code,
      CreatedById = "test",
      Questions = (await Task.WhenAll(test.Questions.Select(async question =>
      {
        var questionId = Guid.NewGuid().ToString();
        var type = await _questionTypesRepo.GetBySpecAsync(new QuestionTypeByNameSpec(question.Type));
        var typeId = type!.QuestionTypeId;
        return new Question()
        {
          QuestionId = questionId,
          Text = question.Text,
          TypeId = typeId,
          TestId = testId,
          Answers = question.Answers is not null ? question.Answers.Select(answer => new Answer()
          {
            AnswerId = Guid.NewGuid().ToString(),
            AnswerText = answer,
            QuestionId = questionId
          }).ToList() : null,
          TestAnswers = new List<TestAnswer>()
        };
      }).ToList())).ToList(),
      AnsweredTests = new List<AnsweredTest>()
    };
    var newTest = await _repository.AddAsync(testToCreate);
    var newTestDto = TestDto.FromTest(newTest);
    return Ok(newTestDto);
  }
}
