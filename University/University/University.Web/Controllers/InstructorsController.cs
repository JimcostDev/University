using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
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
    //[Authorize]
    public class InstructorsController : Controller
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
        //********************CON GET DEVOLVEMOS/RECIBIMOS UN MODELO Y CON POST UN DTO************************
        //CREAR EL MAPEO CON AUTOMAPPER
        private IMapper mapper;

        public InstructorsController()
        {
            this.mapper = MvcApplication.MapperConfiguration.CreateMapper();
        }

        // GET: Instructors
        public async Task<ActionResult> Index(int? id)
        {
            var instructorService = new InstructorService(new InstructorRepository(UniversityContext));

            if (id != null)
            {
                var courses = await instructorService.GetCoursesByInstructor(id.Value);
                ViewBag.Courses = courses.Select(model => mapper.Map<CourseDTO>(model));
            }

            //Con GET mapeamos del modelo al DTO
            var listInstructors = await instructorService.GetAll();
            var listInstructorDTO = listInstructors.Select(model => mapper.Map<InstructorDTO>(model)); 
            return View(listInstructorDTO.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(InstructorDTO instructorDTO)
        {
            if (ModelState.IsValid)
            {
                //Con POST mapeamos del DTO al modelo
                var instructor = mapper.Map<Instructor>(instructorDTO);

                var instructorService = new InstructorService(new InstructorRepository(UniversityContext));
                instructor = await instructorService.Insert(instructor);

                return RedirectToAction("Index", "Instructors");
            }

            return View(instructorDTO);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            var instructorService = new InstructorService(new InstructorRepository(UniversityContext));
            var instructor = await instructorService.GetById(id.Value);
            var instructorDTO = mapper.Map<InstructorDTO>(instructor);
            return View(instructorDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(InstructorDTO instructorDTO)
        {
            if (ModelState.IsValid)
            {

                //Con POST mapeamos del DTO al modelo
                var instructor = mapper.Map<Instructor>(instructorDTO);

                var instructorService = new InstructorService(new InstructorRepository(UniversityContext));
                instructor = await instructorService.Update(instructor);

                return RedirectToAction("Index", "Instructors");
            }

            return View(instructorDTO);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            var instructorService = new InstructorService(new InstructorRepository(UniversityContext));
            await instructorService.Delete(id.Value);
            return RedirectToAction("Index", "Instructors");
        }

        public async Task<ActionResult> Details(int? id)
        {
            var instructorService = new InstructorService(new InstructorRepository(UniversityContext));
            var instructor = await instructorService.GetById(id.Value);

            var instructorDTO = mapper.Map<InstructorDTO>(instructor);
            return View(instructorDTO);
        }
    }
}