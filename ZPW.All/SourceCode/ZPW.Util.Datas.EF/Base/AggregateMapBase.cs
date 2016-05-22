using System.ComponentModel.DataAnnotations.Schema;
using ZPW.Util.Domains;

namespace Util.Datas.EF.Base {

    /// <summary>
    /// 聚合根映射
    /// </summary>
    /// <typeparam name="TEntity">聚合根类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public abstract class AggregateMapBase<TEntity, TKey> : EntityMapBase<TEntity>
         where TEntity : AggregateRoot<TKey> {

        /// <summary>
        /// 映射标识
        /// </summary>
        protected override void MapId() {
            HasKey(t => t.Id);
        }
        /// <summary>
        /// 映射属性
        /// </summary>
        protected override void MapProperties() {
             
            Property(t => t.Version).HasColumnName("Version").IsRowVersion().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed).IsOptional(); 
        }
    }
}
