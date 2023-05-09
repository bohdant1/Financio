using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Financio
{
    public class UserService
    {
        private readonly DBContext _mongoContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CollectionService> _logger;

        public UserService(DBContext context, IMapper mapper, ILogger<CollectionService> logger)
        {
            this._mongoContext = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        public UserOutputDTO CreateUser(UserInputDTO input)
        {
            var user_entity = _mapper.Map<User>(input);
            _mongoContext.Users.InsertOne(user_entity);
            var result = _mapper.Map<UserOutputDTO>(input);
            _logger.LogInformation($"Created user {user_entity.Id}");
            return result;
        }

        public List<ObjectId> GetLikedArticlesByUser(string id)
        {
            var objectId = ObjectId.Parse(id);
            List<ObjectId> articles = _mongoContext.Users.Find(x => x.Id == id).FirstOrDefault().LikedArticles;
            _logger.LogInformation($"Retrived {articles.Count} liked articles of {objectId}");
            return articles; 
        }

        public bool AssignLikedArticleToUser(string articleID, string userID)
        {
            var user = ObjectId.Parse(userID);
            var article = ObjectId.Parse(articleID);
            var user_entity = _mongoContext.Users.Find(x => x.Id == userID).FirstOrDefault();

            if (user_entity != null)
            {
                user_entity.LikedArticles.Add(article);
                _mongoContext.Users.ReplaceOne(u => u.Id == userID, user_entity);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
