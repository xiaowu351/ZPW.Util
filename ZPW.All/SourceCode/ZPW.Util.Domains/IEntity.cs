using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPW.Util.Domains
{
    /// <summary>
    /// 实体
    /// </summary>
    public interface IEntity
    {
    }

    /// <summary>
    /// 实体
    /// </summary>
    /// <typeparam name="TKey">标识类型</typeparam>
    public interface IEntity<out TKey> : IEntity
    {
        /// <summary>
        /// 标识
        /// </summary>
        TKey Id { get; }
    }
}
