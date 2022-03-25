namespace KC.HMS.Core.Abstracts
{
    public class ModelValidationResult
    {
        public ModelValidationResult()
        {

        }
        public bool IsValid { get { return Messages.Any() ? false : true; } }
        public string Message { get { return string.Join(" ", Messages); } }
        public List<string> Messages { get; set; } = new List<string>();
    }
}

