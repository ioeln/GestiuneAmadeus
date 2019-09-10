using ScoalaAmadeus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Repository
{
    public class ClassRepository
    {
        private Models.DBObjects.SchoolsModelsDataContext dbContext;

        public ClassRepository()
        {
            dbContext = new Models.DBObjects.SchoolsModelsDataContext();
        }
        public ClassRepository(Models.DBObjects.SchoolsModelsDataContext dataContext)
        {
            dbContext = dataContext;
        }

        public void Insert(ClassModel classModel)
        {
            classModel.ClassId = Guid.NewGuid();

            if (classModel != null)
            {
                Models.DBObjects.Class dbClassModel = new Models.DBObjects.Class();

                dbClassModel.ClassId = classModel.ClassId;
                dbClassModel.Name = classModel.Name;

                dbContext.Classes.InsertOnSubmit(dbClassModel);
                dbContext.SubmitChanges();
            }
        }
        public List<ClassModel> GetAllClasses()
        {
            List<ClassModel> classesList = new List<ClassModel>();

            foreach (Models.DBObjects.Class dbClassModel in dbContext.Classes)
            {
                ClassModel classModel = new ClassModel();

                classModel.ClassId = dbClassModel.ClassId;
                classModel.Name = dbClassModel.Name;

                classesList.Add(classModel);
            }
            return classesList;
        }
        public ClassModel GetClassById(Guid id)
        {
            ClassModel classModel = new ClassModel();

            Models.DBObjects.Class dbClassModel = dbContext.Classes.FirstOrDefault(m => m.ClassId == id);

            if (dbClassModel != null)
            {
                classModel.ClassId = dbClassModel.ClassId;
                classModel.Name = dbClassModel.Name;
            }
            return classModel;
        }
        public void Update(ClassModel classModel)
        {
            Models.DBObjects.Class dbClassModel = dbContext.Classes.FirstOrDefault(m => m.ClassId == classModel.ClassId);

            if (classModel != null)
            {
                dbClassModel.ClassId = classModel.ClassId;
                dbClassModel.Name = classModel.Name;

                dbContext.SubmitChanges();
            }
        }
        public void Delete(Guid id)
        {
            Models.DBObjects.Class dbClassModel = dbContext.Classes.FirstOrDefault(m => m.ClassId == id);

            dbContext.Classes.DeleteOnSubmit(dbClassModel);
            dbContext.SubmitChanges();
        }
    }
}