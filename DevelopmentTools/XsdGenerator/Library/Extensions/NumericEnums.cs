using System;
using System.CodeDom;
using System.Collections.Generic;
using System.CodeDom.Compiler;

namespace Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions
{
    /// <summary>Enum items which's values are numeric will get same numeric values for enum members</summary>
    /// <remarks>
    /// This class implements CodeDOM-based post-processing extension for <see cref="XsdCodeGenerator"/> Visual Studio Custom Tool.
    /// To use it add a processing instruction to your XSD file.
    /// </remarks>
    /// <example>How to use this extension in XSD file.
    /// <code language="xml"><![CDATA[<?extension Tools.VisualStudioT.GeneratorsT.XsdGenerator.Extensions.NumericEnums?>]]></code></example>
    public class NumericEnums : ICodeExtension{
        /// <summary>Initializes the extension (this implementation does nothing)</summary>
        /// <param name="parameters">Initialization parameters (ignored)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Parameters</c> renamed to <c>parameters</c></version>
        void IExtensionBase.Initialize(System.Collections.Generic.IDictionary<string, string> parameters) { }
        /// <summary>Called when extension shall processs generated CodeDOM</summary>
        /// <param name="code">Object tree representing generated CodeDOM</param>
        /// <param name="schema">Input XML schema</param>
        /// <param name="provider">CodeDOM provider (the language)</param>
        /// <version version="1.5.3">Added documentation</version>
        /// <version version="1.5.3">Parameter <c>Provider</c> renamed to <c>provider</c></version>
        public void Process(System.CodeDom.CodeNamespace code, System.Xml.Schema.XmlSchema schema, CodeDomProvider provider)
        {
            foreach ( CodeTypeDeclaration type in code.Types ){
                ParseType(type);
            }
        }
        private void ParseType(CodeTypeDeclaration  type) {
            if (type.IsEnum) ParseEnum(type);
            else
                foreach (CodeTypeMember member in type.Members)
                    if (member is CodeTypeDeclaration)
                        ParseType((CodeTypeDeclaration)member);
        }
        private void ParseEnum(CodeTypeDeclaration type) {
            foreach(CodeTypeMember member in type.Members)
                if (member is CodeMemberField) {
                    CodeMemberField field = (CodeMemberField)member;
                    if (/*(field.Attributes & MemberAttributes.Static) == MemberAttributes.Static && (field.Attributes & MemberAttributes.Const) == MemberAttributes.Const &&*/
                        field.CustomAttributes != null && field.InitExpression==null) {
                        foreach (CodeAttributeDeclaration attr in field.CustomAttributes) {
                            int attrval;
                            if (attr.AttributeType.BaseType == "System.Xml.Serialization.XmlEnumAttribute" && attr.Arguments[0].Value is CodePrimitiveExpression && ((CodePrimitiveExpression)attr.Arguments[0].Value).Value is string && int.TryParse((string)((CodePrimitiveExpression)attr.Arguments[0].Value).Value, out attrval))
                                field.InitExpression = new CodePrimitiveExpression(attrval);
                        }
                    }
                }
        }
    }
}
