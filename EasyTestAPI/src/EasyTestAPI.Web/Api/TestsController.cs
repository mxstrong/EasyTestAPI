using EasyTestAPI.Core.TestAggregate;
using EasyTestAPI.Infrastructure.Data;
using EasyTestAPI.SharedKernel.Interfaces;
using EasyTestAPI.Web.ApiModels;
using Microsoft.AspNetCore.Mvc;
namespace EasyTestAPI.Web.Api;

public class TestsController : BaseApiController
{
  private readonly IRepository<Test> _repository;
  private readonly AppDbContext _context;
  public TestsController(IRepository<Test> repository, AppDbContext context)
  {
    _repository = repository;
    _context = context;
  }
  [HttpGet]
  public async Task<ActionResult<List<TestDto>>> GetTests()
  {
    var tests = await _repository.ListAsync();
    var testDtos = tests.Select(t => TestDto.FromTest(t)).ToList();
    return Ok(tests);
  }
  [HttpPost]
  public async Task<ActionResult<TestDto>> AddTest([FromBody] AddTestDto test)
  {
    var code = Guid.NewGuid().ToString().Split('-').Take(2).Aggregate((firstPart, secondPart) => firstPart + secondPart);
    // TODO: check for duplicate codes
    var testToCreate = new Test()
    {
      TestId = Guid.NewGuid().ToString(),
      Name = test.Name,
      Description = test.Description,
      Code = code,
      CreatedById = "test",
      Questions = test.Questions.Select(question => new Question()
      {
        QuestionId = Guid.NewGuid().ToString(),
        Text = question.Text,
        TypeId = _context.QuestionTypes.Single(qt => qt.Name == question.Type).QuestionTypeId,
        Answers = question.Answers is not null ? question.Answers.Select(answer => new Answer()
          {
            AnswerId = Guid.NewGuid().ToString(),
            AnswerText = answer
          }).ToList() : new List<Answer>()
      }).ToList()
    };
    var newTest = await _repository.AddAsync(testToCreate);
    var newTestDto = new TestDto();
    return Ok(newTestDto);
  }
}
