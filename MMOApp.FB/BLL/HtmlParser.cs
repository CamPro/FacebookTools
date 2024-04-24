using HtmlAgilityPack;
using KSS.Patterns.Extensions;
using KSS.Patterns.Web;
using System.Collections.Generic;
using System.Linq;

namespace MarketDragon.Automation.Common
{
    public class HtmlParser
    {
        public Dictionary<string, string> FormGetElementsBySibling(HtmlNode form, bool hiddenOnly = false, bool recursive = true)
        {
            Dictionary<string, string> queries;

            if (form.ChildNodes.Count <= 0)
            {
                queries = new Dictionary<string, string>();

                var next = form.NextSibling;
                var tags = new[] { "input", "textarea" };
                do
                {
                    if (next == null || next.OuterHtml == "</form>")
                        break;

                    if (tags.Contains(next.Name) && (!hiddenOnly || next.Attribute("type") == "hidden"))
                    {
                        if (!queries.ContainsKey(next.Name()))
                            queries.Add(next.Name(), next.Value());
                    }

                    if (recursive)
                    {
                        var squeries = FindInput(next, hiddenOnly);
                        queries.AddRange(squeries.Where(sq => !queries.ContainsKey(sq.Key)));
                    }

                    next = next.NextSibling;

                } while (true);
            }
            else
            {
                queries = FindInput(form);
            }

            queries.RemoveIfContains("");

            return queries;
        }

        public List<KeyValuePair<string, string>> FormGetElementListBySibling(HtmlNode form, bool hiddenOnly = false, bool recursive = true)
        {
            List<KeyValuePair<string, string>> queries;

            if (form.ChildNodes.Count <= 0)
            {
                queries = new List<KeyValuePair<string, string>>();

                var next = form.NextSibling;
                var tags = new[] { "input", "textarea" };
                do
                {
                    if (next == null || next.OuterHtml == "</form>")
                        break;

                    if (tags.Contains(next.Name) && (!hiddenOnly || next.Attribute("type") == "hidden"))
                    {
                        queries.Add(new KeyValuePair<string, string>(next.Name(), next.Value()));
                    }

                    if (recursive)
                    {
                        var squeries = FindInputList(next, hiddenOnly);
                        queries.AddRange(squeries);
                    }

                    next = next.NextSibling;

                } while (true);
            }
            else
            {
                queries = FindInputList(form);
            }

            queries.RemoveAll(q => q.Key == "");

            return queries;
        }

        public Dictionary<string, string> FindInput(HtmlNode node, bool hiddenOnly = true)
        {
            var queries = new Dictionary<string, string>();

            var nodes = hiddenOnly
                ? node.Children("input", "type", "hidden")
                : node.Children("input", string.Empty, string.Empty);

            var nodes2 = hiddenOnly
                ? node.Children("textarea", "type", "hidden")
                : node.Children("textarea", string.Empty, string.Empty);

            nodes.AddRange(nodes2);

            nodes.ForEach(n =>
            {
                if (!queries.ContainsKey(n.Name()))
                    queries.Add(n.Name(), n.Value());
            });


            return queries;
        }

        public List<KeyValuePair<string, string>> FindInputList(HtmlNode node, bool hiddenOnly = true)
        {
            var queries = new List<KeyValuePair<string, string>>();

            var nodes = hiddenOnly
                ? node.Children("input", "type", "hidden")
                : node.Children("input", string.Empty, string.Empty);

            var nodes2 = hiddenOnly
                ? node.Children("textarea", "type", "hidden")
                : node.Children("textarea", string.Empty, string.Empty);

            nodes.AddRange(nodes2);

            nodes.ForEach(n =>
            {
                queries.Add(new KeyValuePair<string, string>(n.Name(), n.Value()));
            });

            return queries;
        }

        /// <summary>
        ///  Find a form that contain some text in action attribute.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="actionFootprint"></param>
        /// <returns></returns>
        public HtmlNode FindForm(HtmlNode root, string actionFootprint)
        {
            var form = root.Child(f => f.Action().Contains(actionFootprint), "form", null, null, true);
            return form;
        }

        /// <summary>
        ///  Find a forms that contain some text in action attribute.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="actionFootprint"></param>
        /// <returns></returns>
        public List<HtmlNode> FindForms(HtmlNode root, string actionFootprint)
        {
            var forms = root.Children(f => f.Action().Contains(actionFootprint), "form", "method", "post", true);
            return forms;
        }

        /// <summary>
        /// Get form action with html decoded.
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public string GetFormUrl(HtmlNode form)
        {
            var action = Domainer.HtmlDecode(form.Action());
            return action;
        }
    }
}