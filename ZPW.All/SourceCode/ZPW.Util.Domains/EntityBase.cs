using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZPW.Util.Domains
{
    /// <summary>
    /// 
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
