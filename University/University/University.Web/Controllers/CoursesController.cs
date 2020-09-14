using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
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
    //[Authorize]
    public class CoursesController : Controller
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

        public CoursesController()
        {
            this.mapper = MvcApplication.MapperConfiguration.CreateMapper();
        }

        // GET: Courses
        public async Task<ActionResult> Index(int? id, int? CourseID, int? pageSize, int? page)
        {

            var courseService = new CourseService(new CourseRepository(UniversityContext));

            //Con GET mapeamos del modelo al DTO
            var listCourses = await courseService.GetAll();
            var listCoursesDTO = listCourses.Select(model => mapper.Map<CourseDTO>(model));

            if (id != null)
                ViewBag.Students = await courseService.GetStudentByCourses(id.Value);

            //DropDownList
            ViewData["Courses"] = new SelectList(listCoursesDTO, "CourseID", "TitleCredits");

            //implementando paginación con pagedlist
            pageSize = (pageSize ?? 10);
            page = (page ?? 1);
            ViewBag.PageSize = pageSize;

            return View(listCoursesDTO.ToPagedList(page.Value, pageSize.Value));
        }
        //public static CourseDTO CourseToDTO(Course course) => new CourseDTO
        //{
        //    CourseID = course.CourseID,
        //    Title = course.Title,
        //    Credits = course.Credits
        //};

        // GET: Courses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var courseService = new CourseService(new CourseRepository(UniversityContext));
            var course = await courseService.GetById(id.Value); //SELECT * FROM Course WHERE CourseID = id

            if (course == null)
            {
                return HttpNotFound();
            }

            var courseDTO = mapper.Map<CourseDTO>(course);
            return View(courseDTO);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CourseDTO courseDTO)
        {
            if (ModelState.IsValid)
            {
                var courseService = new CourseService(new CourseRepository(UniversityContext));
                try
                {
                    //Con POST mapeamos del DTO al modelo
                    var course = mapper.Map<Course>(courseDTO);

                    course = await courseService.Insert(course); //INSERT INTO Course VALUES(CourseID,Title,Credits)
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.InnerException.Message);
                }
            }

            return View(courseDTO);
        }

        // GET: Courses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var courseService = new CourseService(new CourseRepository(UniversityContext));
            var course = await courseService.GetById(id.Value); //SELECT * FROM Course WHERE CourseID = id

            if (course == null)
            {
                return HttpNotFound();
            }
            var courseDTO = mapper.Map<CourseDTO>(course);
            return View(courseDTO);
        }

        // POST: Courses/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CourseDTO courseDTO)
        {
            if (ModelState.IsValid)
            {
                var courseService = new CourseService(new CourseRepository(UniversityContext));
                //Con POST mapeamos del DTO al modelo
                var course = mapper.Map<Course>(courseDTO);

                course = await courseService.Update(course);
                return RedirectToAction("Index");
            }
            return View(courseDTO);
        }

        // GET: Courses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var courseService = new CourseService(new CourseRepository(UniversityContext));
            var course = await courseService.GetById(id.Value); //SELECT * FROM Course WHERE CourseID = id

            if (course == null)
            {
                return HttpNotFound();
            }
            var courseDTO = mapper.Map<CourseDTO>(course);
            return View(courseDTO);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            var courseService = new CourseService(new CourseRepository(UniversityContext));
           
            try
            {
                if (!await courseService.DeleteCheckOnEntity(id))
                    await courseService.Delete(id);
                else
                    throw new Exception("ForeignKeys");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var course = await courseService.GetById(id);

                //Con POST mapeamos del DTO al modelo
                var courseDTO = mapper.Map<CourseDTO>(course);
                return View("Delete", courseDTO);
            }

            return RedirectToAction("Index");
        }


    }
}
