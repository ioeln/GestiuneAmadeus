using ScoalaAmadeus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoalaAmadeus.Repository
{
    public class ParentRepository
    {
        private Models.DBObjects.SchoolsModelsDataContext dbContext;

        public ParentRepository()
        {
            this.dbContext = new Models.DBObjects.SchoolsModelsDataContext();
        }

        public ParentRepository(Models.DBObjects.SchoolsModelsDataContext dataContext)
        {
            this.dbContext = dataContext;
        }

        public void Insert(ParentModel parentModel)
        {
            parentModel.ParentId = Guid.NewGuid();

            if (parentModel != null)
            {
                Models.DBObjects.Parent dbParentModel = new Models.DBObjects.Parent();

                dbParentModel.ParentId = parentModel.ParentId;
                dbParentModel.Name = parentModel.Name;
                dbParentModel.Phone = parentModel.Phone;
                dbParentModel.Email = parentModel.Email;

                dbContext.Parents.InsertOnSubmit(dbParentModel);
                dbContext.SubmitChanges();
            }
        }

        public List<ParentModel> GetAllParents()
        {
            List<ParentModel> parentsList = new List<ParentModel>();

            foreach (Models.DBObjects.Parent dbParentModel in dbContext.Parents)
            {
                ParentModel parentModel = new ParentModel();

                if (dbParentModel != null)
                {
                    parentModel.ParentId = dbParentModel.ParentId;
                    parentModel.Name = dbParentModel.Name;
                    parentModel.Phone = dbParentModel.Phone;
                    parentModel.Email = dbParentModel.Email;

                    parentsList.Add(parentModel);
                }
            }
            return parentsList;
        }

        public ParentModel GetParentById(Guid id)
        {
            Models.DBObjects.Parent dbParentModel = dbContext.Parents.FirstOrDefault(m => m.ParentId == id);

            ParentModel parentModel = new ParentModel();

            if (dbParentModel != null)
            {
                parentModel.ParentId = dbParentModel.ParentId;
                parentModel.Name = dbParentModel.Name;
                parentModel.Phone = dbParentModel.Phone;
                parentModel.Email = dbParentModel.Email;
            }

            return parentModel;
        }

        public void Update(ParentModel parentModel)
        {
            Models.DBObjects.Parent dbParentModel = dbContext.Parents.FirstOrDefault(m => m.ParentId == parentModel.ParentId);

            if (parentModel != null)
            {
                dbParentModel.ParentId = parentModel.ParentId;
                dbParentModel.Name = parentModel.Name;
                dbParentModel.Phone = parentModel.Phone;
                dbParentModel.Email = parentModel.Email;

                dbContext.SubmitChanges();
            }
        }

        public void Delete(Guid id)
        {
            Models.DBObjects.Parent dbParentModel = dbContext.Parents.FirstOrDefault(m => m.ParentId == id);

            dbContext.Parents.DeleteOnSubmit(dbParentModel);
            dbContext.SubmitChanges();
        }

    }
}