using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Sw1Tech.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Files")]
    public class FilesController : Controller
    {
        private string PathFiles;
        private readonly IHostingEnvironment _env;

        public FilesController(IHostingEnvironment env)
        {
            _env = env;
            PathFiles = _env.WebRootPath;
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"}
            };
        }
        
        private bool DoCreateDirectoryFiles(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            return result;
        }

        [Route("DoUploadFilesOrcamento")]
        public async Task<IActionResult> UploadFilesOrcamento()
        {
            PathFiles = PathFiles + "../image/client/orcamento/";
            try
            {
                if (DoCreateDirectoryFiles(PathFiles))
                {
                    var Form = await Request.ReadFormAsync();
                    var File = Form.Files.First();
                    var FilePath = PathFiles + File.FileName;
                    if (File.Length > 0)
                    {
                        using (var Stream = new FileStream(FilePath, FileMode.Create))
                        {
                            await File.CopyToAsync(Stream);
                        }
                    }
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                var OriginalMessage = ex.Message;

                while (ex.InnerException != null)
                    ex = ex.InnerException;
                return BadRequest($"{OriginalMessage} | {ex.Message}");
            }
        }

        [HttpPost]
        [Route("DoDownloadFilesOrcamento")]
        public async Task<IActionResult> DoDownloadFilesOrcamento([FromBody] string filename)
        {
            if (filename == null)
                return BadRequest("Arquivo não encontrado");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        [HttpPost]
        [Route("DoApagarFilesOrcamento")]
        public IActionResult DoApagarFilesOrcamento([FromBody] string pathFile)
        {
            if (pathFile == null)
                return BadRequest("Nome do arquivo não informado");

            try
            {
                System.IO.File.Delete('.' + pathFile);
            }
            catch (Exception ex)
            {
                var OriginalMessage = ex.Message;

                while (ex.InnerException != null)
                    ex = ex.InnerException;
                return BadRequest($"{OriginalMessage} | {ex.Message}");
            }
            return Ok(true);
        }


        [Route("DoUploadFilesOrcamentoOcorrencia")]
        public async Task<IActionResult> UploadFilesOrcamentoOcorrencia()
        {
            PathFiles = PathFiles + "../image/client/orcamento/ocorrencia/";
            try
            {
                if (DoCreateDirectoryFiles(PathFiles))
                {
                    var Form = await Request.ReadFormAsync();
                    var File = Form.Files.First();
                    var FilePath = PathFiles + File.FileName;
                    if (File.Length > 0)
                    {
                        using (var Stream = new FileStream(FilePath, FileMode.Create))
                        {
                            await File.CopyToAsync(Stream);
                        }
                    }
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                var OriginalMessage = ex.Message;

                while (ex.InnerException != null)
                    ex = ex.InnerException;
                return BadRequest($"{OriginalMessage} | {ex.Message}");
            }
        }

        [HttpPost]
        [Route("DoApagarFilesOrcamentoOcorrencia")]
        public IActionResult DoApagarFilesOrcamentoOcorrencia([FromBody] string pathFile)
        {
            if (pathFile == null)
                return BadRequest("Nome do arquivo não informado");

            try
            {
                System.IO.File.Delete('.' + pathFile);
            }
            catch (Exception ex)
            {
                var OriginalMessage = ex.Message;

                while (ex.InnerException != null)
                    ex = ex.InnerException;
                return BadRequest($"{OriginalMessage} | {ex.Message}");
            }
            return Ok(true);
        }


    }
}