using Socialix.Common.API;
using Socialix.Common.Constants;

namespace Socialix.Common.Validators
{
    public class InputCheck<T> where T : class
    {
        public static void CheckRequired(InputField<T> inputField, List<DetailError> detailErrors)
        {
            if (typeof(T) == typeof(string))
            {
                if (string.IsNullOrEmpty(inputField.Value.ToString()))
                {
                    detailErrors.Add(new DetailError
                    {
                        FieldName = inputField.FieldName,
                        Value = inputField.Value.ToString(),
                        MessageId = Message.E00005,
                        Message = Message.GetMessageById(Message.E00005)
                    });
                }
            }
            else if (Nullable.GetUnderlyingType(typeof(T)) != null) // Check nullable datatype int?, object?,...
            {
                if (inputField.Value == null)
                {
                    detailErrors.Add(new DetailError
                    {
                        FieldName = inputField.FieldName,
                        Value = null,
                        MessageId = Message.E00005,
                        Message = Message.GetMessageById(Message.E00005)
                    });
                }
            }
            else
            {
                if (EqualityComparer<T>.Default.Equals(inputField.Value, default(T))) // Check default datatype int,...
                {
                    detailErrors.Add(new DetailError
                    {
                        FieldName = inputField.FieldName,
                        Value = inputField?.Value?.ToString(),
                        MessageId = Message.E00005,
                        Message = Message.GetMessageById(Message.E00005)
                    });
                }
            }
        }
    }
}
