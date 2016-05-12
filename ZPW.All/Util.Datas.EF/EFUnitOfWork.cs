using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Datas.EF.Exceptions;
using ZPW.Util.Exceptions;

namespace Util.Datas.EF {
    /// <summary>
    /// entity Framework 工作单元
    /// </summary>
    public abstract class EFUnitOfWork : DbContext, IUnitOfWork {

        protected EFUnitOfWork(string connectionName) : base(connectionName) {
            TraceId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 启动标志
        /// </summary>
        private bool IsStart { get; set; }

        /// <summary>
        /// 跟踪号
        /// </summary>
        public string TraceId { get; private set; }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start() {
            IsStart = true;
        }

        /// <summary>
        /// 提交更新
        /// </summary>
        public void Commit() {
            try {
                SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex) {

                throw new ConcurrencyException(ex);
            }
            catch (DbEntityValidationException ex) {
                throw new EfValidationException(ex);
            }
            finally {
                IsStart = false;
            }
        }

        /// <summary>
        /// 通过启动标识执行提交，如果已启动，则不提交
        /// </summary>
        internal void CommitByStart() {
            if (IsStart)
                return;
            Commit();
        }
    }
}
