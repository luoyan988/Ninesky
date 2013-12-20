using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace System.Web.Mvc
{
    /// <summary>
    /// 分页配置
    /// </summary>
    public class PagerConfig
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get { return (int)Math.Ceiling(TotalRecord / (double)PageSize); } }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecord { get; set; }
        /// <summary>
        /// 记录单位
        /// </summary>
        public string RecordUnit { get; set; }
        /// <summary>
        /// 记录名称
        /// </summary>
        public string RecordName { get; set; }

        public PagerConfig()
        {
            CurrentPage = 1;
            PageSize = 20;
            RecordUnit = "条";
            RecordName = "记录";
        }
    }

    /// <summary>
    /// 分页数据
    /// </summary>
    public class PagerData<T> : List<T>,IEnumerable<T>
    {
        public PagerData(List<T> list, PagerConfig pagerConfig)
        {
            this.AddRange(list);
            Config = pagerConfig;
        }
        public PagerData(List<T> list, int currentPage, int pageSize, int totalRecord)
        {
            this.AddRange(list);
            Config.CurrentPage = currentPage;
            Config.PageSize = pageSize;
            Config.TotalRecord = totalRecord;
        }
        public PagerData(List<T> list, int currentPage, int pageSize, int totalRecord, string recordUnit, string recordName)
        {
            this.AddRange(list);
            Config.CurrentPage = currentPage;
            Config.PageSize = pageSize;
            Config.TotalRecord = totalRecord;
            Config.RecordUnit = recordUnit;
            Config.RecordName = recordName;
        }
        public PagerData(IQueryable<T> list, PagerConfig pagerConfig)
        {
            this.AddRange(list);
            Config = pagerConfig;
        }
        public PagerData(IQueryable<T> list, int currentPage, int pageSize, int totalRecord)
        {
            this.AddRange(list);
            Config.CurrentPage = currentPage;
            Config.PageSize = pageSize;
            Config.TotalRecord = totalRecord;
        }
        public PagerData(IQueryable<T> list, int currentPage, int pageSize, int totalRecord, string recordUnit, string recordName)
        {
            this.AddRange(list);
            Config.CurrentPage = currentPage;
            Config.PageSize = pageSize;
            Config.TotalRecord = totalRecord;
            Config.RecordUnit = recordUnit;
            Config.RecordName = recordName;
        }

        public PagerConfig Config { get; set; }
    }
}

