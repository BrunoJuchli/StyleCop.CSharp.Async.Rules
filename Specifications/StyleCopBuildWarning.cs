namespace Specifications
{
    public class StyleCopBuildWarning
    {
        public string Description { get; set; }

        public string File { get; set; }

        public int Line { get; set; }

        public int Column { get; set; }

        public string Project { get; set; }

        public string CheckId { get; set; }

        public string CheckNameSpace { get; set; }
    }
}