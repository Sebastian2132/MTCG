using SWE1HttpServer.core.Authentication;
using SWE1HttpServer.core.Request;
using SWE1HttpServer.app.DAL;
using SWE1HttpServer.app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE1HttpServer.core.Response;

namespace SWE1HttpServer
{
    class MessageIdentityProvider : IIdentityProvider
    {
        private readonly IUserRepository userRepository;

        public MessageIdentityProvider(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public IIdentity getIdentityforRequest(RequestContext request)
        {
            User currentUser = null;

            if (request.Header.TryGetValue("Authorization", out string authToken))
            {
                const string prefix = "Basic ";
                if (authToken.StartsWith(prefix))
                {
                    currentUser = userRepository.GetUserByAuthToken(authToken.Substring(prefix.Length));
                }
            }

            return currentUser;
        }
    }
}