namespace System.Web.Mvc.Html
{
    public static class PagerExtensions
    {
        #region 普通
        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="actionName">动作名称</param>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="routeValues">路由参数</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="totalPage">总页数</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="totalRecord">总记录数</param>
        /// <param name="recordUnit">记录单位</param>
        /// <param name="recordName">记录名称</param>
        /// <param name="ctrlId">分页控件Id</param>
        /// <param name="cssClass">分页控件css类名</param>
        /// <param name="digitalLinkNum">显示的数组链接个数</param>
        /// <param name="showTotalRecord">显示总记录数</param>
        /// <param name="showCurrentPage">显示当前页</param>
        /// <param name="showTotalPage">显示总页数</param>
        /// <param name="showSelect">显示页码下拉框</param>
        /// <param name="showInput">显示页码输入框</param>
        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, RouteValueDictionary routeValues, int totalPage, int pageSize, int totalRecord, string recordUnit, string recordName, string ctrlId = "NsPager", string cssClass = "NsPager", int digitalLinkNum = 10, bool showTotalRecord = true, bool showCurrentPage = true, bool showTotalPage = true, bool showSelect = false, bool showInput = false)
        {
            int _page = 1;
            if (routeValues["page"] != null)
            {
                int.TryParse(routeValues["page"].ToString(), out _page);
            }
            PagerConfig _config = new PagerConfig { CurrentPage = _page, PageSize = pageSize, TotalRecord = totalRecord, RecordUnit = recordUnit, RecordName = recordName };
            return Pager(htmlHelper, routeValues, _config, ctrlId, cssClass, digitalLinkNum, showTotalRecord, showCurrentPage, showTotalPage, showSelect, showInput);
        }
        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="actionName">动作名称</param>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="routeValues">路由参数</param>
        /// <param name="pageConfig">分页配置</param>
        /// <param name="ctrlId">分页控件Id</param>
        /// <param name="cssClass">分页控件css类名</param>
        /// <param name="digitalLinkNum">显示的数组链接个数</param>
        /// <param name="showTotalRecord">显示总记录数</param>
        /// <param name="showCurrentPage">显示当前页</param>
        /// <param name="showTotalPage">显示总页数</param>
        /// <param name="showSelect">显示页码下拉框</param>
        /// <param name="showInput">显示页码输入框</param>
        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, RouteValueDictionary routeValues, PagerConfig pageConfig, string ctrlId = "NsPager", string cssClass = "NsPager", int digitalLinkNum = 10, bool showTotalRecord = true, bool showCurrentPage = true, bool showTotalPage = true, bool showSelect = false, bool showInput = false)
        {
            string actionName, controllerName;
            UrlHelper _url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            StringBuilder _strBuilder = new StringBuilder("<div id=\"" + ctrlId + "\" class=\"" + cssClass + "\">");
            actionName = routeValues["action"].ToString();
            controllerName = routeValues["controller"].ToString();
            if (routeValues["id"] == null) routeValues["id"] = 0;
            if (showTotalRecord) _strBuilder.Append("共" + pageConfig.TotalRecord + pageConfig.RecordUnit + pageConfig.RecordName + " ");
            if (showCurrentPage) _strBuilder.Append("每页" + pageConfig.PageSize + pageConfig.RecordUnit + " ");
            if (showTotalPage) _strBuilder.Append("第" + pageConfig.CurrentPage + "页/共" + pageConfig.TotalPage + "页 ");
            //首页链接
            if (pageConfig.CurrentPage > 1)
            {
                routeValues["page"] = 1;
                _strBuilder.Append("<a class=\"linkbtn\" href=\"" + _url.Action(actionName, controllerName, routeValues) + "\">首页</a>");
            }
            else _strBuilder.Append("<span class=\"btn\">首页</span>");
            //上一页
            if (pageConfig.CurrentPage > 1)
            {
                routeValues["page"] = pageConfig.CurrentPage - 1;
                _strBuilder.Append("<a class=\"linkbtn\" href=\"" + _url.Action(actionName, controllerName, routeValues) + "\">上一页</a>");
            }
            else _strBuilder.Append("<span class=\"btn\">上一页</span>");
            //数字导航开始
            int _startPage, _endPage;
            //总页数少于要显示的页数，页码全部显示
            if (digitalLinkNum >= pageConfig.TotalPage) { _startPage = 1; _endPage = pageConfig.TotalPage; }
            else//显示指定数量的页码
            {
                int _forward = (int)Math.Ceiling(digitalLinkNum / 2.0);
                if (pageConfig.CurrentPage > _forward)//起始页码大于1
                {
                    _endPage = pageConfig.CurrentPage + digitalLinkNum - _forward;
                    if (_endPage > pageConfig.TotalPage)//结束页码大于总页码结束页码为最后一页
                    {
                        _startPage = pageConfig.TotalPage - digitalLinkNum;
                        _endPage = pageConfig.TotalPage;

                    }
                    else _startPage = pageConfig.CurrentPage - _forward;
                }
                else//起始页码从1开始
                {
                    _startPage = 1;
                    _endPage = digitalLinkNum;
                }
            }
            //向上…
            if (_startPage > 1)
            {
                routeValues["page"] = _startPage - 1;
                _strBuilder.Append("<a class=\"linkbatch\" href=\"" + _url.Action(actionName, controllerName, routeValues) + "\">…</a>");
            }
            //数字
            for (int i = _startPage; i <= _endPage; i++)
            {
                if (i != pageConfig.CurrentPage)
                {
                    routeValues["page"] = i;
                    _strBuilder.Append("<a class=\"linknum\" href=\"" + _url.Action(actionName, controllerName, routeValues) + "\">" + i.ToString() + "</a>");
                }
                else
                {
                    _strBuilder.Append("<span class='currentnum'>" + i.ToString() + "</span>");
                }
            }
            //向下…
            if (_endPage < pageConfig.TotalPage)
            {
                routeValues["page"] = _endPage + 1;
                _strBuilder.Append("<a class=\"linkbatch\" href=\"" + _url.Action(actionName, controllerName, routeValues) + "\">…</a>");
            }
            ////数字导航结束
            //下一页和尾页
            if (pageConfig.CurrentPage < pageConfig.TotalPage)
            {
                routeValues["page"] = pageConfig.CurrentPage + 1;
                _strBuilder.Append("<a class=\"linkbtn\" href=\"" + _url.Action(actionName, controllerName, routeValues) + "\">下一页</a>");
                routeValues["page"] = pageConfig.TotalPage;
                _strBuilder.Append("<a class=\"linkbtn\" href=\"" + _url.Action(actionName, controllerName, routeValues) + "\">尾页</a>");
            }
            else _strBuilder.Append("<span class=\"btn\">下一页</span><span class=\"btn\">尾页</span>");
            //显示页码下拉框
            if (showSelect)
            {
                routeValues["page"] = "-nspageselecturl-";
                _strBuilder.Append(" 跳转到第<select id=\"nspagerselect\" data-url=\"" + _url.Action(actionName, controllerName, routeValues) + "\">");
                for (int i = 1; i <= pageConfig.TotalPage; i++)
                {
                    if (i == pageConfig.CurrentPage) _strBuilder.Append("<option selected=\"selected\" value=\"" + i + "\">" + i + "</option>");
                    else _strBuilder.Append("<option value=\"" + i + "\">" + i + "</option>");
                }
                _strBuilder.Append("</select>页");
                _strBuilder.Append("<script type=\"text/javascript\">$(\"#" + ctrlId + " #nspagerselect\").change(function () { location.href = $(\"#" + ctrlId + " #nspagerselect\").attr(\"data-url\").replace(\"-nspageselecturl-\", $(\"#" + ctrlId + " #nspagerselect\").val());});</script>");
            }
            //显示页码输入框
            if (showInput)
            {
                routeValues["page"] = "-nspagenumurl-";
                _strBuilder.Append("转到第<input id=\"nspagernum\" type=\"text\" data-url=\"" + _url.Action(actionName, controllerName, routeValues) + "\" />页");
                _strBuilder.Append("<script type=\"text/javascript\">$(\"#" + ctrlId + " #nspagernum\").keydown(function (event) {if (event.keyCode == 13) location.href = $(\"#" + ctrlId + " #nspagernum\").attr(\"data-url\").replace(\"-nspagenumurl-\", $(\"#" + ctrlId + " #nspagernum\").val()); });</script>");
            }
            _strBuilder.Append("</div>");
            return MvcHtmlString.Create(_strBuilder.ToString());
        }
        #endregion

        #region Ajax
        /// <summary>
        /// 分页控件-Ajax
        /// </summary>
        /// <param name="ctnrId">内容容器Id</param>
        /// <param name="actionName">动作名称</param>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="routeValues">路由参数</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="totalPage">总页数</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="totalRecord">总记录数</param>
        /// <param name="recordUnit">记录单位</param>
        /// <param name="recordName">记录名称</param>
        /// <param name="ctrlId">分页控件Id</param>
        /// <param name="cssClass">分页控件css类名</param>
        /// <param name="digitalLinkNum">显示的数组链接个数</param>
        /// <param name="showTotalRecord">显示总记录数</param>
        /// <param name="showCurrentPage">显示当前页</param>
        /// <param name="showTotalPage">显示总页数</param>
        /// <param name="showSelect">显示页码下拉框</param>
        /// <param name="showInput">显示页码输入框</param>
        public static MvcHtmlString PagerAjax(this HtmlHelper htmlHelper, string ctnrId, RouteValueDictionary routeValues, int TotalPage, int pageSize, int totalRecord, string recordUnit, string recordName, string ctrlId = "NsPager", string cssClass = "NsPager", int digitalLinkNum = 10, bool showTotalRecord = true, bool showCurrentPage = true, bool showTotalPage = true, bool showSelect = false, bool showInput = false)
        {
            int _page = 1;
            if (routeValues["page"] != null)
            {
                int.TryParse(routeValues["page"].ToString(), out _page);
            }
            PagerConfig _config = new PagerConfig { CurrentPage = _page, PageSize = pageSize, TotalRecord = totalRecord, RecordUnit = recordUnit, RecordName = recordName };
            return PagerAjax(htmlHelper, ctnrId, new RouteValueDictionary(routeValues), _config, ctrlId, cssClass, digitalLinkNum, showTotalRecord, showCurrentPage, showTotalPage, showSelect, showInput);
        }

        /// <summary>
        /// 分页控件-Ajax
        /// <param name="ctnrId">内容容器Id</param>
        /// <param name="actionName">动作名称</param>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="routeValues">路由参数</param>
        /// <param name="pageConfig">分页配置</param>
        /// <param name="ctrlId">分页控件Id</param>
        /// <param name="cssClass">分页控件css类名</param>
        /// <param name="digitalLinkNum">显示的数组链接个数</param>
        /// <param name="showTotalRecord">显示总记录数</param>
        /// <param name="showCurrentPage">显示当前页</param>
        /// <param name="showTotalPage">显示总页数</param>
        /// <param name="showSelect">显示页码下拉框</param>
        /// <param name="showInput">显示页码输入框</param>
        public static MvcHtmlString PagerAjax(this HtmlHelper htmlHelper, string ctnrId, RouteValueDictionary routeValues, PagerConfig pageConfig, string ctrlId = "NsPager", string cssClass = "NsPager", int digitalLinkNum = 10, bool showTotalRecord = true, bool showCurrentPage = true, bool showTotalPage = true, bool showSelect = false, bool showInput = false)
        {
            string actionName, controllerName;
            actionName = routeValues["action"].ToString();
            controllerName = routeValues["controller"].ToString();
            if (routeValues["id"] == null) routeValues["id"] = 0;
            UrlHelper _url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            StringBuilder _strBuilder = new StringBuilder("<div id=\"" + ctrlId + "\" class=\"" + cssClass + "\">");
            if (showTotalRecord) _strBuilder.Append("共" + pageConfig.TotalRecord + pageConfig.RecordUnit + pageConfig.RecordName + " ");
            if (showCurrentPage) _strBuilder.Append("每页" + pageConfig.PageSize + pageConfig.RecordUnit + " ");
            if (showTotalPage) _strBuilder.Append("第" + pageConfig.CurrentPage + "页/共" + pageConfig.TotalPage + "页 ");
            //首页链接
            if (pageConfig.CurrentPage > 1)
            {
                routeValues["page"] = 1;
                _strBuilder.Append("<a class=\"linkbtn\" href=\"" + _url.Action(routeValues["action"].ToString(), routeValues["controller"].ToString(), routeValues) + "\">首页</a>");
            }
            else _strBuilder.Append("<span class=\"btn\">首页</span>");
            //上一页
            if (pageConfig.CurrentPage > 1)
            {
                routeValues["page"] = pageConfig.CurrentPage - 1;
                _strBuilder.Append("<a class=\"linkbtn\" href=\"" + _url.Action(actionName, controllerName, routeValues) + "\">上一页</a>");
            }
            else _strBuilder.Append("<span class=\"btn\">上一页</span>");
            //数字导航开始
            int _startPage, _endPage;
            //总页数少于要显示的页数，页码全部显示
            if (digitalLinkNum >= pageConfig.TotalPage) { _startPage = 1; _endPage = pageConfig.TotalPage; }
            else//显示指定数量的页码
            {
                int _forward = (int)Math.Ceiling(digitalLinkNum / 2.0);
                if (pageConfig.CurrentPage > _forward)//起始页码大于1
                {
                    _endPage = pageConfig.CurrentPage + digitalLinkNum - _forward;
                    if (_endPage > pageConfig.TotalPage)//结束页码大于总页码结束页码为最后一页
                    {
                        _startPage = pageConfig.TotalPage - digitalLinkNum;
                        _endPage = pageConfig.TotalPage;

                    }
                    else _startPage = pageConfig.CurrentPage - _forward;
                }
                else//起始页码从1开始
                {
                    _startPage = 1;
                    _endPage = digitalLinkNum;
                }
            }
            //向上…
            if (_startPage > 1)
            {
                routeValues["page"] = _startPage - 1;
                _strBuilder.Append("<a class=\"linkbatch\" href=\"" + _url.Action(actionName, controllerName, routeValues) + "\">…</a>");
            }
            //数字
            for (int i = _startPage; i <= _endPage; i++)
            {
                if (i != pageConfig.CurrentPage)
                {
                    routeValues["page"] = i;
                    _strBuilder.Append("<a class=\"linknum\" href=\"" + _url.Action(actionName, controllerName, routeValues) + "\">" + i.ToString() + "</a>");
                }
                else
                {
                    _strBuilder.Append("<span class='currentnum'>" + i.ToString() + "</span>");
                }
            }
            //向下…
            if (_endPage < pageConfig.TotalPage)
            {
                routeValues["page"] = _endPage + 1;
                _strBuilder.Append("<a class=\"linkbatch\" href=\"" + _url.Action(actionName, controllerName, routeValues) + "\">…</a>");
            }
            ////数字导航结束
            //下一页和尾页
            if (pageConfig.CurrentPage < pageConfig.TotalPage)
            {
                routeValues["page"] = pageConfig.CurrentPage + 1;
                _strBuilder.Append("<a class=\"linkbtn\" href=\"" + _url.Action(actionName, controllerName, routeValues) + "\">下一页</a>");
                routeValues["page"] = pageConfig.TotalPage;
                _strBuilder.Append("<a class=\"linkbtn\" href=\"" + _url.Action(actionName, controllerName, routeValues) + "\">尾页</a>");
            }
            else _strBuilder.Append("<span class=\"btn\">下一页</span><span class=\"btn\">尾页</span>");
            //显示页码下拉框
            if (showSelect)
            {
                routeValues["page"] = "-nspageselecturl-";
                _strBuilder.Append(" 跳转到第<select id=\"nspagerselect\" data-url=\"" + _url.Action(actionName, controllerName, routeValues) + "\">");
                for (int i = 1; i <= pageConfig.TotalPage; i++)
                {
                    if (i == pageConfig.CurrentPage) _strBuilder.Append("<option selected=\"selected\" value=\"" + i + "\">" + i + "</option>");
                    else _strBuilder.Append("<option value=\"" + i + "\">" + i + "</option>");
                }
                _strBuilder.Append("</select>页");
                _strBuilder.Append("<script type=\"text/javascript\">$(\"#" + ctrlId + " #nspagerselect\").change(function () {$.post($(\"#" + ctrlId + " #nspagerselect\").attr(\"data-url\").replace(\"-nspageselecturl-\", $(\"#" + ctrlId + " #nspagerselect\").val()), function (data) {$(\"#" + ctnrId + "\").html(data);});});</script>");
            }
            //显示页码输入框
            if (showInput)
            {
                routeValues["page"] = "-nspagenumurl-";
                _strBuilder.Append(" 转到第<input id=\"nspagernum\" type=\"text\" data-url=\"" + _url.Action(actionName, controllerName, routeValues) + "\" />页");
                _strBuilder.Append("<script type=\"text/javascript\">$(\"#" + ctrlId + " #nspagernum\").keydown(function (event) {if (event.keyCode == 13) { $.post($(\"#" + ctrlId + " #nspagernum\").attr(\"data-url\").replace(\"-nspagenumurl-\", $(\"#" + ctrlId + " #nspagernum\").val()), function (data) {$(\"#" + ctnrId + "\").html(data);}); } });</script>");
            }
            _strBuilder.Append("<script type=\"text/javascript\">$(\"#" + ctrlId + " a\").click(function () {$.post($(this).attr(\"href\"), function (data) {$(\"#" + ctnrId + "\").html(data);});return false; });</script>");
            _strBuilder.Append("</div>");
            return MvcHtmlString.Create(_strBuilder.ToString());
        }
        #endregion
    }
}
