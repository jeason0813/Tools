﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.20506.1
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Tools.TotalCommanderT.PluginBuilder.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Cannot generate plugin for abstract type {0}..
        '''</summary>
        Friend ReadOnly Property e_Abstract() As String
            Get
                Return ResourceManager.GetString("e_Abstract", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Error while clening intermediate directory..
        '''</summary>
        Friend ReadOnly Property e_CleanIntermediate() As String
            Get
                Return ResourceManager.GetString("e_CleanIntermediate", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Duplicit name {0} scpecified for renaming..
        '''</summary>
        Friend ReadOnly Property e_DuplicitRenameName() As String
            Get
                Return ResourceManager.GetString("e_DuplicitRenameName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Duplicit type {0} specified for renaming..
        '''</summary>
        Friend ReadOnly Property e_DuplicitRenameType() As String
            Get
                Return ResourceManager.GetString("e_DuplicitRenameType", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Process {0} exited with code {1}..
        '''</summary>
        Friend ReadOnly Property e_ExitCode() As String
            Get
                Return ResourceManager.GetString("e_ExitCode", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Error while generating plugins: {0}.
        '''</summary>
        Friend ReadOnly Property e_Generating() As String
            Get
                Return ResourceManager.GetString("e_Generating", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Error while loading assembly: {0}..
        '''</summary>
        Friend ReadOnly Property e_LoadAssembly() As String
            Get
                Return ResourceManager.GetString("e_LoadAssembly", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Cannot load type {0}: {1}.
        '''</summary>
        Friend ReadOnly Property e_LoadType() As String
            Get
                Return ResourceManager.GetString("e_LoadType", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Type {0} does not implement method {1} required by {2} applied onto method {3}..
        '''</summary>
        Friend ReadOnly Property e_MissingPluginMethod() As String
            Get
                Return ResourceManager.GetString("e_MissingPluginMethod", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Cannot generate plugin for type {0} which has not default constructor..
        '''</summary>
        Friend ReadOnly Property e_NoDefaultCTor() As String
            Get
                Return ResourceManager.GetString("e_NoDefaultCTor", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Cannot gerenrate plugin for type {0}, which does not derive from supported plugin type..
        '''</summary>
        Friend ReadOnly Property e_NotAPluginType() As String
            Get
                Return ResourceManager.GetString("e_NotAPluginType", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Cannot generate plugin for non-public type {0}..
        '''</summary>
        Friend ReadOnly Property e_NotPublic() As String
            Get
                Return ResourceManager.GetString("e_NotPublic", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Cannot generate plugin for open generic type {0}..
        '''</summary>
        Friend ReadOnly Property e_OpenGeneric() As String
            Get
                Return ResourceManager.GetString("e_OpenGeneric", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Error while removing intermediate directory {0}.
        '''</summary>
        Friend ReadOnly Property e_RemoveIntermediate() As String
            Get
                Return ResourceManager.GetString("e_RemoveIntermediate", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Environmental variables:.
        '''</summary>
        Friend ReadOnly Property EnvironmentalVariables() As String
            Get
                Return ResourceManager.GetString("EnvironmentalVariables", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Creating user-defined intermediate directory {0}.
        '''</summary>
        Friend ReadOnly Property i_CeateUserDefinedIntermediateDir() As String
            Get
                Return ResourceManager.GetString("i_CeateUserDefinedIntermediateDir", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Copy assembly file {0}.
        '''</summary>
        Friend ReadOnly Property i_CopyAssembly() As String
            Get
                Return ResourceManager.GetString("i_CopyAssembly", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Copying output from {0} to {1}.
        '''</summary>
        Friend ReadOnly Property i_CopyOutput() As String
            Get
                Return ResourceManager.GetString("i_CopyOutput", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Copying pdb file from {0} to {1}.
        '''</summary>
        Friend ReadOnly Property i_CopyPDB() As String
            Get
                Return ResourceManager.GetString("i_CopyPDB", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Copy template from {0}.
        '''</summary>
        Friend ReadOnly Property i_CopyTemplate() As String
            Get
                Return ResourceManager.GetString("i_CopyTemplate", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Create project directory {0}.
        '''</summary>
        Friend ReadOnly Property I_CreateProjectDir() As String
            Get
                Return ResourceManager.GetString("I_CreateProjectDir", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Creating plugin for type {0}..
        '''</summary>
        Friend ReadOnly Property i_CreatingPluginForType() As String
            Get
                Return ResourceManager.GetString("i_CreatingPluginForType", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Executing {0} {1}.
        '''</summary>
        Friend ReadOnly Property i_Executing() As String
            Get
                Return ResourceManager.GetString("i_Executing", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to {0}&gt; Exict code {1}..
        '''</summary>
        Friend ReadOnly Property i_ExitCode() As String
            Get
                Return ResourceManager.GetString("i_ExitCode", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Extract built-in template.
        '''</summary>
        Friend ReadOnly Property i_ExtractTemplate() As String
            Get
                Return ResourceManager.GetString("i_ExtractTemplate", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Generating assembly info.
        '''</summary>
        Friend ReadOnly Property i_GeneratingAssemblyInfo() As String
            Get
                Return ResourceManager.GetString("i_GeneratingAssemblyInfo", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Invoking compiler {0}.
        '''</summary>
        Friend ReadOnly Property i_InvokingCompiler() As String
            Get
                Return ResourceManager.GetString("i_InvokingCompiler", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Plugin type: File System Plugin (wfx).
        '''</summary>
        Friend ReadOnly Property i_PluginType_wfx() As String
            Get
                Return ResourceManager.GetString("i_PluginType_wfx", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Using predefined intermediate directory {0}..
        '''</summary>
        Friend ReadOnly Property i_PredefinedIntermediateDir() As String
            Get
                Return ResourceManager.GetString("i_PredefinedIntermediateDir", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Removing intermediate directory {0}.
        '''</summary>
        Friend ReadOnly Property i_RemoveIntermediate() As String
            Get
                Return ResourceManager.GetString("i_RemoveIntermediate", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Creating temporary intermediate directory {0}.
        '''</summary>
        Friend ReadOnly Property i_TempIntermediateDir() As String
            Get
                Return ResourceManager.GetString("i_TempIntermediateDir", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Usage:
        '''{0} &lt;assembly&gt; &lt;parameters&gt;
        '''assembly    A DLL assembly containing plugin definition
        '''parameters:
        '''/out &lt;outdir&gt; Output directory. Optional. Not set = same as assembly directory
        '''/t &lt;type&gt;     Type name. Optional. Multiple.
        '''              If specified at leas once only types specified will be used (otherwise all the types that qaulify to be TC plugin are used).
        '''              Use name for Assembly.GetType().
        '''/-wfx         Excludes wfx plugins
        '''/-wlx         Excludes wlx plugins
        '''/-wcx         Exclu [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property Usage() As String
            Get
                Return ResourceManager.GetString("Usage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Warning: Cannot delete directory {0}: {1}.
        '''</summary>
        Friend ReadOnly Property w_DelDir() As String
            Get
                Return ResourceManager.GetString("w_DelDir", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to {0} cannot be {1} when {2} is {3}..
        '''</summary>
        Friend ReadOnly Property XcannotbeYwhenZisW() As String
            Get
                Return ResourceManager.GetString("XcannotbeYwhenZisW", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
