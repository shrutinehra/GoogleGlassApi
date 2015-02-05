using System;
using DotNetOpenAuth.OAuth2;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;

/// <summary>
/// Static class holding OAuth 2.0 configuration constants.
/// </summary>
public static class Common
{
    /// The OAuth2.0 Client ID of your project.
    /// </summary>
    public static readonly string ClientID = "611599406119-4fbdib53lrtjojc9fdsjvfi29797nkkh.apps.googleusercontent.com";

    /// <summary>
    /// The OAuth2.0 Client secret of your project.
    /// </summary>
    public static readonly string ClientSecret = "wLhuTVnCPd_X570EOjKar8N-";

    /// <summary>
    /// The OAuth2.0 scopes required by your project.
    /// </summary>
    public static readonly string[] SCOPES = new String[]
        {
            "https://www.googleapis.com/auth/glass.timeline",
            "https://www.googleapis.com/auth/glass.location",
            "https://www.googleapis.com/auth/userinfo.profile"
        };

    /// <summary>
    /// The Redirect URI of your project.
    /// </summary>
    public static readonly string RedirectUri = "http://localhost:54749/SUSGoogleApi/OAuth2Callback.aspx";
    public static NativeApplicationClient GetProvider()
    {
        var provider =
            new NativeApplicationClient(
                GoogleAuthenticationServer.Description, Common.ClientID, Common.ClientSecret);
        return provider;
    }
   
}