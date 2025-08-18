using System;
using System.Collections.Generic;

namespace StarShifters.Core.Runtime
{
	/// <summary>
	/// Контекст выполнения эффектов (узлов xNode): сюда кладём всё,
	/// что может менять карта или угроза: энергия, дистанция, щит и активные бафы.
	/// Важно: Это РАНТАЙМ объект, не ассет.
	/// </summary>
	[Serializable]
	public class EffectContext
	{
		// Основные параметры партии по правилам
		// (значения и лимиты инициализируются текущими параметрами матча)
		public int RemainingDistance; // "Оставшаяся дальность полёта" (HP маршрута) — старт 20 по умолчанию
		public int Fuel;              // Запас топлива (лимит ходов) — старт 10 по умолчанию
		public int Energy;            // Текущая энергия в этот ход — по умолчанию 3 на начале хода
		public int Shield;            // Очки защитного поля на этот ход (сбрасываются в начале — см. порядок хода)

		// Простейший буфер активных бафов по ключам (можно заменить на структурированный тип)
		// public Dictionary<string, int> Buffs = new();

        /// <summary>
        /// Тип ключей бафов. Используем enum, чтобы не путаться в строках.
        /// </summary>
        public enum BuffKey
        {
            EnergyPerTurn,       // +энергия в начале каждого из следующих N ходов
            MovementCardBonus,   // +к значению движения (добавляется к AddMovement)
            DefenseCardBonus     // +к значению защиты (добавляется к AddShield)
        }

        /// <summary>
        /// Один баф с длительностью.
        /// </summary>
        [Serializable]
        public class TimedBuff
        {
            public BuffKey Key;
            public int Value;         // величина бафа (может быть отрицательной для дебафа)
            public int TurnsLeft;     // сколько ходов ещё действует (уменьшается на Start/End хода)

            public TimedBuff(BuffKey key, int value, int turns)
            {
                Key = key; Value = value; TurnsLeft = Math.Max(0, turns);
            }
        }

        /// <summary>
        /// Активные бафы.
        /// </summary>
        public readonly List<TimedBuff> ActiveBuffs = new();

        /// <summary>
        /// Добавить (или «стакнуть») баф.
        /// </summary>
        public void AddTimedBuff(BuffKey key, int value, int turns)
        {
            ActiveBuffs.Add(new TimedBuff(key, value, turns));
        }

        /// <summary>
        /// Вернуть суммарное значение по ключу (на этот ход).
        /// Например, MovementCardBonus суммируется во всех активных бафах.
        /// </summary>
        public int GetBuffSum(BuffKey key)
        {
            int sum = 0;
            for (int i = 0; i < ActiveBuffs.Count; i++)
                if (ActiveBuffs[i].Key == key && ActiveBuffs[i].TurnsLeft > 0)
                    sum += ActiveBuffs[i].Value;
            return sum;
        }

        /// <summary>
        /// Вызывайте в начале хода (до начисления энергии от правил и т.п.).
        /// - Начисляет энергию от бафа EnergyPerTurn.
        /// - Уменьшает таймер у всех бафов (ходим по простому правилу: "ход начался" → осталось на 1 меньше).        
        ///  
        /// ---------- «отложить угрозы» на текущее завершение хода ----------
        /// Если >0, в конце хода угрозы не срабатывают, а счетчик уменьшается.
        /// </summary>
        /// 
        public int DelayThreatsCharges = 0; // увеличивает карта с узлом DelayThreat

        // ---------- Жизненный цикл хода ----------
        public void OnStartTurn()
        {
            // 1) Энергия от бафа
            int bonusEnergy = GetBuffSum(BuffKey.EnergyPerTurn);
            AddEnergy(bonusEnergy);

            // 2) Тикаем длительности
            for (int i = 0; i < ActiveBuffs.Count; i++)
                if (ActiveBuffs[i].TurnsLeft > 0) ActiveBuffs[i].TurnsLeft--;
        }


        // Хелпер для модификаций с безопасными границами
        public void AddEnergy(int delta) => Energy = Math.Max(0, Energy + delta);
		public void AddShield(int delta) => Shield = Math.Max(0, Shield + delta);
		public void AddDistanceDelta(int delta)
			=> RemainingDistance = Math.Max(0, RemainingDistance + delta); // delta может быть отрицательным (движение вперёд)

        // мгновенный штраф к текущей энергии (не ниже 0)
        public void ApplyEnergyPenaltyNow(int amount) => AddEnergy(-Math.Max(0, amount));

        // Удобный флаг для победы/поражения (проверяет внешняя система, но контекст может помочь)
        public bool IsCompleted => RemainingDistance <= 0;
	}
}
