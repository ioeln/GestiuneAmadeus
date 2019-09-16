using ScoalaAmadeus.Models;
using ScoalaAmadeus.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Repository
{
    public class TeacherRepository
    {
        
        private CourseRepository courseRepository = new CourseRepository();

        private Models.DBObjects.SchoolsModelsDataContext dbContext;

        public TeacherRepository()
        {
            dbContext = new Models.DBObjects.SchoolsModelsDataContext();
        }

        public TeacherRepository(Models.DBObjects.SchoolsModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void InsertTeacher(TeacherModel teacherModel)
        {
            teacherModel.TeacherId = Guid.NewGuid();

            if (teacherModel != null)
            {
                Models.DBObjects.Teacher dbTeacherModel = new Models.DBObjects.Teacher();

                dbTeacherModel.TeacherId = teacherModel.TeacherId;
                dbTeacherModel.Name = teacherModel.Name;
                dbTeacherModel.Phone = teacherModel.Phone;
                dbTeacherModel.CourseId = teacherModel.CourseId;

                dbContext.Teachers.InsertOnSubmit(dbTeacherModel);
                dbContext.SubmitChanges();
            }


        }

        public List<TeacherWithCourseNameViewModel> GetAllTeachersWithCourseNames()
        {
            List<TeacherWithCourseNameViewModel> teachersList = new List<TeacherWithCourseNameViewModel>();

            foreach (Models.DBObjects.Teacher dbTeacherModel in dbContext.Teachers)
            {
                TeacherWithCourseNameViewModel teacherWithCourseNameModel = new TeacherWithCourseNameViewModel();

                teacherWithCourseNameModel.TeacherId = dbTeacherModel.TeacherId;
                teacherWithCourseNameModel.Name = dbTeacherModel.Name;
                teacherWithCourseNameModel.Phone = dbTeacherModel.Phone;
                teacherWithCourseNameModel.CourseId = dbTeacherModel.CourseId;

                var courses = courseRepository.GetAllCourses();

                foreach (var course in courses)
                {
                    if (teacherWithCourseNameModel.CourseId == course.CourseId)
                    {
                        teacherWithCourseNameModel.CourseName = course.Name;
                    }
                }
                teachersList.Add(teacherWithCourseNameModel);
            }
            return teachersList;
        }
        public List<TeacherModel> GetAllTeachers()
        {
            List<TeacherModel> teachersList = new List<TeacherModel>();

            foreach (Models.DBObjects.Teacher dbTeacher in dbContext.Teachers)
            {
                if (dbTeacher != null )
                {
                    TeacherModel teacherModel = new TeacherModel();

                    teacherModel.TeacherId = dbTeacher.TeacherId;
                    teacherModel.Name = dbTeacher.Name;
                    teacherModel.Phone = dbTeacher.Phone;
                    teacherModel.CourseId = dbTeacher.CourseId;

                    teachersList.Add(teacherModel);
                }          
            }
            return teachersList;
        }

        public TeacherModel GetTeacherById(Guid Id)
        {
            Models.DBObjects.Teacher dbTeacherModel = dbContext.Teachers.FirstOrDefault(m => m.TeacherId == Id);

            TeacherModel teacherModel = new TeacherModel();

            if (dbTeacherModel != null)
            {
                teacherModel.TeacherId = dbTeacherModel.TeacherId;
                teacherModel.Name = dbTeacherModel.Name;
                teacherModel.Phone = dbTeacherModel.Phone;
                teacherModel.CourseId = dbTeacherModel.CourseId;
            }
            return teacherModel;
        }

        public void UpdateTeacher(TeacherModel teacherModel)
        {
            Models.DBObjects.Teacher existingTeacher = dbContext.Teachers.FirstOrDefault(m => m.TeacherId == teacherModel.TeacherId);

            if (teacherModel != null)
            {
                existingTeacher.TeacherId = teacherModel.TeacherId;
                existingTeacher.Name = teacherModel.Name;
                existingTeacher.Phone = teacherModel.Phone;
                existingTeacher.CourseId = teacherModel.CourseId;

                dbContext.SubmitChanges();
            }

        }


        public void DeleteTeacher(Guid id)
        {
            Models.DBObjects.Teacher teacherToDelete = dbContext.Teachers.FirstOrDefault(m => m.TeacherId == id);

            dbContext.Teachers.DeleteOnSubmit(teacherToDelete);
            
            dbContext.SubmitChanges();



        }

    }
}