using System;

namespace ZPW.Util.Domains.Tests.Samples
{
    /// <summary>
    /// 测试实体
    /// </summary>
    public class Test : EntityBase
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public Test()
            : this(Guid.NewGuid())
        {
        }

        /// <summary>
        /// 初始化员工
        /// </summary>
        /// <param name="id">员工编号</param>
        public Test(Guid id)
            : base(id)
        {
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 添加描述
        /// </summary>
        protected override void AddDescriptions()
        {
            AddDescription("Id:" + Id + ",");
            AddDescription("姓名", Name);
        }
    }
}
