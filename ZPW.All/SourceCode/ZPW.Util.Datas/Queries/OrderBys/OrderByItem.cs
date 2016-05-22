using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit.Datas.Queries.OrderBys {
    /// <summary>
    /// 排序项
    /// </summary>
   public  class OrderByItem {

        /// <summary>
        /// 排序属性
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 排序方向
        /// </summary>
        public OrderDirection Direction { get; private set; }

        /// <summary>
        /// 初始化排序
        /// </summary>
        /// <param name="name">排序属性</param>
        /// <param name="direction">排序方向</param>
        public OrderByItem(string name,OrderDirection direction) {
            this.Name = name;
            this.Direction = direction;
        }

        /// <summary>
        /// 创建排序字符串
        /// </summary>
        /// <returns></returns>
        public string Generate() {
            if (Direction == OrderDirection.Asc)
                return Name;
            return string.Format(" {0} DESC",Name);
        }
    }
}
