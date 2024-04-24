using HtmlAgilityPack;
using IMH.Domain.Facebook;
using KSS.Patterns.Extensions;
using KSS.Patterns.Web;
using MarketDragon.Automation.Social.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMOApp.FB.BLL
{
    public class FacebookParser
    {
        public bool IsEmailConfirmPage(string pageSource)
        {
            var root = HtmlHelper.Root(pageSource);
            return IsEmailConfirmPage(root);
        }

        public bool IsEmailConfirmPage(HtmlNode root)
        {
            var form = FindForm(root, "/confirmemail.php?") ?? FindForm(root, "confirm_code/dialog/submit");

            var isConfirmationPage = form != null;
            if (!isConfirmationPage)
            {
                var changeemail = root.ChildrenLink().FirstOrDefault(l => l.Href().Contains("/changeemail"));
                isConfirmationPage = changeemail != null;
            }

            return isConfirmationPage;
        }

        public bool IsPasswordArquisition(string pageSource)
        {
            var root = HtmlHelper.Root(pageSource);
            var form = FindForm(root, "passwordacquisitionqp");
            var isPasswordArq = form != null;
            return isPasswordArq;
        }

        /// <summary>
        /// Check a page is logged in or not by detecting the login form element
        /// </summary>
        /// <param name="pageSource"></param>
        /// <returns></returns>
        public bool IsMobileLoginPage(string pageSource)
        {
            var root = HtmlHelper.Root(pageSource);

            var form = MobileLoginGetForm(root);
            var email = root.ChildByName("email");
            var pass = root.ChildByName("pass");

            var isLogInForm = form != null && email != null && pass != null;

            return isLogInForm;
        }

        /// <summary>
        /// Get login form element from mobile login page.
        /// </summary>
        /// <returns></returns>
        public HtmlNode MobileLoginGetForm(HtmlNode root)
        {
            var form = root.ChildById("login_form") ?? FindForm(root, "facebook.com/login");
            return form;
        }

        public bool IsCheckPoint(string pageSource)
        {
            var root = HtmlHelper.Root(pageSource);

            return IsCheckPoint(root);
        }

        public bool IsCheckPoint(HtmlNode root)
        {
            var form = root.ChildClass("form", "checkpoint") ??
                       FindForm(root, "/checkpoint/block") ?? FindForm(root, "/login/checkpoint/");

            return form != null;
        }

        /// <summary>
        /// Check is mobile checkpoint page based on text message. Use with English interface only.
        /// </summary>
        /// <param name="pageSource"></param>
        /// <returns></returns>
        public bool MobileIsLocked(string pageSource)
        {
            string[] tokens =
            {
                "Help Secure Your Account", "Please Verify Your Identity", "Enter confirmation code",
                "working hard to make sure everyone on Facebook", "Phone Number Confirmation",
                "your account is temporarily locked", "We need to confirm your identity before you can log in"
            };

            var isSecurePage = pageSource.ContainsAny(tokens, StringComparison.InvariantCultureIgnoreCase);

            return isSecurePage;
        }

        public bool IsWizardPage(string pagSource)
        {
            var root = HtmlHelper.Root(pagSource);
            var links = root.ChildrenLink();
            var skipLink = links.FirstOrDefault(l => l.Href().Contains("/nux/wizard/") || l.Attribute("ajaxify").Contains("/nux/wizard/"));
            var isWizard = skipLink != null;

            return isWizard;
        }

        public bool IsMBasicEnglishInterface(string pageSource)
        {
            var root = HtmlHelper.Root(pageSource);
            return IsMBasicEnglishInterface(root);
        }

        private bool IsMBasicEnglishInterface(HtmlNode root)
        {
            var tokens = new[]
            {
                "Home", "Profile", "Messages", "Notifications", "Chat", "Menu"
            };

            var links = root.ChildrenLink();
            var english = tokens.All(t => links.Any(l => l.InnerText.Contains(t)));

            return english;
        }

        public CheckPointType DefineCheckPointType(string pageSource)
        {
            if (pageSource.Contains("Your account has been disabled"))
                return CheckPointType.Disabled;

            var root = HtmlHelper.Root(pageSource);
            var contact_point = root.ChildByName("contact_point");
            if (contact_point != null && contact_point.Attribute("type").Contains("number"))
                return CheckPointType.Phone;


            return CheckPointType.Unknown;
        }

        /// <summary>
        /// Parser user identity from Home page.
        /// </summary>
        public void MBasicParseUserIdentity(string content, out string userId, out string username)
        {
            userId = username = null;

            var root = HtmlHelper.Root(content);
            var form = FindForm(root, "composer/mbasic/?av=");
            if (form == null)
                return;

            var action = Domainer.HtmlDecode(form.Action());
            var url = FBCommon.BuildMobileLink(action);
            var av = Domainer.GetQueryValueFromUrl(url, "av");
            userId = av;

            string id;
            var profileLink = form.ParentNode.ChildrenLink().FirstOrDefault(l => l.Child("img", true, true, false) != null);
            ParseUserIdentityFromLinkNode(profileLink, out id, out username);

            if (username.IsNullOrWhiteSpace())
            {
                profileLink = root.ChildrenLink().FirstOrDefault(l => l.Href().Contains("v=info"));
                ParseUserIdentityFromLinkNode(profileLink, out id, out username);
            }
        }

        private void ParseUserIdentityFromLinkNode(HtmlNode link, out string userId, out string username)
        {
            userId = username = null;

            if (link != null)
            {
                var urlParser = new FacebookUrlParser();
                var userUrl = FBCommon.BuildMobileBasicLink(link.Href());
                urlParser.ParseUserLink(userUrl, out userId, out username);
                if (username == "profile_picture")
                    username = null;
            }
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
                queries = FindInput(form, hiddenOnly);
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

            var nodes3 = hiddenOnly
                ? node.Children("select", "type", "hidden")
                : node.Children("select", string.Empty, string.Empty);

            nodes.AddRange(nodes2);
            nodes.AddRange(nodes3);

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

            var nodes3 = hiddenOnly
                ? node.Children("select", "type", "hidden")
                : node.Children("select", string.Empty, string.Empty);

            nodes.AddRange(nodes2);
            nodes.AddRange(nodes3);

            nodes.ForEach(n =>
            {
                queries.Add(new KeyValuePair<string, string>(n.Name(), n.Value()));
            });

            return queries;
        }


        /// <summary>
        /// Check whether in checkpoint verification method selection page.
        /// </summary>
        /// <param name="pageSource"></param>
        /// <returns></returns>
        public bool MBasicIsCheckpointVerificationMethodSelectionPage(string pageSource)
        {
            var method = MBasicCheckpointGetMethodSelection(pageSource);
            return method != null;
        }

        public HtmlNode MBasicCheckpointGetMethodSelection(string pageSource)
        {
            var root = HtmlHelper.Root(pageSource);
            var method = root.ChildByName("verification_method");

            return method;
        }

        /// <summary>
        /// In verification method selection page => check has confirm birth date option.
        /// </summary>
        /// <param name="pageSource"></param>
        /// <returns></returns>
        public bool MBasicCheckpointMethodHasDOB(string pageSource)
        {
            return MBasicCheckPointNethodHasOption(pageSource, "2");
        }


        /// <summary>
        /// In verification method selection page => check has unlock method with option value.
        /// </summary>
        /// <param name="pageSource"></param>
        /// <param name="optionValue"></param>
        /// <returns></returns>
        private bool MBasicCheckPointNethodHasOption(string pageSource, string optionValue)
        {
            var root = HtmlHelper.Root(pageSource);
            var form = FindForm(root, "/checkpoint");
            if (form == null)
                return false;

            var queries = FormGetElementsBySibling(form);
            if (queries.ContainsKey("verification_method"))
            {
                var method = root.ChildByName("verification_method");
                var opt = method.Options().FirstOrDefault(o => o.Value() == optionValue);
                bool exist = (opt != null) && opt.Attribute("disabled") != "1";                    

                return exist;
            }

            return false;
        }
        

        /// <summary>
        /// Get error message after submit login on mobile
        /// </summary>
        /// <returns></returns>
        public string MobileLoginErrorMessage(string pageSource)
        {
            var root = HtmlHelper.Root(pageSource);
            var notices = root.ChildById("login-notices") ?? root.Child("div", "data-sigil", "m_login_notice");

            return notices?.InnerText.Trim() ?? string.Empty;
        }

        /// <summary>
        /// Check account disabled. Use English interface.
        /// </summary>
        /// <param name="pageSource"></param>
        /// <returns></returns>
        public bool IsDisabled(string pageSource)
        {
            string[] tokens = { "Your account has been disabled" };
            var isDisabled = pageSource.ContainsAny(tokens);

            return isDisabled;
        }

        /// <summary>
        /// Check whether current page is page to identify friend name by photos.
        /// </summary>
        /// <returns></returns>
        public bool MBasicIsFriendSelectionPage(string pageSource)
        {
            var root = HtmlHelper.Root(pageSource);
            var answer_choices = root.ChildByName("answer_choices");
            var isPage = answer_choices != null;

            return isPage;
        }

        public bool MBasicIsTrustedContactCodeSubmissionPage(string pageSource)
        {
            var root = HtmlHelper.Root(pageSource);
            var code = root.ChildByName("code_1");
            var isCode = code != null;
            return isCode;
        }
        
        public bool MBasicIsCheckpointPhotoUpload(string pageSource)
        {
            var iscp = pageSource.Contains("checkpoint/block") && pageSource.Contains("photo_input");

            return iscp;
        }

        /// <summary>
        /// Get photos from album. Photo detail url may not contain domain name.
        /// </summary>
        /// <param name="pageSource"></param>
        /// <returns></returns>
        public List<FacebookPhoto> MBasicParsePhotosFromAlbum(string pageSource)
        {
            var root = HtmlHelper.Root(pageSource);

            var qphotos = from lk in root.ChildrenLink()
                          let href = lk.Href()
                          where href.Contains("photo.php?")
                          let detailUrl = Domainer.HtmlDecode(href)
                          let image = lk.Child("img")
                          where image != null
                          let photoUrl = image.ImageSource()
                          where photoUrl.NotNullOrWhiteSpace()
                          let photoSource = Domainer.HtmlDecode(photoUrl)
                          select new FacebookPhoto { Url = photoSource, DetailUrl = detailUrl };

            var photos = qphotos.ToList();

            return photos;
        }

        /// <summary>
        /// Parse next page url from album. The url may not contain domain name.
        /// </summary>
        /// <param name="pageSource"></param>
        /// <returns></returns>
        public string MBasicPhotoAlbumParseNextPage(string pageSource)
        {
            var root = HtmlHelper.Root(pageSource);
            var more = root.ChildById("m_more_item");
            if (more == null)
                return null;

            var lk = more.ChildLink();
            if (lk == null)
                return null;

            var next = lk.Href();
            return next;
        }

        public bool MBasicCheckpointMethodHasIdentifyPhotosOfFriends(string pageSource)
        {
            return MBasicCheckPointNethodHasOption(pageSource, "3");
        }

        public CheckpointPhotosOfFriendsData MBasicParsePhotosOfFriendsCheckPointData(string content)
        {
            var root = HtmlHelper.Root(content);
            var image = root.Child(img => img.ImageSource().Contains("friend_name_image"), "img", string.Empty, string.Empty, true);
            if (image == null)
                return null;

            var imageUrl = Domainer.HtmlDecode(image.ImageSource());
            var answer_choices = root.ChildByName("answer_choices");
            var names = answer_choices.Options().Where(o => o.Value() != "-1").Select(o => o.OptionText()).Select(n => Domainer.HtmlDecode(n)).ToList();

            var data = new CheckpointPhotosOfFriendsData();
            data.FriendPhotosUrls.Add(imageUrl);
            data.FriendNames.AddRange(names);

            return data;
        }

        public bool MBasicIsPasswordChangingPage(string pageSource)
        {
            var isPasswordPage = pageSource.Contains("password_new") && pageSource.Contains("password_confirm");
            return isPasswordPage;
        }
    }
}