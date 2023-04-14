using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Financio
{
    public class CollectionService
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CollectionService> _logger;

        public CollectionService(DBContext context, IMapper mapper, ILogger<CollectionService> logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        public CollectionOutputDTO CreateCollection(CollectionInputDTO CollectionInputDTO)
        {
            var collection_entity = _mapper.Map<Collection>(CollectionInputDTO);

            _context.Collections.InsertOne(collection_entity);

            var result = _mapper.Map<CollectionOutputDTO>(collection_entity);

            _logger.LogInformation($"Pushed Collection {collection_entity.Id}");

            return result;
        }

        public CollectionOutputDTO UpdateCollection(CollectionInputDTO CollectionInputDTO, string id)
        {
            var objectId = ObjectId.Parse(id);
            var collection_entity = _mapper.Map<Collection>(CollectionInputDTO);
            collection_entity.Id = id;


            _context.Collections.ReplaceOne(x => x.Id == id, collection_entity);

            var result = _mapper.Map<CollectionOutputDTO>(collection_entity);

            _logger.LogInformation($"Updated Collection {id}");

            return result;
        }

        public List<CollectionOutputDTO> GetAllCollections()
        {
            IEnumerable<Collection> collections = _context.Collections.Find(_ => true).ToList();


            List<CollectionOutputDTO> collectionDTOs = new List<CollectionOutputDTO>();

            foreach (var collection in collections)
            {
                var collectionDTO = _mapper.Map<CollectionOutputDTO>(collection);

                collectionDTOs.Add(collectionDTO);
            }

            _logger.LogInformation($"Retrived all Collections");

            return collectionDTOs;
        }

        public CollectionOutputDTO GetCollectionByID(string id)
        {
            var objectId = ObjectId.Parse(id);

            var collection = _context.Collections.Find(x => x.Id == id).FirstOrDefault();

            _logger.LogInformation($"Retrieved Collection by id {id}");
            return _mapper.Map<CollectionOutputDTO>(collection);
        }
    }
}
