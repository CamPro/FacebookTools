using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using KSS.Patterns.Extensions;

namespace KSS.Patterns.Web
{
    public static class HtmlHelper
    {
        public const string NEW_LINE_TAG = "<br>";
        public const string REGEX = "[REGEX]";
        public static readonly string[] NEW_LINE_TAGS = new[] { "<br />", "<br>", "<br/>" };

        public static string RemoveNewLineTags(string htmlContent)
        {
            return ReplaceNewLineTags(htmlContent, string.Empty);
        }

        public static string ReplaceNewLineTags(string htmlContent, string text)
        {
            string htmlResult = htmlContent;

            foreach (string newLine in NEW_LINE_TAGS)
            {
                htmlResult = htmlResult.Replace(newLine, text);
            }

            return htmlResult;
        }

        public static string RemoveHtmlTags(string htmlContent)
        {
            return RemoveHtmlTags(htmlContent, " ");
        }

        public static string RemoveHtmlTags(string htmlContent, string delimitor)
        {
            const string HTML_TAG_PATTERN = "<.*?>";

            string textContent = Regex.Replace(htmlContent, HTML_TAG_PATTERN, delimitor);

            return RemoveDuplicated(textContent, delimitor);
        }

        public static string RemoveDuplicated(string htmlContent, string checkString)
        {
            string dupString = checkString + checkString;

            while (htmlContent.Contains(dupString))
            {
                htmlContent = htmlContent.Replace(dupString, checkString);
            }

            return htmlContent;
        }

        public static int RemoveChildren(HtmlNode parentNode, string name)
        {
            return RemoveChildren(parentNode, name, true, false, 1);
        }

        public static int RemoveChildren(HtmlNode parentNode, string name, bool first, bool last, int appranceTimes)
        {
            return RemoveChildren(parentNode, name, string.Empty, string.Empty, first, last, appranceTimes);
        }

        public static int RemoveChildrenClass(HtmlNode parentNode, string name, string className, bool first, bool last, int appranceTimes)
        {
            return RemoveChildren(parentNode, name, "class", className, first, last, appranceTimes);
        }

        public static int RemoveChildren(HtmlNode parentNode, string name, string attribute, string attributeValue, bool first, bool last, int appranceTimes)
        {
            if (parentNode == null || parentNode.ChildNodes.Count <= 0)
                return 0;

            List<HtmlNode> deletedNodes = Children(parentNode, name, attribute, attributeValue, first, last, appranceTimes, false);

            foreach (HtmlNode deletedNode in deletedNodes)
            {
                parentNode.RemoveChild(deletedNode);
            }

            return deletedNodes.Count;
        }

        public static int RemoveChildrenRecursive(HtmlNode parentNode, string name)
        {
            return RemoveChildrenRecursive(parentNode, name, true, false, 1);
        }

        public static int RemoveChildrenRecursive(HtmlNode parentNode, string name, bool first, bool last, int appranceTimes)
        {
            return RemoveChildrenRecursive(parentNode, name, string.Empty, string.Empty, first, last, appranceTimes);
        }

        public static int RemoveChildrenRecusiveClass(HtmlNode parentNode, string name, string className, bool first, bool last, int appranceTimes)
        {
            return RemoveChildrenRecursive(parentNode, name, "class", className, first, last, appranceTimes);
        }

        public static int RemoveChildrenRecursive(HtmlNode parentNode, string name, string attribute, string attributeValue, bool first, bool last, int appranceTimes)
        {
            int deletedNodeCount = RemoveChildren(parentNode, name, attribute, attributeValue, first, last, appranceTimes);

            if (deletedNodeCount < appranceTimes)
            {
                foreach (HtmlNode childNode in parentNode.ChildNodes)
                {
                    int childNodeDeletedCount = RemoveChildrenRecursive(childNode, name, attribute, attributeValue, first, last, appranceTimes - deletedNodeCount);
                    deletedNodeCount += childNodeDeletedCount;
                    if (deletedNodeCount == appranceTimes)
                        break;
                }
            }

            return deletedNodeCount;
        }

