using Microsoft.AspNetCore.Mvc;

namespace Financio
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService _articleService;
        private readonly UserService _userService;


        public ArticleController(ArticleService articleService, UserService userService)
        {
            _articleService = articleService;
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ArticleOutputDTO>> GetAll()
        {
            var articles = _articleService.GetAllArticles();
            return Ok(articles);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ArticleOutputDTO>> GetById(string id)
        {
            var article = _articleService.GetArticleByID(id);
            if (article == null)
                return NotFound();

            return Ok(article);
        }

        [HttpGet("GetAllByCollection/{id}")]
        public async Task<ActionResult<ArticleOutputDTO>> GetAllByCollection(string id)
        {
            var articles = _articleService.GetAllArticlesFromCollection(id);
            return Ok(articles);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(ArticleInputDTO article)
        {
            var result = _articleService.CreateArticle(article);

            return Ok(result.Id != null ? result : false);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update(ArticleInputDTO article, string id)
        {
            var result = _articleService.UpdateArticle(article, id);

            return Ok(result != null ? result : false);
        }

        [HttpPost("Like")]
        public async Task<ActionResult> Like(UserLikeDTO likeDTO)
        {
            var result = _userService.AssignLikedArticleToUser(likeDTO.ArticleID, likeDTO.UserID);

            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var result = _articleService.DeleteArticle(id);

            return Ok(result);
        }
    }
}
