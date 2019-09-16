using ScoalaAmadeus.Models;
using ScoalaAmadeus.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Repository
{
    public class StudentRepository
    {
        private Models.DBObjects.SchoolsModelsDataContext dbContext;

        public StudentRepository()
        {
            this.dbContext = new Models.DBObjects.SchoolsModelsDataContext();
        }

        public StudentRepository(Models.DBObjects.SchoolsModelsDataContext dataContext)
        {
            this.dbContext = dataContext;
        }

        public void Insert(StudentModel studentModel)
        {
            studentModel.StudentId = Guid.NewGuid();

            if (studentModel != null)
            {
                Models.DBObjects.Student dbStudentModel = new Models.DBObjects.Student();

                dbStudentModel.StudentId = studentModel.StudentId;
                dbStudentModel.Name = studentModel.Name;
                dbStudentModel.BirthDate = studentModel.BirthDate;
                dbStudentModel.Age = studentModel.Age;
                dbStudentModel.Address = studentModel.Address;
                dbStudentModel.Email = studentModel.Email;
                dbStudentModel.Phone = studentModel.Phone;
                dbStudentModel.ProgramId = studentModel.ProgramId;
                dbStudentModel.TeacherId = studentModel.TeacherId;
                dbStudentModel.Course = studentModel.Course;               
                dbStudentModel.ParentId = studentModel.ParentId;

                dbContext.Students.InsertOnSubmit(dbStudentModel);
                dbContext.SubmitChanges();

            }
        }

        public List<StudentModel> GetAllStudents()
        {
            List<StudentModel> studentsList = new List<StudentModel>();

            foreach (Models.DBObjects.Student dbStudentModel in dbContext.Students)
            {
                StudentModel studentModel = new StudentModel();

                if (dbStudentModel != null)
                {
                    studentModel.StudentId = dbStudentModel.StudentId;
                    studentModel.Name = dbStudentModel.Name;
                    studentModel.BirthDate = dbStudentModel.BirthDate;
                    studentModel.Age = dbStudentModel.Age;
                    studentModel.Address = dbStudentModel.Address;
                    studentModel.Email = dbStudentModel.Email;
                    studentModel.Phone = dbStudentModel.Phone;
                    studentModel.ProgramId = dbStudentModel.ProgramId;
                    
                    studentModel.TeacherId = dbStudentModel.TeacherId;
                    studentModel.Course = dbStudentModel.Course;
                    studentModel.ParentId = dbStudentModel.ParentId;

                    studentsList.Add(studentModel);
                }
            }
            return studentsList;
        }

        public StudentModel GetStudentById(Guid Id)
        {
            Models.DBObjects.Student dbStudentModel = dbContext.Students.FirstOrDefault(m => m.StudentId == Id);

            StudentModel studentModel = new StudentModel();

            if (dbStudentModel != null)
            {
                studentModel.StudentId = dbStudentModel.StudentId;
                studentModel.Name = dbStudentModel.Name;
                studentModel.BirthDate = dbStudentModel.BirthDate;
                studentModel.Age = dbStudentModel.Age;
                studentModel.Address = dbStudentModel.Address;
                studentModel.Email = dbStudentModel.Email;
                studentModel.Phone = dbStudentModel.Phone;
                studentModel.ProgramId = dbStudentModel.ProgramId;
                studentModel.TeacherId = dbStudentModel.TeacherId;
                studentModel.Course = dbStudentModel.Course;
                studentModel.ParentId = dbStudentModel.ParentId;
            }
            return studentModel;
        }

        public void Update(StudentModel studentModel)
        {
            Models.DBObjects.Student dbStudentModel = dbContext.Students.FirstOrDefault(m => m.StudentId == studentModel.StudentId);

            if (studentModel != null)
            {
                dbStudentModel.StudentId = studentModel.StudentId;
                dbStudentModel.Name = studentModel.Name;
                dbStudentModel.BirthDate = studentModel.BirthDate;
                dbStudentModel.Age = studentModel.Age;
                dbStudentModel.Address = studentModel.Address;
                dbStudentModel.Email = studentModel.Email;
                dbStudentModel.Phone = studentModel.Phone;
                dbStudentModel.ProgramId = studentModel.ProgramId;
                
                dbStudentModel.TeacherId = studentModel.TeacherId;
                dbStudentModel.Course = studentModel.Course;
                dbStudentModel.ParentId = studentModel.ParentId;

                dbContext.SubmitChanges();
            }

        }

        public void Delete(Guid Id)
        {
            Models.DBObjects.Student dbStudentModel = dbContext.Students.FirstOrDefault(m => m.StudentId == Id);

            dbContext.Students.DeleteOnSubmit(dbStudentModel);
            dbContext.SubmitChanges();
        }

        public List<StudentModel> GetAllStudentsByTeacherId(Guid Id)
        {
            List<StudentModel> studentsList = new List<StudentModel>();
            List<Student> students = dbContext.Students.Where(x => x.TeacherId == Id).ToList();

            foreach (Models.DBObjects.Student dbStudentModel in students)
            {
               
                    StudentModel studentModel = new StudentModel();

                    studentModel.StudentId = dbStudentModel.StudentId;
                    studentModel.Name = dbStudentModel.Name;
                    studentModel.BirthDate = dbStudentModel.BirthDate;
                    studentModel.Age = dbStudentModel.Age;
                    studentModel.Address = dbStudentModel.Address;
                    studentModel.Email = dbStudentModel.Email;
                    studentModel.Phone = dbStudentModel.Phone;
                    studentModel.ProgramId = dbStudentModel.ProgramId;
                    
                    studentModel.TeacherId = dbStudentModel.TeacherId;
                studentModel.Course = dbStudentModel.Course;
                studentModel.ParentId = dbStudentModel.ParentId;

                    studentsList.Add(studentModel);
                
            }
            return studentsList;
        }
    }
}