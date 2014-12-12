using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPW.Util.Extensions;

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
		}

		#endregion

		#region 字段

		/// <summary>
		/// 描述
		/// </summary>
		private StringBuilder _description;

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
	}
}
