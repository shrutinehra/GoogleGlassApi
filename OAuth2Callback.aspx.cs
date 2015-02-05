using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using DotNetOpenAuth.OAuth2;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Mirror.v1;
using Google.Apis.Mirror.v1.Data;
using Google.Apis.Oauth2.v2;
using Google.Apis.Services;
using Google.Apis;

public partial class OAuth2Callback : System.Web.UI.Page
{
    List<string> _timeLineId= new List<string>(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        GetData();
    }

    protected void GetData()
    {
        var provider = Common.GetProvider();
        var authorizationState = new AuthorizationState(Common.SCOPES) {Callback = new Uri(Common.RedirectUri)};
        String code = Request.QueryString["code"];
        var state = provider.ProcessUserAuthorization(code, authorizationState);
      
        var initializer = new BaseClientService.Initializer
        {
            Authenticator = Utils.GetAuthenticatorFromState(state)
        };

        var userService = new Oauth2Service(initializer);
        String userId = userService.Userinfo.Get().Fetch().Id;

        Utils.StoreCredentials(userId, state);
        HttpContext.Current.Session["UserId"] = userId;

       TimeLineUser(new MirrorService(initializer));
       //DeleteTimeLineItem(new MirrorService(initializer));


    }
  
    private void TimeLineUser(MirrorService mirrorService)
    {
        var item = new TimelineItem
        {
            Text = "Hi There",
            Notification = new NotificationConfig
            {
                Level = "DEFAULT"
            }
        };
        mirrorService.Timeline.Insert(item).Fetch();
     
    }

    private void DeleteTimeLineItem(MirrorService mirrorService)
    {
       
        var listRequest = mirrorService.Timeline.List();
        TimelineListResponse items = listRequest.Fetch();
        foreach (var deleteItem in items.Items)
        {
            mirrorService.Timeline.Delete(deleteItem.Id).Fetch();
        }
       
    }
}