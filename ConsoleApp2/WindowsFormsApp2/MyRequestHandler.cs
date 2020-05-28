﻿using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class MyRequestHandler : IRequestHandler
    {
        private string userName;
        private string password;

        public MyRequestHandler(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }


        //public static readonly string VersionNumberString = String.Format("Chromium: {0}, CEF: {1}, CefSharp: {2}",
        //   Cef.ChromiumVersion, Cef.CefVersion, Cef.CefSharpVersion);

        //bool IRequestHandler.OnBeforeBrowse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, bool isRedirect)
        //{
        //    return false;
        //}

        bool IRequestHandler.OnOpenUrlFromTab(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
        {
            return OnOpenUrlFromTab(browserControl, browser, frame, targetUrl, targetDisposition, userGesture);
        }

        protected virtual bool OnOpenUrlFromTab(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
        {
            return false;
        }

        bool IRequestHandler.OnCertificateError(IWebBrowser browserControl, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback)
        {
            //NOTE: If you do not wish to implement this method returning false is the default behaviour
            // We also suggest you explicitly Dispose of the callback as it wraps an unmanaged resource.
            //callback.Dispose();
            //return false;

            //NOTE: When executing the callback in an async fashion need to check to see if it's disposed
            //if (!callback.IsDisposed)
            //{
            //    using (callback)
            //    {
            //        //To allow certificate
            //        //callback.Continue(true);
            //        //return true;
            //    }
            //}

            return false;
        }

        void IRequestHandler.OnPluginCrashed(IWebBrowser browserControl, IBrowser browser, string pluginPath)
        {
            // TODO: Add your own code here for handling scenarios where a plugin crashed, for one reason or another.
        }

        CefReturnValue OnBeforeResourceLoad(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {
            //Example of how to set Referer
            // Same should work when setting any header

            // For this example only set Referer when using our custom scheme
            //var url = new Uri(request.Url);
            //if (url.Scheme == CefSharpSchemeHandlerFactory.SchemeName)
            //{
            //    //Referrer is now set using it's own method (was previously set in headers before)
            //    request.SetReferrer("http://google.com", ReferrerPolicy.Default);
            //}

            //Example of setting User-Agent in every request.
            //var headers = request.Headers;

            //var userAgent = headers["User-Agent"];
            //headers["User-Agent"] = userAgent + " CefSharp";

            //request.Headers = headers;

            //NOTE: If you do not wish to implement this method returning false is the default behaviour
            // We also suggest you explicitly Dispose of the callback as it wraps an unmanaged resource.
            //callback.Dispose();
            //return false;

            //NOTE: When executing the callback in an async fashion need to check to see if it's disposed
            //if (!callback.IsDisposed)
            //{
            //    using (callback)
            //    {
            //        if (request.Method == "POST")
            //        {
            //            using (var postData = request.PostData)
            //            {
            //                if (postData != null)
            //                {
            //                    var elements = postData.Elements;

            //                    var charSet = request.GetCharSet();

            //                    foreach (var element in elements)
            //                    {
            //                        if (element.Type == PostDataElementType.Bytes)
            //                        {
            //                            var body = element.GetBody(charSet);
            //                        }
            //                    }
            //                }
            //            }
            //        }

            //        //Note to Redirect simply set the request Url
            //        //if (request.Url.StartsWith("https://www.google.com", StringComparison.OrdinalIgnoreCase))
            //        //{
            //        //    request.Url = "https://github.com/";
            //        //}

            //        //Callback in async fashion
            //        //callback.Continue(true);
            //        //return CefReturnValue.ContinueAsync;
            //    }
            //}

            return CefReturnValue.Continue;
        }

        //bool IRequestHandler.GetAuthCredentials(IWebBrowser browserControl, IBrowser browser, IFrame frame, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
        //{
        //    //NOTE: If you do not wish to implement this method returning false is the default behaviour
        //    // We also suggest you explicitly Dispose of the callback as it wraps an unmanaged resource.

        //    callback.Continue(userName, password);

        //    return true;
        //}

        void IRequestHandler.OnRenderProcessTerminated(IWebBrowser browserControl, IBrowser browser, CefTerminationStatus status)
        {
            // TODO: Add your own code here for handling scenarios where the Render Process terminated for one reason or another.
            //  browserControl.Load(CefExample.RenderProcessCrashedUrl);
        }

        bool IRequestHandler.OnQuotaRequest(IWebBrowser browserControl, IBrowser browser, string originUrl, long newSize, IRequestCallback callback)
        {
            //NOTE: If you do not wish to implement this method returning false is the default behaviour
            // We also suggest you explicitly Dispose of the callback as it wraps an unmanaged resource.
            //callback.Dispose();
            //return false;

            //NOTE: When executing the callback in an async fashion need to check to see if it's disposed
            //if (!callback.IsDisposed)
            //{
            //    using (callback)
            //    {
            //        //Accept Request to raise Quota
            //        //callback.Continue(true);
            //        //return true;
            //    }
            //}

            return false;
        }


        bool OnProtocolExecution(IWebBrowser browserControl, IBrowser browser, string url)
        {
            return url.StartsWith("mailto");
        }

        void IRequestHandler.OnRenderViewReady(IWebBrowser browserControl, IBrowser browser)
        {

        }

        bool OnResourceResponse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            //NOTE: You cannot modify the response, only the request
            // You can now access the headers
            //var headers = response.ResponseHeaders;

            return false;
        }

        IResponseFilter GetResourceResponseFilter(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            //var url = new Uri(request.Url);
            //if (url.Scheme == CefSharpSchemeHandlerFactory.SchemeName)
            //{
            //    if (request.Url.Equals(CefExample.ResponseFilterTestUrl, StringComparison.OrdinalIgnoreCase))
            //    {
            //        return new FindReplaceResponseFilter("REPLACE_THIS_STRING", "This is the replaced string!");
            //    }

            //    if (request.Url.Equals("custom://cefsharp/assets/js/jquery.js", StringComparison.OrdinalIgnoreCase))
            //    {
            //        return new AppendResponseFilter(System.Environment.NewLine + "//CefSharp Appended this comment.");
            //    }

            //    return new PassThruResponseFilter();
            //}

            return null;
        }

        void OnResourceLoadComplete(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
        {

        }

        public bool OnSelectClientCertificate(IWebBrowser browserControl, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
        {
            return false;
        }

        public void OnResourceRedirect(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, ref string newUrl)
        {

        }

        bool IRequestHandler.OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
        {
            return false;
        }

        IResourceRequestHandler IRequestHandler.GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            throw new NotImplementedException();
        }

        bool IRequestHandler.GetAuthCredentials(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
        {
            callback.Continue(userName, password);

            return true;
        }

        bool IRequestHandler.OnSelectClientCertificate(IWebBrowser chromiumWebBrowser, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
        {
            throw new NotImplementedException();
        }
    }
}
