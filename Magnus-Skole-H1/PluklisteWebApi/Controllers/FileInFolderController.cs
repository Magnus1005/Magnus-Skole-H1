using Microsoft.AspNetCore.Mvc;
using Plukliste;
using System.Xml.Linq;


namespace PluklisteWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileInFolderController : ControllerBase
    {
        private List<string> Name = new List<string>();

        private readonly ILogger<FileInFolderController> _logger;

        public FileInFolderController(ILogger<FileInFolderController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetFilesInFolder")]
        public IEnumerable<Pluklist> Get()
        {
            BLL _bll = new BLL("C:\\PluklisteOpgave\\import", "C:\\PluklisteOpgave\\export", "C:\\PluklisteOpgave\\print", "C:\\PluklisteOpgave\\letterTemplates");

            Name = _bll.readListOfFiles();

            return Enumerable.Range(1, Name.Count).Select(index => new Pluklist
            {
                Adresse = _bll.readFile(Name[index - 1]).Adresse,
                Forsendelse = _bll.readFile(Name[index - 1]).Forsendelse,
                Name = Name[index - 1],
                Status = _bll.readFile(Name[index - 1]).Status,
                Lines = _bll.readFile(Name[index - 1]).Lines,
            })
            .ToArray();
        }
    }
}