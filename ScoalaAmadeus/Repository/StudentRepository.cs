using ScoalaAmadeus.Models;
using ScoalaAmadeus.Models.DBObjects;
using ScoalaAmadeus.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ScoalaAmadeus.Repository
{
    public class StudentRepository
    {
        private ProgramRepository programRepository = new ProgramRepository();
        private TeacherRepository teacherRepository = new TeacherRepository();
        private ParentRepository parentRepository = new ParentRepository();

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
        // For child delete
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
                    studentModel.ParentId = dbStudentModel.ParentId;

                    studentsList.Add(studentModel);
                
            }
            return studentsList;
        }

        public List<StudentModel> GetAllStudentsByProgramId(Guid id)
        {
            List<StudentModel> studentsList = new List<StudentModel>();
            List<Student> students = dbContext.Students.Where(x => x.ProgramId == id).ToList();

            foreach (Student dbStudentModel in students)
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
                studentModel.ParentId = dbStudentModel.ParentId;

                studentsList.Add(studentModel);
            }
            return studentsList;
        }
        public List<StudentModel> GetAllStudentsByParentId(Guid id)
        {
            List<StudentModel> studentsList = new List<StudentModel>();
            List<Student> students = dbContext.Students.Where(x => x.ParentId == id).ToList();

            foreach (Student dbStudentModel in students)
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
                studentModel.ParentId = dbStudentModel.ParentId;

                studentsList.Add(studentModel);
            }
            return studentsList;
        }

        public List<StudentWithPropNamesViewModel> GetAllStudentsWithPropNames()
        {
            List<StudentWithPropNamesViewModel> studentsList = new List<StudentWithPropNamesViewModel>();
            
            foreach (Models.DBObjects.Student dbStudentModel in dbContext.Students)
            {
                StudentWithPropNamesViewModel studentModel = new StudentWithPropNamesViewModel();

                studentModel.StudentId = dbStudentModel.StudentId;
                studentModel.Name = dbStudentModel.Name;
                studentModel.BirthDate = dbStudentModel.BirthDate;
                studentModel.Age = dbStudentModel.Age;
                studentModel.Address = dbStudentModel.Address;
                studentModel.Email = dbStudentModel.Email;
                studentModel.Phone = dbStudentModel.Phone;
                studentModel.ProgramId = dbStudentModel.ProgramId;

                List<ProgramModel> programs = programRepository.GetAllPrograms();
                foreach (ProgramModel program in programs)
                {
                    if (program.ProgramId == dbStudentModel.ProgramId)
                    {
                        studentModel.ProgramName = program.Name;
                    }
                    
                }
                studentModel.TeacherId = dbStudentModel.TeacherId;

                List<TeacherModel> teachers = teacherRepository.GetAllTeachers();
                foreach (TeacherModel teacher in teachers)
                {
                    if (teacher.TeacherId == dbStudentModel.TeacherId)
                    {
                        studentModel.TeacherName = teacher.Name;
                    }
                }

                var Teachercourses = teacherRepository.GetAllTeachersWithCourseNames();
                foreach (TeacherWithCourseNameViewModel teacher in Teachercourses )
                {
                    if (teacher.TeacherId == dbStudentModel.TeacherId)
                    {
                        studentModel.CourseName = teacher.CourseName;
                    }
                    
                }

                studentModel.ParentId = dbStudentModel.ParentId;

                List<ParentModel> parents = parentRepository.GetAllParents();
                foreach (ParentModel parent in parents)
                {
                    if (parent.ParentId == studentModel.ParentId)
                    {
                        studentModel.ParentName = parent.Name;
                    }                   
                }
                studentsList.Add(studentModel);
            }
            return studentsList;
        }

        public List<StudentWithPropNamesViewModel> GetStudentsByTeacherId(Guid Id)
        {
            List<StudentWithPropNamesViewModel> studentsList = new List<StudentWithPropNamesViewModel>();
            List<Student> students = dbContext.Students.Where(x => x.TeacherId == Id).ToList();

            foreach (Models.DBObjects.Student dbStudentModel in students)
            {

                StudentWithPropNamesViewModel studentModel = new StudentWithPropNamesViewModel();

                studentModel.StudentId = dbStudentModel.StudentId;
                studentModel.Name = dbStudentModel.Name;
                studentModel.BirthDate = dbStudentModel.BirthDate;
                studentModel.Age = dbStudentModel.Age;
                studentModel.Address = dbStudentModel.Address;
                studentModel.Email = dbStudentModel.Email;
                studentModel.Phone = dbStudentModel.Phone;
                studentModel.ProgramId = dbStudentModel.ProgramId;

                List<ProgramModel> programs = programRepository.GetAllPrograms();
                foreach (ProgramModel program in programs)
                {
                    if (program.ProgramId == dbStudentModel.ProgramId)
                    {
                        studentModel.ProgramName = program.Name;
                    }

                }
                studentModel.TeacherId = dbStudentModel.TeacherId;

                List<TeacherModel> teachers = teacherRepository.GetAllTeachers();
                foreach (TeacherModel teacher in teachers)
                {
                    if (teacher.TeacherId == dbStudentModel.TeacherId)
                    {
                        studentModel.TeacherName = teacher.Name;
                    }
                }

                var Teachercourses = teacherRepository.GetAllTeachersWithCourseNames();
                foreach (TeacherWithCourseNameViewModel teacher in Teachercourses)
                {
                    if (teacher.TeacherId == dbStudentModel.TeacherId)
                    {
                        studentModel.CourseName = teacher.CourseName;
                    }

                }

                studentModel.ParentId = dbStudentModel.ParentId;

                List<ParentModel> parents = parentRepository.GetAllParents();
                foreach (ParentModel parent in parents)
                {
                    studentModel.ParentName = parent.Name;
                }
                studentsList.Add(studentModel);

            }
            return studentsList;
        }
    }
}