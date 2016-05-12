using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Datas.EF {
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable {
        /// <summary>
        /// 启动
        /// </summary>
        void Start();
        /// <summary>
        /// 提交更新
        /// </summary>
        void Commit();
    }
}
