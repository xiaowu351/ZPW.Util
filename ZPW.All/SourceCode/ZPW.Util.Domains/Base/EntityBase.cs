using System;

namespace ZPW.Util.Domains.Base {
    /// <summary>
    /// 领域实体GUID
    /// </summary>
    public class EntityBase : EntityBase<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public EntityBase(Guid id) : base(id)
        {
        }
    }
}
