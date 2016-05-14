using System.Data.Entity.ModelConfiguration;
using ZPW.Util.Domains;

namespace Util.Datas.EF.Base {
    /// <summary>
    /// 实体映射
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EntityMapBase<TEntity> : EntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity {

        /// <summary>
        /// 初始化映射
        /// </summary>
        protected EntityMapBase() {
            MapTable();
            MapId();
            MapProperties();
            MapAssociations();
        }

        /// <summary>
        /// 映射表
        /// </summary>
        protected abstract void MapTable();

        /// <summary>
        /// 映射标识
        /// </summary>
        protected abstract void MapId();

        /// <summary>
        /// 映射属性
        /// </summary>
        protected virtual void MapProperties() { }

        /// <summary>
        /// 映射导航属性
        /// </summary>
        protected virtual void MapAssociations() { }
    }
}
