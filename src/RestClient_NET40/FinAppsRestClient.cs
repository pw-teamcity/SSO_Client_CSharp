using System;
using System.Reflection;
using FinApps.SSO.RestClient_Base;
using FinApps.SSO.RestClient_Base.Annotations;
using FinApps.SSO.RestClient_Base.Model;
using RestSharp;
using RestSharp.Validation;

namespace FinApps.SSO.RestClient_NET40
{
    [UsedImplicitly]
    public class FinAppsRestClient : IFinAppsRestClient
    {
        #region private members and constructor

        private const string ApiVersion = "1";
        private readonly IGenericRestClient _genericRestClient;

        public FinAppsRestClient(string baseUrl, string companyIdentifier, string companyToken)
            : this(new GenericRestClient(string.Format("{0}v{1}/", baseUrl, ApiVersion)), companyIdentifier, companyToken)
        {
        }

        public FinAppsRestClient(IGenericRestClient genericRestClient, string companyIdentifier, string companyToken)
        {
            CompanyIdentifier = companyIdentifier;
            CompanyToken = companyToken;
            _genericRestClient = genericRestClient;
        }

        private static string AssemblyVersion
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                var assemblyName = new AssemblyName(assembly.FullName);
                return assemblyName.Version.ToString();
            }
        }

        private static string UserAgent
        {
            get { return string.Format("finapps-csharp/{0} (.NET {1})", AssemblyVersion, Environment.Version); }
        }

        private static string CompanyIdentifier { get; set; }

        private static string CompanyToken { get; set; }

        private static string FinAppsToken
        {
            get { return string.Format("{0}:{1}", CompanyIdentifier, CompanyToken); }
        }

        private static RestRequest CreateRestRequest(Method method, string resource)
        {
            var request = new RestRequest(method)
            {
                Resource = resource,
                Timeout = TimeSpan.FromSeconds(60.0).Milliseconds
            };
            
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Accept-charset", "utf-8");
            request.AddHeader("User-Agent", UserAgent);
            request.AddHeader("X-FinApps-Token", FinAppsToken);

            return request;
        }

        #endregion

        public FinAppsUser NewUser(FinAppsUser finAppsUser)
        {
            Require.Argument("Email", finAppsUser.Email);
            Require.Argument("Password", finAppsUser.Password);
            Require.Argument("PostalCode", finAppsUser.PostalCode);

            RestRequest request = CreateRestRequest(Method.POST, ApiUris.NewUser);
            request.AddParameter("Email", finAppsUser.Email);
            request.AddParameter("Password", finAppsUser.Password);
            request.AddParameter("PostalCode", finAppsUser.PostalCode);
            if (!string.IsNullOrWhiteSpace(finAppsUser.FirstName)) 
                request.AddParameter("FirstName", finAppsUser.FirstName);
            if (!string.IsNullOrWhiteSpace(finAppsUser.LastName)) 
                request.AddParameter("LastName", finAppsUser.LastName);

            return _genericRestClient.Execute<FinAppsUser>(request);
        }

        public FinAppsUser NewSession(FinAppsCredentials credentials, string clientIp)
        {
            RestRequest request = CreateRestRequest(Method.POST, ApiUris.NewSession);        
            if (string.IsNullOrWhiteSpace(clientIp))
                request.AddParameter("ClientIp", clientIp);

            return _genericRestClient.Execute<FinAppsUser>(request, credentials.Email, credentials.FinAppsUserToken);
        }

        public FinAppsUser UpdateUserProfile(FinAppsCredentials credentials, FinAppsUser finAppsUser)
        {
            Require.Argument("Email", finAppsUser.Email);
            Require.Argument("PostalCode", finAppsUser.PostalCode);

            RestRequest request = CreateRestRequest(Method.PUT, ApiUris.UpdateUserProfile);

            request.AddParameter("Email", finAppsUser.Email);
            request.AddParameter("PostalCode", finAppsUser.PostalCode);
            if (!string.IsNullOrWhiteSpace(finAppsUser.FirstName))
                request.AddParameter("FirstName", finAppsUser.FirstName);
            if (!string.IsNullOrWhiteSpace(finAppsUser.LastName))
                request.AddParameter("LastName", finAppsUser.LastName);

            return _genericRestClient.Execute<FinAppsUser>(request, credentials.Email, credentials.FinAppsUserToken);
        }

        public FinAppsUser UpdateUserPassword(FinAppsCredentials credentials, string oldPassword,
            string newPassword)
        {
            Require.Argument("OldPassword", oldPassword);
            Require.Argument("NewPassword", newPassword);

            RestRequest request = CreateRestRequest(Method.PUT, ApiUris.UpdateUserPassword);
            request.AddParameter("OldPassword", oldPassword);
            request.AddParameter("NewPassword", newPassword);

            return _genericRestClient.Execute<FinAppsUser>(request, credentials.Email, credentials.FinAppsUserToken);
        }

        public FinAppsUser DeleteUser(FinAppsCredentials credentials)
        {
            RestRequest request = CreateRestRequest(Method.DELETE, ApiUris.DeleteUser);
            return _genericRestClient.Execute<FinAppsUser>(request, credentials.Email, credentials.FinAppsUserToken);
        }
    }
}