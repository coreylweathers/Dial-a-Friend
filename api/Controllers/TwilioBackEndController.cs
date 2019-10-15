using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio.Jwt;
using Twilio.Jwt.Client;
using Twilio.TwiML;
using Twilio.TwiML.Messaging;
using Twilio.Types;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwilioBackEndController : ControllerBase
    {
        [HttpGet("token")]
        public async Task<IActionResult> GetToken()
        {
            const string ACCOUNTSID = "<REPLACE_ACCOUNT_SID_HERE>";
            const string AUTHTOKEN = "<REPLACE_AUTH_TOKEN_HERE>";
            const string APPSID = "<REPLACE_APP_SID_HERE>";

            var scopes = new HashSet<IScope>
            {
                new OutgoingClientScope(APPSID),
                new IncomingClientScope("tester")
            };

            var capability = new ClientCapability(ACCOUNTSID, AUTHTOKEN, scopes: scopes);
            return await Task.FromResult(Content(capability.ToJwt(), "application/jwt"));
        }

        [HttpPost("voice")]
        public async Task<IActionResult> PostVoiceRequest([FromForm] string phone)
        {
            var destination = !phone.StartsWith('+') ? $"+{phone}" : phone;

            var response = new VoiceResponse();
            var dial = new Twilio.TwiML.Voice.Dial
            {
                CallerId = "<REPLACE_YOUR_TWILIO_NUMBER_HERE>"
            };
            dial.Number(new PhoneNumber(destination));

            response.Append(dial);

            return await Task.FromResult(Content(response.ToString(), "application/xml"));
        }

        [HttpPost("sms")]
        public async Task<IActionResult> PostSmsRequest()
        {
            var response = new MessagingResponse();
            var msg = new Message();
            msg.Body("SO you reached this eh... You shouldn't have");

            response.Append(msg);
            return await Task.FromResult(Content(response.ToString(), "application/xml"));
        }
    }
}