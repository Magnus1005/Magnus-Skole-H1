using Microsoft.AspNetCore.Mvc;
using Plukliste;
using System.Xml.Linq;


namespace PluklisteWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AfslutPluksedelController : ControllerBase
    {
        private List<string> Name = new List<string>();

        private readonly ILogger<AfslutPluksedelController> _logger;

        public AfslutPluksedelController(ILogger<AfslutPluksedelController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "PostAfslutPluksedel")]

        public async Task<ActionResult<int>> PostTodoItem(int itemIndex)
        {
            BLL _bll = new BLL("C:\\PluklisteOpgave\\import", "C:\\PluklisteOpgave\\export", "C:\\PluklisteOpgave\\print", "C:\\PluklisteOpgave\\letterTemplates");
            _bll.setIndex(itemIndex);
            _bll.moveFile();

            //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return itemIndex;
        }
    }
}