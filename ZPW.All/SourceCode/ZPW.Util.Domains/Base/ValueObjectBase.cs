using System;
using System.Linq;

namespace ZPW.Util.Domains.Base {

    /// <summary>
    /// 领域对象
    /// </summary>
    /// <typeparam name="TValueObject"></typeparam>
    public class ValueObjectBase<TValueObject> : DomainBase, IEquatable<TValueObject>
        where TValueObject : ValueObjectBase<TValueObject>
    {
        #region Equals接口实现和重写
        /// <summary>
        /// 相等性比较
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TValueObject other)
        {
            return this == other;
        }

        /// <summary>
        /// 相等性比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as TValueObject);
        }
        #endregion

        #region 运算符重载
        /// <summary>
        /// == 运算符重载
        /// </summary>
        /// <param name="valueObject1"></param>
        /// <param name="valueObject2"></param>
        /// <returns></returns>
        public static bool operator ==(ValueObjectBase<TValueObject> valueObject1, ValueObjectBase<TValueObject> valueObject2)
        {
            if ((object)valueObject1 == null && (object)valueObject2 == null)
                return true;
            if ((object)valueObject1 == null || (object)valueObject2 == null)
                return false;
            if (valueObject1.GetType() != valueObject2.GetType())
                return false;
            var properties = valueObject1.GetType().GetProperties();
            return properties.All(property => property.GetValue(valueObject1,null) == property.GetValue(valueObject2,null));
        }

        /// <summary>
        /// ！= 运算符重载
        /// </summary>
        /// <param name="valueObject1"></param>
        /// <param name="valueObject2"></param>
        /// <returns></returns>
        public static bool operator !=(ValueObjectBase<TValueObject> valueObject1, ValueObjectBase<TValueObject> valueObject2)
        {
            return !(valueObject1 == valueObject2);
        }
        #endregion

        #region GetHashCode（获取哈希）
        public override int GetHashCode()
        {
            var properties = GetType().GetProperties();
            return properties.Select(property => property.GetValue(this,null))
                      .Where(value => value != null)
                      .Aggregate(0, (current, value) => current ^ value.GetHashCode());
        }
        #endregion

        public virtual TValueObject Clone()
        {
            return (TValueObject)MemberwiseClone();
        }
        
    }
}
