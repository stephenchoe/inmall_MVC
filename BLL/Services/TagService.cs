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
    public interface ITagService
    {
        Tag GetById(int id);
        Tag Create(Tag tag);
        void Update(Tag tag);
        void Delete(int id);

        IEnumerable<Tag> GetTagsByKeyword(string keyword);
        IEnumerable<Tag> GetHotTags(int count);

        Tag GetTagByName(string name);
    }
    public class TagService : ITagService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITagRepository tagRepository;
        public TagService(IUnitOfWork unitOfWork, ITagRepository tagRepository)
        {
            var context = unitOfWork.Context;
            tagRepository.Context = context;

            this.unitOfWork = unitOfWork;
            this.tagRepository = tagRepository;
        }
       
        public Tag GetById(int id)
        {
            return tagRepository.GetById(id);
        }
        public Tag Create(Tag tag)
        {
            tagRepository.Insert(tag);
            Save();

            if (tag.Id > 0) return tag;
            return null;
        }
        public void Update(Tag tag)
        {
            tagRepository.Update(tag);
            Save();
        }
        public void Delete(int id)
        {
            var tag = GetById(id);

            tagRepository.Delete(tag);
            Save();

        }
        public Tag GetTagByName(string name)
        {
            return tagRepository.Get(c => c.Name == name);
        }
        public IEnumerable<Tag> GetTagsByKeyword(string keyword)
        {
            return tagRepository.GetMany(c => c.Name.Contains(keyword));
        }
        public IEnumerable<Tag> GetHotTags(int count)
        {
            return tagRepository.GetAll().OrderByDescending(t => t.Clicks).Take(count).ToList();
        }

        void Save()
        {
            unitOfWork.Commit();
        }
    }
}
