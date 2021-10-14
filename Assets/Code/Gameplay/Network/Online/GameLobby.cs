using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Json.Net;
using System.Security.Cryptography;
using ThirdParty.BouncyCastle.Math;

using WebSocketSharp;
using WebSocketSharp.Server;
using WebSocketSharp.Net;

using UnityEngine;
using System.IO;

namespace Gameplay
{
    public class GameLobby : WebSocketBehavior
    {
        public NetworkManager Manager { get; private set; }

        public GameLobby(NetworkManager netManager)
        {
            OriginValidator = origin =>
            {
                return origin == "http://sharp.com";
            };

            CookiesValidator = (req, res) =>
            {
                bool hasAuthToken = false;
                foreach (Cookie cookie in req)
                {
                    if (cookie.Name == "AuthToken")
                    {
                        hasAuthToken = true;
                    }
                }
                if (hasAuthToken == false)
                {
                    res.Add(new Cookie("AuthToken", ""));
                }
                return hasAuthToken;
            };

            Manager = netManager;
        }

        private static byte[] Base64UrlDecode(string input)
        {
            var output = input;
            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding
            switch (output.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 1: output += "==="; break; // Three pad chars
                case 2: output += "=="; break; // Two pad chars
                case 3: output += "="; break; // One pad char
                default: throw new System.Exception("Illegal base64url string!");
            }
            var converted = Convert.FromBase64String(output); // Standard base64 decoder
            return converted;
        }

        private bool TryValidateToken(string token, out TokenPayload payload)
        {
            payload = null;
            string[] parts = token.Split('.');
            if (parts.Length != 3)
            {
                return false;
            }

            TokenHeader header = JsonNet.Deserialize<TokenHeader>(Encoding.UTF8.GetString(Base64UrlDecode(parts[0])));
            payload = JsonNet.Deserialize<TokenPayload>(Encoding.UTF8.GetString(Base64UrlDecode(parts[1])));

            string region = "";
            string userPoolId = "";
            System.Net.WebRequest req = System.Net.WebRequest.Create($"https://cognito-idp.{region}.amazonaws.com/{userPoolId}/.well-known/jwks.json");
            System.Net.WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            JsonWebTokenKeyCollection collection = JsonNet.Deserialize<JsonWebTokenKeyCollection>(Out);
            JsonWebTokenKey key = collection.GetKey(header.kid);

            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.ImportParameters(new RSAParameters
            {
                Exponent = new BigInteger(Base64UrlDecode(key.e)).ToByteArrayUnsigned(),
                Modulus = new BigInteger(Base64UrlDecode(key.n)).ToByteArrayUnsigned()
            });
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(parts[0] + "." + parts[1]));

            RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(provider);
            rsaDeformatter.SetHashAlgorithm("SHA256");
            if (rsaDeformatter.VerifySignature(hash, Base64UrlDecode(parts[2])) == false)
            {
                return false;
            }

            payload = JsonNet.Deserialize<TokenPayload>(Encoding.UTF8.GetString(Base64UrlDecode(parts[1])));
            return true;
        }

        /*var jwks = { 
            "keys":[
            { "alg":"RS256",
                "e":"AQAB",
                "kid":"vpEnrx3qj3YmyVkwCLXFlxMlgafrfdEN1P5eMxcTBDI = ",
                "kty":"RSA",
                "n":"l_Hup2MJV6P - xR6n646Q07TqZ7JwJnNvhFQYO6XhnbAaU_Z - QQuIP0yo9EW4MHmB - U0KCib2mC57Q - UxWoiGmPL8qz6Z5o9fHAf92vZwyOj2Ga2pUlunsz4KTvvPtl7G7o89EQiPnwXOStCOg6oC3YdwuZsutpykJIUVA64g7nnmL0y4SOfeROXbY__kdfd - vF6oq6o_4G31ZIYw2edK9Ozx_7FT_3UIH6QCrdqc - YaWWCfsf7RirtnIpaiSEDxwcfZ6AEGtjp9MdIhs7CkMdff5gvaEByqP4g5meyc7fxw558hVxKEUWUVbBqPwQwZuhbD_Q3pYIShu1ciZjj8exQ",
                "use":"sig"
                },
            { "alg":"RS256",
                "e":"AQAB",
                "kid":"8fZHgeyypqM4CopdvKVP7XzSH9DFr / g54Eozv6QxLCs = ",
                "kty":"RSA",
                "n":"kl2_z3IVg1VQLmfL7bX - DGKpNlcfTFAUWhb5r0MBGq7Qka0y7qpuDdFIY - zRrRCVQzoXhuStzncdfjultIVKJ3wU84T5lw6XapO98yVBAnYi9EdRnv3P02LNHzOFLGfJdGPEag658yuereMp05I1eld8pc7rVyHD82IQNsJbAM5QRc6D7CH - VD4QtNbKKM - eyjqdFhu3_2WHdYXZHfdpDG0MhU_yD2cEMk1jBVuOoX1OY0lolLfE200QR5 - OPieG8lgVQAVnUv1zETboeeVdrM3muxTgMzoZYtYmbC9 - LgQIFjPFfZ4kJ - m1erDiZh6VKu0LAIYNUvsP0He7XaSpfw",
                "use":"sig"
                }]
        };*/
        protected override void OnOpen()
        {
            bool hasAuthToken = false;
            TokenPayload payload = null;

            foreach (Cookie cook in Context.CookieCollection)
            {
                if (cook.Name == "AuthToken")
                {
                    if (TryValidateToken(cook.Value, out payload))
                    {
                        Debug.Log(payload.sub);
                    }
                    hasAuthToken = true;
                }
            }

            if (hasAuthToken == false)
            {
                Context.WebSocket.CloseAsync(CloseStatusCode.InvalidData, "Client did not provide AuthToken");
                return;
            }

            Context.WebSocket.OnMessage += (sender, e) =>
            {
                Debug.Log("[Server] Message received");
            };

            OnlineConnector user = new OnlineConnector(Context.WebSocket, payload.sub, new Logger("blue"));

            Manager.IncomingConnection(user);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Debug.Log("[Server] Connection closed");
        }

        protected override void OnError(WebSocketSharp.ErrorEventArgs e)
        {
            Debug.Log("[Server] Error");
            Debug.Log(e.Exception.ToString());
        }
    }
}
