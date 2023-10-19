using ArticleSystem.Database;
using ArticleSystem.DTOs;
using ArticleSystem.Entity;
using ArticleSystem.Exceptions;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ArticleSystem.Repository
{
    public class TagRepository : ITagRepository
    {
        private ArticleDbContext _context;
        private IMapper _mapper;

        public TagRepository(ArticleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddTag(TagAddDto tagDto)
        {
            var targetArticle = _context.Articles.Include(x => x.Tags).FirstOrDefault(x => x.Id == tagDto.ArticleId);

            if (targetArticle is null) throw new NoArticleToTaggedException("There's no article to tagged");

            tagDto.Value = tagDto.Value.Replace(" ", "");
            Tag newTag = _mapper.Map<Tag>(tagDto);

            if (!targetArticle.Tags.Any(x => x.Value.ToLower() == tagDto.Value.ToLower())) targetArticle.Tags.Add(newTag);
            else throw new TagAlreadyExistException("Tag already exist");

            _context.Update(targetArticle);
            _context.SaveChanges();
        }

        public void AddTagRange(TagListAddDto tagsDto)
        {
            var targetArticle = _context.Articles.Include(x => x.Tags).FirstOrDefault(x => x.Id == tagsDto.ArticleId);
            Boolean exceptionSwitch = false; //set 'true' if some tags have duplicate

            if (targetArticle is null) throw new NoArticleToTaggedException("There's no article to tagged");

            for (int i = 0; i < tagsDto.Value.Count; i++)
            {
                tagsDto.Value[i] = tagsDto.Value[i].Replace(" ", "");
            }

            List<Tag> newTags = _mapper.Map<List<Tag>>(tagsDto);

            foreach (var item in newTags)
            {
                if (targetArticle.Tags.IsNullOrEmpty() || !targetArticle.Tags.Any(x => x.Value.ToLower() == item.Value.ToLower())) targetArticle.Tags.Add(item);
                else exceptionSwitch = true;
            }

            _context.Update(targetArticle);
            _context.SaveChanges();

            if (exceptionSwitch) throw new TagAlreadyExistException("Some Tags already exist");
        }

        public void DeleteTag(TagDeleteDto tagDto)
        {
            var tagList = _context.Articles.Include(x => x.Tags)
                .FirstOrDefault(x => x.Id == tagDto.ArticleId && x.Tags.Any(tag => tag.Value.ToLower() == tagDto.Value.ToLower()));

            Tag? tagToRemove = null;

            if (tagList is not null) tagToRemove = tagList.Tags.FirstOrDefault(tag => tag.Value.ToLower() == tagDto.Value.ToLower());

            if (tagToRemove is null) throw new NoTagToRemoveException("There's no tag to remove");

            _context.Tags.Remove(tagToRemove);
            _context.SaveChanges();
        }

        public void DeleteAllTag(int articleId)
        {
            var potentialArticle = _context.Articles.Include(x => x.Tags).FirstOrDefault(x => x.Id == articleId);

            if (potentialArticle is null) throw new NotExistException("Article not exist");

            var tagList = potentialArticle.Tags.ToList();

            if (tagList.IsNullOrEmpty()) return;

            _context.Tags.RemoveRange(tagList);
            _context.SaveChanges();
        }
    }
}
