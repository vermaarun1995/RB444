#pragma checksum "D:\Projects\RB444\RB444.Admin\Views\Report\RollingCommision.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8acf4b283ed3161b242846dd594d481e2ffcfa61"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Report_RollingCommision), @"mvc.1.0.view", @"/Views/Report/RollingCommision.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8acf4b283ed3161b242846dd594d481e2ffcfa61", @"/Views/Report/RollingCommision.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a1871d6f29a24f2b7248b00157ae42a3fab81e50", @"/Views/_ViewImports.cshtml")]
    public class Views_Report_RollingCommision : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<RB444.Model.ViewModel.RollingCommisionVM>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Projects\RB444\RB444.Admin\Views\Report\RollingCommision.cshtml"
  
    ViewData["Title"] = "RollingCommision";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""card card-custom"">
    <div class=""card-header flex-wrap py-5"">
        <div class=""card-title"">
            <h3 class=""card-label"">
                Rolling Commision
            </h3>
        </div>

    </div>
    <div class=""card-body"">
        <!--begin: Datatable-->
        <table class=""table table-striped table-hover table-bordered table-head-custom"" id=""kt_datatable"">
            <thead style=""background-color: #1e1e2d;"">
                <tr>
                    <th>
                        Id
                    </th>
                    <th>
                        Fancy
                    </th>
                    <th>
                        Matka
                    </th>
                    <th>
                        Casino
                    </th>
                    <th>
                        Binary
                    </th>
                    <th>
                        Sport Book
                    </th>
                    <th>
       ");
            WriteLiteral("                 Bookmaker\r\n                    </th>\r\n                    <th>\r\n                        From -> To\r\n                    </th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n");
#nullable restore
#line 48 "D:\Projects\RB444\RB444.Admin\Views\Report\RollingCommision.cshtml"
                 if (Model != null && Model.Count() > 0)
                {
                    foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 53 "D:\Projects\RB444\RB444.Admin\Views\Report\RollingCommision.cshtml"
                           Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 54 "D:\Projects\RB444\RB444.Admin\Views\Report\RollingCommision.cshtml"
                           Write(item.Fancy);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 55 "D:\Projects\RB444\RB444.Admin\Views\Report\RollingCommision.cshtml"
                           Write(item.Matka);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 56 "D:\Projects\RB444\RB444.Admin\Views\Report\RollingCommision.cshtml"
                           Write(item.Casino);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 57 "D:\Projects\RB444\RB444.Admin\Views\Report\RollingCommision.cshtml"
                           Write(item.Binary);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 58 "D:\Projects\RB444\RB444.Admin\Views\Report\RollingCommision.cshtml"
                           Write(item.SportBook);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 59 "D:\Projects\RB444\RB444.Admin\Views\Report\RollingCommision.cshtml"
                           Write(item.Bookmaker);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 60 "D:\Projects\RB444\RB444.Admin\Views\Report\RollingCommision.cshtml"
                           Write(item.FromUserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("<b> -> </b>");
#nullable restore
#line 60 "D:\Projects\RB444\RB444.Admin\Views\Report\RollingCommision.cshtml"
                                                        Write(item.ToUserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n");
#nullable restore
#line 62 "D:\Projects\RB444\RB444.Admin\Views\Report\RollingCommision.cshtml"
                    }
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<RB444.Model.ViewModel.RollingCommisionVM>> Html { get; private set; }
    }
}
#pragma warning restore 1591
