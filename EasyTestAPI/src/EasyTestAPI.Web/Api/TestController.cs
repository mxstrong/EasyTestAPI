using Microsoft.AspNetCore.Mvc;
namespace EasyTestAPI.Web.Api;

public class TestController : BaseApiController
{
  [HttpGet]
  public IActionResult Index()
  {
    return Ok(new 
    {
      value = "test value"
    });
  }
}
