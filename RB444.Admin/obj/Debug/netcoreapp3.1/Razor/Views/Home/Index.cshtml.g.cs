#pragma checksum "D:\Projects\RB444\RB444.Admin\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3819ffeb5f39ab5e3a79bea691714c50e46b7629"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "D:\Projects\RB444\RB444.Admin\Views\Home\Index.cshtml"
using RB444.Data.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3819ffeb5f39ab5e3a79bea691714c50e46b7629", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a1871d6f29a24f2b7248b00157ae42a3fab81e50", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Projects\RB444\RB444.Admin\Views\Home\Index.cshtml"
  
    ViewBag.Title = "Dashboard";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";   

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>
    table th, table td {
        border-top: none;
        padding: 3px 0 3px 10px;
        vertical-align: middle;
    }

    table tr {
        border-bottom: 1px solid #e0e6e6;
    }

    .count {
        width: 110px;
        text-align: center;
    }

    .back {
        background: #72bbef;
        color: #1e1e1e;
        padding: 2px 10px;
        font-weight: 700;
    }

    .count span {
        min-width: 50px;
        display: inline-flex;
    }

    .back, .lay {
        font-size: 11px;
    }

    .lay {
        background: #faa9ba;
        color: #1e1e1e;
        padding: 2px 10px;
        font-weight: 700;
    }
</style>

<div class=""container"">
    <h2>Welcome you are login as a ");
#nullable restore
#line 48 "D:\Projects\RB444\RB444.Admin\Views\Home\Index.cshtml"
                              Write(ViewBag.LoginUser.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral(".</h2>\r\n\r\n");
            WriteLiteral("</div>\r\n\r\n");
            DefineSection("PageJs", async() => {
                WriteLiteral(@"
    <script>
        $(function () {
            $(""#SportMenu li a"").click(function () {
                var id = $(this).attr(""data-id"");
                $(""tbody"").hide();
                $(""#"" + id).show();
            });
        });
    </script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
