using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SignalR.Models.Http
{
    public static class UrlInfo
    {
        public enum UrlType
        {
            Page,
            Images,
            Zip,
            Music,
            Viode,
            None
        }
        public static string Get(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            var html = string.Empty;
            var strs = str.Split(' ').Distinct();
            foreach (var strUrl in strs)
            {
                if (IsUrl(strUrl))
                {
                    var urlType = ContentType(strUrl);
                    switch (urlType)
                    {
                        case UrlType.Page:
                            html += OfPage(strUrl, Title(strUrl));
                            break;
                        case UrlType.Music:
                            html += OfAudio(strUrl);
                            break;
                        case UrlType.Images:
                            html += OfImages(strUrl);
                            break;
                        case UrlType.Zip:
                            html += OfZip(strUrl);
                            break;
                        case UrlType.None:
                            html += OfNone(strUrl);
                            break;
                        default:
                            html += "";
                            break;
                    }
                }
            }
            return str + html;
        }


        /// <summary>
        /// 获取URL的返回类型 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static UrlType ContentType(string url)
        {
            try
            {
                UrlType result = UrlType.Page;

                if (!url.StartsWith("http://") && !url.StartsWith("https://") && !url.StartsWith("ftp://"))
                {
                    url = "http://" + url;
                }

                var http = WebRequest.Create(url);
                http.Method = "HEAD";//设置Method为HEAD

                var response = (HttpWebResponse)http.GetResponse();
                var types = response.ContentType.Split(';');
                foreach (var type in types)
                {
                    var _types = type.Split('/');
                    foreach (var item in _types)
                    {
                        switch (item)
                        {
                            case "image":
                                result = UrlType.Images;
                                break;
                            case "video":
                                result = UrlType.Viode;
                                break;
                            case "audio":
                                result = UrlType.Music;
                                break;
                            case "zip":
                                result = UrlType.Zip;
                                break;
                            case "exe":
                                result = UrlType.Zip;
                                break;
                        }
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return UrlType.None;
            }
        }

        private static string OfAudio(string url)
        {
            return string.Format("<p><embed autoplay='false' src='{0}'  width='200' height='45' /></p>", url);
        }
        private static string OfImages(string url)
        {
            return string.Format("<p><img src='{0}' alt='' class='img-thumbnail' style='max-width: 300px;'></p>", url);
        }
        private static string OfZip(string url)
        {
            return string.Format("<p><div class='well center-block' style='max-width: 400px;'><a href='{0}' target='_blank' class='btn btn-primary btn-lg btn-block' type='button'>下载文件</a></div></p>", url);
        }
        private static string OfPage(string url, string title)
        {
            var result = string.Format(@"<blockquote><p><a href='{1}' target='_blank'>{0}</a></p><footer>来自 <cite title='{1}'>{1}</cite></footer></blockquote>", title, url);

            return result;
        }

        private static string OfNone(string url)
        {
            return string.Format("<div class='alert alert-warning alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>提示</strong> 网址：{0} 无效</div>", url);
        }

        /// <summary>
        /// 判断是否URL
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static bool IsUrl(string str)
        {
            //((http|ftp|https)://)
            const string regexStr = @"(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?";
            var regex = new Regex(regexStr, RegexOptions.IgnoreCase);
            return regex.Match(str).Success;
        }

        // 获取网页的HTML内容，根据网页的charset自动判断Encoding
        private static string GetHtml(string url)
        {
            return GetHtml(url, null);
        }

        // 获取网页的HTML内容，指定Encoding
        private static string GetHtml(string url, Encoding encoding)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://") && !url.StartsWith("ftp://"))
            {
                url = "http://" + url;
            }
            var buf = new WebClient().DownloadData(url);
            if (encoding != null) return encoding.GetString(buf);
            var html = Encoding.UTF8.GetString(buf);
            encoding = GetEncoding(html);
            if (encoding == null || Equals(encoding, Encoding.UTF8)) return html;
            return encoding.GetString(buf);
        }

        // 根据网页的HTML内容提取网页的Encoding
        private static Encoding GetEncoding(string html)
        {
            const string pattern = @"(?i)\bcharset=(?<charset>[-a-zA-Z_0-9]+)";
            var charset = Regex.Match(html, pattern).Groups["charset"].Value;
            try
            {
                return Encoding.GetEncoding(charset);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        // 根据网页的HTML内容提取网页的Title
        private static string GetTitle(string html)
        {
            const string pattern = @"(?si)<title(?:\s+(?:""[^""]*""|'[^']*'|[^""'>])*)?>(?<title>.*?)</title>";
            return Regex.Match(html, pattern).Groups["title"].Value.Trim();
        }

        // 打印网页的Encoding和Title  
        private static string Title(string url)
        {
            var html = GetHtml(url);
            var query = GetTitle(html);
            var result = query.Length > 24 ? query.Substring(0, 24) + "..." : query;
            return result;
        }
    }
}