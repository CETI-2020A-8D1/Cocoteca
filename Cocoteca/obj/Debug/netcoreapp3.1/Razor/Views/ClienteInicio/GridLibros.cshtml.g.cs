#pragma checksum "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\ClienteInicio\GridLibros.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c56e92b260dced10004b90ab4cb97f0e54cc3392"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ClienteInicio_GridLibros), @"mvc.1.0.view", @"/Views/ClienteInicio/GridLibros.cshtml")]
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
#line 1 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\_ViewImports.cshtml"
using Cocoteca;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\_ViewImports.cshtml"
using Cocoteca.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\ClienteInicio\GridLibros.cshtml"
using Cocoteca.Models.Cliente.Equipo1;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c56e92b260dced10004b90ab4cb97f0e54cc3392", @"/Views/ClienteInicio/GridLibros.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7008286320c2f7fc57de55478842c73248a66509", @"/Views/_ViewImports.cshtml")]
    public class Views_ClienteInicio_GridLibros : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-comprar"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Equipo_Tripas", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Libro_Vista", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\ClienteInicio\GridLibros.cshtml"
  
    ViewData["Title"] = ViewBag.Categoria.nombre;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<main>\r\n    <section class=\"jumbotron bg-white text-center mb-0\">\r\n        <h1 class=\"display-4\">");
#nullable restore
#line 9 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\ClienteInicio\GridLibros.cshtml"
                         Write(ViewBag.Categoria.nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n    </section>\r\n\r\n    <div class=\"py-2 bg-light\">\r\n        <div class=\"container py-4\" style=\"background-color:#FFD1BD\">\r\n");
#nullable restore
#line 14 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\ClienteInicio\GridLibros.cshtml"
             if (ViewBag.Libros.Count > 0)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"row py-4\">\r\n");
#nullable restore
#line 17 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\ClienteInicio\GridLibros.cshtml"
                     foreach (MtoCatLibroItem libro in ViewBag.Libros)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"col-sm-6 col-md-4\">\r\n                            <div class=\"card mb-4\">\r\n\r\n                                <img");
            BeginWriteAttribute("src", " src=\"", 696, "\"", 715, 1);
#nullable restore
#line 22 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\ClienteInicio\GridLibros.cshtml"
WriteAttributeValue("", 702, libro.imagen, 702, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-img-top\"");
            BeginWriteAttribute("alt", " alt=\"", 737, "\"", 756, 1);
#nullable restore
#line 22 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\ClienteInicio\GridLibros.cshtml"
WriteAttributeValue("", 743, libro.titulo, 743, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"50px\" height=\"400px\">\r\n                                <div class=\"card-body\">\r\n                                    <h4 class=\"card-title\"> ");
#nullable restore
#line 24 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\ClienteInicio\GridLibros.cshtml"
                                                       Write(libro.titulo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                                    <h5 class=\"card-text\"> ");
#nullable restore
#line 25 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\ClienteInicio\GridLibros.cshtml"
                                                      Write(libro.autor);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                                    <h5 class=\"card-text\"> Precio: </h5>\r\n                                    <h5 class=\"card-text\"> ");
#nullable restore
#line 27 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\ClienteInicio\GridLibros.cshtml"
                                                      Write(libro.precio);

#line default
#line hidden
#nullable disable
            WriteLiteral(" cocoins</h5>\r\n                                    <div class=\"d-flex justify-content-between align-items-center\">\r\n                                        <div class=\"btn-group\">\r\n                                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c56e92b260dced10004b90ab4cb97f0e54cc33927962", async() => {
                WriteLiteral("Ver Libro");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 30 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\ClienteInicio\GridLibros.cshtml"
                                                                                                                                 WriteLiteral(libro.idlibro);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                            <button class=""btn btn-sm btn-outline-secondary"" style=""background-color:#FCA12D"">Agregar a Carrito</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
");
#nullable restore
#line 37 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\ClienteInicio\GridLibros.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n");
#nullable restore
#line 40 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\ClienteInicio\GridLibros.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <p>No hay libros</p>\r\n");
#nullable restore
#line 44 "C:\Users\edalf\Source\Repos\Cocoteca\Cocoteca\Views\ClienteInicio\GridLibros.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</main>\r\n\r\n");
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
