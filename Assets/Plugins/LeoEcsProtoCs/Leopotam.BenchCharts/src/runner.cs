// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System.Diagnostics;

namespace Leopotam.BenchCharts {
    public static class BenchChartRunner {
        public static bool Start (IBenchChartTest test, BenchChartReport result, bool warmup = true, int cycles = 10) {
            if (test == null || result == null || cycles <= 0) { return false; }
            var testsCount = result.TestsCount ();
            if (warmup) {
                for (var i = 0; i < testsCount; i++) {
                    test.OnInit (i);
                    test.OnRun (i);
                    test.OnDestroy (i);
                }
            }
            Stopwatch sw = new ();
            var ticks2ms = 1000.0 / Stopwatch.Frequency / cycles;
            for (var i = 0; i < testsCount; i++) {
                test.OnInit (i);
                sw.Reset ();
                sw.Start ();
                for (var avg = 0; avg < cycles; avg++) {
                    test.OnRun (i);
                }
                sw.Stop ();
                result.Add (sw.ElapsedTicks * ticks2ms);
                test.OnDestroy (i);
            }

            return true;
        }
    }

    public interface IBenchChartTest {
        void OnInit (int it);
        void OnRun (int it);
        void OnDestroy (int it);
    }
}
