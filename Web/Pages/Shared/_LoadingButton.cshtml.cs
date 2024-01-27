namespace Web.Pages.Shared
{
    public class LoadingButton
    {
        public string Content { get; set; }
        public string Classes { get; set; }

        public LoadingButton(string content, string classes)
        {
            Content = content;
            Classes = classes;
        }
    }
}
