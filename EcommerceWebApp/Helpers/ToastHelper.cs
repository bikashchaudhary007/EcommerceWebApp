using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace EcommerceWebApp.Helpers
{
    public class ToastHelper
    {
        /// <summary>
        /// Registers a simple toast notification on the client.
        /// </summary>
        /// <param name="page">The current Page instance.</param>
        /// <param name="message">Text to show.</param>
        /// <param name="isError">If true, shows error style; otherwise success.</param>
        public static void RegisterToast(Page page, string message, bool isError)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));
            if (message == null) message = "";

            string bgColor = isError ? "#e74c3c" : "#2ecc71";
            string escaped = HttpUtility.JavaScriptStringEncode(message, addDoubleQuotes: true); // keeps quotes safe

            string script = $@"
                (function(){{
                    var toast = document.createElement('div');
                    toast.textContent = {escaped};
                    toast.style.position = 'fixed';
                    toast.style.bottom = '20px';
                    toast.style.right = '20px';
                    toast.style.padding = '12px 20px';
                    toast.style.background = '{bgColor}';
                    toast.style.color = 'white';
                    toast.style.borderRadius = '6px';
                    toast.style.boxShadow = '0 2px 10px rgba(0,0,0,0.2)';
                    toast.style.fontFamily = 'sans-serif';
                    toast.style.zIndex = 9999;
                    toast.style.opacity = '0';
                    toast.style.transition = 'opacity .3s ease-in-out';
                    document.body.appendChild(toast);
                    requestAnimationFrame(function(){{ toast.style.opacity = '1'; }});
                    setTimeout(function(){{ toast.style.opacity = '0'; setTimeout(function(){{ toast.remove(); }},300); }}, 3500);
                }})();";

            ScriptManager.RegisterStartupScript(page, page.GetType(), "toast_" + Guid.NewGuid(), script, true);
        }
    }
}