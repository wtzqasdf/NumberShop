using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using NumberShop.Models.Contexts;
using NumberShop.Models.Tables;
using NumberShop.Models.RestParams.Merch;
using NumberShop.Commons;
using Microsoft.EntityFrameworkCore;

namespace NumberShop.Models.Repository
{
    public class MerchTypeRepository : IDisposable
    {
        MerchTypeDbContext _merchTypeDbContext;

        public MerchTypeRepository(MerchTypeDbContext merchTypeDbContext)
        {
            _merchTypeDbContext = merchTypeDbContext;
        }

        public void AddMerchType(MerchType_C model)
        {
            MerchType m = new MerchType();
            m.Name = model.Name;
            m.TypeID = Guid.NewGuid().ToString();
            _merchTypeDbContext.MerchTypes.Add(m);
            _merchTypeDbContext.SaveChanges();
        }

        public void UpdateMerchType(MerchType_U model)
        {
            MerchType merch = (from t in _merchTypeDbContext.MerchTypes where t.TypeID == model.TypeID select t).ToArray().FirstOrDefault();
            if (merch != null)
            {
                merch.Name = model.Name;
                _merchTypeDbContext.SaveChanges();
            }
        }

        public void DeleteMerchType(MerchType_D model)
        {
            MerchType merch = (from t in _merchTypeDbContext.MerchTypes where t.TypeID == model.TypeID select t).ToArray().FirstOrDefault();
            if (merch != null)
            {
                _merchTypeDbContext.MerchTypes.Remove(merch);
                _merchTypeDbContext.SaveChanges();
            }
        }

        public MerchType[] GetMerchTypes()
        {
            return (from t in _merchTypeDbContext.MerchTypes select t).AsNoTracking().ToArray();
        }

        public void Dispose()
        {
            _merchTypeDbContext = null;
        }
    }
}
