namespace Financio
{
    public class ArticleOutputDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public Collection Collection { get; set; }
    }
}
