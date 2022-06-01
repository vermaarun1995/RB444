#pragma checksum "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "399dd3ddd1ef5943358624e02bd11eb4a673118f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_UserProfile), @"mvc.1.0.view", @"/Views/User/UserProfile.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Projects\RB444\RB444.Admin\Views\_ViewImports.cshtml"
using RB444.Admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Projects\RB444\RB444.Admin\Views\_ViewImports.cshtml"
using RB444.Admin.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"399dd3ddd1ef5943358624e02bd11eb4a673118f", @"/Views/User/UserProfile.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a1871d6f29a24f2b7248b00157ae42a3fab81e50", @"/Views/_ViewImports.cshtml")]
    public class Views_User_UserProfile : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<RB444.Model.ViewModel.UserProfileVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml"
  
    ViewData["Title"] = "UserProfile";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>
    .profile-box {
        border: 1px solid #EBEBEB;
        border-radius: 8px;
        background: #fff;
    }

        .profile-box .box-headline {
            background: #273F51;
            color: #fff;
            padding: 5px 20px;
            border: 1px solid #EBEBEB;
            border-radius: 8px 8px 0 0;
        }

        .profile-box .profile-info {
            padding: 0 20px;
        }

    .row.info-block {
        padding: 10px 0;
        border-bottom: 2px solid #EBEBEB;
        align-items: center;
    }

        .row.info-block label {
            margin: 0;
        }

        .row.info-block:last-child {
            border: none;
        }
</style>

");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "399dd3ddd1ef5943358624e02bd11eb4a673118f4301", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 41 "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.UserId);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
<div class=""container"">
    <div class=""profile-box"">
        <div class=""box-headline"" ><h2 style=""color: #f3e8e8 !important;"">Account Details</h2></div>
        <div class=""profile-info"">
            <div class=""row info-block"">
                <div class=""col-2""><label>Name</label></div>
                <div class=""col-8"">");
#nullable restore
#line 48 "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml"
                              Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n            </div>\r\n            <div class=\"row info-block\">\r\n                <div class=\"col-2\"><label>Commission</label></div>\r\n                <div class=\"col-8\">");
#nullable restore
#line 52 "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml"
                              Write(Model.Commision);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
            </div>
            <div class=""row info-block"">
                <div class=""col-2""><label>Rolling Commission</label></div>
                <div class=""col-8"">
                    <input type=""checkbox"" style=""display:none"" id=""RollingCommission"" ");
#nullable restore
#line 57 "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml"
                                                                                   Write(Model.RollingCommission ? "checked" : string.Empty);

#line default
#line hidden
#nullable disable
            WriteLiteral(" />\r\n                    <i class=\"fas fa-eye update-rolling-commission show-btn\"></i>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 61 "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml"
             if (Model.IsAdmin)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <div class=""row info-block"">
                    <div class=""col-2""><label>Agent Rolling Commission</label></div>
                    <div class=""col-8"">
                        <input type=""checkbox"" style=""display:none"" id=""AgentRollingCommission"" ");
#nullable restore
#line 66 "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml"
                                                                                            Write(Model.AgentRollingCommission ? "checked" : string.Empty);

#line default
#line hidden
#nullable disable
            WriteLiteral(" />\r\n                        <i class=\"fas fa-eye update-rolling-commission show-btn\"></i>\r\n                    </div>\r\n                </div>\r\n");
#nullable restore
#line 70 "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"row info-block\">\r\n                <div class=\"col-2\"><label>Currency</label></div>\r\n                <div class=\"col-8\">IN</div>\r\n            </div>\r\n            <div class=\"row info-block\">\r\n");
#nullable restore
#line 76 "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml"
                 if (Model.IsAdmin)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"col-2\"><label>Rate</label></div>\r\n                    <div class=\"col-8\">20</div>\r\n");
#nullable restore
#line 80 "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"col-2\"><label>Exposure Limit</label></div>\r\n                    <div class=\"col-8\"><span>");
#nullable restore
#line 84 "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml"
                                        Write(Model.ExposureLimit);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><input type=\"text\" style=\"display:none\" id=\"ExposureLimit\"");
            BeginWriteAttribute("value", " value=\"", 3085, "\"", 3113, 1);
#nullable restore
#line 84 "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml"
WriteAttributeValue("", 3093, Model.ExposureLimit, 3093, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" /><i class=\"fas fa-edit ml-5 update-exposure-limit show-btn\"></i></div>\r\n");
#nullable restore
#line 85 "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n            <div class=\"row info-block\">\r\n                <div class=\"col-2\"><label>Mobile Number</label></div>\r\n                <div class=\"col-8\"><span>");
#nullable restore
#line 89 "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml"
                                    Write(Model.MobileNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><input type=\"text\" style=\"display:none\" id=\"MobileNumber\"");
            BeginWriteAttribute("value", " value=\"", 3464, "\"", 3491, 1);
#nullable restore
#line 89 "D:\Projects\RB444\RB444.Admin\Views\User\UserProfile.cshtml"
WriteAttributeValue("", 3472, Model.MobileNumber, 3472, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" /><i class=""fas fa-edit ml-5 update-mobile-number show-btn""></i></div>
            </div>
            <div class=""row info-block"">
                <div class=""col-2""><label>Password</label></div>
                <div class=""col-8""><span>*********</span><input type=""text"" style=""display:none"" id=""Password"" /><i class=""fas fa-edit ml-5 update-password show-btn""></i></div>
            </div>
        </div>
    </div>
</div>

");
            DefineSection("PageJs", async() => {
                WriteLiteral(@"
    <script>
        $(function () {
            $("".show-btn"").click(function () {
                $(this).parent().find(""input"").show().focus();
                $(this).hide();
                $(this).parent().find(""span"").hide();
            });

            //$("".info-block input"").on('keypress', function (e) {
            //    if (e.which == 13) {
            //        UpdateUserProfile($(this));
            //    }
            //});

            $("".info-block input"").focusout(function () {
                if (confirm(""Are you sure, you want to update user information?"")) {
                    UpdateUserProfile($(this));
                }
                else {
                    $(this).hide();
                    $(this).parent().find(""i"").show().focus();
                    $(this).hide();
                    $(this).parent().find(""span"").show();
                }
            });

            function UpdateUserProfile($this) {
                var id = $this.attr(""id"");");
                WriteLiteral(@"
                var value = $this.val();
                var userId = $(""#UserId"").val();

                var postData = {
                    ""id"": id,
                    ""value"": value,
                    ""userId"": userId
                };

                var url = ""/user/UpdateUserProfile"";

                $.post(url, postData, function (data) {
                    if (data.issuccess) {
                        toastr.success(data.message);
                        window.location.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                });
            }
        });
    </script>
");
            }
            );
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<RB444.Model.ViewModel.UserProfileVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
