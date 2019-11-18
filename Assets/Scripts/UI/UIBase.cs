using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIBase : MonoBehaviour
    {
        public bool Visible = true;
        public float TransitionTime = .15f;

        CanvasGroup canvasGroup;
        Coroutine transitionCoroutine;
        float transitionState = 1;

        public float Opacity
        {
            get => canvasGroup.alpha;
            protected set => canvasGroup.alpha = value;
        }

        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            transitionState = Visible ? 1 : 0;
            ApplyVisibilityTransition(transitionState);
            if (Visible)
                AfterShow();
            else
                AfterHide();
        }

        protected virtual void ApplyVisibilityTransition(float t)
        {
            Opacity = t;
        }
        protected virtual void AfterHide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        protected virtual void AfterShow()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        IEnumerator ShowProcess()
        {
            foreach (var t in Utility.TimerNormalized(TransitionTime))
            {
                transitionState = Mathf.Lerp(transitionState, 1, t);
                ApplyVisibilityTransition(transitionState);
                yield return null;
            }
            AfterShow();
        }

        IEnumerator HideProcess()
        {
            foreach (var t in Utility.TimerNormalized(TransitionTime))
            {
                transitionState = Mathf.Lerp(transitionState, 0, t);
                ApplyVisibilityTransition(transitionState);
                yield return null;
            }
            AfterHide();
        }

        public Coroutine Hide()
        {
            if (transitionCoroutine != null)
                StopCoroutine(transitionCoroutine);
            transitionCoroutine = StartCoroutine(HideProcess());
            return transitionCoroutine;
        }

        public Coroutine Show()
        {
            if (transitionCoroutine != null)
                StopCoroutine(transitionCoroutine);
            transitionCoroutine = StartCoroutine(ShowProcess());
            return transitionCoroutine;
        }
    }
}

