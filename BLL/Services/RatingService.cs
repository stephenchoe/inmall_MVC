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
    public interface IRatingService
    {
        int Create(int productId, int star);
        Rating GetById(int id);
        Rating Create(Rating rating);
        void Update(Rating rating);
        void Delete(int id);

    }
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRatingRepository ratingRepository;
        public RatingService(IUnitOfWork unitOfWork, IRatingRepository ratingRepository)
        {
            var context = unitOfWork.Context;
            ratingRepository.Context = context;

            this.unitOfWork = unitOfWork;
            this.ratingRepository = ratingRepository;
        }
        public int Create(int productId, int star)
        {
            var rating = new Rating
            {
                 ProductId=productId,
                 Star=star                  
            };
            ratingRepository.Insert(rating);
            Save();

            return rating.Id;
        }

        public Rating GetById(int id)
        {
            return ratingRepository.GetById(id);
        }
        public Rating Create(Rating rating)
        {
            ratingRepository.Insert(rating);
            Save();

            if (rating.Id > 0) return rating;
            return null;
        }
        public void Update(Rating rating)
        {
            ratingRepository.Update(rating);
            Save();
        }
        public void Delete(int id)
        {
            var rating = GetById(id);

            ratingRepository.Delete(rating);
            Save();

        }



        void Save()
        {
            unitOfWork.Commit();
        }
    }
}
