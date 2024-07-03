using FormBackend.Model;
using FormBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormBackend.Controllers{

    [ApiController]
    [Route("[controller]")] 
    public class FormController: ControllerBase{
        private readonly FormService _service;
        public FormController(FormService service){
            _service = service;
        }

        [HttpPost]
        [Route("AddForm")]
        public IActionResult AddForm([FromBody] FormModel form) => _service.NewForm(form) ? Ok("Successfully created") : BadRequest("Could not create Form");

        [HttpGet]
        [Route("GetAllForms")]
        public IEnumerable<FormModel> GetForms() => _service.GetForms();

        // [HttpGet]
        // [Route("FilterByFirstName")]
        // public IEnumerable<FormModel> FilterByFirstName() => _service.FilterByFirstName();

        // [HttpGet]
        // [Route("FilterByLastName")]
        // public IEnumerable<FormModel> FilterByLastName() => _service.FilterByLastName();

        [HttpPut]
        [Route("EditForm")]
        public IActionResult EditForm([FromBody] FormModel form) => _service.EditForm(form) ? Ok("Successfully updated") : BadRequest();

        [HttpDelete]
        [Route("DeleteForm/{id}")]
        public IActionResult DeleteForm(int id) => _service.DeleteForm(id) ? Ok("Deleted successfully") : BadRequest("Form not found");
    }
}