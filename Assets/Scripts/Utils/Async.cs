using System;
using System.Collections;
using UnityEngine;

namespace MeldingMadness.Utils {
    public class Async {

        public static void RunAfterSeconds(MonoBehaviour mb, float delay, Action fn) {
            mb.StartCoroutine(RunAfter(new WaitForSeconds(delay), fn));
        }
        public static void RunEndOfFrame(MonoBehaviour mb, Action fn) {
            mb.StartCoroutine(RunAfter(new WaitForEndOfFrame(), fn));
        }

        public static IEnumerator RunAfter(YieldInstruction yieldInstruction, Action fn) {
            yield return yieldInstruction;
            fn();
            yield return null;
        }
    }
}