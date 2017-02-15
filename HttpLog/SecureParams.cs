using static ClearSecureData.ClearData;

namespace HttpLog
{
    public interface IParams
    {
        SecureStringFormat UrlFormat { get; set; }
        string UrlKey { get; set; }
        SecureStringFormat BodyFormat { get; set; }
        string BodyKey { get; set; }
        SecureStringFormat ResponseFormat { get; set; }
        string ResponseKey { get; set; }
    }
    public class SecureParams : IParams
    {
        public SecureStringFormat BodyFormat { get; set; }
        public string BodyKey { get; set; }
        public SecureStringFormat ResponseFormat { get; set; }
        public string ResponseKey { get; set; }
        public SecureStringFormat UrlFormat { get; set; }
        public string UrlKey { get; set; }
    }
}
