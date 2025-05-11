namespace server.Services
{
    public interface IGuidEncoderService
    {
        string EncodeGuidToBase64(Guid guid);
        Guid DecodeBase64ToGuid(string base64);
    }
} 