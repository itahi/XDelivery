//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18063
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XamlStaticHelperNamespace {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XamlBuildTask", "4.0.0.0")]
    internal class _XamlStaticHelper {
        
        private static System.WeakReference schemaContextField;
        
        private static System.Collections.Generic.IList<System.Reflection.Assembly> assemblyListField;
        
        internal static System.Xaml.XamlSchemaContext SchemaContext {
            get {
                System.Xaml.XamlSchemaContext xsc = null;
                if ((schemaContextField != null)) {
                    xsc = ((System.Xaml.XamlSchemaContext)(schemaContextField.Target));
                    if ((xsc != null)) {
                        return xsc;
                    }
                }
                if ((AssemblyList.Count > 0)) {
                    xsc = new System.Xaml.XamlSchemaContext(AssemblyList);
                }
                else {
                    xsc = new System.Xaml.XamlSchemaContext();
                }
                schemaContextField = new System.WeakReference(xsc);
                return xsc;
            }
        }
        
        internal static System.Collections.Generic.IList<System.Reflection.Assembly> AssemblyList {
            get {
                if ((assemblyListField == null)) {
                    assemblyListField = LoadAssemblies();
                }
                return assemblyListField;
            }
        }
        
        private static System.Collections.Generic.IList<System.Reflection.Assembly> LoadAssemblies() {
            System.Collections.Generic.IList<System.Reflection.Assembly> assemblyList = new System.Collections.Generic.List<System.Reflection.Assembly>();
            assemblyList.Add(Load("Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a" +
                        "3a"));
            assemblyList.Add(Load("Microsoft.VisualBasic.Compatibility.Data, Version=10.0.0.0, Culture=neutral, Publ" +
                        "icKeyToken=b03f5f7f11d50a3a"));
            assemblyList.Add(Load("Microsoft.VisualBasic.Compatibility, Version=10.0.0.0, Culture=neutral, PublicKey" +
                        "Token=b03f5f7f11d50a3a"));
            assemblyList.Add(Load("Microsoft.VisualBasic, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f" +
                        "11d50a3a"));
            assemblyList.Add(Load("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
            assemblyList.Add(Load("System.ComponentModel.DataAnnotations, Version=4.0.0.0, Culture=neutral, PublicKe" +
                        "yToken=31bf3856ad364e35"));
            assemblyList.Add(Load("System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11" +
                        "d50a3a"));
            assemblyList.Add(Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
            assemblyList.Add(Load("System.Data.DataSetExtensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b" +
                        "77a5c561934e089"));
            assemblyList.Add(Load("System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
            assemblyList.Add(Load("System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934" +
                        "e089"));
            assemblyList.Add(Load("System.Data.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0" +
                        "89"));
            assemblyList.Add(Load("System.Deployment, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50" +
                        "a3a"));
            assemblyList.Add(Load("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
            assemblyList.Add(Load("System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" +
                        ""));
            assemblyList.Add(Load("System.Printing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e3" +
                        "5"));
            assemblyList.Add(Load("System.Runtime.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b7" +
                        "7a5c561934e089"));
            assemblyList.Add(Load("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c56193" +
                        "4e089"));
            assemblyList.Add(Load("System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f1" +
                        "1d50a3a"));
            assemblyList.Add(Load("System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"));
            assemblyList.Add(Load("System.Web.Services, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d" +
                        "50a3a"));
            assemblyList.Add(Load("System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c5619" +
                        "34e089"));
            assemblyList.Add(Load("System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
            assemblyList.Add(Load("System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e08" +
                        "9"));
            assemblyList.Add(Load("CrystalDecisions.CrystalReports.Engine, Version=13.0.2000.0, Culture=neutral, Pub" +
                        "licKeyToken=692fbea5521e1304"));
            assemblyList.Add(Load("CrystalDecisions.ReportSource, Version=13.0.2000.0, Culture=neutral, PublicKeyTok" +
                        "en=692fbea5521e1304"));
            assemblyList.Add(Load("CrystalDecisions.Shared, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692" +
                        "fbea5521e1304"));
            assemblyList.Add(Load("CrystalDecisions.Windows.Forms, Version=13.0.2000.0, Culture=neutral, PublicKeyTo" +
                        "ken=692fbea5521e1304"));
            assemblyList.Add(Load("FlashControlV71, Version=1.0.3187.32366, Culture=neutral, PublicKeyToken=692fbea5" +
                        "521e1304"));
            assemblyList.Add(Load("Microsoft.QualityTools.Testing.Fakes, Version=11.0.0.0, Culture=neutral, PublicKe" +
                        "yToken=b03f5f7f11d50a3a"));
            assemblyList.Add(Load("Microsoft.SqlServer.ConnectionInfo, Version=11.0.0.0, Culture=neutral, PublicKeyT" +
                        "oken=89845dcd8080cc91"));
            assemblyList.Add(Load("Microsoft.SqlServer.ManagedConnections, Version=11.0.0.0, Culture=neutral, Public" +
                        "KeyToken=89845dcd8080cc91"));
            assemblyList.Add(Load("Microsoft.SqlServer.Management.Collector, Version=11.0.0.0, Culture=neutral, Publ" +
                        "icKeyToken=89845dcd8080cc91"));
            assemblyList.Add(Load("Microsoft.SqlServer.Management.Sdk.Sfc, Version=11.0.0.0, Culture=neutral, Public" +
                        "KeyToken=89845dcd8080cc91"));
            assemblyList.Add(Load("Microsoft.SqlServer.Management.Utility, Version=11.0.0.0, Culture=neutral, Public" +
                        "KeyToken=89845dcd8080cc91"));
            assemblyList.Add(Load("Microsoft.SqlServer.Management.UtilityEnum, Version=11.0.0.0, Culture=neutral, Pu" +
                        "blicKeyToken=89845dcd8080cc91"));
            assemblyList.Add(Load("Microsoft.SqlServer.Smo, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845d" +
                        "cd8080cc91"));
            assemblyList.Add(Load("Microsoft.SqlServer.SmoExtended, Version=11.0.0.0, Culture=neutral, PublicKeyToke" +
                        "n=89845dcd8080cc91"));
            assemblyList.Add(Load("Microsoft.VisualBasic.PowerPacks.Vs, Version=10.0.0.0, Culture=neutral, PublicKey" +
                        "Token=b03f5f7f11d50a3a"));
            assemblyList.Add(System.Reflection.Assembly.GetExecutingAssembly());
            return assemblyList;
        }
        
        private static System.Reflection.Assembly Load(string assemblyNameVal) {
            System.Reflection.AssemblyName assemblyName = new System.Reflection.AssemblyName(assemblyNameVal);
            byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
            System.Reflection.Assembly asm = null;
            try {
                asm = System.Reflection.Assembly.Load(assemblyName.FullName);
            }
            catch (System.Exception ) {
                System.Reflection.AssemblyName shortName = new System.Reflection.AssemblyName(assemblyName.Name);
                if ((publicKeyToken != null)) {
                    shortName.SetPublicKeyToken(publicKeyToken);
                }
                asm = System.Reflection.Assembly.Load(shortName);
            }
            return asm;
        }
    }
}