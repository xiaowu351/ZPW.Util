using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPW.Util.Domains.Repositories {
    /// <summary>
    /// 分页
    /// </summary>
    public class Pager : IPager {

        /// <summary>
        /// 初始化页
        /// </summary>
        public Pager() : this(1) { }
        /// <summary>
        /// 初始化页
        /// </summary>
        /// <param name="page">页索引</param>
        /// <param name="pageSize">每页显示行数，默认20</param>
        /// <param name="totalCount">总行数</param>
        public Pager(int page, int pageSize = 20, int totalCount = 0) {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        private int _pageIndex;

        /// <summary>
        /// 页索引
        /// </summary>
        public int Page {
            get {
                if (_pageIndex <= 0) {
                    _pageIndex = 1;
                }
                return _pageIndex;
            }
            set {
                _pageIndex = value;
            }
        }

        /// <summary>
        /// 每页显示行数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount {
            get {
                if (TotalCount == 0)
                    return 0;
                if (TotalCount % PageSize == 0)
                    return TotalCount / PageSize;
                return TotalCount / PageSize + 1;
            } 
        }

        /// <summary>
        /// 跳过的行数
        /// </summary>
        public int SkipCount {
            get {
                if (Page > PageCount)
                    Page = PageCount;
                return PageSize * (Page - 1);
            } 
        }

        /// <summary>
        /// 排序条件
        /// </summary>
        public string Order { get; set; }

    }


}
