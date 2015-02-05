using System;
using System.Web;
using DotNetOpenAuth.OAuth2;

public partial class Default : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
    Run();
    }
  
    private  void Run()
    {

        var authorizationState = new AuthorizationState(Common.SCOPES) {Callback = new Uri(Common.RedirectUri)};
        var builder =
                new UriBuilder(Common.GetProvider().RequestUserAuthorization(authorizationState));
        var queryParameters = HttpUtility.ParseQueryString(builder.Query);

        queryParameters.Set("access_type", "offline");
        queryParameters.Set("approval_prompt", "force");

        builder.Query = queryParameters.ToString();
        HttpContext.Current.Response.Redirect(builder.Uri.ToString());
    }
}