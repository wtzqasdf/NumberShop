using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberShop.Models.Contexts;
using NumberShop.Models.Tables;
using NumberShop.Models.RestParams.Merch;
using NumberShop.Models.Tables.External;
using Microsoft.EntityFrameworkCore;

namespace NumberShop.Models.Repository
{
    public class MerchDetailRepository : IDisposable
    {
        MerchDetailDbContext _merchDetailDbContext;

        public MerchDetailRepository(MerchDetailDbContext merchDetailDbContext)
        {
            _merchDetailDbContext = merchDetailDbContext;
        }

        public void AddMerchDetail(MerchDetail_C model)
        {
            MerchDetail m = new MerchDetail();
            m.MerchID = Guid.NewGuid().ToString();
            m.TypeID = model.TypeID;
            m.Name = model.Name;
            m.Price = model.Price;
            m.Description = model.Description;
            m.Remain = model.Remain;
            _merchDetailDbContext.MerchDetails.Add(m);
            _merchDetailDbContext.SaveChanges();
        }

        public MerchDetail[] GetMerchDetails()
        {
            return (from t in _merchDetailDbContext.MerchDetails select t).AsNoTracking().ToArray();
        }

        public MerchDetail[] GetMerchDetails(string typeID)
        {
            return (from t in _merchDetailDbContext.MerchDetails where t.TypeID == typeID select t).AsNoTracking().ToArray();
        }

        public MerchDetail GetMerchDetail(string merchID)
        {
            return (from t in _merchDetailDbContext.MerchDetails where t.MerchID == merchID select t).ToArray().FirstOrDefault();
        }

        public void UpdateMerchDetail(MerchDetail_U model)
        {
            MerchDetail detail = (from t in _merchDetailDbContext.MerchDetails where t.MerchID == model.MerchID select t).ToArray().FirstOrDefault();
            if (detail != null)
            {
                detail.Name = model.Name;
                detail.Description = model.Description;
                detail.Price = model.Price;
                detail.TypeID = model.TypeID;
                detail.Remain = model.Remain;
                _merchDetailDbContext.SaveChanges();
            }
        }

        public void DeleteMerchDetail(MerchDetail_D model)
        {
            MerchDetail detail = (from t in _merchDetailDbContext.MerchDetails where t.MerchID == model.MerchID select t).ToArray().FirstOrDefault();
            if (detail != null)
            {
                _merchDetailDbContext.MerchDetails.Remove(detail);
                _merchDetailDbContext.SaveChanges();
            }
        }

        public void ReduceMerchesCount(MerchOrder[] orders)
        {
            MerchDetail[] details = (from t1 in orders join t2 in _merchDetailDbContext.MerchDetails on t1.MerchID equals t2.MerchID select t2).ToArray();
            for (int i = 0; i < details.Length; i++)
            {
                MerchDetail detail = details[i];
                MerchOrder order = (from t in orders where t.MerchID == detail.MerchID select t).ToArray().FirstOrDefault();
                detail.Remain -= order.Count;
            }
            _merchDetailDbContext.SaveChanges();
        }

        public bool CheckMerchCountIsEnough(CartMerch[] merches, out string[] msgs)
        {
            List<string> messages = new List<string>();
            foreach (CartMerch merch in merches)
            {
                MerchDetail detail = (from t in _merchDetailDbContext.MerchDetails where t.MerchID == merch.MerchID select t).ToArray().FirstOrDefault();
                if (merch.Count > detail.Remain)
                {
                    messages.Add($"無法購買{detail.Name}, 數量不足");
                }
            }
            msgs = messages.ToArray();
            return messages.Count == 0;
        }

        public void Dispose()
        {
            _merchDetailDbContext = null;
        }
    }
}