        public static int RemoveChildrenAfter(HtmlNode parentNode, HtmlNode childNode)
        {
            List<HtmlNode> removedNodes = new List<HtmlNode>();
            bool reach = false;
            foreach (HtmlNode node in parentNode.ChildNodes)
            {
                if (node == childNode)
                {
                    reach = true;
                    continue;
                }

                if (reach)
                    removedNodes.Add(node);
            }

            foreach (HtmlNode node in removedNodes)
            {
                parentNode.RemoveChild(node);
            }

            return removedNodes.Count;
        }

        public static HtmlNode GetNodeByXPath(HtmlNode node, string xpath)
        {
            return node.OwnerDocument.DocumentNode.SelectSingleNode(xpath);
        }

        public static List<HtmlNode> GetNodesByXPath(HtmlNode node, string xpath)
        {
            var nodes = node.OwnerDocument.DocumentNode.SelectNodes(xpath);
            if (nodes == null) return new List<HtmlNode>();

            return nodes.ToList();
        }

        public static List<HtmlNode> GetImageNodes(HtmlNode node)
        {
            return GetNodesByXPath(node, "//img");
        }

        public static List<HtmlNode> GetHrefNodes(HtmlNode node)
        {
            return GetNodesByXPath(node, "//a");
        }

        /// <summary>
        /// Childs the specified parent node. Search NON recursive
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static HtmlNode Child(HtmlNode parentNode, string name)
        {
            List<HtmlNode> nodes = Children(parentNode, name);
            return nodes != null && nodes.Count > 0 ? nodes[0] : null;
        }

