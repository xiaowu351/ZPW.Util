using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Datas.EF.Base {
    /// <summary>
    /// 值对象映射
    /// </summary>
    /// <typeparam name="TValueObject">值对象类型</typeparam>
    public abstract class ValueObjectMapBase<TValueObject> : ComplexTypeConfiguration<TValueObject>
        where TValueObject : class {

        /// <summary>
        /// 初始化值对象映射
        /// </summary>
        protected ValueObjectMapBase() {
            MapProperties();
        }

        /// <summary>
        /// 映射属性
        /// </summary>
        protected abstract void MapProperties();

    }
}
