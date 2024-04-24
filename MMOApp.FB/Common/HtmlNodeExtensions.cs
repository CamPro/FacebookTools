using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Linq;

namespace KSS.Patterns.Web
{
    public static class HtmlNodeExtensions
    {
        public static int RemoveChildren(this HtmlNode parentNode, string name)
        {
            return HtmlHelper.RemoveChildren(parentNode, name);
        }

        public static int RemoveChildren(this HtmlNode parentNode, string name, bool first, bool last, int appranceTimes)
        {
            return HtmlHelper.RemoveChildren(parentNode, name, first, last, appranceTimes);
        }

        public static int RemoveChildrenClass(this HtmlNode parentNode, string name, string className, bool first, bool last, int appranceTimes)
        {
            return HtmlHelper.RemoveChildrenClass(parentNode, name, className, first, last, appranceTimes);
        }

        public static int RemoveChildren(this HtmlNode parentNode, string name, string attribute, string attributeValue, bool first, bool last, int appranceTimes)
        {
            return HtmlHelper.RemoveChildren(parentNode, name, attribute, attributeValue, first, last, appranceTimes);
        }

        public static int RemoveChildrenRecusive(this HtmlNode parentNode, string name)
        {
            return HtmlHelper.RemoveChildrenRecursive(parentNode, name);
        }

        public static int RemoveChildrenRecusive(this HtmlNode parentNode, string name, bool first, bool last, int appranceTimes)
        {
            return HtmlHelper.RemoveChildrenRecursive(parentNode, name, first, last, appranceTimes);
        }

        public static int RemoveChildrenRecusiveClass(HtmlNode parentNode, string name, string className, bool first, bool last, int appranceTimes)
        {
            return HtmlHelper.RemoveChildrenRecusiveClass(parentNode, name, className, first, last, appranceTimes);
        }

        public static int RemoveChildrenRecusive(this HtmlNode parentNode, string name, string attribute, string attributeValue, bool first, bool last, int appranceTimes)
        {
            return HtmlHelper.RemoveChildrenRecursive(parentNode, name, attribute, attributeValue, first, last, appranceTimes);
        }

        public static int RemoveChildrenAfter(this HtmlNode parentNode, HtmlNode childNode)
        {
            return HtmlHelper.RemoveChildrenAfter(parentNode, childNode);
        }

        public static HtmlNode GetNodeByXPath(this HtmlNode node, string xpath)
        {
            return HtmlHelper.GetNodeByXPath(node, xpath);
        }

        public static List<HtmlNode> GetNodesByXPath(this HtmlNode node, string xpath)
        {
            return HtmlHelper.GetNodesByXPath(node, xpath);
        }

        /// <summary>
        /// Get child node, none recursive
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HtmlNode Child(this HtmlNode parentNode, string name)
        {
            return HtmlHelper.Child(parentNode, name);
        }

        public static bool HasChild(this HtmlNode parentNode, HtmlNode childNode, bool recursive = false)
        {
            return HtmlHelper.HasChild(parentNode, childNode, recursive);
        }

        /// <summary>
        /// Childrens the specified parent node. Non recusive
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static List<HtmlNode> Children(this HtmlNode parentNode, string name)
        {
            return HtmlHelper.Children(parentNode, name);
        }

        public static HtmlNode Child(this HtmlNode parentNode, string name, bool first, bool last, bool recursive)
        {
            return HtmlHelper.Child(parentNode, name, first, last, recursive);
        }

        public static List<HtmlNode> Children(this HtmlNode parentNode, string name, bool first, bool last, int appranceTimes, bool recursive)
        {
            return HtmlHelper.Children(parentNode, name, first, last, appranceTimes, recursive);
        }

        public static HtmlNode ChildClass(this HtmlNode parentNode, string name, string className, bool first = true, bool last = true, bool recursive = true)
        {
            return HtmlHelper.ChildClass(parentNode, name, className, first, last, recursive);
        }

        public static HtmlNode Child(this HtmlNode parentNode, string name, string attribute, string attributeValue, bool first = true, bool last = true, bool recursive = true)
        {
            return HtmlHelper.Child(parentNode, name, attribute, attributeValue, first, last, recursive);
        }

        public static List<HtmlNode> ChildrenClass(this HtmlNode parentNode, string name, string className, bool first = true, bool last = true, int appranceTimes = -1, bool recursive = true)
        {
            return HtmlHelper.ChildrenClass(parentNode, name, className, first, last, appranceTimes, recursive);
        }

        public static List<HtmlNode> Children(this HtmlNode parentNode, string name, string attribute, string attributeValue, bool first = true, bool last = true, int appranceTimes = -1, bool recursive = true)
        {
            return HtmlHelper.Children(parentNode, name, attribute, attributeValue, first, last, appranceTimes, recursive);
        }

        public static HtmlNode ChildByInnerHtmlClass(this HtmlNode parentNode, string innerHtml, string name, string className, bool recursive)
        {
            return HtmlHelper.ChildByInnerHtmlClass(parentNode, innerHtml, name, className, recursive);
        }

        public static HtmlNode ChildByInnerHtml(this HtmlNode parentNode, string innerHtml, string name, string attribute, string attributeValue, bool recursive)
        {
            return HtmlHelper.ChildByInnerHtml(parentNode, innerHtml, name, attribute, attributeValue, recursive);
        }

        public static HtmlNode Next(this HtmlNode node)
        {
            return HtmlHelper.Next(node);
        }

        public static HtmlNode Next(this HtmlNode node, string name)
        {
            return HtmlHelper.Next(node, name);
        }

