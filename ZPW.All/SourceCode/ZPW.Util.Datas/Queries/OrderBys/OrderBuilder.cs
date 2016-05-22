using System.Collections.Generic;
using System.Linq;
using ZPW.Util.Extensions;

namespace Unit.Datas.Queries.OrderBys {

    /// <summary>
    /// 排序生成器
    /// </summary>
    public  class OrderBuilder {
        /// <summary>
        /// 排序项集合
        /// </summary>
        private List<OrderByItem> Items { get; set; }

        /// <summary>
        /// 初始化排序生成器
        /// </summary>
        public OrderBuilder() {
            Items = new List<OrderByItem>();
        }

        /// <summary>
        /// 生成排序字符串
        /// </summary>
        /// <returns></returns>
        public string Generate() {
            return Items.Select(t => t.Generate()).ToList().Splice();
        }

        /// <summary>
        /// 添加排序
        /// </summary>
        /// <param name="name">排序属性</param>
        /// <param name="desc">是否降序</param>
        public void Add(string name,bool desc = false) {
            if (desc == false)
                Items.Add(new OrderByItem(name, OrderDirection.Asc));
            else
                Items.Add(new OrderByItem(name,OrderDirection.Desc));
        }
        /// <summary>
        /// 添加排序
        /// </summary>
        /// <param name="name">排序属性</param>
        /// <param name="direction">排序方向</param>
        public void Add(string name,OrderDirection direction) {
            Items.Add(new OrderByItem(name,direction));
        }
    }
}
