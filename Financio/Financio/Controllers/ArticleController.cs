using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Financio
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService _articleService;
        private readonly IMapper _mapper;



        public ArticleController(ArticleService articleService, IMapper mapper)
        {
            _articleService = articleService;
            _mapper = mapper;
        }

        [HttpGet("GetAllArticles")]
        public async Task<ActionResult<ArticleOutputDTO>> GetAllArticles()
        {
            var articles = _articleService.GetAllArticles();
            return Ok(articles);
        }

        [HttpGet("GetArticleById/{id}")]
        public async Task<ActionResult<ArticleOutputDTO>> GetArticleById(string id)
        {
            var article = _articleService.GetArticleByID(id);
            if (article == null)
                return NotFound();

            return Ok(article);
        }



        [HttpPost("CreateArticle")]
        public async Task<ActionResult> CreateArticle(ArticleInputDTO article)
        {
            var result = _articleService.CreateArticle(article);

            return Ok(result.Id != null ? result : false);
        }



        [HttpPut("UpdateArticle/{id}")]
        public async Task<ActionResult> UpdateArticle(ArticleInputDTO article, string id)
        {
            var result = _articleService.UpdateArticle(article, id);

            return Ok(result != null ? result : false);
        }



        [HttpDelete("DeleteArticle/{id}")]
        public async Task<ActionResult> DeleteArticle(string id)
        {
            var result = _articleService.DeleteArticle(id);

            return Ok(result);
        }
    }
}
