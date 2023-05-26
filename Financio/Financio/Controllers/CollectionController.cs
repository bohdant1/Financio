using Microsoft.AspNetCore.Mvc;

namespace Financio.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CollectionController : ControllerBase
    {
        private readonly CollectionService _collectionService;

        public CollectionController(CollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<CollectionOutputDTO>> GetAll()
        {
            var collections = _collectionService.GetAllCollections();
            collections[0].Name += "__HelloBohdan";
            return Ok(collections);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<CollectionOutputDTO>> GetById(string id)
        {
            var collection = _collectionService.GetCollectionByID(id);
            if (collection == null)
                return NotFound();

            return Ok(collection);
        }



        [HttpPost("Create")]
        public async Task<ActionResult> Create(CollectionInputDTO collection)
        {
            var result = _collectionService.CreateCollection(collection);

            return Ok(result.Id != null ? result : false);
        }



        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update(CollectionInputDTO collection, string id)
        {
            var result = _collectionService.UpdateCollection(collection, id);

            return Ok(result != null ? result : false);
        }
    }
}
