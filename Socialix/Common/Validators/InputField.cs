namespace Socialix.Common.Validators
{
    public class InputField<T> where T : class
    {
        public string FieldName { get; set; }   
        public T Value { get; set; }
    }
}
