#pragma checksum "C:\src\AppMvcCompletaAspNetCore\MinhaAppMvcCompleta\src\DevIO.App\Views\Shared\_PartialMensagens.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c00583d496fc66db062ebc27972eb20b87808925"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__PartialMensagens), @"mvc.1.0.view", @"/Views/Shared/_PartialMensagens.cshtml")]
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
#line 1 "C:\src\AppMvcCompletaAspNetCore\MinhaAppMvcCompleta\src\DevIO.App\Views\_ViewImports.cshtml"
using DevIO.App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\src\AppMvcCompletaAspNetCore\MinhaAppMvcCompleta\src\DevIO.App\Views\_ViewImports.cshtml"
using DevIO.App.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c00583d496fc66db062ebc27972eb20b87808925", @"/Views/Shared/_PartialMensagens.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3f4e717c4124c55203fe493fa8cdf7ca43650315", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__PartialMensagens : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\src\AppMvcCompletaAspNetCore\MinhaAppMvcCompleta\src\DevIO.App\Views\Shared\_PartialMensagens.cshtml"
 if (TempData["msg"] != null)
{


#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""alert alert-success alert-dismissible bg-success text-white border-0 fade show"" role=""alert"">
        <button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
            <span aria-hidden=""true"">&times;</span>
        </button>
        <strong>");
#nullable restore
#line 9 "C:\src\AppMvcCompletaAspNetCore\MinhaAppMvcCompleta\src\DevIO.App\Views\Shared\_PartialMensagens.cshtml"
           Write(TempData["msg"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>\r\n    </div>\r\n");
#nullable restore
#line 11 "C:\src\AppMvcCompletaAspNetCore\MinhaAppMvcCompleta\src\DevIO.App\Views\Shared\_PartialMensagens.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 13 "C:\src\AppMvcCompletaAspNetCore\MinhaAppMvcCompleta\src\DevIO.App\Views\Shared\_PartialMensagens.cshtml"
 if (TempData["erro"] != null)
{


#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""alert alert-danger alert-dismissible bg-danger text-white border-0 fade show"" role=""alert"">
        <button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
            <span aria-hidden=""true"">&times;</span>
        </button>
        <strong>");
#nullable restore
#line 20 "C:\src\AppMvcCompletaAspNetCore\MinhaAppMvcCompleta\src\DevIO.App\Views\Shared\_PartialMensagens.cshtml"
           Write(TempData["Erro"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>\r\n    </div>\r\n");
#nullable restore
#line 22 "C:\src\AppMvcCompletaAspNetCore\MinhaAppMvcCompleta\src\DevIO.App\Views\Shared\_PartialMensagens.cshtml"

}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
