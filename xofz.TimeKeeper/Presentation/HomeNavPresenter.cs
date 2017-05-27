using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xofz.TimeKeeper.Presentation
{
    using System.Threading;
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.TimeKeeper.UI;
    using xofz.UI;

    public sealed class HomeNavPresenter : Presenter
    {
        public HomeNavPresenter(
            HomeNavUi ui, 
            ShellUi shell,
            MethodWeb web) 
            : base(ui, shell)
        {
            this.ui = ui;
            this.web = web;
        }

        public void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            this.ui.StatisticsKeyTapped += this.ui_StatisticsKeyTapped;
            this.ui.ExitKeyTapped += this.ui_ExitKeyTapped;

            var w = this.web;
            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        private void ui_StatisticsKeyTapped()
        {
        }

        private void ui_ExitKeyTapped()
        {
            var w = this.web;
            w.Run<Navigator>(n => n.Present<ShutdownPresenter>());
        }

        private int setupIf1;
        private readonly HomeNavUi ui;
        private readonly MethodWeb web;
    }
}

public class CharWeightFinder
{
    public string charWeight(string s)
    {
        var dict = this.fillDictionary();
        s = s.ToLowerInvariant();
        for (var i = 0; i < s.Length; ++i)
        {
            dict[s[i]]++;
        }

        var letterCollector = new LinkedList<char>();
        foreach (var kvp in dict.OrderByDescending(kvp => kvp.Value)
            .ThenBy(kvp => kvp.Key))
        {
            for (var i = 0; i < kvp.Value; ++i)
            {
                letterCollector.AddLast(kvp.Key);
            }
        }

        var sb = new StringBuilder();
        foreach (var letter in letterCollector.Distinct())
        {
            sb.Append(letter);
            sb.Append('{');
            sb.Append(letterCollector.Count(l => letter == l));
            sb.Append('}');
        }

        return sb.ToString();
    }

    private IDictionary<char, int> fillDictionary()
    {
        var dictionary = new Dictionary<char, int>();
        var keysString = "abcdefghijklmnopqrstuvwxyz0123456789";
        for (var i = 0; i < keysString.Length; ++i)
        {
            dictionary.Add(keysString[i], 0);
        }

        return dictionary;
    }
}