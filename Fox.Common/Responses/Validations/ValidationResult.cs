namespace Fox.Common.Responses
{
    public class ValidationResult
    {
        public string Message { get; set; }
        public string FieldName { get; set; }

        public ValidationResult(string field, string message)
        {
            this.FieldName = field;
            this.Message = message;
        }
    }
}
