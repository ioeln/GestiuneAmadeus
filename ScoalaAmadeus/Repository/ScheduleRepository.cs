using ScoalaAmadeus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Repository
{
    public class ScheduleRepository
    {
        private Models.DBObjects.SchoolsModelsDataContext dbContext;

        public ScheduleRepository()
        {
            dbContext = new Models.DBObjects.SchoolsModelsDataContext();
        }
        public ScheduleRepository(Models.DBObjects.SchoolsModelsDataContext dataContext)
        {
            dbContext = dataContext;
        }

        public void Insert(ScheduleModel scheduleModel)
        {
            scheduleModel.ScheduleId = Guid.NewGuid();

            if (scheduleModel != null)
            {
                Models.DBObjects.Schedule dbScheduleModel = new Models.DBObjects.Schedule();

                dbScheduleModel.ScheduleId = scheduleModel.ScheduleId;
                dbScheduleModel.Day = scheduleModel.Day;
                dbScheduleModel.StudentId = scheduleModel.StudentId;
                dbScheduleModel.TeacherId = scheduleModel.TeacherId;
                dbScheduleModel.ClassId = scheduleModel.ClassId;

                dbContext.Schedules.InsertOnSubmit(dbScheduleModel);
                dbContext.SubmitChanges();
            }
        }
        public List<ScheduleModel> GetAllSchedules()
        {
            List<ScheduleModel> schedulesList = new List<ScheduleModel>();

            foreach (Models.DBObjects.Schedule dbScheduleModel in dbContext.Schedules)
            {
                ScheduleModel scheduleModel = new ScheduleModel();

                scheduleModel.ScheduleId = dbScheduleModel.ScheduleId;
                scheduleModel.Day = dbScheduleModel.Day;
                scheduleModel.StudentId = dbScheduleModel.StudentId;
                scheduleModel.TeacherId = dbScheduleModel.TeacherId;
                scheduleModel.ClassId = dbScheduleModel.ClassId;

                schedulesList.Add(scheduleModel);
            }
            return schedulesList;
        }
        public ScheduleModel GetScheduleById(Guid id)
        {
            Models.DBObjects.Schedule dbScheduleModel = dbContext.Schedules.FirstOrDefault(m => m.ScheduleId == id);

            ScheduleModel scheduleModel = new ScheduleModel();

            if (dbScheduleModel != null)
            {
                scheduleModel.ScheduleId = dbScheduleModel.ScheduleId;
                scheduleModel.Day = dbScheduleModel.Day;
                scheduleModel.StudentId = dbScheduleModel.StudentId;
                scheduleModel.TeacherId = dbScheduleModel.TeacherId;
                scheduleModel.ClassId = dbScheduleModel.ClassId;
            }
            return scheduleModel;
        }

        public void Update(ScheduleModel scheduleModel)
        {
            Models.DBObjects.Schedule dbScheduleModel = dbContext.Schedules.FirstOrDefault(m => m.ScheduleId == scheduleModel.ScheduleId);

            if (scheduleModel != null)
            {
                dbScheduleModel.ScheduleId = scheduleModel.ScheduleId;
                dbScheduleModel.Day = scheduleModel.Day;
                dbScheduleModel.StudentId = scheduleModel.StudentId;
                dbScheduleModel.TeacherId = scheduleModel.TeacherId;
                dbScheduleModel.ClassId = scheduleModel.ClassId;

                dbContext.SubmitChanges();
            }
        }

        public void Delete(Guid id)
        {
            Models.DBObjects.Schedule dbScheduleModel = dbContext.Schedules.FirstOrDefault(m => m.ScheduleId == id);

            dbContext.Schedules.DeleteOnSubmit(dbScheduleModel);
            dbContext.SubmitChanges();
        }
    }
}