using StarShifters.Core.Data;
using StarShifters.Core.Runtime;        // EffectContext
using StarShifters.Effects;

namespace StarShifters.Core.Integration.CardHouse

{
    /// <summary>
    /// Рантайм-представление карты: ссылка на CardDef + быстрый доступ к графу и стоимости.
    /// Это то, что лежит в колоде/руке и разыгрывается.
    /// </summary>
    public class CardRuntime
    {
        public readonly CardDef Def;

        public CardRuntime(CardDef def) { Def = def; }

        public int EnergyCost => Def.EnergyCost;
        public CardType Type => Def.Type;
        public EffectGraph EffectGraph => Def.EffectGraph;

        /// <summary>
        /// Можно ли сыграть карту при текущей энергии?
        /// </summary>
        /// 
        public bool CanPlay(EffectContext context) => context != null && context.Energy >= EnergyCost;

        /// <summary>
        /// Пытается разыграть карту: проверяет энергию, списывает её и запускает граф эффектов.
        /// Возвращает true/false и причину отказа (если есть).
        /// </summary>
        public bool TryPlay(EffectContext context, out string failReason)
        {
            failReason = null;

            if (context == null)
            {
                failReason = "Нет контекста хода.";
                return false;
            }

            if (!CanPlay(context))
            {
                failReason = $"Недостаточно энергии: нужно {EnergyCost}, доступно {context.Energy}.";
                return false;
            }

            // 1) списываем энергию (стоимость из CardDef)
            context.AddEnergy(-EnergyCost);

            // 2) выполняем эффекты через xNode-граф
            EffectGraph?.Run(context);
            return true;
        }

        /*public void Play(EffectContext context)
        {
            // Списание энергии — по вашим правилам карта стоит 1..3 (мы это валидируем в SO)
            context.AddEnergy(-EnergyCost);

            // Запуск графа эффектов
            EffectGraph?.Run(context);
        }*/
    }
}
