using System.Linq;
using LtiLibrary.NetCore.Lti.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SnapSaves.Helpers;
using SnapSaves.Models;

namespace SnapSaves.Controllers
{
    public class LtiController : Controller
    {
        private readonly string _consumerKey;
        private readonly string _sharedSecret;

        public LtiController(IOptions<LtiSettings> ltiSettings)
        {
            _consumerKey = ltiSettings.Value.ConsumerKey;
            _sharedSecret = ltiSettings.Value.SharedSecret;
        }

        [HttpPost]
        public async Task<IActionResult> Launch()
        {
            // Read the form data from the HTTP request
            var form = await HttpContext.Request.ReadFormAsync();

            // Log incoming form data
            Console.WriteLine("Debug: Incoming form data:");
            foreach (var kvp in form)
            {
                Console.WriteLine($"{kvp.Key} = {kvp.Value}");
            }

            // Validate required LTI parameters
            var requiredParameters = new[] { "lti_message_type", "lti_version", "resource_link_id", "user_id" };
            foreach (var param in requiredParameters)
            {
                if (!form.ContainsKey(param))
                {
                    Console.WriteLine($"Error: Missing required parameter: {param}");
                    return BadRequest($"Missing required parameter: {param}");
                }
            }

            // Create the LtiRequest object
            var ltiRequest = new LtiRequest
            {
                ConsumerKey = _consumerKey,
                HttpMethod = HttpContext.Request.Method,
                Url = new Uri($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}")
            };

            // Add parameters to the LtiRequest
            foreach (var kvp in form)
            {
                if (kvp.Key != "oauth_signature")
                {
                    ltiRequest.AddParameter(kvp.Key, kvp.Value);
                }
            }

            // Validate the OAuth signature
            var providedSignature = form["oauth_signature"];
            var generatedSignature = LtiSignatureGenerator.GenerateSignature(ltiRequest, _sharedSecret);

            Console.WriteLine($"Generated Signature: {generatedSignature}");
            Console.WriteLine($"Provided Signature: {providedSignature}");

            if (generatedSignature != providedSignature)
            {
                Console.WriteLine("Error: Invalid OAuth signature.");
                return Unauthorized("Invalid OAuth signature.");
            }

            // Extract LTI parameters for logging
            var userId = ltiRequest.UserId;
            var roles = ltiRequest.Roles;
            var resourceLinkId = ltiRequest.ResourceLinkId;

            Console.WriteLine($"LTI Launch: UserId={userId}, Roles={roles}, ResourceLinkId={resourceLinkId}");

            // Respond with the appropriate content
            return View("LtiLaunch", ltiRequest);
        }
    }
}