        public static bool HasChild(HtmlNode parentNode, HtmlNode childNode, bool recursive = false)
        {
            if (parentNode.ChildNodes.Contains(childNode))
                return true;

            if (recursive)
            {
                foreach (var node in parentNode.ChildNodes)
                {
                    if (HasChild(node, childNode, true))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Childrens the specified parent node. Non recusive
        /// </summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static List<HtmlNode> Children(HtmlNode parentNode, string name)
        {
            return Children(parentNode, name, true, false, -1, false);
        }

        public static HtmlNode Child(HtmlNode parentNode, string name, bool first, bool last, bool recursive)
        {
            List<HtmlNode> nodes = Children(parentNode, name, first, last, 1, recursive);
            return nodes != null && nodes.Count > 0 ? nodes[0] : null;
        }

        public static List<HtmlNode> Children(HtmlNode parentNode, string name, bool first, bool last, int appranceTimes, bool recursive)
        {
            return Children(parentNode, name, string.Empty, string.Empty, first, last, appranceTimes, recursive);
        }

        public static HtmlNode ChildClass(HtmlNode parentNode, string name, string className, bool first = true, bool last = true, bool recursive = true)
        {
            return Child(parentNode, name, "class", className, first, last, recursive);
        }

        public static HtmlNode Child(HtmlNode parentNode, string name, string attribute, string attributeValue, bool first = true, bool last = true, bool recursive = true)
        {
            List<HtmlNode> nodes = Children(parentNode, name, attribute, attributeValue, first, last, 1, recursive);
            return nodes != null && nodes.Count > 0 ? nodes[0] : null;
        }

        public static List<HtmlNode> ChildrenClass(HtmlNode parentNode, string name, string className, bool first = true, bool last = true, int appranceTimes = -1, bool recursive = true)
        {
            return Children(parentNode, name, "class", className, first, last, appranceTimes, recursive);
        }

        public static List<HtmlNode> Children(HtmlNode parentNode, string name, string attribute, string attributeValue, bool first = true, bool last = true, int appranceTimes = -1, bool recursive = true)
        {
            List<HtmlNode> childNodes = new List<HtmlNode>();
            if (parentNode == null)
                return childNodes;

            foreach (HtmlNode node in parentNode.ChildNodes)
            {
                if (MatchName(node, name) && MatchAttribute(node, attribute, attributeValue))
                {
                    childNodes.Add(node);
                    continue;
                }

                if (recursive)
                {
                    // find in child nodes
                    List<HtmlNode> nodes = Children(node, name, attribute, attributeValue, first, last, appranceTimes, true);
                    childNodes.AddRange(nodes);
                }
            }

            List<HtmlNode> foundNodes = new List<HtmlNode>();

            // add frist node
            if (first && childNodes.Count > 0)
                foundNodes.Add(childNodes[0]);

            // middle nodes
            foreach (HtmlNode node in childNodes)
            {
                if (appranceTimes > 0 && foundNodes.Count >= appranceTimes)
                    break;

                if (!foundNodes.Contains(node))
                    foundNodes.Add(node);
            }

            // last node
            if (last && childNodes.Count > 0)
            {
                HtmlNode lastNode = childNodes[childNodes.Count - 1];
                if (!foundNodes.Contains(lastNode))
                    foundNodes.Add(lastNode);
            }

            return foundNodes;
        }
        /*
                public static List<HtmlNode> GetNodes(HtmlNode parentNode, string path)
                {
                    string lastPath = path;
                    int index = path.LastIndexOf('|');
                    if (index >= 0)
                    {
                        string subPath = path.Substring(0, index);
                        lastPath = path.Substring(index + 1, path.Length - index - 1);

                        parentNode = GetNode(parentNode, subPath);
                        if (parentNode == null)
                            return new List<HtmlNode>();
                    }

                    string tag, attribute, attributeValue;
                    bool recusive, mandatory;

                    try
                    {
                        ExtractPath(lastPath, out tag, out attribute, out attributeValue, out recusive, out mandatory);
                    }
                    catch (ArgumentException)
                    {
                        throw new ArgumentException("The run path is invalid: " + lastPath, "path", null);
                    }

                    List<HtmlNode> childElements = GetChildNodes(parentNode, tag, attribute, attributeValue, true, true, -1, recusive);
                    if (mandatory && (childElements == null || childElements.Count <= 0))
                        throw new SiteStructureChangedException(string.Empty, lastPath, path);

                    return childElements;
                }

                /// <summary>
                /// Get element by path. The path template: tag:attribute:value:recusive:mandatory
                /// Ex: a or :id:grid or :id:grid:1 or div:class:footer or span:class:highlight:0
                /// </summary>
                /// <typeparam name="T"></typeparam>
                /// <param name="parentNode"></param>
                /// <param name="path"></param>
                /// <returns></returns>
                public static HtmlNode GetNode(HtmlNode parentNode, string path)
                {
                    if (string.IsNullOrEmpty(path))
                        return parentNode;

                    string subPath, nextPath;

                    int index = path.IndexOf('|');
                    if (index >= 0)
                    {
                        subPath = path.Substring(0, index);
                        nextPath = path.Substring(index + 1, path.Length - index - 1);
                    }
                    else
                    {
                        subPath = path;
                        nextPath = string.Empty;
                    }

                    string tag, attribute, attributeValue;
                    bool recusive, mandatory;

                    try
                    {
                        ExtractPath(subPath, out tag, out attribute, out attributeValue, out recusive, out mandatory);
                    }
                    catch (ArgumentException)
                    {
                        throw new ArgumentException("The run path is invalid at: " + subPath, "path", null); ;
                    }

                    HtmlNode childNode;
                    if (attribute.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                    {
                        childNode = GetElementById<T>(attributeValue);
                    }
                    else
                    {
                        childNode = string.IsNullOrEmpty(nextPath)
                                        ? GetChildElement<T>(parentNode, tag, attribute, attributeValue, recusive)
                                        : GetChildElement<Element>(parentNode, tag, attribute, attributeValue, recusive);
                    }

                    if (childNode == null && mandatory)
                        throw new SiteStructureChangedException(string.Empty, subPath, path);

                    return childNode == null ? null : GetElement<T>(childNode, nextPath);
                }*/

        private static void ExtractPath(string path, out string tag, out string attribute, out string attributeValue, out bool recursive, out bool mandatory)
        {
            if (string.IsNullOrEmpty(path) || path.IndexOf('|') >= 0)
            {
                tag = attribute = attributeValue = string.Empty;
                recursive = mandatory = false;
            }
            else
            {
                string[] parts = path.Split(':');
                int partCount = parts.Length;
                if (partCount <= 0 || partCount > 5)
                    throw new ArgumentException("Path is invalid: " + path, "path");

                tag = parts[0];
                attribute = partCount > 1 ? parts[1] : string.Empty;
                attributeValue = partCount > 2 ? parts[2] : string.Empty;
                recursive = partCount > 3 && parts[3] == "1" ? true : false;
                mandatory = partCount > 4 && parts[4] == "1" ? true : false;
            }
        }

        private static bool MatchName(HtmlNode node, string name)
        {
            if (string.IsNullOrEmpty(name))
                return true;

            return node != null && node.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase);
        }

        private static bool MatchAttribute(HtmlNode node, string attribute, string attributeValue)
        {
            if (node == null || string.IsNullOrEmpty(attribute) || string.IsNullOrEmpty(attributeValue))
                return true;

            string value = node.GetAttributeValue(attribute, string.Empty).Trim();

            if (attributeValue.Contains(REGEX))
                return Regex.IsMatch(value, attributeValue.Replace(REGEX, string.Empty));

            return value.Equals(attributeValue, StringComparison.InvariantCultureIgnoreCase);
        }

        public static HtmlNode Root(string rawHtmlContent)
        {
            if (rawHtmlContent.IsNullOrWhiteSpace())
                return null;

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(rawHtmlContent);

            return document.DocumentNode;
        }

        public static HtmlNode HtmlDocRoot(string htmlContent)
        {
            int index = htmlContent.IndexOf("<html", StringComparison.InvariantCultureIgnoreCase);
            if (index >= 0)
            {
                htmlContent = htmlContent.Remove(0, index);
                return Root(htmlContent);
            }

            return null;
        }

        public static HtmlNode AnonymousRoot(string htmlContent)
        {
            return Root(string.Format("<xyz>{0}</xyz>", htmlContent));
        }

        public static HtmlNode ChildByInnerHtmlClass(HtmlNode parentNode, string innerHtml, string name, string className, bool recursive)
        {
            return ChildByInnerHtml(parentNode, innerHtml, name, "class", className, recursive);
        }

        public static HtmlNode ChildByInnerHtml(HtmlNode parentNode, string innerHtml, string name, string attribute, string attributeValue, bool recursive)
        {
            foreach (var childNode in parentNode.ChildNodes)
            {
                if (MatchName(childNode, name) && MatchAttribute(childNode, attribute, attributeValue) && childNode.InnerHtml.Equals(innerHtml))
                {
                    return childNode;
                }

                if (recursive && childNode.InnerHtml.Contains(innerHtml))
                {
                    var foundNode = ChildByInnerHtml(childNode, innerHtml, name, attribute, attributeValue, true);
                    if (foundNode != null)
                        return foundNode;
                }
            }

            return null;
        }

        public static HtmlNode Next(HtmlNode node)
        {
            return Next(node, string.Empty);
        }

        public static HtmlNode Next(HtmlNode node, string name)
        {
            return Next(node, name, string.Empty, string.Empty);
        }

        public static HtmlNode NextClass(HtmlNode node, string name, string className)
        {
            return Next(node, name, "class", className);
        }

        public static HtmlNode Next(HtmlNode node, string name, string attribute, string attributeValue)
        {
            do
            {
                HtmlNode nextNode = node.NextSibling;

                if (nextNode == null)
                    return null;

                if (MatchName(nextNode, name) && MatchAttribute(nextNode, attribute, attributeValue))
                    return nextNode;

                node = nextNode;
            } while (true);
        }

        public static HtmlNode Previous(HtmlNode node)
        {
            return Previous(node, string.Empty);
        }

        public static HtmlNode Previous(HtmlNode node, string name)
        {
            return Previous(node, name, string.Empty, string.Empty);
        }

        public static HtmlNode PreviousClass(HtmlNode node, string name, string className)
        {
            return Previous(node, name, "class", className);
        }

        public static HtmlNode Previous(HtmlNode node, string name, string attribute, string attributeValue)
        {
            do
            {
                HtmlNode prevNode = node.PreviousSibling;

                if (prevNode == null)
                    return null;

                if (MatchName(prevNode, name) && MatchAttribute(prevNode, attribute, attributeValue))
                    return prevNode;

                node = prevNode;
            } while (true);
        }

        public static HtmlNode ChildById(HtmlNode root, string id)
        {
            return Child(root, string.Empty, "id", id, true, true, true);
        }

        /// <summary>
        /// Childs the name of the by class, rescusively
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="className">Name of the class.</param>
        /// <returns></returns>
        public static HtmlNode ChildByClassName(HtmlNode root, string className)
        {
            return Child(root, string.Empty, "class", className, true, true, true);
        }

        public static List<HtmlNode> ChildrenByClassName(HtmlNode root, string className)
        {
            return Children(root, string.Empty, "class", className, true, true, -1, true);
        }

        /// <summary>
        /// Childs the name of the by name attribute, rescusively
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="name">Name of the class.</param>
        /// <returns></returns>
        public static HtmlNode ChildByName(HtmlNode root, string name)
        {
            return Child(root, string.Empty, "name", name, true, true, true);
        }

        /// <summary>
        /// Find children that has attribute "name" match a specific value.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<HtmlNode> ChildrenByName(HtmlNode root, string name)
        {
            return Children(root, string.Empty, "name", name, true, true, -1, true);
        }

        public static string ClassName(HtmlNode node)
        {
            return Attribute(node, "class");
        }

        public static string Id(HtmlNode node)
        {
            return Attribute(node, "id");
        }

        public static bool HasAttribute(HtmlNode node, string attributeName)
        {
            return node != null && node.Attributes.Contains(attributeName);
        }

        public static string Attribute(HtmlNode node, string attributeName)
        {
            return node != null && node.Attributes.Contains(attributeName) ? node.Attributes[attributeName].Value : string.Empty;
        }

        public static string StyleValue(HtmlNode node, string styleName)
        {
            if (!node.Attributes.Contains("style"))
                return string.Empty;

            var style = node.Attributes["style"].Value;
            style = Domainer.HtmlDecode(style);

            return (from s in style.Split(';')
                    let pair = s.Split(':')
                    where pair.Length == 2 && pair[0].Trim().Equals(styleName)
                    select pair[1].Trim()).FirstOrDefault();
        }

        public static bool CheckHasStyle(HtmlNode node, string styleName, string styleValue)
        {
            var style = StyleValue(node, styleName);

            return style != null && style.Equals(styleValue, StringComparison.InvariantCultureIgnoreCase);
        }

        public static HtmlNode ChildLink(HtmlNode node, bool recusive = true)
        {
            return Children(node, "a", true, true, 1, recusive).FirstOrDefault();
        }

        public static List<HtmlNode> ChildrenLink(HtmlNode node, bool recusive = true)
        {
            if (recusive)
            {
                return node.Descendants("a").ToList();
                //return node.SelectNodes("//a[@href]").ToList();
            }
            else
            {
                return Children(node, "a", true, true, -1, recusive);
            }
        }

        public static string Href(HtmlNode linkNode)
        {
            return Attribute(linkNode, "href");
        }

        public static string Anchor(HtmlNode linkNode)
        {
            return linkNode.InnerText;
        }

        public static string Action(HtmlNode formNode)
        {
            return Attribute(formNode, "action").Trim();
        }

        public static string Name(HtmlNode formNode)
        {
            return Attribute(formNode, "name").Trim();
        }

        public static string Value(HtmlNode formNode)
        {
            return Attribute(formNode, "value").Trim();
        }

        public static string ImageSource(HtmlNode imageNode)
        {
            return Attribute(imageNode, "src");
        }

        public static List<HtmlNode> Options(HtmlNode selectNode)
        {
            return Children(selectNode, "option", true, true, -1, false);
        }

        public static string SearchOptionValeByText(HtmlNode selectNode, string text)
        {
            var options = Options(selectNode);
            var value = options.Where(o => OptionText(o) == text).Select(o => Value(o)).FirstOrDefault();

            return value;
        }

        public static string OptionText(HtmlNode optionNode)
        {
            var html = optionNode.OuterHtml;
            if (html.Contains("</option>"))
                return optionNode.InnerText;

            return optionNode.NextSibling.InnerText;
        }


        public static HtmlNode Parent(HtmlNode node, Func<HtmlNode, bool> check)
        {
            if (node == null)
                return null;

            HtmlNode parent = node.ParentNode;
            do
            {
                if (parent == null)
                    return null;

                if (check(parent))
                    return parent;

                parent = parent.ParentNode;
            } while (true);
        }

        public static HtmlNode ChildClass(HtmlNode parentNode, Func<HtmlNode, bool> check, string name, string className, bool recusive)
        {
            return Child(parentNode, check, name, "class", className, recusive);
        }

        public static HtmlNode Child(HtmlNode parentNode, Func<HtmlNode, bool> check, string name, string attribute, string attributeValue, bool recursive)
        {
            foreach (var childNode in parentNode.ChildNodes)
            {
                if (MatchName(childNode, name) && MatchAttribute(childNode, attribute, attributeValue) && check(childNode))
                {
                    return childNode;
                }

                if (recursive)
                {
                    var foundNode = Child(childNode, check, name, attribute, attributeValue, true);
                    if (foundNode != null)
                        return foundNode;
                }
            }

            return null;
        }

        public static List<HtmlNode> Children(HtmlNode parentNode, Func<HtmlNode, bool> check, string name, string attribute, string attributeValue, bool recursive)
        {
            var children = new List<HtmlNode>();

            foreach (var childNode in parentNode.ChildNodes)
            {
                if (MatchName(childNode, name) && MatchAttribute(childNode, attribute, attributeValue) && check(childNode))
                {
                    children.Add(childNode);
                }

                if (recursive)
                {
                    var subChild = Children(childNode, check, name, attribute, attributeValue, true);
                    children.AddRange(subChild);
                }
            }

            return children;
        }

        public static bool CheckHidden(HtmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            return CheckHasStyle(node, "visibility", "hidden") || CheckHasStyle(node, "display", "none");
        }

        public static bool CheckShow(HtmlNode node)
        {
            return !CheckHidden(node);
        }

        public static void RemoveCommentNodes(HtmlNode node)
        {
            if (node.NodeType == HtmlNodeType.Comment)
            {
                node.ParentNode.RemoveChild(node);
                return;
            }

            var childNodes = node.ChildNodes.ToList();
            foreach (var childNode in childNodes)
            {
                RemoveCommentNodes(childNode);
            }
        }

        public static string GetMetaRefreshUrl(HtmlNode root)
        {
            var refresh = Child(root, "meta", "http-equiv", "refresh");
            if (refresh != null && refresh.ParentNode.Name != "noscript")
            {
                var token = "url=";
                var content = Attribute(refresh, "content").ToLowerInvariant();
                var index = content.IndexOf(token) + token.Length;
                var redirectUrl = content.Substring(index, content.Length - index);
                redirectUrl = Domainer.HtmlDecode(redirectUrl.Trim());
                //var redirectUrl = content.Split(';').First(s => s.Contains("url=")).Replace("url=", string.Empty);

                return redirectUrl;
            }

            return string.Empty;
        }
    }
}