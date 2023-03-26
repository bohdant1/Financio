using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using AutoMapper.QueryableExtensions;

namespace Financio
{
    public class ArticleService
    {
        private readonly DBContext _mongoContext;
        private readonly BlobStorageContext _blobContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ArticleService> _logger;

        public ArticleService(DBContext context, BlobStorageContext blobContext, IMapper mapper, ILogger<ArticleService> logger) 
        {
            this._mongoContext = context;
            this._blobContext = blobContext;
            this._blobContext.SetContainer("Article");
            this._mapper = mapper;
            this._logger = logger;
        }
        public ArticleOutputDTO CreateArticle(ArticleInputDTO articleInputDTO)
        {
            var article_entity = _mapper.Map<Article>(articleInputDTO);
            article_entity.Date = DateTime.Now;
            article_entity.Id = ObjectId.GenerateNewId().ToString();

            _blobContext.Upload(article_entity.Text, article_entity.Id);

            _mongoContext.Articles.InsertOne(article_entity);

            var result = _mapper.Map<ArticleOutputDTO>(article_entity);

            _logger.LogInformation($"Pushed article {article_entity.Id}");

            return result;
        }

        public ArticleOutputDTO UpdateArticle(ArticleInputDTO articleInputDTO, string id)
        {
            //TODO FIX DATETIME PROBLEM
            var objectId = ObjectId.Parse(id);
            var article_entity = _mapper.Map<Article>(articleInputDTO);
            article_entity.Id = id;

            _mongoContext.Articles.ReplaceOne(x => x.Id == id, article_entity);

            var result = _mapper.Map<ArticleOutputDTO>(article_entity);

            _logger.LogInformation($"Updated article {id}");

            return result;
        }

        public bool DeleteArticle(string id)
        {
            var objectId = ObjectId.Parse(id);

            DeleteResult result = _mongoContext.Articles.DeleteOne(x => x.Id == id);

            _logger.LogInformation($"Deleted {result.DeletedCount} article(s) with {id}");

            return result.DeletedCount > 0;
        }

        public List<ArticleOutputDTO> GetAllArticles()
        {
            var articles = _mongoContext.Articles.AsQueryable();
            var collections = _mongoContext.Collections.AsQueryable();

            var articlesWithCollections = articles.ToList()
                .Select(a => {
                    a.Collections = collections.Where(c => a.CollectionIds.Contains(c.Id)).ToList();
                    return a;
                }).ToList();


            List<ArticleOutputDTO> articleDTOs = new List<ArticleOutputDTO>();

            foreach (var article in articlesWithCollections)
            {
                var articleDTO = _mapper.Map<ArticleOutputDTO>(article);

                articleDTOs.Add(articleDTO);
            }

            _logger.LogInformation($"Retrived all articles");

            return articleDTOs; 
        }

        public ArticleOutputDTO GetArticleByID(string id)
        {
            var objectId = ObjectId.Parse(id);

            var articles = _mongoContext.Articles.AsQueryable();
            var collections = _mongoContext.Collections.AsQueryable();

            var articleWithCollections = articles.Where(a => a.Id == id).FirstOrDefault();
            if (articleWithCollections != null)
            {
                articleWithCollections.Collections = collections.Where(c => articleWithCollections.CollectionIds.Contains(c.Id)).ToList();
            }

            _logger.LogInformation($"Retrieved article by id {id}");
            return _mapper.Map<ArticleOutputDTO>(articleWithCollections);
        }
    }
}
