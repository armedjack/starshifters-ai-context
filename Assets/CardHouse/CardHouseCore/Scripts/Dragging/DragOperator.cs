using UnityEngine;

namespace CardHouse
{
    [RequireComponent(typeof(Homing)), RequireComponent(typeof(Turning)), RequireComponent(typeof(Scaling))]
    public class DragOperator : MonoBehaviour
    {
        public DragDetector MyDragDetector;

        public DragAction DragAction;
        public float DragSwell = 1.2f;
        public bool PointUpWhenDragged = true;

        public SeekerScriptableSet PresentationSeekers;

        Homing MyHoming;
        Turning MyTurning;
        Scaling MyScaling;


        private void Awake()
        {
            MyHoming = GetComponent<Homing>();
            MyTurning = GetComponent<Turning>();
            MyScaling = GetComponent<Scaling>();
            if (MyDragDetector == null)
            {
                MyDragDetector = GetComponent<DragDetector>();
            }

            // Ensure dragging hooks are set even if UnityEvents were lost in the prefab.
            if (MyDragDetector != null)
            {
                if (MyDragDetector.OnDragStart.GetPersistentEventCount() == 0)
                {
                    MyDragDetector.OnDragStart.AddListener(() => SetDragState(true));
                }
                if (MyDragDetector.OnDragEnd.GetPersistentEventCount() == 0)
                {
                    MyDragDetector.OnDragEnd.AddListener(() => SetDragState(false));
                }
            }
        }

        public void SetDragState(bool newState)
        {
            if (MyDragDetector == null)
                return;

            if (newState)
            {
                if (UseDragSwell)
                {
                    MyScaling.StartSeeking(DragSwell);
                }
                Dragging.Instance.BeginDragging(MyDragDetector, MyHoming, MyTurning, PointUpWhenDragged);
            }
            else
            {
                if (UseDragSwell)
                {
                    MyScaling.StartSeeking(1f);
                }
                Dragging.Instance.StopDragging();
            }
        }

        bool UseDragSwell => DragSwell > 0 && DragSwell != 1;
    }
}
