using System.Security.Cryptography;
using System.Text;

namespace GeoDataApi.Services;

public class HashService : IHashService
{
    public string ComputeSHA256(string text)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("SuperSecretKey"));
        hmac.ComputeHash(Encoding.UTF8.GetBytes(text));
        StringBuilder stringBuilder = new();

        foreach (var character in hmac.Hash)
        {
            stringBuilder.Append(character.ToString("x2"));
        }

        return stringBuilder.ToString();
    }
}