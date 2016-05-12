using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPW.Util.Validations;

namespace ZPW.Util.Domains {
    /// <summary>
    /// 聚合根
    /// </summary>
    /// <typeparam name="TKey">标识类型</typeparam>
    public abstract class AggregateRoot<TKey> : EntityBase<TKey>, IAggregateRoot<TKey> {

        /// <summary>
        /// 初始化聚合根
        /// </summary>
        /// <param name="id"></param>
        protected AggregateRoot(TKey id) : base(id) {

        }

        /// <summary>
        /// 版本号（乐观锁）
        /// </summary>
        public byte[] Version { get; set; }
 
    }

    /// <summary>
    /// 聚合根
    /// </summary>
    public abstract class AggregateRoot : AggregateRoot<Guid> {
        
        /// <summary>
        /// 初始化聚合根
        /// </summary>
        /// <param name="id"></param>
        public AggregateRoot(Guid id) : base(id) {
        }

        protected override void Validate(ValidationResultCollection results) {
            if(Id == Guid.Empty) {
                results.Add(new ValidationResult("Id不能为空"));
            }
        }
    }
}
