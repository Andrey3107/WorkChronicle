#pragma checksum "C:\Users\Andrey\source\repos\OnlineShop\Views\Catalog\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3df5db8511543ba5f2569fc3f7f775907857a4d1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Catalog_Index), @"mvc.1.0.view", @"/Views/Catalog/Index.cshtml")]
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
#line 1 "C:\Users\Andrey\source\repos\OnlineShop\Views\_ViewImports.cshtml"
using OnlineShop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Andrey\source\repos\OnlineShop\Views\_ViewImports.cshtml"
using OnlineShop.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3df5db8511543ba5f2569fc3f7f775907857a4d1", @"/Views/Catalog/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6d90dc07541a5c6d64c93e024fee34b85f8e0e69", @"/Views/_ViewImports.cshtml")]
    public class Views_Catalog_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<OnlineShop.Models.Product>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Andrey\source\repos\OnlineShop\Views\Catalog\Index.cshtml"
  
    ViewBag.Title = "Catalog";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<table class=\"table\">\r\n    <tr><th>Product Name</th><th>Cost</th><th></th></tr>\r\n");
#nullable restore
#line 8 "C:\Users\Andrey\source\repos\OnlineShop\Views\Catalog\Index.cshtml"
     foreach (var product in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>");
#nullable restore
#line 11 "C:\Users\Andrey\source\repos\OnlineShop\Views\Catalog\Index.cshtml"
           Write(product.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 12 "C:\Users\Andrey\source\repos\OnlineShop\Views\Catalog\Index.cshtml"
           Write(product.Cost);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>\r\n                <button class=\"btn btn-sm btn-primary\" data-product=\"");
#nullable restore
#line 14 "C:\Users\Andrey\source\repos\OnlineShop\Views\Catalog\Index.cshtml"
                                                                Write(product.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\">\r\n                    Order\r\n                </button>\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 19 "C:\Users\Andrey\source\repos\OnlineShop\Views\Catalog\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</table>

<script>
    $("".btn"").on(""click"", function (e) {
        $.ajax({
            url: ""Catalog/OrderProduct?id="" + $(e.target).data(""product""),
            type: ""GET"",
            contentType: ""application/json; charset=utf-8"",
            dataType: ""json"",
            success: (response) => {
                if (response.success) {
                    alert(""Product was successfuly ordered!"");
                }
                else {
                    alert(""Something went wront!"");
                }
            },
            error: () => alert(""Something went wront!"")
        });
    });
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<OnlineShop.Models.Product>> Html { get; private set; }
    }
}
#pragma warning restore 1591