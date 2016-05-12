﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ZPW.Util.Domains.Factory;
using ZPW.Util.Domains.ValidationHandlers;
using ZPW.Util.Domains.ValidationHandlers.Impl;
using ZPW.Util.Extensions;
using ZPW.Util.Validations;

namespace ZPW.Util.Domains
{
    /// <summary>
    /// 领域实体
    /// </summary>
    /// <typeparam name="TKey">标识类型</typeparam>
    public abstract class EntityBase<TKey> : DomainBase
    {

        #region 构造方法

        /// <summary>
        /// 初始化领域实体
        /// </summary>
        /// <param name="id">标识</param>
        protected EntityBase(TKey id)
        {
            Id = id;
        }

        #endregion



        #region Id(标识)

        /// <summary>
        /// 标识
        /// </summary>
        [Required]
        public TKey Id { get; private set; }

        #endregion

        #region Equals(相等运算)

        /// <summary>
        /// 相等运算
        /// </summary>
        public override bool Equals(object entity)
        {
            if (entity == null)
                return false;
            if (!(entity is EntityBase<TKey>))
                return false;
            return this == (EntityBase<TKey>)entity;
        }

        #endregion

        #region GetHashCode(获取哈希)

        /// <summary>
        /// 获取哈希
        /// </summary>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

        #region ==(相等比较)

        /// <summary>
        /// 相等比较
        /// </summary>
        /// <param name="entity1">领域实体1</param>
        /// <param name="entity2">领域实体2</param>
        public static bool operator ==(EntityBase<TKey> entity1, EntityBase<TKey> entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
                return true;
            if ((object)entity1 == null || (object)entity2 == null)
                return false;
            if (entity1.Id == null)
                return false;
            if (entity1.Id.Equals(default(TKey)))
                return false;
            return entity1.Id.Equals(entity2.Id);
        }

        #endregion

        #region !=(不相等比较)

        /// <summary>
        /// 不相等比较
        /// </summary>
        /// <param name="entity1">领域实体1</param>
        /// <param name="entity2">领域实体2</param>
        public static bool operator !=(EntityBase<TKey> entity1, EntityBase<TKey> entity2)
        {
            return !(entity1 == entity2);
        }

        #endregion
 
    }
}
