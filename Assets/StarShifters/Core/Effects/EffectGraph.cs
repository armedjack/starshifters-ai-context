#if true || UNITY_EDITOR || UNITY_STANDALONE

using UnityEngine;
using XNode;
using StarShifters.Core.Runtime;

namespace StarShifters.Effects
{
	/// <summary>
	/// Граф эффектов карты/угрозы. Хранится как ассет и исполняется в рантайме.
	/// </summary>
	[CreateAssetMenu(fileName = "EffectGraph", menuName = "StarShifters/Effects/EffectGraph")]
	public class EffectGraph : NodeGraph
	{
		/// <summary>
		/// Точка входа для выполнения графа. 
		/// Мы ищем "стартовые" ноды и запускаем их по очереди.
		/// </summary>
		public void Run(EffectContext context)
		{
			// Простейшая реализация: выполняем все ноды, помеченные как стартовые.
			// Позже можно добавить полноценную систему потоков/последовательностей.
			foreach (var node in nodes)
			{
				if (node is IEffectNode effectNode && effectNode.IsEntry)
				{
					effectNode.Execute(context);
				}
			}
		}
	}

	/// <summary>
	/// Контракт для узлов эффектов: уметь выполняться и опционально быть "входной" нодой.
	/// </summary>
	public interface IEffectNode
	{
		bool IsEntry { get; }
		void Execute(EffectContext context);
	}

	/// <summary>
	/// Базовый узел для эффектов. От него наследуются конкретные действия (Движение, Щит, Бафы).
	/// </summary>
	public abstract class EffectNodeBase : Node, IEffectNode
	{
		[Input(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Multiple)]
		public EffectNodeBase InputFlow;

		[Output(backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Multiple)]
		public EffectNodeBase OutputFlow;

		[SerializeField] private bool isEntry;
		public bool IsEntry => isEntry;

		/// <summary>
		/// Выполнение ноды. Дочерние классы реализуют конкретную логику.
		/// </summary>
		public abstract void Execute(EffectContext context);

		/// <summary>
		/// Вспомогательный метод: пройтись дальше по графу (всем выходным коннектам).
		/// </summary>
		protected void Continue(EffectContext context)
		{
			var ports = GetOutputPort(nameof(OutputFlow));
			if (ports != null)
			{
				foreach (var connection in ports.GetConnections())
				{
					if (connection.node is IEffectNode next)
						next.Execute(context);
				}
			}
		}
	}
}
#endif
