using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using DAL;
using DAL.Infrastructure;

namespace BLL
{
    public interface IADService
    {
        AD GetById(int id);
        AD Create(AD ad);
        void Update(AD ad);
        void Delete(int id);



        IEnumerable<AD> GetAds(bool active = true);

    }
    public class ADService : IADService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IADRepository adRepository;
        public ADService(IUnitOfWork unitOfWork, IADRepository adRepository)
        {
            var context = unitOfWork.Context;
            adRepository.Context = context;

            this.unitOfWork = unitOfWork;
            this.adRepository = adRepository;
        }
       
        public AD GetById(int id)
        {
            return adRepository.GetById(id);
        }
        public AD Create(AD ad)
        {
            adRepository.Insert(ad);
            Save();

            if (ad.Id > 0) return ad;
            return null;
        }
        public void Update(AD ad)
        {
            adRepository.Update(ad);
            Save();
        }
        public void Delete(int id)
        {
            var ad = GetById(id);

            adRepository.Delete(ad);
            Save();

        }

        public IEnumerable<AD> GetAds(bool active = true)
        {
            return adRepository.GetMany(c => c.Active == active).OrderByDescending(c => c.DisplayOrder);
        }

        void Save()
        {
            unitOfWork.Commit();
        }
    }
}
