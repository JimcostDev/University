using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using University.BL.Data;
using University.BL.DTOs;
using University.BL.Models;
using University.BL.Repositories.Implements;
using University.BL.Services.Implements;

namespace University.Web.Controllers
{
    public class OfficeAssignmentsController : Controller
    {
        private UniversityContext _universityContext;

        public UniversityContext UniversityContext
        {
            get
            {
                return _universityContext ?? HttpContext.GetOwinContext().Get<UniversityContext>();
            }
            private set
            {
                _universityContext = value;
            }
        }

        //********************CON GET DEVOLVEMOS UN MODELO Y CON POST UN DTO************************
        //CREAR EL MAPEO CON AUTOMAPPER
        private IMapper mapper;

        public OfficeAssignmentsController()
        {
            this.mapper = MvcApplication.MapperConfiguration.CreateMapper();
        }

        // GET: OfficeAssignments
        public async Task<ActionResult> Index()
        {
            var officeAssignmentService = new OfficeAssignmentService(new OfficeAssignmentRepository(UniversityContext));

            //Con GET mapeamos del modelo al DTO
            var listofficeAssignments = await officeAssignmentService.GetAll();
            var listofficeAssignmentsDTO = listofficeAssignments.Select(model => mapper.Map<OfficeAssignmentDTO>(model));

            return View(listofficeAssignmentsDTO);
        }

        public async Task<ActionResult> Create()
        {
            var instructorService = new InstructorService(new InstructorRepository(UniversityContext));
            var instructors = await instructorService.GetAll();
            var instructorDTO = instructors.Select(model => mapper.Map<InstructorDTO>(model));


            ViewData["Instructors"] = new SelectList(instructorDTO, "ID", "FullName");
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(OfficeAssignmentDTO officeAssignmentDTO)
        {
            //para que la lista se siga cargando
            var instructorService = new InstructorService(new InstructorRepository(UniversityContext));
            var instructors = await instructorService.GetAll();
            var instructorDTO = instructors.Select(model => mapper.Map<InstructorDTO>(model));


            ViewData["Instructors"] = new SelectList(instructorDTO, "ID", "FullName", officeAssignmentDTO.InstructorID);
            if (ModelState.IsValid)
            {
                var officeAssignmentService = new OfficeAssignmentService(new OfficeAssignmentRepository(UniversityContext));
                var officeAssignment = mapper.Map<OfficeAssignment>(officeAssignmentDTO);
                officeAssignment = await officeAssignmentService.Insert(officeAssignment);

                return RedirectToAction("Index", "OfficeAssignments");
            }
           
            return View(officeAssignmentDTO);
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var officeAssignmentService = new OfficeAssignmentService(new OfficeAssignmentRepository(UniversityContext));
            var officeAssignment = await officeAssignmentService.GetById(id.Value); //SELECT * FROM Course WHERE CourseID = id

            if (officeAssignment == null)
            {
                return HttpNotFound();
            }

            var officeAssignmentDTO = mapper.Map<OfficeAssignmentDTO>(officeAssignment);
            return View(officeAssignmentDTO);
        }
        //GET
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var officeAssignmentService = new OfficeAssignmentService(new OfficeAssignmentRepository(UniversityContext));
            var officeAssignment = await officeAssignmentService.GetById(id.Value); //SELECT * FROM Course WHERE CourseID = id

            if (officeAssignment == null)
            {
                return HttpNotFound();
            }
            var officeAssignmentDTO = mapper.Map<OfficeAssignmentDTO>(officeAssignment);
            return View(officeAssignmentDTO);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(OfficeAssignmentDTO officeAssignmentDTO)
        {
            if (ModelState.IsValid)
            {

                //Con POST mapeamos del DTO al modelo
                var officeAssignment = mapper.Map<OfficeAssignment>(officeAssignmentDTO);

                var officeAssignmentService = new OfficeAssignmentService(new OfficeAssignmentRepository(UniversityContext));
                officeAssignment = await officeAssignmentService.Update(officeAssignment);

                return RedirectToAction("Index", "OfficeAssignments");
            }

            return View(officeAssignmentDTO);
        }
        public async Task<ActionResult> Delete(int? id)
        {
            var officeAssignmentService = new OfficeAssignmentService(new OfficeAssignmentRepository(UniversityContext));
            await officeAssignmentService.Delete(id.Value);
            return RedirectToAction("Index", "OfficeAssignments");
        }
    }
}