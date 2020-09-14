using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
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
    public class StudentsController : Controller
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

        public StudentsController()
        {
            this.mapper = MvcApplication.MapperConfiguration.CreateMapper();
        }

        // GET: Students
        public async Task<ActionResult> Index(int? id, int? pageSize, int? page)
        {
            var studentService = new StudentService(new StudentRepository(UniversityContext));

            //Con GET mapeamos del modelo al DTO
            var listStudent = await studentService.GetAll();
            var listStudentDTO = listStudent.Select(model => mapper.Map<StudentDTO>(model));

            if (id != null)
                ViewBag.Courses = await studentService.GetCoursesByStudent(id.Value);

            //implementando paginación con pagedlist
            pageSize = (pageSize ?? 10);
            page = (page ?? 1);
            ViewBag.PageSize = pageSize;

            return View(listStudentDTO.ToPagedList(page.Value, pageSize.Value));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                //Con POST mapeamos del DTO al modelo
                var student = mapper.Map<Student>(studentDTO);

                var studentService = new StudentService(new StudentRepository(UniversityContext));
                student = await studentService.Insert(student);

                return RedirectToAction("Index", "Students");
            }

            return View(studentDTO);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            var studentService = new StudentService(new StudentRepository(UniversityContext));
            var student = await studentService.GetById(id.Value);
            var studentDTO = mapper.Map<StudentDTO>(student);
            return View(studentDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {

                //Con POST mapeamos del DTO al modelo
                var student = mapper.Map<Student>(studentDTO);

                var studentService = new StudentService(new StudentRepository(UniversityContext));
                student = await studentService.Update(student);

                return RedirectToAction("Index", "Students");
            }

            return View(studentDTO);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            var studentService = new StudentService(new StudentRepository(UniversityContext));
            await studentService.Delete(id.Value);

            //var instructorDTO = mapper.Map<InstructorDTO>(instructor);
            return RedirectToAction("Index", "Students");
        }

        public async Task<ActionResult> Details(int? id)
        {
            var studentService = new StudentService(new StudentRepository(UniversityContext));
            var student = await studentService.GetById(id.Value);

            var studentDTO = mapper.Map<StudentDTO>(student);
            return View(studentDTO);
        }
    }
}