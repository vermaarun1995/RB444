#pragma checksum "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "64302c3db0192bd16fb4841b4d27642c36d822dd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_OtherSetting_LogoImages), @"mvc.1.0.view", @"/Views/OtherSetting/LogoImages.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"64302c3db0192bd16fb4841b4d27642c36d822dd", @"/Views/OtherSetting/LogoImages.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a1871d6f29a24f2b7248b00157ae42a3fab81e50", @"/Views/_ViewImports.cshtml")]
    public class Views_OtherSetting_LogoImages : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<RB444.Data.Entities.Logo>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/common/UserProfile.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("account-upload-img"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("image-input-wrapper"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("profile image"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("request-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("autocomplete", new global::Microsoft.AspNetCore.Html.HtmlString("off"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/custom.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml"
  
    ViewData["Title"] = "LogoImages";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""container"">
    <h2>Logo Images</h2>
    <div class=""row p-4"">
        <button class=""btn btn-primary btn-sm w-auto"" onclick=""getRequest()"">Add Logo</button>
    </div>
    <table class=""sportesTbl table table-striped table-dark"">
        <thead>
            <tr>
                <th>S NO.</th>
                <th>Image</th>
                <th>ACTION</th>
                <th></th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 21 "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml"
             if (Model != null)
            {
                int i = 1;
                foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <th scope=\"row\">");
#nullable restore
#line 27 "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml"
                                   Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                        <td><img style=\"height:50px; width:100px;\"");
            BeginWriteAttribute("src", " src=\"", 887, "\"", 910, 2);
            WriteAttributeValue("", 893, "../", 893, 3, true);
#nullable restore
#line 28 "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml"
WriteAttributeValue("", 896, item.FilePath, 896, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" /></td>\r\n");
#nullable restore
#line 29 "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml"
                         if (item.Status == true)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <td");
            BeginWriteAttribute("id", " id=\"", 1030, "\"", 1050, 2);
            WriteAttributeValue("", 1035, "Status_", 1035, 7, true);
#nullable restore
#line 31 "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml"
WriteAttributeValue("", 1042, item.Id, 1042, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"color:lawngreen;\">\r\n                                <a");
            BeginWriteAttribute("onclick", " onclick=\"", 1113, "\"", 1151, 3);
            WriteAttributeValue("", 1123, "statusChange(", 1123, 13, true);
#nullable restore
#line 32 "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml"
WriteAttributeValue("", 1136, item.Id, 1136, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1144, ",false)", 1144, 7, true);
            EndWriteAttribute();
            WriteLiteral(">Active</a>\r\n                            </td>\r\n");
#nullable restore
#line 34 "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <td style=\"color:orangered;\">\r\n                                <a");
            BeginWriteAttribute("onclick", " onclick=\"", 1377, "\"", 1414, 3);
            WriteAttributeValue("", 1387, "statusChange(", 1387, 13, true);
#nullable restore
#line 38 "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml"
WriteAttributeValue("", 1400, item.Id, 1400, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1408, ",true)", 1408, 6, true);
            EndWriteAttribute();
            WriteLiteral(">DeActive</a>\r\n                            </td>\r\n");
#nullable restore
#line 40 "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                     \r\n                        <td>\r\n                            <a");
            BeginWriteAttribute("onclick", " onclick=\"", 1575, "\"", 1608, 3);
            WriteAttributeValue("", 1585, "deleteRequest(", 1585, 14, true);
#nullable restore
#line 43 "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml"
WriteAttributeValue("", 1599, item.Id, 1599, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1607, ")", 1607, 1, true);
            EndWriteAttribute();
            WriteLiteral("><i class=\"fa fa-trash\"></i></a>\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 46 "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml"
                    i++;
                }
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        </tbody>
    </table>
</div>

<div class=""all-modals"">
    <div class=""modal fade"" id=""registerModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""registerModal"" aria-hidden=""true"">
        <div class=""modal-dialog modal-dialog-centered"" role=""document"">
            <div class=""modal-content"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "64302c3db0192bd16fb4841b4d27642c36d822dd12030", async() => {
                WriteLiteral(@"
                    <div class=""modal-header"">
                        <h5 class=""modal-title"" id=""exampleModalLongTitle"">Logo Image</h5>
                        <button type=""button"" class=""close"" onclick=""closeModel();"">
                            ×
                        </button>
                    </div>
                    <div class=""modal-body"">
                        <div class=""form-horizontal"">
                            <div class=""error"" style=""display:none;"" id=""error""></div>
                            <div class=""image-input image-input-empty image-input-outline"">
                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "64302c3db0192bd16fb4841b4d27642c36d822dd12946", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n                                <label class=\"btn btn-xs btn-icon btn-circle btn-white btn-hover-text-primary btn-shadow\" data-action=\"change\" data-toggle=\"tooltip\"");
                BeginWriteAttribute("title", " title=\"", 3068, "\"", 3076, 0);
                EndWriteAttribute();
                WriteLiteral(@" data-original-title=""Change avatar"">
                                    <i class=""fa fa-pen icon-sm text-muted""></i>
                                    <input type=""file"" id=""ProfileImage"" name=""file"" hidden accept=""image/*"" />
                                </label>                              
                                <span class=""btn btn-xs btn-icon btn-circle btn-white btn-hover-text-primary btn-shadow resetBtn"" data-action=""remove"" data-toggle=""tooltip"" title=""Remove avatar"">
                                    <i class=""fa fa-times ""></i>
                                </span>
                            </div>
                            <div class=""form-group d-flex justify-content-between align-items-center mb-75"">
                                <div class=""form-group d-flex justify-content-between align-items-center mb-75"">
                                    <div class=""col-md-12 mt-10 d-flex"">
                                        <input type=""checkbox"" id=""Status"" style");
                WriteLiteral(@"=""width: 20px;height: 20px;"">
                                        <label class=""form-label ml-2"" style=""font-size: 20px!important;"">Status</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class=""modal-footer"">
                        <button type=""button"" id=""cancle"" class=""btn btn-light"" onclick=""closeModel();"">Cancel</button>
                        <button type=""button"" id=""btn-save"" class=""btn btn-primary font-weight-bold"" onclick=""return saveRequest()"">Save</button>
                    </div>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n");
            DefineSection("PageJs", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "64302c3db0192bd16fb4841b4d27642c36d822dd18025", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
    <script type=""text/javascript"">
        $(document).ready(function () {
            var accountUploadImg = $('#account-upload-img');
            var accountUploadBtn = $('#ProfileImage');

            // Update user photo on click of button
            if (accountUploadBtn) {
                accountUploadBtn.on('change', function (e) {
                    var reader = new FileReader(),
                        files = e.target.files;
                    reader.onload = function () {
                        if (accountUploadImg) {
                            accountUploadImg.attr('src', reader.result);
                        }
                    };
                    reader.readAsDataURL(files[0]);
                });
            }
            // Reset Photo
            $("".resetBtn"").click(function () {
                $(""#ProfileImage"").val("""");
                accountUploadImg.attr('src', '/common/UserProfile.png');
            });
        });

        function getRequest() ");
                WriteLiteral(@"{
            $('#registerModal').modal('show');
        }

        function closeModel() {
            $('#registerModal').modal('hide');
        }

        function saveRequest() {
            let formdata = new FormData();
            let fileList = document.getElementById(""ProfileImage"").files;
            if (fileList.length > 0) {
                for (const file of fileList) {
                    formdata.append(file.name, file);
                }
                formdata.append(""Status"", $(""#Status"").prop(""checked""));
                let uploadAttachmentJson = {
                    apiUrl: """);
#nullable restore
#line 144 "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml"
                        Write(Url.Action("AddUpdatedLogoImages", "OtherSetting"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                    postData: formdata
                }
                $("".loader"").show();
                promiseAjaxPost.call(uploadAttachmentJson, false).then(
                    (res) => {
                        $("".loader"").hide();;
                        if (res.IsSuccess && res.Status == 200) {
                            messagePopup.responseSuccess(res.Message).then(
                                (r) => { if (r == true) { location.reload() } }
                            )
                        } else {
                            messagePopup.responseError(res.Message);
                        }
                    },
                    (err) => {
                        messagePopup.error(err.statusText);
                    }
                )
            }
            else {
                messagePopup.responseError(""Please upload Image."");
            }
        }

        function deleteRequest(id) {
            const formData = new FormData();
         ");
                WriteLiteral("   formData.append(\"id\", id);\r\n\r\n            let addRequestJson = {\r\n                apiUrl: \"");
#nullable restore
#line 174 "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml"
                    Write(Url.Action("DeleteLogo", "OtherSetting"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                postData: formData
            }
            $("".loader"").show();
            promiseAjaxPost.call(addRequestJson, false).then(
                (res) => {
                    $("".loader"").hide();;
                    if (res.IsSuccess && res.Status == 200) {
                        messagePopup.responseSuccess(res.Message).then(
                            (r) => { if (r == true) { location.reload(); } }
                        )
                    } else if (res.IsSuccess && res.Status == 208) {
                        messagePopup.responseWarning(res.Message);
                    } else {
                        messagePopup.responseError(res.Message);
                    }
                },
                (err) => {
                    messagePopup.error(err.statusText);
                }
            )
            return false;
        }

        function statusChange(id, status) {
            if (id == null || id == 0) {
                return false;
      ");
                WriteLiteral("      }\r\n            const formData = new FormData();\r\n            formData.append(\"id\", id);\r\n             formData.append(\"status\", status);\r\n             formData.append(\"api\", 1);\r\n\r\n            let addRequestJson = {\r\n                apiUrl: \"");
#nullable restore
#line 208 "D:\Projects\RB444\RB444.Admin\Views\OtherSetting\LogoImages.cshtml"
                    Write(Url.Action("ChangeStatus", "OtherSetting"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                postData: formData
            }
            $("".loader"").show();
            promiseAjaxPost.call(addRequestJson, false).then(
                (res) => {
                    $("".loader"").hide();;
                    if (res.IsSuccess && res.Status == 200) {
                        messagePopup.responseSuccess(res.Message).then(
                            (r) => { if (r == true) { location.reload(); } }
                        )
                    } else if (res.IsSuccess && res.Status == 208) {
                        messagePopup.responseWarning(res.Message);
                    } else {
                        messagePopup.responseError(res.Message);
                    }
                },
                (err) => {
                    messagePopup.error(err.statusText);
                }
            )
            return false;
        }

    </script>
");
            }
            );
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<RB444.Data.Entities.Logo>> Html { get; private set; }
    }
}
#pragma warning restore 1591