        public static HtmlNode NextClass(this HtmlNode node, string name, string className)
        {
            return HtmlHelper.NextClass(node, name, className);
        }

        public static HtmlNode Next(this HtmlNode node, string name, string attribute, string attributeValue)
        {
            return HtmlHelper.Next(node, name, attribute, attributeValue);
        }

        public static HtmlNode Previous(this HtmlNode node)
        {
            return HtmlHelper.Previous(node);
        }

        public static HtmlNode Previous(this HtmlNode node, string name)
        {
            return HtmlHelper.Previous(node, name);
        }

        public static HtmlNode PreviousClass(this HtmlNode node, string name, string className)
        {
            return HtmlHelper.PreviousClass(node, name, className);
        }

        public static HtmlNode Previous(this HtmlNode node, string name, string attribute, string attributeValue)
        {
            return HtmlHelper.Previous(node, name, attribute, attributeValue);
        }

        public static HtmlNode ChildById(this HtmlNode root, string id)
        {
            return HtmlHelper.ChildById(root, id);
        }

        public static HtmlNode ChildByClassName(this HtmlNode root, string className)
        {
            return HtmlHelper.ChildByClassName(root, className);
        }

        public static List<HtmlNode> ChildrenByClassName(this HtmlNode root, string className)
        {
            return HtmlHelper.ChildrenByClassName(root, className);
        }

        /// <summary>
        /// Childs the name of the by name attribute, rescusively
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="name">Name of the element.</param>
        /// <returns></returns>
        public static HtmlNode ChildByName(this HtmlNode root, string name)
        {
            return HtmlHelper.ChildByName(root, name);
        }

        /// <summary>
        /// Find children that has attribute "name" match a specific value.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<HtmlNode> ChildrenByName(this HtmlNode root, string name)
        {
            return HtmlHelper.ChildrenByName(root, name);
        }

        public static string ClassName(this HtmlNode node)
        {
            return HtmlHelper.ClassName(node);
        }

        public static string Id(this HtmlNode node)
        {
            return HtmlHelper.Id(node);
        }

        public static bool HasAttribute(this HtmlNode node, string attributeName)
        {
            return HtmlHelper.HasAttribute(node, attributeName);
        }

        public static string Attribute(this HtmlNode node, string attributeName)
        {
            return HtmlHelper.Attribute(node, attributeName);
        }

        public static string StyleValue(this HtmlNode node, string styleName)
        {
            return HtmlHelper.StyleValue(node, styleName);
        }

        public static bool CheckHasStyle(this HtmlNode node, string styleName, string styleValue)
        {
            return HtmlHelper.CheckHasStyle(node, styleName, styleValue);
        }

        public static HtmlNode ChildLink(this HtmlNode node, bool recursive = true)
        {
            return HtmlHelper.ChildLink(node, recursive);
        }

        public static List<HtmlNode> ChildrenLink(this HtmlNode node, bool recursive = true)
        {
            return HtmlHelper.ChildrenLink(node, recursive);
        }

        public static string Href(this HtmlNode linkNode)
        {
            return HtmlHelper.Href(linkNode);
        }

        public static string Anchor(this HtmlNode linkNode)
        {
            return HtmlHelper.Anchor(linkNode);
        }

        public static string Action(this HtmlNode formNode)
        {
            return HtmlHelper.Action(formNode);
        }

        public static string Name(this HtmlNode formNode)
        {
            return HtmlHelper.Name(formNode);
        }

        public static string Value(this HtmlNode formNode)
        {
            return HtmlHelper.Value(formNode);
        }

        public static string ImageSource(this HtmlNode imageNode)
        {
            return HtmlHelper.ImageSource(imageNode);
        }

        public static List<HtmlNode> Options(this HtmlNode imageNode)
        {
            return HtmlHelper.Options(imageNode);
        }

        public static string SearchOptionValeByText(this HtmlNode selectNode, string text)
        {
            return HtmlHelper.SearchOptionValeByText(selectNode, text);
        }

        public static string OptionText(this HtmlNode optionNode)
        {
            return HtmlHelper.OptionText(optionNode);
        }

        public static HtmlNode Parent(this HtmlNode node, Func<HtmlNode, bool> check)
        {
            return HtmlHelper.Parent(node, check);
        }

        public static HtmlNode ChildClass(this HtmlNode parentNode, Func<HtmlNode, bool> check, string name, string className, bool recursive)
        {
            return HtmlHelper.ChildClass(parentNode, check, name, className, recursive);
        }

        public static HtmlNode Child(this HtmlNode parentNode, Func<HtmlNode, bool> check, string name, string attribute, string attributeValue, bool recursive)
        {
            return HtmlHelper.Child(parentNode, check, name, attribute, attributeValue, recursive);
        }

        public static List<HtmlNode> Children(this HtmlNode parentNode, Func<HtmlNode, bool> check, string name, string attribute, string attributeValue, bool recursive)
        {
            return HtmlHelper.Children(parentNode, check, name, attribute, attributeValue, recursive);
        }

        public static bool CheckHidden(this HtmlNode node)
        {
            return HtmlHelper.CheckHidden(node);
        }

        public static bool CheckShow(this HtmlNode node)
        {
            return HtmlHelper.CheckShow(node);
        }

        public static void DetachFromParent(this HtmlNode node)
        {
            node.ParentNode.RemoveChild(node);
        }

        public static string HtmlMetaRefreshUrl(this HtmlNode node)
        {
            return HtmlHelper.GetMetaRefreshUrl(node);
        }
    }
}