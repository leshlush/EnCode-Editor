using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using LtiLibrary.NetCore.Lti.v1;

namespace SnapSaves.Helpers
{   
    public class LtiSignatureGenerator
    {
    public static string GenerateSignature(LtiRequest ltiRequest, string sharedSecret)
        {
            if (ltiRequest == null)
                throw new ArgumentNullException(nameof(ltiRequest), "LtiRequest cannot be null.");

            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentException("Shared secret cannot be null or empty.", nameof(sharedSecret));

            // Step 1: Collect and sort parameters, ensuring no duplicates
            var parameters = ltiRequest.Parameters
                .GroupBy(kvp => kvp.Key) // Group by key to remove duplicates
                .Select(g => g.First()) // Take the first occurrence of each key
                .OrderBy(kvp => kvp.Key) // Sort parameters alphabetically by key
                .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}");

            // Step 2: Create the parameter string
            var parameterString = string.Join("&", parameters);

            // Step 3: Construct the base string
            var baseString = $"{ltiRequest.HttpMethod.ToUpper()}&{Uri.EscapeDataString(ltiRequest.Url.ToString())}&{Uri.EscapeDataString(parameterString)}";
            Console.WriteLine($"Base String: {baseString}");

            // Step 4: Construct the signing key
            var signingKey = $"{Uri.EscapeDataString(sharedSecret)}&";
            Console.WriteLine($"Signing Key: {signingKey}");

            // Step 5: Generate the HMAC-SHA1 signature
            using var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(signingKey));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(baseString));

            // Step 6: Return the Base64-encoded signature
            return Convert.ToBase64String(hash);
        }


     }
}