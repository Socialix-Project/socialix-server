namespace Socialix.Common.API
{
    /// <summary>
    /// ApiBaseResponse
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ApiBaseResponse<T>
    {
        public string? MessageId { get; set; }
        public string? Message { get; set; }
        public bool? Success { get; set; }
        public abstract T? Response { get; set; }
        public List<DetailError>? DetailErrorList { get; set; }
    }
}
