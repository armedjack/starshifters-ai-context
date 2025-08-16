using System.Collections.Generic;
using UnityEngine;
using StarShifters.Core.Runtime;
using StarShifters.Core.Data;

namespace StarShifters.Core.Integration.Threats
{
    /// <summary>
    /// Менеджер угроз: управляет пулом, выкладкой на стол и разрешением.
    /// В начале хода выкладывает 1–3 угрозы (по правилам), в конце — срабатывают эффекты.
    /// </summary>
    public class ThreatController : MonoBehaviour
    {
        [Header("Исходный пул угроз (пресет)")]
        public ThreatPreset ThreatPreset;

        [Header("Сколько угроз выкладывать в начале хода")]
        [Min(1)] public int MinPerTurn = 1;
        [Min(1)] public int MaxPerTurn = 3;

        // Рантайм-структуры
        private readonly List<ThreatRuntime> _drawPile = new();
        private readonly List<ThreatRuntime> _activeBoard = new(); // угрозы текущего хода (на столе)
        private readonly List<ThreatRuntime> _discardPile = new();

        private bool _initialized;

        // --------- Инициализация пула ---------
        public void Initialize()
        {
            _drawPile.Clear(); _discardPile.Clear(); _activeBoard.Clear();
            if (ThreatPreset == null)
            {
                Debug.LogWarning("[ThreatController] ThreatPreset не назначен — пул пуст.");
                _initialized = false; return;
            }

            var list = ThreatPreset.BuildList();
            if (list == null || list.Count == 0)
            {
                Debug.LogWarning("[ThreatController] ThreatPreset пуст — пул пуст.");
                _initialized = false; return;
            }

            foreach (var def in list)
                if (def != null)
                    _drawPile.Add(new ThreatRuntime(def));

            Shuffle(_drawPile);
            _initialized = true;
            Debug.Log($"[ThreatController] Initialize OK: Draw={_drawPile.Count}");
        }

        private void EnsureInit()
        {
            if (!_initialized) Initialize();
        }

        // --------- БОЕВАЯ ЛОГИКА ---------
        /// <summary>
        /// Выложить угрозы на стол в начале хода (п.3 правил).
        /// Выбираем случайное число N в диапазоне [MinPerTurn..MaxPerTurn].
        /// </summary>
        public void SpawnThreatsForTurn()
        {
            EnsureInit();

            // Очистим стол на всякий случай (на концепт: угрозы живут ровно один ход)
            //_activeBoard.Clear();

            int n = Random.Range(MinPerTurn, MaxPerTurn + 1);
            for (int i = 0; i < n; i++)
            {
                if (_drawPile.Count == 0)
                {
                    if (_discardPile.Count == 0) break; // вообще нечего брать
                    _drawPile.AddRange(_discardPile);
                    _discardPile.Clear();
                    Shuffle(_drawPile);
                }

                var thr = _drawPile[_drawPile.Count - 1];
                _drawPile.RemoveAt(_drawPile.Count - 1);
                _activeBoard.Add(thr);
            }

            Debug.Log($"[ThreatController] Выложено угроз: {_activeBoard.Count} (запрошено {n}).");
        }

        /// <summary>
        /// Разрешить угрозы в конце хода (п.9 правил): применить их эффекты и отправить в сброс.
        /// </summary>
        public void ResolveActiveThreats(EffectContext ctx)
        {
            for (int i = 0; i < _activeBoard.Count; i++)
            {
                var thr = _activeBoard[i];
                thr.Resolve(ctx);
            }
            // После срабатывания отправляем все на сброс
            _discardPile.AddRange(_activeBoard);
            _activeBoard.Clear();
        }

        /// <summary>Пропустить срабатывание угроз в этот конец хода (DelayThreats): оставить на столе.</summary>
        public void SkipResolveThisTurn()
        {
            // Ничего не делаем: угрозы остаются лежать на столе до следующего конца хода.
            Debug.Log($"[ThreatController] Угрозы отложены, на столе остаётся: {_activeBoard.Count}.");
        }

        // --------- Дебаг-хелперы/утилиты ---------
        public IReadOnlyList<ThreatRuntime> ActiveBoard => _activeBoard;
        public int DrawCount => _drawPile.Count;
        public int DiscardCount => _discardPile.Count;

        private static void Shuffle(List<ThreatRuntime> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
