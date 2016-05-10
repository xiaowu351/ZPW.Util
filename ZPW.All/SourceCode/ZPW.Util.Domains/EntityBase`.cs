using System.Collections.Generic;
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
    public abstract class EntityBase<TKey>
	{

		#region 构造方法

		/// <summary>
		/// 初始化领域实体
		/// </summary>
		/// <param name="id">标识</param>
		protected EntityBase(TKey id)
		{
			Id = id;
            _handler = new ValidationHandler();

        }

		#endregion

		#region 字段

		/// <summary>
		/// 描述
		/// </summary>
		private StringBuilder _description;

        /// <summary>
        /// 验证规则集合
        /// </summary>
        private readonly List<IValidationRule> _rules;

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

		#region ToString(输出领域对象的状态)

		/// <summary>
		/// 输出领域对象的状态
		/// </summary>
		public override string ToString()
		{
			_description = new StringBuilder();
			AddDescriptions();
			return _description.ToString().TrimEnd().TrimEnd(',');
		}

		/// <summary>
		/// 添加描述
		/// </summary>
		protected virtual void AddDescriptions()
		{
		}

		/// <summary>
		/// 添加描述
		/// </summary>
		protected void AddDescription(string description)
		{
			if (string.IsNullOrWhiteSpace(description))
				return;
			_description.Append(description);
		}

		/// <summary>
		/// 添加描述
		/// </summary>
		protected void AddDescription<T>(string name, T value)
		{
			if (string.IsNullOrWhiteSpace(value.ToStr()))
				return;
			_description.AppendFormat("{0}:{1},", name, value);
		}

        #endregion

        #region ValidationHandler
        /// <summary>
        /// 验证处理器
        /// </summary>
        private IValidationHandler _handler;

        /// <summary>
        /// 设置验证处理器
        /// </summary>
        /// <param name="handler">验证处理器</param>
        public void SetValidationHandler(IValidationHandler handler)
        {
            if (handler == null)
                return;
            _handler = handler;
        }
        #endregion

        #region Validate

        /// <summary>
        /// 添加验证规则
        /// </summary>
        /// <param name="rule">验证规则</param>
        public void AddValidationRule(IValidationRule rule)
        {
            if (rule == null)
                return;
            _rules.Add(rule);
        }

        /// <summary>
        /// 验证
        /// </summary>
        public virtual void Validate()
        {
            var results = GetValidationResult(); 
            _handler.Handle(results);
        }


        /// <summary>
        /// 获取验证结果
        /// </summary>
        private ValidationResultCollection GetValidationResult()
        {
            var result = ValidationFactory.Create().Validate(this);
            Validate(result);
            foreach (var rule in _rules)
                result.Add(rule.Validate());
            return result;
        }

        /// <summary>
        /// 验证并添加到验证结果集合
        /// </summary>
        /// <param name="results">验证结果集合</param>
        protected virtual void Validate(ValidationResultCollection results)
        {
        }
        #endregion
    }
}
