using ScoalaAmadeus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Repository
{
    public class ProgramRepository
    {
        private Models.DBObjects.SchoolsModelsDataContext dbContext;

        public ProgramRepository()
        {
            this.dbContext = new Models.DBObjects.SchoolsModelsDataContext();
        }

        public ProgramRepository(Models.DBObjects.SchoolsModelsDataContext dataContext)
        {
            this.dbContext = dataContext;
        }

        public void Insert(ProgramModel programModel)
        {
            programModel.ProgramId = Guid.NewGuid();

            if (programModel != null)
            {
                Models.DBObjects.Program dbProgram = new Models.DBObjects.Program();

                dbProgram.ProgramId = programModel.ProgramId;
                dbProgram.Name = programModel.Name;
                dbProgram.Hours_Mounth = programModel.Hours_Mounth;

                dbContext.Programs.InsertOnSubmit(dbProgram);
                dbContext.SubmitChanges();
            }
        }

        public List<ProgramModel> GetAllPrograms()
        {
            List<ProgramModel> programsList = new List<ProgramModel>();

            foreach (Models.DBObjects.Program dbProgram in dbContext.Programs)
            {
                ProgramModel programModel = new ProgramModel();

                programModel.ProgramId = dbProgram.ProgramId;
                programModel.Name = dbProgram.Name;
                programModel.Hours_Mounth = dbProgram.Hours_Mounth;

                programsList.Add(programModel);
            }
            return programsList;
        }

        public ProgramModel GetProgramById(Guid Id)
        {
            Models.DBObjects.Program dbProgramModel = dbContext.Programs.FirstOrDefault(m => m.ProgramId == Id);

            ProgramModel programModel = new ProgramModel();

            if (dbProgramModel != null)
            {               
                programModel.ProgramId = dbProgramModel.ProgramId;
                programModel.Name = dbProgramModel.Name;
                programModel.Hours_Mounth = dbProgramModel.Hours_Mounth;  
            }
            return programModel;
        }

        public void Update(ProgramModel programModelPassed)
        {
            Models.DBObjects.Program dbProgramModel = dbContext.Programs.FirstOrDefault(m => m.ProgramId == programModelPassed.ProgramId);

            if (programModelPassed != null)
            {
                dbProgramModel.ProgramId = programModelPassed.ProgramId;
                dbProgramModel.Name = programModelPassed.Name;
                dbProgramModel.Hours_Mounth = programModelPassed.Hours_Mounth;

                dbContext.SubmitChanges();
            }
        }

        public void Delete(Guid Id)
        {
            Models.DBObjects.Program programToDelete = dbContext.Programs.FirstOrDefault(m => m.ProgramId == Id);

            dbContext.Programs.DeleteOnSubmit(programToDelete);
            dbContext.SubmitChanges();
        }
    }
}