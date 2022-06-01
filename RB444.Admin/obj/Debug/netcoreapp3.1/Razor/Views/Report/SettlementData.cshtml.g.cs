#pragma checksum "D:\Projects\RB444\RB444.Admin\Views\Report\SettlementData.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "12d6996d9915958901c32408dd3aea2f1d82ba0b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Report_SettlementData), @"mvc.1.0.view", @"/Views/Report/SettlementData.cshtml")]
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
#nullable restore
#line 2 "D:\Projects\RB444\RB444.Admin\Views\Report\SettlementData.cshtml"
using RB444.Data.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"12d6996d9915958901c32408dd3aea2f1d82ba0b", @"/Views/Report/SettlementData.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a1871d6f29a24f2b7248b00157ae42a3fab81e50", @"/Views/_ViewImports.cshtml")]
    public class Views_Report_SettlementData : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<RB444.Data.Entities.Bets>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_SettlementList", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/custom.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\Projects\RB444\RB444.Admin\Views\Report\SettlementData.cshtml"
  
    Layout = "~/Views/Shared/_AdminLayout.cshtml";

    var sportsList = ViewBag.SportsList as List<Sports>;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container"">
    <h2>Settlement Data</h2>
    <div class=""row p-4 form-group"">
        <div class=""col-md-3"">
            <label class=""control-label"">Sport Name</label>

            <select class=""form-control"" id=""ddlSportName"" onchange=""getSeriesName()"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "12d6996d9915958901c32408dd3aea2f1d82ba0b5054", async() => {
                WriteLiteral("Please Select Sport");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 17 "D:\Projects\RB444\RB444.Admin\Views\Report\SettlementData.cshtml"
                 foreach (var item in sportsList)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "12d6996d9915958901c32408dd3aea2f1d82ba0b6493", async() => {
#nullable restore
#line 19 "D:\Projects\RB444\RB444.Admin\Views\Report\SettlementData.cshtml"
                                        Write(item.SportName);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 19 "D:\Projects\RB444\RB444.Admin\Views\Report\SettlementData.cshtml"
                       WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 20 "D:\Projects\RB444\RB444.Admin\Views\Report\SettlementData.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </select>
        </div>
        <div class=""col-md-3"">
            <label class=""control-label"">Event Name</label>
            <select class=""form-control"" id=""ddlSeries"">
            </select>

        </div>

        <div class=""col-md-3 mt-auto"">
            <button class=""btn btn-primary"" type=""button"" onclick=""SearchBetData()"">Search</button>
        </div>
    </div>
    <div id=""searchList"">
        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "12d6996d9915958901c32408dd3aea2f1d82ba0b8907", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 35 "D:\Projects\RB444\RB444.Admin\Views\Report\SettlementData.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("PageJs", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "12d6996d9915958901c32408dd3aea2f1d82ba0b10584", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    <script type=\"text/javascript\">\r\n        function getSeriesName() {\r\n            const formData = new FormData();\r\n            formData.append(\"SportId\", $(\'#ddlSportName\').val());\r\n\r\n            let getEventJson = {\r\n                apiUrl: \"");
#nullable restore
#line 47 "D:\Projects\RB444\RB444.Admin\Views\Report\SettlementData.cshtml"
                    Write(Url.Action("GetBetEventData", "Report"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                postData: formData
            }
            $("".loader"").show();
            promiseAjaxPost.call(getEventJson, false).then(
                (res) => {
                    $("".loader"").hide();;
                    $('#ddlSeries').find('option').remove().end();
                    if (res.length>0) {
                        $.each(res, function (key, value) {
                            $('#ddlSeries').append($(""<option></option>"").val(value.eventId).html(value.event));
                        });
                    }
                    else {
                       
                        $('#ddlSeries').append($(""<option></option>"").val(0).html('No Event'));
                    }
                },
                (err) => {
                    messagePopup.error(err.statusText);
                }
            )
        }

        function SearchBetData() {
            const formData = new FormData();
            formData.append(""EventId"", $('#ddlSeries').val()");
                WriteLiteral(");\r\n\r\n            let betListJson = {\r\n                apiUrl: \"");
#nullable restore
#line 76 "D:\Projects\RB444\RB444.Admin\Views\Report\SettlementData.cshtml"
                    Write(Url.Action("GetSettlementDataList", "Report"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                postData: formData,
                dataType: 'html'
            }
            $("".loader"").show();
            promiseAjaxPost.call(betListJson, false).then(
                (res) => {
                    $("".loader"").hide();;
                    $(""#searchList"").html(res);
                },
                (err) => {
                    messagePopup.error(err.statusText);
                }
            )
        }
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<RB444.Data.Entities.Bets>> Html { get; private set; }
    }
}
#pragma warning restore 1591
