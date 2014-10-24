using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;

namespace WalletInspector2.WebUI.Code
{
    public static class Extensions
    {
        public static Guid Id(this IPrincipal principal)
        {
            if(principal != null)
            {
                var userId = principal.Identity.GetUserId();

                return userId == null ? Guid.Empty : new Guid(userId);
            }

            return Guid.Empty;
        }
    }
}