using ScoalaAmadeus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ScoalaAmadeus.Repository
{
    public class CourseRepository
    {
        private Models.DBObjects.SchoolsModelsDataContext dbContext;

        public CourseRepository()
        {
            dbContext = new Models.DBObjects.SchoolsModelsDataContext();
        }

        public CourseRepository(Models.DBObjects.SchoolsModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void InsertCourse(CourseModel courseModel)
        {
            courseModel.CourseId = Guid.NewGuid();

            if (courseModel != null)
            {
                Models.DBObjects.Course dbCourseModel = new Models.DBObjects.Course();

                dbCourseModel.CourseId = courseModel.CourseId;
                dbCourseModel.Name = courseModel.Name;
                dbCourseModel.Price = courseModel.Price;

                dbContext.Courses.InsertOnSubmit(dbCourseModel);
                dbContext.SubmitChanges();
            }
   
        }

        public List<CourseModel> GetAllCourses()
        {
            List<CourseModel> coursesList = new List<CourseModel>();

            foreach (Models.DBObjects.Course dbcourseModel in dbContext.Courses)
            {
                CourseModel courseModel = new CourseModel();

                courseModel.CourseId = dbcourseModel.CourseId;
                courseModel.Name = dbcourseModel.Name;
                courseModel.Price = dbcourseModel.Price;

                coursesList.Add(courseModel);
            }

            return coursesList;
        }

        public CourseModel GetCourseById(Guid Id)
        {
            Models.DBObjects.Course dbCourseModel = dbContext.Courses.FirstOrDefault(m => m.CourseId == Id);

            CourseModel courseModel = new CourseModel();

            if (dbCourseModel != null)
            {
                courseModel.CourseId = dbCourseModel.CourseId;
                courseModel.Name = dbCourseModel.Name;
                courseModel.Price = dbCourseModel.Price;
            }

            return courseModel;

        }

        public void UpdateCourse(CourseModel courseModel)
        {
            Models.DBObjects.Course dbCourseToEdit = dbContext.Courses.FirstOrDefault(m => m.CourseId == courseModel.CourseId);

            if (courseModel != null)
            {
                dbCourseToEdit.CourseId = courseModel.CourseId;
                dbCourseToEdit.Name = courseModel.Name;
                dbCourseToEdit.Price = courseModel.Price;

                dbContext.SubmitChanges();
            }

        }

        public void DeleteCourse(Guid Id)
        {
            Models.DBObjects.Course courseToDelete = dbContext.Courses.FirstOrDefault(m => m.CourseId == Id);

            dbContext.Courses.DeleteOnSubmit(courseToDelete);
            dbContext.SubmitChanges();
        }

    }
}