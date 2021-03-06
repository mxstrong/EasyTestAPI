using System.Security.Claims;
using EasyTestAPI.Core.TestAggregate;
using EasyTestAPI.Core.TestAggregate.Specifications;
using EasyTestAPI.Infrastructure.Data;
using EasyTestAPI.SharedKernel.Interfaces;
using EasyTestAPI.Web.ApiModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace EasyTestAPI.Web.Api;

public class TestsController : BaseApiController
{
  private readonly IRepository<Test> _repository;
  private readonly IReadRepository<QuestionType> _questionTypesRepo;
  private readonly IReadRepository<Question> _questionsRepo;
  private readonly IRepository<AnsweredTest> _answeredTestsRepo;
  private readonly AppDbContext _context;
  public TestsController(IRepository<Test> repository, IReadRepository<QuestionType> questionTypesRepo, IReadRepository<Question> questionsRepo, IRepository<AnsweredTest> answeredTestsRepo, AppDbContext context)
  {
    _repository = repository;
    _questionTypesRepo = questionTypesRepo;
    _questionsRepo = questionsRepo;
    _answeredTestsRepo = answeredTestsRepo;
    _context = context;
  }
  [HttpGet]
  public async Task<ActionResult<List<TestDto>>> GetTests()
  {
    var tests = await _repository.ListAsync(new TestWithQuestionsAndAnswersSpec());
    var testDtos = tests.Count > 0 ? tests.Select(t => TestDto.FromTest(t)).ToList() : new List<TestDto>();
    return Ok(tests);
  }
  [HttpGet("{code}")]
  public async Task<ActionResult<TestDto>> GetTestByCode(string code)
  {
    var test = await _repository.GetBySpecAsync(new TestByCodeSpec(code));
    if (test is null)
    {
      return BadRequest("No test found with specified code");
    }
    var testDto = TestDto.FromTest(test);
    return Ok(testDto);
  }
  [Authorize]
  [HttpPost]
  public async Task<ActionResult<TestDto>> AddTest([FromBody] AddTestDto test)
  {
    var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    var code = Guid.NewGuid().ToString().Split('-').Take(2).Aggregate((firstPart, secondPart) => firstPart + secondPart);
    // TODO: check for duplicate codes
    var testId = Guid.NewGuid().ToString();
    var types = await _questionTypesRepo.ListAsync();
    var testToCreate = new Test()
    {
      TestId = testId,
      Name = test.Name,
      Description = test.Description,
      Code = code,
      CreatedById = userId,
      Questions = test.Questions.Select(question =>
      {
        var questionId = Guid.NewGuid().ToString();
        var type = types.Single(type => type.Name == question.Type);
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
            AnswerText = answer.Answer,
            IsCorrect = answer.IsCorrect,
            QuestionId = questionId
          }).ToList() : null,
          TestAnswers = new List<TestAnswer>()
        };
      }).ToList().ToList(),
      AnsweredTests = new List<AnsweredTest>()
    };
    var newTest = await _repository.AddAsync(testToCreate);
    var newTestDto = TestDto.FromTest(newTest);
    return Ok(newTestDto);
  }

  [Authorize]
  [HttpPost("answered")]
  public async Task<ActionResult<TestDto>> AnswerTest([FromBody] List<TestAnswerDto> answers)
  {
    if (answers.Count() < 1)
    {
      return ValidationProblem("No answers provided");
    }
    var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    var firstQuestion = await _questionsRepo.GetByIdAsync(answers[0].QuestionId);
    var testId = firstQuestion!.TestId;
    var answeredTest = new AnsweredTest()
    {
      AnsweredTestId = Guid.NewGuid().ToString(),
      TestId = testId,
      UserId = userId,
      SolvedAt = DateTime.UtcNow
    };
    var questions = await _questionsRepo.ListAsync(new QuestionByTestIdSpec(testId));

    answeredTest.TestAnswers = answers.Select(answer => {
      var question = questions.Find(question => question.QuestionId == answer.QuestionId);
      bool? isCorrect = null;
      if (question!.QuestionType.Name != "open")
      {
        var correctAnswer = question.Answers!.FirstOrDefault(a => a.IsCorrect);
        isCorrect = correctAnswer is not null ? correctAnswer.AnswerText == answer.Answer : false;
      }
      return new TestAnswer()
      {
        TestAnswerId = Guid.NewGuid().ToString(),
        Answer = answer.Answer!,
        IsCorrect = isCorrect,
        AnsweredTestId = answeredTest.AnsweredTestId,
        QuestionId = answer.QuestionId
      };
    }).ToList();

    await _answeredTestsRepo.AddAsync(answeredTest);
    await _answeredTestsRepo.SaveChangesAsync();
    return StatusCode(201);
  }

  [Authorize]
  [HttpGet("user")]
  public async Task<ActionResult<UserTestsDto>> GetUserTests() 
  {
    var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    var createdTests = await _repository.ListAsync(new TestByCreatorIdSpec(userId));
    var solvedTests = await _answeredTestsRepo.ListAsync(new AnsweredTestBySolverIdSpec(userId));
    var userTests = new UserTestsDto()
    {
      CreatedTests = createdTests is not null ? createdTests.Select(test => new TestWithSolutionDto()
      {
        TestId = test.TestId,
        Code = test.Code,
        Description = test.Description,
        Name = test.Name,
        Questions = test.Questions is not null ? test.Questions.Select(question => QuestionDto.FromQuestion(question)).ToList() : new List<QuestionDto>(),
        Solutions = test.AnsweredTests is not null ? test.AnsweredTests.Select(answer => new TestSolutionDto()
        {
          AnsweredTestId = answer.AnsweredTestId,
          Solver = UserDto.FromUser(answer.User),
          SolvedAt = answer.SolvedAt.ToString()
        }).ToList() : new List<TestSolutionDto>()
      }).ToList() : new List<TestWithSolutionDto>(),
      SolvedTests = solvedTests is not null ? solvedTests.Select(test => new AnsweredTestDto()
      {
        AnsweredTestId = test.AnsweredTestId,
        TestName = test.Test.Name,
        Description = test.Test.Description ?? "",
        SolvedAt = test.SolvedAt.ToString()
      }).ToList() : new List<AnsweredTestDto>()
    };
    return Ok(userTests);
  }

  [HttpGet("answered/{answeredTestId}")]
  public async Task<ActionResult<SolvedTestDto>> GetSolvedTest(string answeredTestId)
  {
    var solvedTest = await _answeredTestsRepo.GetBySpecAsync(new AnsweredTestFullByIdSpec(answeredTestId));
    if (solvedTest is null)
    {
      return ValidationProblem("Test not found");
    }
    var solvedTestDto = new SolvedTestDto()
    {
      AnsweredTestId = solvedTest!.AnsweredTestId,
      SolvedAt = solvedTest.SolvedAt.ToString(),
      TotalScore = solvedTest.TestAnswers.Count(answer => answer.IsCorrect == true),
      QuestionCount = solvedTest.Test.Questions.Count(),
      TestName = solvedTest.Test.Name,
      Questions = solvedTest.Test.Questions.Select(question => QuestionDto.FromQuestion(question)).ToList(),
      Answers = solvedTest.TestAnswers.Select(ta => new TestAnswerDto()
      {
        QuestionId = ta.QuestionId,
        Answer = ta.Answer
      }).ToList(),
    };
    return Ok(solvedTestDto);
  }
}
