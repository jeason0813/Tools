using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace Tools.VisualStudioT.GeneratorsT.ResXFileGenerator
{
    /// <summary>Provides support for strongly-typed resources. This class cannot be inherited.</summary>
    /// <version version="1.5.3.">This class is new in version 1.5.3. It was mooved from namespace <c>DMKSoftware.CodeGenerators</c>.</version>
    /// <version version="1.5.3">Modules are generated for Visual Basic. For other languages class constructor is made <see langword="private"/> (previously <see langword="internal"/>).</version>
    /// <version version="1.5.3">Members of resource class (module for VB) are always <see langword="public"/>, even when the class (module) is <see langword="internal"/>.</version>
    public static class StronglyTypedResourceBuilderEx
    {
        private const string CultureInfoFieldName = "_resourceCulture";
		private const string InternalSyncObjectFieldName = "_internalSyncObject";
        private const string CultureInfoPropertyName = "Culture";
        private const int DocCommentLengthThreshold = 0x200;
        private const string DocCommentSummaryStart = "<summary>";
        private const string DocCommentSummaryEnd = "</summary>";
        private const char ReplacementChar = '_';
        private const string ResMgrFieldName = "_resourceManager";
        private const string ResMgrPropertyName = "ResourceManager";
		private const string InternalSyncObjectPropertyName = "InternalSyncObject";
        private const string FormatSuffix = "Format";
        private static readonly  string MismatchedResourceName = Resources.MismatchedResourceName;
        private static readonly string StringTruncatedComment = Resources.StringTruncatedComment;
        private static readonly string StringPropertyComment = Resources.StringPropertyComment;
        private static readonly string NonStringPropertyComment = Resources.NonStringPropertyComment;
        private static readonly string FormatMethodComment = Resources.FormatMethodComment;
        private static readonly string FormatStubMethodComment = Resources.FormatStubMethodComment;
        private static readonly string ClassComments1Format = Resources.ClassComments1Format;
        private static readonly string ClassComments2Format = Resources.ClassComments2Format;
        private static readonly string ClassCommentsCopyright = Resources.ClassCommentsCopyright;
        private static readonly string ResMgrPropertyComment = Resources.ResMgrPropertyComment;
        private static readonly string ParamCommentStatement = Resources.ParamCommentStatement;
        private static readonly string MethodReturnValueComment = Resources.MethodReturnValueComment;
        private static readonly string StubMethodReturnValueComment = Resources.StubMethodReturnValueComment;
        private static readonly string CulturePropertyComment1 = Resources.CulturePropertyComment1;
        private static readonly string CulturePropertyComment2 = Resources.CulturePropertyComment2;
        private static readonly string InvalidIdentifier = Resources.InvalidIdentifier;
        private static readonly string ClassDocComment = Resources.ClassDocComment;
        private static readonly string CannotCreatePropertyForResource = Resources.CannotCreatePropertyForResource;
        private static readonly string ErrorInFormatPropertyForResource = Resources.ErrorInFormatPropertyForResource;
        private static readonly string CannotCreateFormatMethod = Resources.CannotCreateFormatMethod;
        private static readonly string InternalSyncObjectPropertyComment = Resources.InternalSyncObjectPropertyComment;

        private static readonly char[] charsToReplace = new char[] { 
                ' ', '\x00a0', '.', ',', ';', '|', '~', '@', '#', '%', '^', '&', '*', '+', '-', '/', 
                '\\', '<', '>', '?', '[', ']', '(', ')', '{', '}', '"', '\'', ':', '!'
        };

        private static void AddGeneratedCodeAttributeforMember(CodeTypeMember typeMember)
        {
            CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(GeneratedCodeAttribute)));
            codeAttributeDeclaration.AttributeType.Options = CodeTypeReferenceOptions.GlobalReference;

			Type thisType = typeof(StronglyTypedResourceBuilderEx);
			Version assemblyVersion = thisType.Assembly.GetName().Version;

			CodeAttributeArgument stronglyTypedResourceBuilderExAttributeArgument = new CodeAttributeArgument(new CodePrimitiveExpression(thisType.FullName));
			CodeAttributeArgument primitiveExpressionAttributeArgument = new CodeAttributeArgument(new CodePrimitiveExpression(assemblyVersion.ToString()));
            codeAttributeDeclaration.Arguments.Add(stronglyTypedResourceBuilderExAttributeArgument);
            codeAttributeDeclaration.Arguments.Add(primitiveExpressionAttributeArgument);
            
            typeMember.CustomAttributes.Add(codeAttributeDeclaration);
        }

        /// <summary>Generates a class file that contains strongly-typed properties that match the resources referenced in the specified collection.</summary>
        /// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit"></see> container.</returns>
        /// <param name="baseName">The name of the class to be generated.</param>
        /// <param name="internalClass">true to generate an internal class; false to generate a public class.</param>
        /// <param name="unmatchable">A list that contains each resource name for which a property cannot be generated. Typically, a property cannot be generated because the resource name is not a valid identifier.</param>
        /// <param name="generatedCodeNamespace">The namespace of the class to be generated.</param>
        /// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider"></see>  class that provides the language in which the class will be generated. When langauge is Visual Basic (<see cref="System.CodeDom.Compiler.CodeDomProvider.FileExtension"/> is vb (case insensitive)) modle is generated instead of class.</param>
        /// <param name="resourceList">An <see cref="T:System.Collections.IDictionary"></see> collection where each dictionary entry key/value pair is the name of a resource and the value of the resource.</param>
        /// <exception cref="T:System.ArgumentNullException">resourceList, basename, or codeProvider is null.</exception>
        /// <param name="logicalName">Name of resource stream in assembly</param>
        /// <version version="1.5.3">Since version 1.5.3 modules are generated for Visual Basic.</version>
        public static CodeCompileUnit Create(Type callerType, IDictionary resourceList, string baseName, string generatedCodeNamespace,
            CodeDomProvider codeProvider, bool internalClass, List<ResourceErrorData> unmatchable, string logicalName)
        { //logicalName added by �onny
            return Create(callerType, resourceList, baseName, generatedCodeNamespace, null, codeProvider, internalClass, unmatchable, logicalName);
        }

        /// <summary>Generates a class file that contains strongly-typed properties that match the resources in the specified .resx file.</summary>
        /// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit"></see> container.</returns>
		/// <param name="callerType">The type of the caller class.</param>
        /// <param name="baseName">The name of the class to be generated.</param>
        /// <param name="internalClass">true to generate an internal class; false to generate a public class.</param>
        /// <param name="unmatchable">A list that contains each resource name for which a property cannot be generated. Typically, a property cannot be generated because the resource name is not a valid identifier.</param>
        /// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider"></see>  class that provides the language in which the class will be generated. When langauge is Visual Basic (<see cref="System.CodeDom.Compiler.CodeDomProvider.FileExtension"/> is vb (case insensitive)) modle is generated instead of class.</param>
        /// <param name="generatedCodeNamespace">The namespace of the class to be generated.</param>
        /// <param name="resxFile">The name of a .resx file used as input.</param>
        /// <exception cref="T:System.ArgumentNullException">basename or codeProvider is null.</exception>
        /// <param name="logicalName">Name of resource stream in assembly</param>
        /// <version version="1.5.3">Since version 1.5.3 modules are generated for Visual Basic.</version>
        public static CodeCompileUnit Create(Type callerType, string resxFile, string baseName, string generatedCodeNamespace,
            CodeDomProvider codeProvider, bool internalClass, List<ResourceErrorData> unmatchable, string logicalName)
        {//logicalName added by �onny
            return Create(callerType, resxFile, baseName, generatedCodeNamespace, null, codeProvider, internalClass, unmatchable, logicalName);
        }

        /// <summary>Generates a class file that contains strongly-typed properties that match the resources referenced in the specified collection.</summary>
        /// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit"></see> container.</returns>
		/// <param name="callerType">The type of the caller class.</param>
        /// <param name="baseName">The name of the class to be generated.</param>
        /// <param name="internalClass">true to generate an internal class; false to generate a public class.</param>
        /// <param name="resourcesNamespace">The namespace of the resource to be generated. </param>
        /// <param name="generatedCodeNamespace">The namespace of the class to be generated.</param>
        /// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider"></see> object that provides the language in which the class will be generated. When langauge is Visual Basic (<see cref="System.CodeDom.Compiler.CodeDomProvider.FileExtension"/> is vb (case insensitive)) modle is generated instead of class.</param>
        /// <param name="resourceList">An <see cref="T:System.Collections.IDictionary"></see> collection where each dictionary entry key/value pair is the name of a resource and the value of the resource.</param>
        /// <param name="unmatchable">A list that contains each resource name for which a property cannot be generated. Typically, a property cannot be generated because the resource name is not a valid identifier.</param>
        /// <exception cref="T:System.ArgumentNullException">resourceList, basename, or codeProvider is null.</exception>
        /// <version version="1.5.3">Since version 1.5.3 modules are generated for Visual Basic.</version>
        public static CodeCompileUnit Create(Type callerType, IDictionary resourceList, string baseName,
            string generatedCodeNamespace, string resourcesNamespace, CodeDomProvider codeProvider,
            bool internalClass, List<ResourceErrorData> unmatchable,string logicalName)
        {//logicalName added by �onny
            if (null == resourceList)
                throw new ArgumentNullException("resourceList");
            
            Dictionary<string, ResourceData> resourceDataDictionary = new Dictionary<string, ResourceData>(StringComparer.InvariantCultureIgnoreCase);
            foreach (DictionaryEntry resourceEntry in resourceList)
            {
                ResourceData resourceData;
                ResXDataNode resXNode = resourceEntry.Value as ResXDataNode;
                if (null != resXNode)
                {
                    string resourceKey = (string)resourceEntry.Key;
                    if (resourceKey != resXNode.Name)
                        throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, MismatchedResourceName,
                            resourceKey, resXNode.Name));
                    
                    string valueTypeName = resXNode.GetValueTypeName((AssemblyName[]) null);
                    Type valueType = Type.GetType(valueTypeName);
                    
                    string valueText = null;
                    if (valueType == typeof(string))
                        valueText = (string) resXNode.GetValue((AssemblyName[]) null);
                    
                    resourceData = new ResourceData(valueType, valueText);
                }
                else
                {
                    Type valueType = (null == resourceEntry.Value) ? typeof(object) : resourceEntry.Value.GetType();
                    resourceData = new ResourceData(valueType, resourceEntry.Value as string);
                }

                resourceDataDictionary.Add((string) resourceEntry.Key, resourceData);
            }

            return InternalCreate(callerType, resourceDataDictionary, baseName, generatedCodeNamespace,
                resourcesNamespace, codeProvider, internalClass, unmatchable, logicalName); //logicalName added by �onny
        }

        /// <summary>Generates a class file that contains strongly-typed properties that match the resources in the specified .resx file.</summary>
        /// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit"></see> container.</returns>
		/// <param name="callerType">The type of the caller class.</param>
        /// <param name="baseName">The name of the class to be generated.</param>
        /// <param name="internalClass">true to generate an internal class; false to generate a public class.</param>
        /// <param name="resourcesNamespace">The namespace of the resource to be generated. </param>
        /// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider"></see> class that provides the language in which the class will be generated. When langauge is Visual Basic (<see cref="System.CodeDom.Compiler.CodeDomProvider.FileExtension"/> is vb (case insensitive)) modle is generated instead of class.</param>
        /// <param name="unmatchable">A list that contains each resource name for which a property cannot be generated. Typically, a property cannot be generated because the resource name is not a valid identifier.</param>
        /// <param name="generatedCodeNamespace">The namespace of the class to be generated.</param>
        /// <param name="resxFile">The name of a .resx file used as input.</param>
        /// <exception cref="T:System.ArgumentNullException">basename or codeProvider is null.</exception>
        /// <param name="logicalName">Name of resource stream in assembly</param>
        /// <version version="1.5.3">Since version 1.5.3 modules are generated for Visual Basic.</version>
		public static CodeCompileUnit Create(Type callerType, string resxFile, string baseName, string generatedCodeNamespace,
            string resourcesNamespace, CodeDomProvider codeProvider, bool internalClass, List<ResourceErrorData> unmatchable, string logicalName)
        {   //logicalName added by �onny
            if (null == resxFile)
                throw new ArgumentNullException("resxFile");
            
            Dictionary<string, ResourceData> resourceDataDictionary = new Dictionary<string, ResourceData>(StringComparer.InvariantCultureIgnoreCase);
            using (ResXResourceReader resXReader = new ResXResourceReader(resxFile))
            {
                resXReader.UseResXDataNodes = true;
                foreach (DictionaryEntry resourceEntry in resXReader)
                {
                    ResXDataNode resXNode = (ResXDataNode)resourceEntry.Value;
                    
                    string valueTypeName = resXNode.GetValueTypeName((AssemblyName[]) null);
                    Type valueType = Type.GetType(valueTypeName);
                    
                    string valueText = null;
                    if (valueType == typeof(string))
                        valueText = (string)resXNode.GetValue((AssemblyName[]) null);
                    
                    ResourceData resourceData = new ResourceData(valueType, valueText);
                    resourceDataDictionary.Add((string) resourceEntry.Key, resourceData);
                }
            }

            return InternalCreate(callerType, resourceDataDictionary, baseName, generatedCodeNamespace, resourcesNamespace,
                codeProvider, internalClass, unmatchable, logicalName);
        }

        private static bool DefineResourceFetchingProperty(string propertyName, string resourceName,
            ResourceData resourceData, CodeTypeDeclaration resourceClass, bool internalClass, bool useStatic)
        {
            CodeMethodReturnStatement propertyReturnStatement;
            CodeMemberProperty resourceProperty = new CodeMemberProperty();
            resourceProperty.Name = propertyName;
            resourceProperty.HasGet = true;
            resourceProperty.HasSet = false;
            
            Type resourceDataType = resourceData.Type;
            if (null == resourceDataType)
                return false;

            if (typeof(MemoryStream) == resourceDataType)
                resourceDataType = typeof(UnmanagedMemoryStream);

            while (!resourceDataType.IsPublic)
                resourceDataType = resourceDataType.BaseType;

            CodeTypeReference resourceDataTypeReference = new CodeTypeReference(resourceDataType);
            resourceProperty.Type = resourceDataTypeReference;
            /*if (internalClass)
                resourceProperty.Attributes = MemberAttributes.Assembly;
            else*///�
                resourceProperty.Attributes = MemberAttributes.Public;

            if (useStatic)
                resourceProperty.Attributes |= MemberAttributes.Static;

            CodePropertyReferenceExpression resMgrPropertyReferenceExpression = new CodePropertyReferenceExpression(null, ResMgrPropertyName);
            CodeFieldReferenceExpression cultureInfoFieldReference = new CodeFieldReferenceExpression(useStatic ? null : ((CodeExpression) new CodeThisReferenceExpression()), CultureInfoFieldName);
            bool stringResourceDataType = typeof(string) == resourceDataType;
            bool memoryStreamResourceDataType = (typeof(UnmanagedMemoryStream) == resourceDataType) ||
                (typeof(MemoryStream) == resourceDataType);
            string methodName = "GetObject";
            string codeCommentString = string.Format(CultureInfo.CurrentCulture, NonStringPropertyComment, resourceName);
            if (stringResourceDataType)
            {
                methodName = "GetString";
                string resourceStringValue = resourceData.ValueIfString;
                if (resourceStringValue.Length > DocCommentLengthThreshold)
                {
                    resourceStringValue = string.Format(CultureInfo.CurrentCulture, StringTruncatedComment,
                        resourceStringValue.Substring(0, DocCommentLengthThreshold));
                }

                resourceStringValue = SecurityElement.Escape(resourceStringValue);
                codeCommentString = string.Format(CultureInfo.CurrentCulture,
                    StringPropertyComment, resourceStringValue);
            }
            else if (memoryStreamResourceDataType)
                methodName = "GetStream";

			AddComments(resourceProperty, codeCommentString);

            CodeExpression invokeCodeExpression = new CodeMethodInvokeExpression(resMgrPropertyReferenceExpression, methodName,
                new CodeExpression[] { new CodePrimitiveExpression(resourceName), cultureInfoFieldReference });
            if (stringResourceDataType || memoryStreamResourceDataType)
                propertyReturnStatement = new CodeMethodReturnStatement(invokeCodeExpression);
            else
                propertyReturnStatement = new CodeMethodReturnStatement(new CodeCastExpression(resourceDataTypeReference, invokeCodeExpression));
            
            resourceProperty.GetStatements.Add(propertyReturnStatement);
            
            resourceClass.Members.Add(resourceProperty);

            return true;
        }

		private static void AddComments(CodeTypeMember codeTypeMember, params string[] comments)
		{
			if (null == codeTypeMember)
				throw new ArgumentNullException("codeTypeMember");

			if (null == comments)
				throw new ArgumentNullException("comments");

			codeTypeMember.Comments.Add(new CodeCommentStatement(DocCommentSummaryStart, true));

			foreach(string comment in comments)
				codeTypeMember.Comments.Add(new CodeCommentStatement(comment, true));

			codeTypeMember.Comments.Add(new CodeCommentStatement(DocCommentSummaryEnd, true));
		}

        private static void DefineFormattedResourceFetchingMethod(string methodName, string propertyName,
            string resourceName, ResourceData resourceData, CodeTypeDeclaration resourceClass,
            bool internalClass, bool useStatic, int numberOfArguments)
        {
            Type stringType = resourceData.Type;
            Type objectType = typeof(object);
            CodeMethodReturnStatement methodReturnStatement;
            CodeMemberMethod resourceFormatMethod = new CodeMemberMethod();
            resourceFormatMethod.Name = methodName;

            for (int index = 0; index < numberOfArguments; ++index)
            {
                CodeParameterDeclarationExpression parameterDeclarationExpression =
                    new CodeParameterDeclarationExpression(objectType, "arg" + index.ToString(CultureInfo.InvariantCulture));
                resourceFormatMethod.Parameters.Add(parameterDeclarationExpression);
            }
           
			// Prevent the code analysis error indicating to use a param array 
			if (numberOfArguments > 3)
			{
				CodeAttributeDeclaration suppressAttributeDeclaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(SuppressMessageAttribute)));
				suppressAttributeDeclaration.AttributeType.Options = CodeTypeReferenceOptions.GlobalReference;
				suppressAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression("Microsoft.Design")));
				suppressAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression("CA1025:ReplaceRepetitiveArgumentsWithParamsArray")));
				resourceFormatMethod.CustomAttributes.Add(suppressAttributeDeclaration);

			}
            
            CodeTypeReference stringTypeReference = new CodeTypeReference(stringType);
            resourceFormatMethod.ReturnType = stringTypeReference;
            /*if (internalClass)
                resourceFormatMethod.Attributes = MemberAttributes.Assembly;
            else*///�
                resourceFormatMethod.Attributes = MemberAttributes.Public;

            if (useStatic)
                resourceFormatMethod.Attributes |= MemberAttributes.Static;

            string resourceStringValue = resourceData.ValueIfString;
            if (resourceStringValue.Length > DocCommentLengthThreshold)
            {
                resourceStringValue = string.Format(CultureInfo.CurrentCulture, StringTruncatedComment,
                    resourceStringValue.Substring(0, DocCommentLengthThreshold));
            }
            resourceStringValue = SecurityElement.Escape(resourceStringValue);
			string codeCommentString = numberOfArguments > 0 ? string.Format(CultureInfo.CurrentCulture,
				FormatMethodComment, resourceStringValue) : string.Format(CultureInfo.CurrentCulture,
				FormatStubMethodComment, propertyName);
			AddComments(resourceFormatMethod, codeCommentString);

            for (int index = 0; index < numberOfArguments; ++index)
            {
                resourceFormatMethod.Comments.Add(new CodeCommentStatement(string.Format(CultureInfo.InvariantCulture,
                    ParamCommentStatement, index), true));
            }

			string returnValueCommentString = numberOfArguments > 0 ? MethodReturnValueComment :
				string.Format(StubMethodReturnValueComment, propertyName);
            resourceFormatMethod.Comments.Add(new CodeCommentStatement(returnValueCommentString, true));

			CodePropertyReferenceExpression resPropertyReferenceExpression = new CodePropertyReferenceExpression(null, propertyName);

			if (numberOfArguments > 0)
			{
				CodeFieldReferenceExpression cultureInfoFieldReference = new CodeFieldReferenceExpression(useStatic ? null : ((CodeExpression)new CodeThisReferenceExpression()),
					CultureInfoFieldName);
				CodeTypeReferenceExpression stringTypeReferenceExpression = new CodeTypeReferenceExpression(stringTypeReference);
				CodeMethodReferenceExpression stringFormatMethodReference = new CodeMethodReferenceExpression(stringTypeReferenceExpression,
					"Format");
				CodeExpression[] formatExpressionParameters = new CodeExpression[2 + numberOfArguments];
				formatExpressionParameters[0] = cultureInfoFieldReference;
				formatExpressionParameters[1] = resPropertyReferenceExpression;
				for (int index = 0; index < numberOfArguments; ++index)
				{
					CodeVariableReferenceExpression parameterReference = new CodeVariableReferenceExpression("arg" +
						index.ToString(CultureInfo.InvariantCulture));
					formatExpressionParameters[2 + index] = parameterReference;
				}
				CodeExpression stringFormatExpression = new CodeMethodInvokeExpression(stringFormatMethodReference,
					formatExpressionParameters);

				methodReturnStatement = new CodeMethodReturnStatement(stringFormatExpression);
			}
			else
				methodReturnStatement = new CodeMethodReturnStatement(resPropertyReferenceExpression);

			resourceFormatMethod.Statements.Add(methodReturnStatement);

            resourceClass.Members.Add(resourceFormatMethod);
        }

        private static void EmitBasicClassMembers(Type callerType, CodeTypeDeclaration resourceClass, string nameSpace, string baseName,
            string resourcesNamespace, bool internalClass, bool useStatic, bool supportsTryCatch, string logicalName, bool generateCTor)
        {    //logicalName added by �onny
			// Full class name generation
            string fullClassName;
            if (resourcesNamespace != null)
            {
                if (resourcesNamespace.Length > 0)
                    fullClassName = resourcesNamespace + '.' + baseName;
                else
                    fullClassName = baseName;
            }
            else if ((nameSpace != null) && (nameSpace.Length > 0))
                fullClassName = nameSpace + '.' + baseName;
            else
                fullClassName = baseName;

			// Generation of class comments
			string callerTypeName = callerType != null ? callerType.Name : "NULL";
            CodeCommentStatement classCommentStatement = new CodeCommentStatement(string.Format(ClassComments1Format, callerTypeName));
            resourceClass.Comments.Add(classCommentStatement);

			classCommentStatement = new CodeCommentStatement(string.Format(ClassComments2Format, callerTypeName));
            resourceClass.Comments.Add(classCommentStatement);

			classCommentStatement = new CodeCommentStatement(string.Format(ClassCommentsCopyright, DateTime.Today.Year));
			resourceClass.Comments.Add(classCommentStatement);

			// Generation of the SuppressMessage attribute
            CodeAttributeDeclaration suppressAttributeDeclaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(SuppressMessageAttribute)));
            suppressAttributeDeclaration.AttributeType.Options = CodeTypeReferenceOptions.GlobalReference;
            suppressAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression("Microsoft.Performance")));
            suppressAttributeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression("CA1811:AvoidUncalledPrivateCode")));
            
			// Generation of the constructor
            if (generateCTor) {
                CodeConstructor codeConstructor = new CodeConstructor();
                codeConstructor.CustomAttributes.Add(suppressAttributeDeclaration);
                codeConstructor.Attributes = MemberAttributes.Private;
                resourceClass.Members.Add(codeConstructor);
            }

			// Generation of the _resourceManager field
            CodeTypeReference resourceManagerTypeReference = new CodeTypeReference(typeof(ResourceManager), CodeTypeReferenceOptions.GlobalReference);
			CodeMemberField resourceManagerMemberField = new CodeMemberField(resourceManagerTypeReference, ResMgrFieldName);
			resourceManagerMemberField.Attributes = MemberAttributes.Private;
            if (useStatic)
				resourceManagerMemberField.Attributes |= MemberAttributes.Static;
			resourceClass.Members.Add(resourceManagerMemberField);

			// Generation of the _internalSyncObject field
			CodeTypeReference objectTypeReference = new CodeTypeReference(typeof(object), CodeTypeReferenceOptions.GlobalReference);
			CodeMemberField internalSyncObjectMemberField = new CodeMemberField(objectTypeReference, InternalSyncObjectFieldName);
			internalSyncObjectMemberField.Attributes = MemberAttributes.Private;
			if (useStatic)
				internalSyncObjectMemberField.Attributes |= MemberAttributes.Static;
			resourceClass.Members.Add(internalSyncObjectMemberField);

			// Generation of the _resourceCulture field
            CodeTypeReference cultureInfoTypeReference = new CodeTypeReference(typeof(CultureInfo), CodeTypeReferenceOptions.GlobalReference);
			CodeMemberField cultureInfoMemberField = new CodeMemberField(cultureInfoTypeReference, CultureInfoFieldName);
			cultureInfoMemberField.Attributes = MemberAttributes.Private;
            if (useStatic)
				cultureInfoMemberField.Attributes |= MemberAttributes.Static;
			resourceClass.Members.Add(cultureInfoMemberField);

			// Generation of the InternalSyncObject property
			CodeMemberProperty internalSyncObjectProperty = new CodeMemberProperty();
			resourceClass.Members.Add(internalSyncObjectProperty);
			internalSyncObjectProperty.Name = InternalSyncObjectPropertyName;
			internalSyncObjectProperty.HasGet = true;
			internalSyncObjectProperty.HasSet = false;
			internalSyncObjectProperty.Type = objectTypeReference;
			/*if (internalClass)
				internalSyncObjectProperty.Attributes = MemberAttributes.Assembly;
			else*///�
				internalSyncObjectProperty.Attributes = MemberAttributes.Public;

			if (useStatic)
				internalSyncObjectProperty.Attributes |= MemberAttributes.Static;

			CodeTypeReference interlockedCodeTypeReference = new CodeTypeReference(typeof(Interlocked), CodeTypeReferenceOptions.GlobalReference);

			CodeMethodReferenceExpression referenceEqualsMethodReference = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(object)),
				"ReferenceEquals");
			CodeFieldReferenceExpression internalSyncObjectFieldReference = new CodeFieldReferenceExpression(null, InternalSyncObjectFieldName);
			CodeMethodInvokeExpression referenceEqualInvokeExpression = new CodeMethodInvokeExpression(referenceEqualsMethodReference,
				new CodeExpression[] { internalSyncObjectFieldReference, new CodePrimitiveExpression(null) });
			CodeObjectCreateExpression objectCreateExpression = new CodeObjectCreateExpression(objectTypeReference, new CodeExpression[] {});
			CodeMethodReferenceExpression compareExchangeMethodReference = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(interlockedCodeTypeReference),
				"CompareExchange");
			CodeMethodInvokeExpression compareExchangeMethodInvokeExpression = new CodeMethodInvokeExpression(compareExchangeMethodReference,
				new CodeDirectionExpression(FieldDirection.Ref, internalSyncObjectFieldReference), objectCreateExpression,
				new CodePrimitiveExpression(null));
			internalSyncObjectProperty.GetStatements.Add(new CodeConditionStatement(referenceEqualInvokeExpression,
				new CodeExpressionStatement(compareExchangeMethodInvokeExpression)));
			internalSyncObjectProperty.GetStatements.Add(new CodeMethodReturnStatement(internalSyncObjectFieldReference));

			AddComments(internalSyncObjectProperty, InternalSyncObjectPropertyComment);
			
			// Generation of the ResourceManager property
            CodeMemberProperty resourceManagerProperty = new CodeMemberProperty();
            resourceClass.Members.Add(resourceManagerProperty);
            resourceManagerProperty.Name = ResMgrPropertyName;
            resourceManagerProperty.HasGet = true;
            resourceManagerProperty.HasSet = false;
            resourceManagerProperty.Type = resourceManagerTypeReference;
            /*if (internalClass)
                resourceManagerProperty.Attributes = MemberAttributes.Assembly;
            else*///�
                resourceManagerProperty.Attributes = MemberAttributes.Public;

            if (useStatic)
                resourceManagerProperty.Attributes |= MemberAttributes.Static;

            CodeTypeReference eitorBrowsableTypeReference = new CodeTypeReference(typeof(EditorBrowsableState));
            eitorBrowsableTypeReference.Options = CodeTypeReferenceOptions.GlobalReference;
            CodeAttributeArgument codeAttributeArgument = new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(eitorBrowsableTypeReference), "Advanced"));
            CodeAttributeDeclaration codeAttributeDeclaration = new CodeAttributeDeclaration("System.ComponentModel.EditorBrowsableAttribute", new CodeAttributeArgument[] { codeAttributeArgument });
            codeAttributeDeclaration.AttributeType.Options = CodeTypeReferenceOptions.GlobalReference;
            resourceManagerProperty.CustomAttributes.Add(codeAttributeDeclaration);

			CodeFieldReferenceExpression resourceManagerFieldReference = new CodeFieldReferenceExpression(null, ResMgrFieldName);
			CodeMethodInvokeExpression referenceEqualsMethodInvokeExpression = new CodeMethodInvokeExpression(referenceEqualsMethodReference,
				resourceManagerFieldReference, new CodePrimitiveExpression(null));

			CodeTypeReference monitorCodeTypeReference = new CodeTypeReference(typeof(Monitor), CodeTypeReferenceOptions.GlobalReference);

			CodeMethodReferenceExpression monitorEnterMethodReference = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(monitorCodeTypeReference),
				"Enter");
			CodePropertyReferenceExpression internalSyncObjectPropertyReference = new CodePropertyReferenceExpression(null, InternalSyncObjectPropertyName);
			CodeMethodInvokeExpression monitorEnterMethodInvokeExpression = new CodeMethodInvokeExpression(monitorEnterMethodReference,
				internalSyncObjectPropertyReference);

			CodePropertyReferenceExpression assemblyPropertyReference = new CodePropertyReferenceExpression(new CodeTypeOfExpression(new CodeTypeReference(resourceClass.Name)),
				"Assembly");
			CodeObjectCreateExpression resourceManagerCreateExpression = new CodeObjectCreateExpression(resourceManagerTypeReference,
                new CodePrimitiveExpression(logicalName), assemblyPropertyReference); //logicalName changed by �onny

			CodeMethodReferenceExpression interlockedExchangeMethodReference = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(interlockedCodeTypeReference),
				"Exchange");
			CodeMethodInvokeExpression interlockedExchangeMethodInvokeExpression = new CodeMethodInvokeExpression(interlockedExchangeMethodReference,
				new CodeDirectionExpression(FieldDirection.Ref, resourceManagerFieldReference), resourceManagerCreateExpression);

			CodeMethodReferenceExpression monitorExitMethodReference = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(monitorCodeTypeReference),
				"Exit");
			CodeMethodInvokeExpression monitorExitMethodInvokeExpression = new CodeMethodInvokeExpression(monitorExitMethodReference,
				internalSyncObjectPropertyReference);

			CodeTryCatchFinallyStatement tryFinallyStatement = new CodeTryCatchFinallyStatement();
			tryFinallyStatement.TryStatements.Add(new CodeConditionStatement(referenceEqualsMethodInvokeExpression,
				new CodeExpressionStatement(interlockedExchangeMethodInvokeExpression)));
			tryFinallyStatement.FinallyStatements.Add(monitorExitMethodInvokeExpression);

			resourceManagerProperty.GetStatements.Add(new CodeConditionStatement(referenceEqualsMethodInvokeExpression,
				new CodeExpressionStatement(monitorEnterMethodInvokeExpression), tryFinallyStatement));
			resourceManagerProperty.GetStatements.Add(new CodeMethodReturnStatement(resourceManagerFieldReference));

			AddComments(resourceManagerProperty, ResMgrPropertyComment);

			// Generation of the Culture property
            CodeMemberProperty cultureProperty = new CodeMemberProperty();
            resourceClass.Members.Add(cultureProperty);
            cultureProperty.Name = CultureInfoPropertyName;
            cultureProperty.HasGet = true;
            cultureProperty.HasSet = true;
            cultureProperty.Type = cultureInfoTypeReference;
            /*if (internalClass)
                cultureProperty.Attributes = MemberAttributes.Assembly;
            else*///�
                cultureProperty.Attributes = MemberAttributes.Public;

            if (useStatic)
                cultureProperty.Attributes |= MemberAttributes.Static;
            cultureProperty.CustomAttributes.Add(codeAttributeDeclaration);

            CodeFieldReferenceExpression resourceCultureFieldReference = new CodeFieldReferenceExpression(null, CultureInfoFieldName);
            cultureProperty.GetStatements.Add(new CodeMethodReturnStatement(resourceCultureFieldReference));

            CodePropertySetValueReferenceExpression codePropertySetValueReferenceExpression = new CodePropertySetValueReferenceExpression();
            cultureProperty.SetStatements.Add(new CodeAssignStatement(resourceCultureFieldReference, codePropertySetValueReferenceExpression));

			AddComments(cultureProperty, CulturePropertyComment1, CulturePropertyComment2);
        }

        private static CodeCompileUnit InternalCreate(Type callerType, Dictionary<string, ResourceData> resourceList,
            string baseName, string generatedCodeNamespace, string resourcesNamespace,
            CodeDomProvider codeProvider, bool internalClass, List<ResourceErrorData> unmatchable, string logicalName)
        { //logicalName added by �onny
            if (null == baseName)
                throw new ArgumentNullException("baseName");
            
            if (null == codeProvider)
                throw new ArgumentNullException("codeProvider");
            
            Dictionary<string, string> reverseFixupTable;
            SortedList<string, ResourceData> validResources = VerifyResourceNames(resourceList, codeProvider, unmatchable,
                out reverseFixupTable);
            
            string verifiedBaseName = baseName;
            if (!codeProvider.IsValidIdentifier(verifiedBaseName))
            {
                string adjustedBaseName = VerifyResourceName(verifiedBaseName, codeProvider);
                if (adjustedBaseName != null)
                    verifiedBaseName = adjustedBaseName;
            }

            if (!codeProvider.IsValidIdentifier(verifiedBaseName))
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, InvalidIdentifier, verifiedBaseName));

            if (!string.IsNullOrEmpty(generatedCodeNamespace) &&
                !codeProvider.IsValidIdentifier(generatedCodeNamespace))
            {
                string adjustedCodeNamespace = VerifyResourceName(generatedCodeNamespace, codeProvider, true);
                if (adjustedCodeNamespace != null)
                    generatedCodeNamespace = adjustedCodeNamespace;
            }

            CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
            codeCompileUnit.ReferencedAssemblies.Add("System.dll");
            codeCompileUnit.UserData.Add("AllowLateBound", false);
            codeCompileUnit.UserData.Add("RequireVariableDeclaration", true);

            CodeNamespace codeNamespace = new CodeNamespace(generatedCodeNamespace);
            codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
            codeCompileUnit.Namespaces.Add(codeNamespace);

            CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(verifiedBaseName);
            codeNamespace.Types.Add(codeTypeDeclaration);
            AddGeneratedCodeAttributeforMember(codeTypeDeclaration);

			TypeAttributes typeAttributes = TypeAttributes.AutoLayout;
			if (!internalClass)
				typeAttributes |= TypeAttributes.Public;
            codeTypeDeclaration.TypeAttributes = typeAttributes;
			AddComments(codeTypeDeclaration, ClassDocComment);
            if (codeProvider.FileExtension.ToLowerInvariant() == "vb")//Visual Basic - generate module
                codeTypeDeclaration.UserData.Add("Module", true);

            CodeTypeReference debuggerNonUserCodeTypeReference = new CodeTypeReference(typeof(DebuggerNonUserCodeAttribute),
				CodeTypeReferenceOptions.GlobalReference);
            codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration(debuggerNonUserCodeTypeReference));

			CodeAttributeDeclaration suppressAttributeTypeDeclaration = new CodeAttributeDeclaration(new CodeTypeReference(typeof(SuppressMessageAttribute)));
			suppressAttributeTypeDeclaration.AttributeType.Options = CodeTypeReferenceOptions.GlobalReference;
			suppressAttributeTypeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression("Microsoft.Naming")));
			suppressAttributeTypeDeclaration.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression("CA1724:TypeNamesShouldNotMatchNamespaces")));
        	codeTypeDeclaration.CustomAttributes.Add(suppressAttributeTypeDeclaration);

            bool useStatic = internalClass || codeProvider.Supports(GeneratorSupport.PublicStaticMembers);
            bool supportsTryCatch = codeProvider.Supports(GeneratorSupport.TryCatchStatements);
            EmitBasicClassMembers(callerType, codeTypeDeclaration, generatedCodeNamespace, baseName,
                resourcesNamespace, internalClass, useStatic, supportsTryCatch, logicalName, codeProvider.FileExtension.ToLowerInvariant() != "vb");  //logicalName added by �onny

			SortedList<string, ResourceData> formatMethods = new SortedList<string, ResourceData>(validResources.Count,
				StringComparer.InvariantCultureIgnoreCase);

			// Generate resource fetching properties
			foreach (KeyValuePair<string, ResourceData> validResourceEntry in validResources)
            {
				string initialResourceKey;
				if (!reverseFixupTable.TryGetValue(validResourceEntry.Key, out initialResourceKey))
					initialResourceKey = validResourceEntry.Key;

				if (!DefineResourceFetchingProperty(validResourceEntry.Key, initialResourceKey,
					validResourceEntry.Value, codeTypeDeclaration, internalClass, useStatic))
				{
					unmatchable.Add(new ResourceErrorData(validResourceEntry.Key.ToString(),
						string.Format(CultureInfo.CurrentCulture, CannotCreatePropertyForResource,
							validResourceEntry.Key.ToString())));
				}
				else if (typeof(string) == validResourceEntry.Value.Type)
					formatMethods.Add(validResourceEntry.Key, validResourceEntry.Value);
            }

			// Generate resource fetching format methods
			foreach (KeyValuePair<string, ResourceData> formatMethodEntry in formatMethods)
			{
				try
				{
					string methodName = formatMethodEntry.Key + FormatSuffix;

					// Check for duplicate method names
					bool uniqueMethodName = true;
					foreach (CodeTypeMember codeTypeMember in codeTypeDeclaration.Members)
					{
						if (null == codeTypeMember)
							continue;

						if (codeTypeMember.Name == methodName)
						{
							uniqueMethodName = false;
							break;
						}
					}

					if (!uniqueMethodName)
						continue;

					int numberOfArguments = FormatValidator.Parse(formatMethodEntry.Value.ValueIfString);
					if (codeProvider.IsValidIdentifier(methodName))
					{
						string initialResourceKey;
						if (!reverseFixupTable.TryGetValue(formatMethodEntry.Key, out initialResourceKey))
							initialResourceKey = formatMethodEntry.Key;

						DefineFormattedResourceFetchingMethod(methodName, formatMethodEntry.Key,
							initialResourceKey, formatMethodEntry.Value, codeTypeDeclaration,
							internalClass, useStatic, numberOfArguments);
					}
					else
					{
						unmatchable.Add(new ResourceErrorData(methodName,
							string.Format(CultureInfo.CurrentCulture, CannotCreateFormatMethod,
								methodName, formatMethodEntry.Key)));
					}
				}
				catch (FormatException ex)
				{
					unmatchable.Add(new ResourceErrorData(formatMethodEntry.Key.ToString(),
						string.Format(CultureInfo.CurrentCulture, ErrorInFormatPropertyForResource,
							ex.Message, formatMethodEntry.Key.ToString())));
				}
			}
            
            CodeGenerator.ValidateIdentifiers(codeCompileUnit);
            
            return codeCompileUnit;
        }

        /// <summary>Generates a valid resource string based on the specified input string and code generator.</summary>
        /// <returns>A valid resource name with any invalid tokens replaced with the underscore (_) character, or null if the string still contains invalid characters according to the language specified by the generator parameter.</returns>
        /// <param name="provider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider"></see> object that specifies the target language to use.</param>
        /// <param name="key">The string to verify and, if necessary, convert to a valid resource name.</param>
        /// <exception cref="T:System.ArgumentNullException">key or generator is null.</exception>
        public static string VerifyResourceName(string key, CodeDomProvider provider)
        {
            return VerifyResourceName(key, provider, false);
        }

        private static string VerifyResourceName(string key, CodeDomProvider provider, bool isNameSpace)
        {
            if (null == key)
                throw new ArgumentNullException("key");
            
            if (null == provider)
                throw new ArgumentNullException("provider");

            for (int index = 0; index < charsToReplace.Length; index++)
            {
                char ch = charsToReplace[index];
                if (!isNameSpace || ((ch != '.') && (ch != ':')))
                    key = key.Replace(ch, ReplacementChar);
            }

            if (provider.IsValidIdentifier(key))
                return key;

            key = provider.CreateValidIdentifier(key);
            if (provider.IsValidIdentifier(key))
                return key;

            key = "_" + key;
            if (provider.IsValidIdentifier(key))
                return key;

            return null;
        }

        private static SortedList<string, ResourceData> VerifyResourceNames(Dictionary<string, ResourceData> resourceList,
            CodeDomProvider codeProvider, List<ResourceErrorData> invalidResources, out Dictionary<string, string> reverseFixupTable)
        {
			reverseFixupTable = new Dictionary<string, string>(0, StringComparer.InvariantCultureIgnoreCase);

            SortedList<string, ResourceData> validResources = new SortedList<string, ResourceData>(resourceList.Count,
				StringComparer.InvariantCultureIgnoreCase);
            Dictionary<string, ResourceData>.Enumerator resourceListEnumerator = resourceList.GetEnumerator();
            try
            {
                while (resourceListEnumerator.MoveNext())
                {
                    KeyValuePair<string, ResourceData> resourcePair = resourceListEnumerator.Current;
                    
                    string resourceKey = resourcePair.Key;
                    if ((string.Equals(resourceKey, ResMgrPropertyName) ||
                        string.Equals(resourceKey, CultureInfoPropertyName) ||
						string.Equals(resourceKey, InternalSyncObjectPropertyName)) ||
                        (typeof(void) == resourcePair.Value.Type))
                    {
                        invalidResources.Add(new ResourceErrorData(resourceKey,
                            string.Format(CultureInfo.CurrentCulture, CannotCreatePropertyForResource, resourceKey)));

                        continue;
                    }

                    if (((resourceKey.Length <= 0) || (resourceKey[0] != '$')) &&
                        (((resourceKey.Length <= 1) || (resourceKey[0] != '>')) || (resourceKey[1] != '>')))
                    {
                        if (!codeProvider.IsValidIdentifier(resourceKey))
                        {
                            string adjustedResourceKey = VerifyResourceName(resourceKey, codeProvider, false);
                            if (adjustedResourceKey == null)
                            {
                                invalidResources.Add(new ResourceErrorData(resourceKey,
                                    string.Format(CultureInfo.CurrentCulture, CannotCreatePropertyForResource, resourceKey)));

                                continue;
                            }

							string originalResourceKey;
							if (reverseFixupTable.TryGetValue(adjustedResourceKey, out originalResourceKey))
                            {
                                if (!ContainsInvalidKey(invalidResources, originalResourceKey))
                                {
                                    invalidResources.Add(new ResourceErrorData(originalResourceKey,
                                        string.Format(CultureInfo.CurrentCulture, CannotCreatePropertyForResource,
                                            originalResourceKey)));
                                }
                                
                                if (validResources.ContainsKey(adjustedResourceKey))
                                    validResources.Remove(adjustedResourceKey);

                                invalidResources.Add(new ResourceErrorData(resourceKey,
                                    string.Format(CultureInfo.CurrentCulture, CannotCreatePropertyForResource, resourceKey)));

                                continue;
                            }

                            reverseFixupTable[adjustedResourceKey] = resourceKey;
                            resourceKey = adjustedResourceKey;
                        }

                        ResourceData resourceData = resourcePair.Value;
                        if (!validResources.ContainsKey(resourceKey))
                        {
                            validResources.Add(resourceKey, resourceData);
                            continue;
                        }

						string initialResourceKey;
						if (reverseFixupTable.TryGetValue(resourceKey, out initialResourceKey))
                        {
                            if (!ContainsInvalidKey(invalidResources, initialResourceKey))
                            {
                                invalidResources.Add(new ResourceErrorData(initialResourceKey,
                                    string.Format(CultureInfo.CurrentCulture, CannotCreatePropertyForResource,
                                        initialResourceKey)));
                            }
                            
                            reverseFixupTable.Remove(resourceKey);
                        }

                        invalidResources.Add(new ResourceErrorData(resourcePair.Key,
                            string.Format(CultureInfo.CurrentCulture, CannotCreatePropertyForResource,
                                resourcePair.Key)));
                        validResources.Remove(resourceKey);
                    }
                }
            }
            finally
            {
                resourceListEnumerator.Dispose();
            }

            return validResources;
        }

        private static bool ContainsInvalidKey(List<ResourceErrorData> invalidResources, string resourceKey)
        {
            bool containsInvalidKey = false;

            foreach (ResourceErrorData resErrorData in invalidResources)
            {
                if (resErrorData.ResourceKey == resourceKey)
                {
                    containsInvalidKey = true;
                    break;
                }
            }

            return containsInvalidKey;
        }
    }
}

