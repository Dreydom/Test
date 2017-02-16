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
    ///<summary>
    ///Ключей для строк можно задавать несколько, для этого нужно просто написать их в строке через запятую.
    ///<para>Например, BodyKey="user,password"</para>
    ///</summary>
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
