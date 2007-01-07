Imports System.Windows.Forms, Tools.Collections.Generic, Tools.Windows.Forms.Utilities
Imports System.Drawing.Design, System.ComponentModel.Design, Tools.ComponentModel
#If Config <= Beta Then 'Stage: Beta
Namespace Windows.Forms
    ''' <summary><see cref="System.Windows.Forms.LinkLabel"/> with improved design-time behavior</summary>
    <Author("Đonny", "dzonny.dz@gmail.com"), Version(1, 0, LastChange:="1/7/2007")> _
    <ToolboxItemFilter("System.Windows.Forms")> _
    <Drawing.ToolboxBitmap(GetType(System.Windows.Forms.LinkLabel))> _
    <DefaultEvent("LinkClicked"), ToolboxItem(True), DefaultProperty("Items")> _
    Public Class LinkLabel : Inherits System.Windows.Forms.LinkLabel
        ''' <summary>Contains value of the <see cref="Items"/> property</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private WithEvents _Items As New ListWithEvents(Of LinkLabelItem)(True)
        ''' <summary>CTor</summary>
        Public Sub New()
            Items.AllowAddCancelableEventsHandlers = False
        End Sub
        ''' <summary>Gets text currently displayed by this <see cref="LinkLabel"/></summary>
        ''' <value>Property is read-only, exception <see cref="NotSupportedException"/> will be thrown when trying to set it</value>
        ''' <exception cref="NotSupportedException">Trying to set this property</exception>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(False)> _
        Public NotOverridable Overrides Property Text() As String
            Get
                Return MyBase.Text
            End Get
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Set(ByVal value As String)
                Throw New NotSupportedException("Text cannot be changed via the Text property")
            End Set
        End Property
        ''' <summary>Gets the range in the text treated as a link.</summary>
        ''' <value>Property is read-only, exception <see cref="NotSupportedException"/> will be thrown when trying to set it</value>
        ''' <returns>A System.Windows.Forms.LinkArea that represents the area treated as a link.</returns>
        <EditorBrowsable(EditorBrowsableState.Never), Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property LinkArea() As LinkArea
            Get
                Return MyBase.LinkArea
            End Get
            Set(ByVal value As LinkArea)
                Throw New NotSupportedException("LinkArea cannot be set.")
            End Set
        End Property
        ''' <summary>List of all items in label</summary>
        ''' <remarks><see cref="ListWithEvents(Of LinkLabelItem).AllowAddCancelableEventsHandlers"/> is set to False</remarks>
        <Category(CategoryAttributeValues.Properties.Appearance), Description("List of all items in label")> _
        <Editor(GetType(LinkLabelItemsEditor), GetType(UITypeEditor))> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        <TypeConverter(GetType(ItemsNameTypeConverter))> _
        <RefreshProperties(RefreshProperties.All)> _
        Public ReadOnly Property Items() As ListWithEvents(Of LinkLabelItem)
            Get
                Return _Items
            End Get
        End Property

        ''' <summary>Simple converter taht shows the text (Items) as representation of property in property grid</summary>
        Private Class ItemsNameTypeConverter : Inherits CollectionConverter
            ''' <summary>Converts the given value object to the specified destination type.</summary>
            ''' <param name="culture">The culture to which value will be converted.</param>
            ''' <param name="context">An <see cref="System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
            ''' <param name="destinationType">The System.Type to convert the value to.</param>
            ''' <param name="value">The <see cref="System.Object"/> to convert. This parameter must inherit from <see cref="System.Collections.ICollection"/>.</param>
            ''' <returns>An <see cref="System.Object"/> that represents the converted value.</returns>
            ''' <exception cref="System.ArgumentNullException">destinationType is null.</exception>
            ''' <exception cref="System.NotSupportedException">The conversion cannot be performed</exception>
            Public Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
                If destinationType.Equals(GetType(String)) Then
                    Return "(Items)"
                End If
                Return MyBase.ConvertTo(context, culture, value, destinationType)
            End Function
        End Class

#Region "List event handlers"
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Sub _Items_Added(ByVal sender As ListWithEvents(Of LinkLabelItem), ByVal e As ListWithEvents(Of LinkLabelItem).ItemIndexEventArgs) Handles _Items.Added
            RegenerateContent()
        End Sub
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Sub _Items_Cleared(ByVal sender As ListWithEvents(Of LinkLabelItem), ByVal e As ListWithEvents(Of LinkLabelItem).CountEventArgs) Handles _Items.Cleared
            RegenerateContent()
        End Sub
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Sub _Items_ItemChanged(ByVal sender As ListWithEvents(Of LinkLabelItem), ByVal e As ListWithEvents(Of LinkLabelItem).ItemIndexEventArgs) Handles _Items.ItemChanged
            RegenerateContent()
        End Sub

        ''' <summary>
        ''' Handles the <see cref="ListWithEvents(Of LinkLabelItem).ItemValueChanged"/> event of <see cref="_Items"/>.
        ''' When property of item changes specific action is taken depending on the property.
        ''' </summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters (expected to be <see cref="IReportsChange.ValueChangedEventArgs"/>)</param>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Sub _Items_ItemValueChanged(ByVal sender As ListWithEvents(Of LinkLabelItem), ByVal e As ListWithEvents(Of LinkLabelItem).ItemValueChangedEventArgs) Handles _Items.ItemValueChanged
            Dim l As Link = Nothing
            If TypeOf e.Item Is LinkItem Then
                Try
                    l = Links(e.Item)
                Catch ex As ArgumentException
                    RegenerateContent()
                    Return
                Catch ex As InvalidOperationException
                    RegenerateContent()
                    Return
                End Try
            End If
            If l IsNot Nothing AndAlso TypeOf e.Item Is LinkItem AndAlso TypeOf e.OriginalEventArgs Is IReportsChange.ValueChangedEventArgsBase Then
                With CType(e.OriginalEventArgs, IReportsChange.ValueChangedEventArgsBase)
                    Select Case .ValueName
                        Case LinkItem.LinkDataPropertyName  'Do nothing
                        Case LinkItem.DescriptionPropertyName
                            l.Description = CType(e.OriginalEventArgs, IReportsChange.ValueChangedEventArgs(Of String)).NewValue
                        Case LinkItem.NamePropertyName
                            l.Name = CType(e.OriginalEventArgs, IReportsChange.ValueChangedEventArgs(Of String)).NewValue
                        Case LinkItem.TagPropertyName
                            l.Tag = CType(e.OriginalEventArgs, IReportsChange.ValueChangedEventArgs(Of Object)).NewValue
                        Case LinkItem.VisitedPropertyName
                            l.Visited = CType(e.OriginalEventArgs, IReportsChange.ValueChangedEventArgs(Of Boolean)).NewValue
                        Case LinkItem.EnabledPropertyName
                            l.Enabled = CType(e.OriginalEventArgs, IReportsChange.ValueChangedEventArgs(Of Boolean)).NewValue
                        Case AutoLink.LinkURIPropertyName 'Normally do nothing
                            If Not TypeOf e.Item Is AutoLink Then RegenerateContent() 'This handles situation caused by non-LinkLabel LinkItem-derived class
                        Case Else 'This should not happen with LinkLabel-included classes
                            RegenerateContent()
                    End Select
                End With
            Else
                RegenerateContent()
            End If
        End Sub
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Sub _Items_Removed(ByVal sender As ListWithEvents(Of LinkLabelItem), ByVal e As ListWithEvents(Of LinkLabelItem).ItemIndexEventArgs) Handles _Items.Removed
            RegenerateContent()
        End Sub
        ''' <summary>Regenerates content of <see cref="LinkLabel"/></summary>
        ''' <remarks>Called by handlers of events of the <see cref="Items"/> <see cref="ListWithEvents(Of LinkLabelItem)"/></remarks>
        Protected Overridable Sub RegenerateContent()
            MyBase.Text = ""
            Dim tb As New System.Text.StringBuilder
            For Each itm As LinkLabelItem In Items
                tb.Append(itm.Text)
            Next itm
            MyBase.Links.Clear()
            MyBase.Text = tb.ToString
            Dim start As Integer = 0
            For Each itm As LinkLabelItem In Items
                If TypeOf itm Is LinkItem Then
                    Dim l As Link = MyBase.Links.Add(start, itm.Text.Length, itm)
                    With CType(itm, LinkItem)
                        l.Description = .Description
                        l.Enabled = .Enabled
                        l.Visited = .Visited
                        l.Name = .Name
                        l.Tag = .Tag
                    End With
                End If
                start += itm.Text.Length
            Next itm
        End Sub
#End Region
        ''' <summary>Raises the <see cref="System.Windows.Forms.LinkLabel.LinkClicked"/> event.</summary>
        ''' <param name="e">A <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> that contains the event data.</param>
        ''' <remarks>Note for inheritors: Call base class <see cref="OnLinkClicked"/> in order to raise <see cref="LinkClicked"/> and <see cref="System.Windows.Forms.LinkLabel.LinkClicked"/> events and <see cref="AutoLink"/> to be followed</remarks>
        Protected Overrides Sub OnLinkClicked(ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
            MyBase.OnLinkClicked(e)
            Dim ea As New LinkClickedEventArgs(e.Link, e.Button)
            RaiseEvent LinkClicked(Me, ea)
            If TypeOf e.Link.LinkData Is AutoLink AndAlso (e.Button = System.Windows.Forms.MouseButtons.Left OrElse e.Button = System.Windows.Forms.MouseButtons.None) AndAlso CType(e.Link.LinkData, AutoLink).LinkURI IsNot Nothing Then
                Try
                    Process.Start(CType(e.Link.LinkData, AutoLink).LinkPath)
                    ea.Item.Visited = True
                Catch : End Try
            End If
        End Sub
        ''' <summary>Raised after link is clicked</summary>
        ''' <param name="sender">The source of the event</param>
        ''' <param name="e">Event parameters (contains info about link and item being clicked)</param>
        ''' <remarks><list type="1">
        ''' <listheader>The order of events is following</listheader>
        ''' <item><see cref="System.Windows.Forms.LinkLabel.LinkClicked"/></item>
        ''' <item><see cref="LinkClicked"/></item>
        ''' <item>If the item being clicked is <see cref="AutoLink"/> and its <see cref="AutoLink.LinkURI"/> is not null and <see cref="LinkClickedEventArgs.Button"/> is <see cref="System.Windows.Forms.MouseButtons.Left"/> or <see cref="System.Windows.Forms.MouseButtons.None"/> then Uri <see cref="AutoLink.LinkURI"/> is opened via <see cref="Process.Start"/></item>
        ''' </list>
        ''' </remarks>
        Public Shadows Event LinkClicked(ByVal sender As LinkLabel, ByVal e As LinkClickedEventArgs)

        ''' <summary>Arguments of the <see cref="LinkClicked"/> event</summary>
        Public Class LinkClickedEventArgs : Inherits LinkLabelLinkClickedEventArgs
            ''' <summary>Contains value of the <see cref="Item"/> property</summary>
            <EditorBrowsable()> _
            Private ReadOnly _Item As LinkItem
            ''' <summary>Contains va lue of the <see cref="LinkURI"/> property</summary>
            <EditorBrowsable()> _
            Private ReadOnly _LinkURI As Uri = Nothing
            ''' <summary>CTor</summary>
            ''' <param name="link"><see cref="Link"/> that was clicked</param>
            ''' <param name="button">The mouse button used to click</param>
            Public Sub New(ByVal link As Link, ByVal button As MouseButtons)
                MyBase.New(link, button)
                _Item = link.LinkData
                If TypeOf link.LinkData Is AutoLink Then _LinkURI = CType(link.LinkData, AutoLink).LinkURI
            End Sub
            ''' <summary>The item that was clicked</summary>
            Public ReadOnly Property Item() As LinkItem
                Get
                    Return _Item
                End Get
            End Property
            ''' <summary>In case the item is of the type <see cref="AutoLink"/> then contains value of the <see cref="AutoLink.LinkURI"/> of the item.</summary>
            Public ReadOnly Property LinkURI() As Uri
                Get
                    Return _LinkURI
                End Get
            End Property
        End Class

        ''' <summary>Performs click event on <see cref="LinkLabel"/>. Causes raising the <see cref="Click"/> event</summary>
        ''' <remarks><see cref="LinkLabel.MouseDown"/>, <see cref="LinkLabel.MouseUp"/> and <see cref="MouseClick"/> events are not raised</remarks>
        Public Overridable Sub PerformClick()
            OnClick(EventArgs.Empty)
        End Sub
        ''' <summary>Perform click event on specified item of <see cref="LinkLabel"/></summary>
        ''' <param name="Item">Item on which perform the click</param>
        ''' <param name="button">Mouse button to be simulated</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Item"/> is null</exception>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Item"/> not found in the <see cref="Items"/> collection</exception>
        ''' <exception cref="InvalidOperationException">
        ''' <paramref name="Item"/> found in the <see cref="Items"/> colletion but not found as <see cref="Link.LinkData"/> in the <see cref="Links"/> collection. Note: This is internal error of <see cref="LinkLabel"/> and thus should not be thrown if <see cref="LinkLabel"/> is implemented properly.
        ''' Do not change value of <see cref="Link.LinkData"/> or this exception will be thrown though there is no bug in <see cref="LinkLabel"/>.
        ''' </exception>
        ''' <remarks>
        ''' <para>Causes raising <see cref="Click"/> and <see cref="LinkClicked"/> events</para>
        ''' <para><see cref="LinkLabel.MouseDown"/>, <see cref="LinkLabel.MouseUp"/> and <see cref="MouseClick"/> events are not raised</para>
        ''' </remarks>
        Public Overridable Sub PerformClick(ByVal Item As LinkItem, Optional ByVal button As MouseButtons = System.Windows.Forms.MouseButtons.None)
            If Item Is Nothing Then Throw New ArgumentNullException("Item", "Item cannot be null")
            If Items.Contains(Item) Then
                For Each l As Link In MyBase.Links
                    If l.LinkData Is Item Then
                        OnLinkClicked(New LinkLabelLinkClickedEventArgs(l, button))
                        Return
                    End If
                Next l
                Throw New InvalidOperationException("LinkLabel internal exception: Link for item not found")
            Else
                Throw New ArgumentOutOfRangeException("Item", "Cannot locate Item in Items")
            End If
        End Sub

        ''' <summary>Gets the collection of links contained within the <see cref="LinkLabel"/>.</summary>
        ''' <remarks>
        ''' <para>This shadowes property is read-only. Do not use unshadowing workarounds to obtain read-write acces to te <see cref="System.Windows.Forms.LinkLabel.Links"/> property - it will cause unxpected behaviour. Use the <see cref="Items"/> collection instead.</para>
        ''' <para>Note that any change to the <see cref="Items"/> collections causes complete change of <see cref="Links"/> collection</para>
        ''' <para>Do not change <see cref="Link.Start"/> or <see cref="Link.Length"/> properties unless you know what you are doing.</para>
        ''' </remarks>
        <Browsable(False)> _
        Public Shadows ReadOnly Property Links() As IReadOnlyList(Of Link)
            Get
                Return New ReadOnlyListAdapter(Of Link)(New List(Of Link)(MyBase.Links))
            End Get
        End Property
        ''' <summary>Gets acces to link associated with <see cref="LinkItem"/> contained in <see cref="Items"/></summary>
        ''' <param name="Item">Item which link to obtain</param>
        ''' <returns><see cref="Link"/> which <paramref name="Item"/> is represented by</returns>
        ''' <exception cref="ArgumentOutOfRangeException"><paramref name="Item"/> not found in <see cref="Items"/></exception>
        ''' <exception cref="InvalidOperationException">
        ''' <paramref name="Item"/> found in the <see cref="Items"/> colletion but not found as <see cref="Link.LinkData"/> in the <see cref="Links"/> collection. Note: This is internal error of <see cref="LinkLabel"/> and thus should not be thrown if <see cref="LinkLabel"/> is implemented properly.
        ''' Do not change value of <see cref="Link.LinkData"/> or this exception will be thrown though there is no bug in <see cref="LinkLabel"/>.</exception>
        ''' <remarks>
        ''' <para>Do not make any changes in the <see cref="Items"/> collection after modyfiyng any link or after creating reference to it. Also do not change values of items contained in the <see cref="Items"/> collection. These changes causes invalidation of all links.</para>
        ''' <para>Do not change <see cref="Link.Start"/> or <see cref="Link.Length"/> properties unless you know what you are doing.</para>
        ''' </remarks>
        <Browsable(False)> _
        Public Overridable Shadows ReadOnly Property Links(ByVal Item As LinkItem) As Link
            Get
                If Items.Contains(Item) Then
                    For Each l As Link In MyBase.Links
                        If l.LinkData Is Item Then Return l
                    Next l
                    Throw New InvalidOperationException("LinkLabel internal error: Item found in Items but not found in Links")
                Else
                    Throw New ArgumentOutOfRangeException("Item", "Item not found in Items")
                End If
            End Get
        End Property

#Region "Item classes"
        ''' <summary>Common base for items in <see cref="LinkLabel.Items"/></summary>
        <DebuggerDisplay("{ToString}"), DefaultProperty("Text"), DefaultEvent("Changed")> _
        Public MustInherit Class LinkLabelItem : Implements IReportsChange
            ''' <summary>Value of the <see cref="IReportsChange.ValueChangedEventArgs(Of String).ValueName"/> passed in the <see cref="Changed"/> event when the <see cref="Text"/> property changes</summary>
            Public Const TextPropertyName As String = "Text"
            ''' <summary>Text to be shown</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _Text As String
            ''' <summary>Gets or sets text shown in place of this item</summary>
            ''' <remarks>Note for inheritors: Call <see cref="OnChanged"/> after change of value (unless calling base class setter <see cref="Text"/>)</remarks>
            <Description("Text to be show in place of this item"), Category(CategoryAttributeValues.Properties.Appearance)> _
            <DefaultValue("")> _
            <Editor(GetType(MultilineStringEditor), GetType(UITypeEditor))> _
            Public Overridable Property Text() As String
                <DebuggerStepThrough()> _
                Get
                    Return _Text
                End Get
                Set(ByVal value As String)
                    Dim OldVal As String = Text
                    _Text = value
                    OnChanged(New IReportsChange.ValueChangedEventArgs(Of String)(OldVal, value, TextPropertyName))
                End Set
            End Property
            ''' <summary>String representation of this instance</summary>
            Public Overrides Function ToString() As String
                Return Text
            End Function
            ''' <summary>Raised when value of member changes</summary>
            ''' <param name="sender">The source of the event</param>
            ''' <param name="e">Event information</param>
            ''' <remarks><paramref name="e"/>Should contain additional information that can be used in event-handling code</remarks>
            Public Event Changed(ByVal sender As IReportsChange, ByVal e As System.EventArgs) Implements IReportsChange.Changed
            ''' <summary>Raises the <see cref="Changed"/> event</summary>
            ''' <param name="e">Event parameters</param>
            ''' <remarks>Note for inheritors: Always call base class <see cref="OnChanged"/> method in order the event to be raised</remarks>
            Protected Overridable Sub OnChanged(ByVal e As EventArgs)
                RaiseEvent Changed(Me, e)
            End Sub
        End Class

        ''' <summary>Non-link (text only) item of <see cref="LinkLabel"/></summary>
        <Drawing.ToolboxBitmap(GetType(Label))> _
        Public Class TextItem : Inherits LinkLabelItem
            ''' <summary>CTor (initializes with an empty string)</summary>
            Sub New()
                Text = ""
            End Sub
            ''' <summary>CTor (initializes with text to display)</summary>
            ''' <param name="Text">Text to be displayed</param>
            Sub New(ByVal Text As String)
                Me.Text = Text
            End Sub
        End Class

        ''' <summary>Generic link</summary>
        <Drawing.ToolboxBitmap(GetType(System.Windows.Forms.LinkLabel))> _
        Public Class LinkItem : Inherits LinkLabelItem
            ''' <summary>Value of the <see cref="IReportsChange.ValueChangedEventArgs(Of Object).ValueName"/> passed in the <see cref="Changed"/> event when the <see cref="LinkData"/> property changes</summary>
            Public Const LinkDataPropertyName As String = "LinkData"
            ''' <summary>Value of the <see cref="IReportsChange.ValueChangedEventArgs(Of String).ValueName"/> passed in the <see cref="Changed"/> event when the <see cref="Description"/> property changes</summary>
            Public Const DescriptionPropertyName As String = "Description"
            ''' <summary>Value of the <see cref="IReportsChange.ValueChangedEventArgs(Of String).ValueName"/> passed in the <see cref="Changed"/> event when the <see cref="Name"/> property changes</summary>
            Public Const NamePropertyName As String = "Name"
            ''' <summary>Value of the <see cref="IReportsChange.ValueChangedEventArgs(Of Object).ValueName"/> passed in the <see cref="Changed"/> event when the <see cref="Tag"/> property changes</summary>
            Public Const TagPropertyName As String = "Tag"
            ''' <summary>Value of the <see cref="IReportsChange.ValueChangedEventArgs(Of Boolean).ValueName"/> passed in the <see cref="Changed"/> event when the <see cref="Visited"/> property changes</summary>
            Public Const VisitedPropertyName As String = "Visited"
            ''' <summary>Value of the <see cref="IReportsChange.ValueChangedEventArgs(Of Boolean).ValueName"/> passed in the <see cref="Changed"/> event when the <see cref="Enabled"/> property changes</summary>
            Public Const EnabledPropertyName As String = "Enabled"
            ''' <summary>CTor (initializes with an empty string as <see cref="Text"/> and null as <see cref="LinkData"/></summary>
            <DebuggerStepThrough()> _
            Public Sub New()
                Me.New("")
            End Sub
            ''' <summary>String representation</summary>
            ''' <remarks>If <see cref="Name"/> is not an empty string then returns <see cref="Name"/> otherwise returns <see cref="Text"/></remarks>
            Public Overrides Function ToString() As String
                If Me.Name <> "" Then Return Name Else Return Me.Text
            End Function
            ''' <summary>CTor (initializes <see cref="Text"/> and optionally <see cref="LinkData"/></summary>
            ''' <param name="Text">Text to be shown</param>
            ''' <param name="LinkData">Data associtaed with new link</param>
            Public Sub New(ByVal Text As String, Optional ByVal LinkData As Object = Nothing)
                Me.Text = Text
                Me.LinkData = LinkData
            End Sub
            ''' <summary>Data associated with the link</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _LinkData As Object
            ''' <summary>Gets or sets data associated with the link</summary>
            ''' <remarks>Note for inheritors: Call <see cref="OnChanged"/> after tha value is changed (unless calling base class setter <see cref="LinkData"/>)</remarks>
            <Category(CategoryAttributeValues.Properties.Behavior), Description("Data associated with this link")> _
            <System.ComponentModel.DefaultValue(GetType(Object), "null pointer")> _
            <System.ComponentModel.TypeConverter(GetType(ObjectStringConverter))> _
            Public Overridable Property LinkData() As Object
                <DebuggerStepThrough()> _
                Get
                    Return _LinkData
                End Get
                Set(ByVal value As Object)
                    Dim old As Object = LinkData
                    _LinkData = value
                    OnChanged(New IReportsChange.ValueChangedEventArgs(Of Object)(old, value, LinkDataPropertyName))
                End Set
            End Property
#Region "Other props"
            ''' <summary>Stores value for the <see cref="Link.Description"/> of <see cref="Link"/> that represents this <see cref="LinkItem"/></summary>
            ''' <remarks>Note for inheritors: Call base class setter or <see cref="OnChanged"/> method in order to raise the <see cref="Changed"/> event</remarks>
            <Category(CategoryAttributeValues.Properties.Appearance), Description("Stores value for the Link.Description of Link that represents this LinkItem")> _
            <DefaultValue("")> _
            Public Overridable Property Description() As String
                <DebuggerStepThrough()> Get
                    Return _Description
                End Get
                <DebuggerStepThrough()> Set(ByVal value As String)
                    Dim old As String = Description
                    _Description = value
                    OnChanged(New IReportsChange.ValueChangedEventArgs(Of String)(old, value, DescriptionPropertyName))
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Description"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Description As String = ""
            ''' <summary>Stores value for the <see cref="Link.Name"/> of <see cref="Link"/> that represents this <see cref="LinkItem"/></summary>
            ''' <remarks>Note for inheritors: Call base class setter or <see cref="OnChanged"/> method in order to raise the <see cref="Changed"/> event</remarks>
            <Category(CategoryAttributeValues.Properties.Misc), Description("Stores value for the Link.Name of Link that represents this LinkItem")> _
            <DefaultValue(""), DisplayName(NamePropertyName)> _
            Public Overridable Property Name() As String
                <DebuggerStepThrough()> Get
                    Return _Name
                End Get
                <DebuggerStepThrough()> Set(ByVal value As String)
                    Dim old As String = Name
                    _Name = value
                    OnChanged(New IReportsChange.ValueChangedEventArgs(Of String)(old, value, NamePropertyName))
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Name"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Name As String = ""
            ''' <summary>Stores value for the <see cref="Link.Tag"/> of <see cref="Link"/> that represents this <see cref="LinkItem"/></summary>
            ''' <remarks>Note for inheritors: Call base class setter or <see cref="OnChanged"/> method in order to raise the <see cref="Changed"/> event</remarks>
            <Category(CategoryAttributeValues.Properties.Data), Description("Stores value for the Link.Tag of Link that represents this LinkItem")> _
            <System.ComponentModel.DefaultValue(GetType(Object), "null pointer")> _
            <System.ComponentModel.TypeConverter(GetType(ObjectStringConverter))> _
            Public Overridable Property Tag() As Object
                <DebuggerStepThrough()> Get
                    Return _Tag
                End Get
                <DebuggerStepThrough()> Set(ByVal value As Object)
                    Dim old As Object = Tag
                    _Tag = value
                    OnChanged(New IReportsChange.ValueChangedEventArgs(Of Object)(old, value, TagPropertyName))
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Tag"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Tag As Object = Nothing
            ''' <summary>Stores value for the <see cref="Link.Visited"/> of <see cref="Link"/> that represents this <see cref="LinkItem"/></summary>
            ''' <remarks>Note for inheritors: Call base class setter or <see cref="OnChanged"/> method in order to raise the <see cref="Changed"/> event</remarks>
            <Category(CategoryAttributeValues.Properties.Appearance), Description("Stores value for the Link.Visited of Link that represents this LinkItem")> _
            <DefaultValue(False)> _
            Public Overridable Property Visited() As Boolean
                <DebuggerStepThrough()> Get
                    Return _Visited
                End Get
                <DebuggerStepThrough()> Set(ByVal value As Boolean)
                    Dim old As Boolean = Visited
                    _Visited = value
                    OnChanged(New IReportsChange.ValueChangedEventArgs(Of Boolean)(old, value, VisitedPropertyName))
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Visited"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Visited As Boolean = False
            ''' <summary>Stores value for the <see cref="Link.Enabled"/> of <see cref="Link"/> that represents this <see cref="LinkItem"/></summary>
            ''' <remarks>Note for inheritors: Call base class setter or <see cref="OnChanged"/> method in order to raise the <see cref="Changed"/> event</remarks>
            <Category(CategoryAttributeValues.Properties.Appearance), Description("Stores value for the Link.Enabled of Link that represents this LinkItem")> _
            <DefaultValue(True)> _
            Public Overridable Property Enabled() As Boolean
                <DebuggerStepThrough()> Get
                    Return _Enabled
                End Get
                <DebuggerStepThrough()> Set(ByVal value As Boolean)
                    Dim old As Boolean = Enabled
                    _Enabled = value
                    OnChanged(New IReportsChange.ValueChangedEventArgs(Of Boolean)(old, value, EnabledPropertyName))
                End Set
            End Property
            ''' <summary>Contains value of the <see cref="Enabled"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> Private _Enabled As Boolean = True
#End Region
        End Class

        ''' <summary>Link that performs navigation automatically</summary>
        <DebuggerDisplay("{ToString} ({LinkPath})")> _
        Public Class AutoLink : Inherits LinkItem
            ''' <summary>Value of the <see cref="IReportsChange.ValueChangedEventArgs(Of Uri).ValueName"/> passed in the <see cref="Changed"/> event when the <see cref="LinkURI"/> property changes</summary>
            Public Const LinkURIPropertyName As String = "LinkURI"
#Region "CTors"
            ''' <summary>CTor</summary>
            ''' <param name="Text">Text to display</param>
            Public Sub New(ByVal Text As String)
                Me.Text = Text
                LinkURI = Nothing
            End Sub
            ''' <summary>CTor</summary>
            Public Sub New()
                Me.New("")
            End Sub
            ''' <summary>CTor</summary>
            ''' <param name="Text">Text to display</param>
            ''' <param name="LinkURI">URI of target of new link</param>
            Public Sub New(ByVal Text As String, ByVal LinkURI As Uri)
                Me.Text = Text
                Me.LinkURI = LinkURI
            End Sub
            ''' <summary>CTor</summary>
            ''' <param name="Text">Text to display</param>
            ''' <param name="LinkPath">Path of target of new link</param>
            ''' <exception cref="System.ArgumentNullException"><paramref name="value"/> is null -and- type of <paramref name="value"/> is <see cref="String"/></exception>
            ''' <exception cref="System.UriFormatException">Type of <paramref name="value"/> is <see cref="String"/> -and- <paramref name="value"/> is empty.-or- The scheme specified in <paramref name="value"/> is not correctly formed. See <see cref="System.Uri.CheckSchemeName"/>.-or- <paramref name="value"/> contains too many slashes.-or- The password specified in <paramref name="value"/> is not valid.-or- The host name specified in <paramref name="value"/> is not valid.-or- The file name specified in <paramref name="value"/> is not valid. -or- The user name specified in <paramref name="value"/> is not valid.-or- The host or authority name specified in <paramref name="value"/> cannot be terminated by backslashes.-or- The port number specified in <paramref name="value"/> is not valid or cannot be parsed.-or- The length of <paramref name="value"/> exceeds 65534 characters.-or- The length of the scheme specified in <paramref name="value"/> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="value"/>.-or- The MS-DOS path specified in <paramref name="value"/> must start with c:\\.</exception>
            Public Sub New(ByVal Text As String, ByVal LinkPath As String)
                Me.Text = Text
                Me.LinkPath = LinkPath
            End Sub
            ''' <summary>CTor - initializes with URI and uses URI's string representation as text</summary>
            ''' <param name="LinkURI">URI of terger of new link</param>
            Public Sub New(ByVal LinkURI As Uri)
                Me.New(LinkURI.ToString, LinkURI)
            End Sub
#End Region
            ''' <summary>Target of link</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _LinkURI As Uri
            ''' <summary>Data associated with the link</summary>
            ''' <value>New associated data (NOTE: value must be of type <see cref="Uri"/> or of type <see cref="String"/> that cab be used as parameter of <see cref="Uri"/>'s CTor)</value>
            ''' <returns>Data associated with the link</returns>
            ''' <exception cref="System.ArgumentNullException"><paramref name="value"/> is null -and- type of <paramref name="value"/> is <see cref="String"/></exception>
            ''' <exception cref="System.UriFormatException">Type of <paramref name="value"/> is <see cref="String"/> -and- <paramref name="value"/> is empty.-or- The scheme specified in <paramref name="value"/> is not correctly formed. See <see cref="System.Uri.CheckSchemeName"/>.-or- <paramref name="value"/> contains too many slashes.-or- The password specified in <paramref name="value"/> is not valid.-or- The host name specified in <paramref name="value"/> is not valid.-or- The file name specified in <paramref name="value"/> is not valid. -or- The user name specified in <paramref name="value"/> is not valid.-or- The host or authority name specified in <paramref name="value"/> cannot be terminated by backslashes.-or- The port number specified in <paramref name="value"/> is not valid or cannot be parsed.-or- The length of <paramref name="value"/> exceeds 65534 characters.-or- The length of the scheme specified in <paramref name="value"/> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="value"/>.-or- The MS-DOS path specified in <paramref name="value"/> must start with c:\\.</exception>
            ''' <exception cref="InvalidCastException">Type of <paramref name="value"/> is neither <see cref="Uri"/> nor <see cref="String"/></exception>
            <EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use type safe variant (LinkURI or LinkPath) instead")> _
            <Browsable(False), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
            <System.ComponentModel.DefaultValue(GetType(Object), "null pointer")> _
            <System.ComponentModel.TypeConverter(GetType(ObjectStringConverter))> _
            Public Overrides Property LinkData() As Object
                <DebuggerStepThrough()> _
                Get
                    Return MyBase.LinkData
                End Get
                Set(ByVal value As Object)
                    If TypeOf value Is Uri Then
                        LinkURI = value
                    ElseIf TypeOf value Is String OrElse value Is Nothing Then
                        LinkPath = value
                    Else
                        Throw New InvalidCastException("Value can be converter neither to Uri nor to String")
                    End If
                End Set
            End Property
            ''' <summary>Gets or sets URI to navigate to</summary>
            ''' <value>Actuall URI or target of the link</value>
            ''' <returns>New URI of target of the link</returns>
            ''' <remarks>Note for inheritors: Call <see cref="OnChanged"/> after value is changed (unless calling base class setter <see cref="LinkURI"/>)</remarks>
            <Category(CategoryAttributeValues.Properties.Behavior), Description("URI of target of the link")> _
            <DefaultValue(GetType(Uri), "null pointer")> _
            <TypeConverter(GetType(UriTypeConverter))> _
            Public Overridable Property LinkURI() As Uri
                <DebuggerStepThrough()> _
                Get
                    Return _LinkURI
                End Get
                Set(ByVal value As Uri)
                    Dim old As Uri = LinkURI
                    _LinkURI = value
                    MyBase.LinkData = _LinkURI
                    OnChanged(New IReportsChange.ValueChangedEventArgs(Of Uri)(old, value, LinkURIPropertyName))
                End Set
            End Property
            ''' <summary>Gets or sets URI (in form of path string) to navigate to</summary>
            ''' <returns>Actuall path of target of the link</returns>
            ''' <value>New path of target of the link</value>
            ''' <exception cref="System.UriFormatException"><paramref name="value"/> is empty.-or- The scheme specified in <paramref name="value"/> is not correctly formed. See <see cref="System.Uri.CheckSchemeName"/>.-or- <paramref name="value"/> contains too many slashes.-or- The password specified in <paramref name="value"/> is not valid.-or- The host name specified in <paramref name="value"/> is not valid.-or- The file name specified in <paramref name="value"/> is not valid. -or- The user name specified in <paramref name="value"/> is not valid.-or- The host or authority name specified in <paramref name="value"/> cannot be terminated by backslashes.-or- The port number specified in <paramref name="value"/> is not valid or cannot be parsed.-or- The length of <paramref name="value"/> exceeds 65534 characters.-or- The length of the scheme specified in <paramref name="value"/> exceeds 1023 characters.-or- There is an invalid character sequence in <paramref name="value"/>.-or- The MS-DOS path specified in <paramref name="value"/> must start with c:\\.</exception>
            ''' <remarks>
            ''' <para>Exceptions thrown by <see cref="Uri"/>'s CTor</para>
            ''' <para>Note for inheritors: Call <see cref="OnChanged"/> (unless callin base class setter <see cref="LinkPath"/> or <see cref="LinkURI"/></para>
            ''' <para>Change of this value causes raising <see cref="Changed"/> event with <see cref="IReportsChange.ValueChangedEventArgs(Of Uri).ValueName"/> set to <see cref="LinkURIPropertyName"/></para>
            ''' </remarks>
            <Category(CategoryAttributeValues.Properties.Behavior), Description("Path (string representation of URI) of target of the link")> _
            <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
            <DefaultValue("")> _
            Public Property LinkPath() As String
                <DebuggerStepThrough()> _
                Get
                    If _LinkURI Is Nothing Then
                        Return ""
                    Else
                        Return _LinkURI.ToString
                    End If
                End Get
                Set(ByVal value As String)
                    Dim old As Uri = LinkURI
                    If value = "" Then
                        _LinkURI = Nothing
                        OnChanged(New IReportsChange.ValueChangedEventArgs(Of Uri)(old, Nothing, LinkURIPropertyName))
                    Else
                        _LinkURI = New Uri(value)
                        OnChanged(New IReportsChange.ValueChangedEventArgs(Of Uri)(old, LinkURI, LinkURIPropertyName))
                    End If
                    MyBase.LinkData = _LinkURI
                End Set
            End Property
        End Class
#End Region

        ''' <summary>Allows editing of the <see cref="Items"/> collection at design-time</summary>
        Public Class LinkLabelItemsEditor : Inherits CollectionEditor
            ''' <summary>CTor</summary>
            ''' <remarks>Initializes base class <see cref="CollectionEditor"/> with type <see cref="ListWithEvents(Of LinkLabel.LinkLabelItem)"/></remarks>
            Public Sub New()
                Me.New(False)
            End Sub
            ''' <summary>CTor</summary>
            ''' <param name="UseStandardEditorForm">Initial value for the <see cref="UseStandardEditorForm"/> property</param>
            ''' <remarks>Initializes base class <see cref="CollectionEditor"/> with type <see cref="ListWithEvents(Of LinkLabel.LinkLabelItem)"/></remarks>
           Public Sub New(ByVal UseStandardEditorForm As Boolean)
                MyBase.New(GetType(ListWithEvents(Of LinkLabel.LinkLabelItem)))
            End Sub
            ''' <summary>Contains value of the <see cref="UseStandardEditorForm"/> property</summary>
            <EditorBrowsable(EditorBrowsableState.Never)> _
            Private _UseStandardEditorForm As Boolean
            ''' <summary>Determines behavior of the <see cref="CreateCollectionForm"/> function. If True the that function returns <see cref="CollectionEditor.CreateCollectionForm"/> otherwise returns instance of <see cref="CollectionForm"/></summary>
            <DefaultValue(False)> _
            Public Property UseStandardEditorForm() As Boolean
                <DebuggerStepThrough()> Get
                    Return _UseStandardEditorForm
                End Get
                <DebuggerStepThrough()> Set(ByVal value As Boolean)
                    _UseStandardEditorForm = value
                End Set
            End Property
            ''' <summary>Types of items tha can be added into collection</summary>
            Private Types As Type() = {GetType(TextItem), GetType(LinkItem), GetType(AutoLink)}

            ''' <summary>Gets the data types that this collection editor can contain.</summary>
            ''' <returns>An array of data types that this collection can contain.</returns>
            <DebuggerStepThrough()> Protected Overrides Function CreateNewItemTypes() As System.Type()
                Return Types
            End Function
            ''' <summary>Creates a new instance of the specified collection item type.</summary>
            ''' <param name="itemType">The type of item to create.</param>
            ''' <returns>A new instance of the specified object.</returns>
            ''' <exception cref="ArgumentException"><paramref name="itemType"/> doesn't represent supported type - supported types are: <list><item><see cref="TextItem"/></item> <item><see cref="LinkItem"/></item> <see cref="AutoLink"/></list></exception>
            Protected Overrides Function CreateInstance(ByVal itemType As System.Type) As Object
                Dim ret As LinkLabelItem
                If itemType.Equals(GetType(TextItem)) Then
                    ret = New TextItem("TextItem")
                ElseIf itemType.Equals(GetType(AutoLink)) Then
                    ret = New AutoLink("AutoLink")
                ElseIf itemType.Equals(GetType(LinkItem)) Then
                    ret = New LinkItem("LinkItem")
                ElseIf itemType.Equals(GetType(LinkLabelItem)) Then
                    ret = New TextItem("textItem")
                Else
                    Throw New ArgumentException("Type " & itemType.FullName & " is not supported", "itemType")
                End If
                Return ret
            End Function
            ''' <summary>Gets the data type that this collection contains.</summary>
            ''' <returns><see cref="LinkLabelItem"/> type</returns>
            Protected Overrides Function CreateCollectionItemType() As Type
                Return GetType(LinkLabelItem)
            End Function
            ''' <summary>Creates a new form to display and edit the current collection.</summary>
            ''' <returns>
            ''' <para>A <see cref="System.ComponentModel.Design.CollectionEditor.CollectionForm"/> to provide as the user interface for editing the collection.</para>
            ''' <para>Depending on <see cref="UseStandardEditorForm"/> returns either <see cref="System.ComponentModel.Design.CollectionEditor.CollectionForm"/> returned by <see cref="System.ComponentModel.Design.CollectionEditor.CreateCollectionForm"/> or instance or <see cref="CollectionForm"/></para>
            ''' </returns>
            Protected Overrides Function CreateCollectionForm() As System.ComponentModel.Design.CollectionEditor.CollectionForm
                If UseStandardEditorForm Then
                    Return MyBase.CreateCollectionForm
                Else
                    Return New CollectionForm(Me)
                End If
            End Function
            Public Overrides Function EditValue(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal provider As System.IServiceProvider, ByVal value As Object) As Object
                EditValue = MyBase.EditValue(context, provider, value)
                If context IsNot Nothing AndAlso context.Instance IsNot Nothing AndAlso provider IsNot Nothing Then
                    MyBase.Context.OnComponentChanged()
                End If
            End Function
            ''' <summary>Provides a modal dialog box for editing the contents of the <see cref="ListWithEvents(Of LinkLabelItem)"/>  using a <see cref="System.Drawing.Design.UITypeEditor"/>.</summary>
            <Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
            Protected Shadows Class CollectionForm
                Inherits CollectionEditor.CollectionForm

#Region "WindowsForms Designer generated code"
                ''' <summary>Form overrides dispose to clean up the component list.</summary>
                <System.Diagnostics.DebuggerNonUserCode()> _
                Protected Overrides Sub Dispose(ByVal disposing As Boolean)
                    Try
                        If disposing AndAlso components IsNot Nothing Then
                            components.Dispose()
                        End If
                    Finally
                        MyBase.Dispose(disposing)
                    End Try
                End Sub
                ''' <summary>Shows information about select items</summary>
                Private WithEvents lblItemInfo As System.Windows.Forms.Label
                ''' <summary>Required by the Windows Form Designer</summary>
                Private components As System.ComponentModel.IContainer

                ''' <summary>Initializes components</summary>
                ''' <remarks>
                ''' NOTE: The following procedure is required by the Windows Form Designer
                ''' It can be modified using the Windows Form Designer.  
                ''' Do not modify it using the code editor.
                ''' </remarks>
                <System.Diagnostics.DebuggerStepThrough()> _
                <EditorBrowsable(EditorBrowsableState.Advanced)> _
                Private Sub InitializeComponent()
                    Me.components = New System.ComponentModel.Container
                    Me.splMain = New System.Windows.Forms.SplitContainer
                    Me.tlpItems = New System.Windows.Forms.TableLayoutPanel
                    Me.lstItems = New System.Windows.Forms.ListBox
                    Me.cmdUp = New System.Windows.Forms.Button
                    Me.cmdDown = New System.Windows.Forms.Button
                    Me.tosAdd = New System.Windows.Forms.ToolStrip
                    Me.tsbAdd = New System.Windows.Forms.ToolStripSplitButton
                    Me.tosRemove = New System.Windows.Forms.ToolStrip
                    Me.tsbRemove = New System.Windows.Forms.ToolStripButton
                    Me.cmdCancel = New System.Windows.Forms.Button
                    Me.cmdOK = New System.Windows.Forms.Button
                    Me.pgrProperty = New System.Windows.Forms.PropertyGrid
                    Me.totTT = New System.Windows.Forms.ToolTip(Me.components)
                    Me.lblItemInfo = New System.Windows.Forms.Label
                    Me.splMain.Panel1.SuspendLayout()
                    Me.splMain.Panel2.SuspendLayout()
                    Me.splMain.SuspendLayout()
                    Me.tlpItems.SuspendLayout()
                    Me.tosAdd.SuspendLayout()
                    Me.tosRemove.SuspendLayout()
                    Me.SuspendLayout()
                    '
                    'splMain
                    '
                    Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
                    Me.splMain.Location = New System.Drawing.Point(0, 0)
                    Me.splMain.Name = "splMain"
                    '
                    'splMain.Panel1
                    '
                    Me.splMain.Panel1.Controls.Add(Me.tlpItems)
                    '
                    'splMain.Panel2
                    '
                    Me.splMain.Panel2.Controls.Add(Me.pgrProperty)
                    Me.splMain.Panel2.Controls.Add(Me.lblItemInfo)
                    Me.splMain.Size = New System.Drawing.Size(548, 459)
                    Me.splMain.SplitterDistance = 248
                    Me.splMain.TabIndex = 0
                    '
                    'tlpItems
                    '
                    Me.tlpItems.ColumnCount = 3
                    Me.tlpItems.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
                    Me.tlpItems.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
                    Me.tlpItems.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
                    Me.tlpItems.Controls.Add(Me.lstItems, 0, 0)
                    Me.tlpItems.Controls.Add(Me.cmdUp, 2, 0)
                    Me.tlpItems.Controls.Add(Me.cmdDown, 2, 1)
                    Me.tlpItems.Controls.Add(Me.tosAdd, 0, 3)
                    Me.tlpItems.Controls.Add(Me.tosRemove, 1, 3)
                    Me.tlpItems.Controls.Add(Me.cmdCancel, 2, 3)
                    Me.tlpItems.Controls.Add(Me.cmdOK, 2, 2)
                    Me.tlpItems.Dock = System.Windows.Forms.DockStyle.Fill
                    Me.tlpItems.Location = New System.Drawing.Point(0, 0)
                    Me.tlpItems.Name = "tlpItems"
                    Me.tlpItems.RowCount = 4
                    Me.tlpItems.RowStyles.Add(New System.Windows.Forms.RowStyle)
                    Me.tlpItems.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
                    Me.tlpItems.RowStyles.Add(New System.Windows.Forms.RowStyle)
                    Me.tlpItems.RowStyles.Add(New System.Windows.Forms.RowStyle)
                    Me.tlpItems.Size = New System.Drawing.Size(248, 459)
                    Me.tlpItems.TabIndex = 1
                    '
                    'lstItems
                    '
                    Me.tlpItems.SetColumnSpan(Me.lstItems, 2)
                    Me.lstItems.Dock = System.Windows.Forms.DockStyle.Fill
                    Me.lstItems.FormattingEnabled = True
                    Me.lstItems.Location = New System.Drawing.Point(3, 3)
                    Me.lstItems.Name = "lstItems"
                    Me.tlpItems.SetRowSpan(Me.lstItems, 3)
                    Me.lstItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
                    Me.lstItems.Size = New System.Drawing.Size(186, 420)
                    Me.lstItems.TabIndex = 0
                    '
                    'cmdUp
                    '
                    Me.cmdUp.Anchor = System.Windows.Forms.AnchorStyles.Top
                    Me.cmdUp.AutoSize = True
                    Me.cmdUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
                    Me.cmdUp.Enabled = False
                    Me.cmdUp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    Me.cmdUp.Location = New System.Drawing.Point(206, 3)
                    Me.cmdUp.Name = "cmdUp"
                    Me.cmdUp.Size = New System.Drawing.Size(28, 24)
                    Me.cmdUp.TabIndex = 1
                    Me.cmdUp.Text = "▲"
                    Me.totTT.SetToolTip(Me.cmdUp, "Move up")
                    Me.cmdUp.UseVisualStyleBackColor = True
                    '
                    'cmdDown
                    '
                    Me.cmdDown.Anchor = System.Windows.Forms.AnchorStyles.Top
                    Me.cmdDown.AutoSize = True
                    Me.cmdDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
                    Me.cmdDown.Enabled = False
                    Me.cmdDown.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    Me.cmdDown.Location = New System.Drawing.Point(206, 33)
                    Me.cmdDown.Name = "cmdDown"
                    Me.cmdDown.Size = New System.Drawing.Size(28, 24)
                    Me.cmdDown.TabIndex = 2
                    Me.cmdDown.Text = "▼"
                    Me.totTT.SetToolTip(Me.cmdDown, "Move down")
                    Me.cmdDown.UseVisualStyleBackColor = True
                    '
                    'tosAdd
                    '
                    Me.tosAdd.Anchor = System.Windows.Forms.AnchorStyles.None
                    Me.tosAdd.CanOverflow = False
                    Me.tosAdd.Dock = System.Windows.Forms.DockStyle.None
                    Me.tosAdd.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
                    Me.tosAdd.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbAdd})
                    Me.tosAdd.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
                    Me.tosAdd.Location = New System.Drawing.Point(26, 434)
                    Me.tosAdd.Name = "tosAdd"
                    Me.tosAdd.ShowItemToolTips = False
                    Me.tosAdd.Size = New System.Drawing.Size(43, 20)
                    Me.tosAdd.TabIndex = 3
                    Me.tosAdd.TabStop = True
                    '
                    'tsbAdd
                    '
                    Me.tsbAdd.AutoToolTip = False
                    Me.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
                    Me.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta
                    Me.tsbAdd.Name = "tsbAdd"
                    Me.tsbAdd.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
                    Me.tsbAdd.Size = New System.Drawing.Size(42, 17)
                    Me.tsbAdd.Text = "&Add"
                    Me.tsbAdd.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
                    '
                    'tosRemove
                    '
                    Me.tosRemove.Anchor = System.Windows.Forms.AnchorStyles.None
                    Me.tosRemove.CanOverflow = False
                    Me.tosRemove.Dock = System.Windows.Forms.DockStyle.None
                    Me.tosRemove.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
                    Me.tosRemove.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbRemove})
                    Me.tosRemove.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
                    Me.tosRemove.Location = New System.Drawing.Point(118, 434)
                    Me.tosRemove.Name = "tosRemove"
                    Me.tosRemove.ShowItemToolTips = False
                    Me.tosRemove.Size = New System.Drawing.Size(51, 20)
                    Me.tosRemove.TabIndex = 4
                    Me.tosRemove.TabStop = True
                    '
                    'tsbRemove
                    '
                    Me.tsbRemove.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
                    Me.tsbRemove.AutoToolTip = False
                    Me.tsbRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
                    Me.tsbRemove.DoubleClickEnabled = True
                    Me.tsbRemove.ImageTransparentColor = System.Drawing.Color.Magenta
                    Me.tsbRemove.Name = "tsbRemove"
                    Me.tsbRemove.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
                    Me.tsbRemove.Size = New System.Drawing.Size(50, 17)
                    Me.tsbRemove.Text = "&Remove"
                    '
                    'cmdCancel
                    '
                    Me.cmdCancel.Anchor = System.Windows.Forms.AnchorStyles.None
                    Me.cmdCancel.AutoSize = True
                    Me.cmdCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
                    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
                    Me.cmdCancel.Location = New System.Drawing.Point(195, 433)
                    Me.cmdCancel.Name = "cmdCancel"
                    Me.cmdCancel.Size = New System.Drawing.Size(50, 23)
                    Me.cmdCancel.TabIndex = 6
                    Me.cmdCancel.Text = "&Cancel"
                    Me.totTT.SetToolTip(Me.cmdCancel, "Closes editor and dicsard changes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Note: Changes on individual items are not disc" & _
                            "areded" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "only Add, Up, Down, Remove operations can be canceled.")
                    Me.cmdCancel.UseVisualStyleBackColor = True
                    '
                    'cmdOK
                    '
                    Me.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.None
                    Me.cmdOK.AutoSize = True
                    Me.cmdOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
                    Me.cmdOK.Location = New System.Drawing.Point(204, 404)
                    Me.cmdOK.Name = "cmdOK"
                    Me.cmdOK.Size = New System.Drawing.Size(32, 23)
                    Me.cmdOK.TabIndex = 5
                    Me.cmdOK.Text = "&OK"
                    Me.totTT.SetToolTip(Me.cmdOK, "Closes editor and applies changes")
                    Me.cmdOK.UseVisualStyleBackColor = True
                    '
                    'pgrProperty
                    '
                    Me.pgrProperty.Dock = System.Windows.Forms.DockStyle.Fill
                    Me.pgrProperty.Location = New System.Drawing.Point(0, 13)
                    Me.pgrProperty.Name = "pgrProperty"
                    Me.pgrProperty.Size = New System.Drawing.Size(296, 446)
                    Me.pgrProperty.TabIndex = 0
                    '
                    'lblItemInfo
                    '
                    Me.lblItemInfo.Dock = System.Windows.Forms.DockStyle.Top
                    Me.lblItemInfo.Location = New System.Drawing.Point(0, 0)
                    Me.lblItemInfo.Name = "lblItemInfo"
                    Me.lblItemInfo.Size = New System.Drawing.Size(296, 13)
                    Me.lblItemInfo.TabIndex = 1
                    Me.lblItemInfo.Text = "Select item"
                    Me.lblItemInfo.AutoSize = True
                    Me.lblItemInfo.AutoEllipsis = True
                    '
                    'form
                    '
                    Me.AcceptButton = Me.cmdOK
                    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
                    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
                    Me.CancelButton = Me.cmdCancel
                    Me.ClientSize = New System.Drawing.Size(548, 459)
                    Me.Controls.Add(Me.splMain)
                    Me.MaximizeBox = False
                    Me.MinimizeBox = False
                    Me.Name = "form"
                    Me.ShowIcon = False
                    Me.ShowInTaskbar = False
                    Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
                    Me.Text = "Tools.Windows.Forms.LinkLabel.Items Editor"
                    Me.splMain.Panel1.ResumeLayout(False)
                    Me.splMain.Panel2.ResumeLayout(False)
                    Me.splMain.ResumeLayout(False)
                    Me.tlpItems.ResumeLayout(False)
                    Me.tlpItems.PerformLayout()
                    Me.tosAdd.ResumeLayout(False)
                    Me.tosAdd.PerformLayout()
                    Me.tosRemove.ResumeLayout(False)
                    Me.tosRemove.PerformLayout()
                    Me.ResumeLayout(False)

                End Sub
                ''' <summary>Main <see cref="SplitContainer"/> that splits form into part of list and part of <see cref="PropertyGrid"/></summary>
                Private WithEvents splMain As System.Windows.Forms.SplitContainer
                ''' <summary>List part of collection is located here</summary>
                Private WithEvents tlpItems As System.Windows.Forms.TableLayoutPanel
                ''' <summary>Shows list of collection items</summary>
                Private WithEvents lstItems As System.Windows.Forms.ListBox
                ''' <summary>Moves selected item up</summary>
                Private WithEvents cmdUp As System.Windows.Forms.Button
                ''' <summary>Moves selected item down</summary>
                Private WithEvents cmdDown As System.Windows.Forms.Button
                ''' <summary>Contains <see cref="tsbAdd"/></summary>
                Private WithEvents tosAdd As System.Windows.Forms.ToolStrip
                ''' <summary>Contains items for adding new items to the collection</summary>
                Private WithEvents tsbAdd As System.Windows.Forms.ToolStripSplitButton
                ''' <summary>Contains <see cref="tsbRemove"/></summary>
                Private WithEvents tosRemove As System.Windows.Forms.ToolStrip
                ''' <summary>Removes selected items</summary>
                Private WithEvents tsbRemove As System.Windows.Forms.ToolStripButton
                ''' <summary>Displays and allows edit properties of selected items</summary>
                Private WithEvents pgrProperty As System.Windows.Forms.PropertyGrid
                ''' <summary>Closes form with no changes on collection</summary>
                Private WithEvents cmdCancel As System.Windows.Forms.Button
                ''' <summary>Closes form and applies changes on collection</summary>
                Private WithEvents cmdOK As System.Windows.Forms.Button
                ''' <summary>Displays tool tip text on some controls</summary>
                Private WithEvents totTT As System.Windows.Forms.ToolTip
#End Region
                ''' <summary>Contains value of the <see cref="Editor"/> property</summary>
                <EditorBrowsable(EditorBrowsableState.Never)> _
                Private _Editor As LinkLabelItemsEditor
                ''' <summary>Initializes a new instance of the <see cref="LinkLabelItemsEditor"/> class.</summary>
                ''' <param name="editor">The <see cref="LinkLabelItemsEditor"/> to use for editing the collection.</param>
                ''' <exception cref="ArgumentException">
                ''' <see cref="LinkLabelItemsEditor.CollectionType"/> of <paramref name="Editor"/> is not <see cref="ListWithEvents(Of LinkLabelItem)"/>
                ''' -or-
                ''' <see cref="LinkLabelItemsEditor.CollectionItemType"/> of <paramref name="Editor"/> is not <see cref="LinkLabelItem"/>
                ''' -or-
                ''' Any <see cref="LinkLabelItemsEditor.NewItemTypes"/> of <paramref name="Editor"/> is not <see cref="LinkLabelItem"/>
                ''' </exception>
                Public Sub New(ByVal Editor As LinkLabelItemsEditor)
                    MyBase.New(Editor)
                    If Not Editor.CollectionType.Equals(GetType(ListWithEvents(Of LinkLabelItem))) AndAlso Not Editor.CollectionType.IsSubclassOf(GetType(ListWithEvents(Of LinkLabelItem))) Then
                        Throw New ArgumentException("Editor.CollectionType must be ListWithEvents(Of LinkLabelItem)")
                    End If
                    If Not Editor.CollectionItemType.Equals(GetType(LinkLabelItem)) AndAlso Not Editor.CollectionType.IsSubclassOf(GetType(LinkLabelItem)) Then
                        Throw (New ArgumentException("Editor.CollectionItemType must be LinkLabelItem"))
                    End If
                    Me.Editor = Editor

                    MyClass.InitializeComponent()
                    lblItemInfo.MaximumSize = New Drawing.Size(lblItemInfo.MaximumSize.Width, lblItemInfo.Size.Height * 3)

                    For Each t As Type In Editor.NewItemTypes
                        If Not t.IsSubclassOf(GetType(LinkLabelItem)) AndAlso Not t.Equals(GetType(LinkLabelItem)) Then
                            Throw New ArgumentException("All types in Editor.NewItemTypes must inherit from LinkLabelItem")
                        Else
                            Dim itm As ToolStripItem = tsbAdd.DropDownItems.Add(t.Name)
                            itm.Tag = t
                            Dim inhB As Object() = t.GetCustomAttributes(GetType(Drawing.ToolboxBitmapAttribute), True)
                            Dim nInhB As Object() = t.GetCustomAttributes(GetType(Drawing.ToolboxBitmapAttribute), False)
                            Dim Bitmap As Drawing.ToolboxBitmapAttribute = Nothing
                            If nInhB IsNot Nothing AndAlso nInhB.Length > 0 Then
                                Bitmap = nInhB(0)
                            ElseIf inhB IsNot Nothing AndAlso inhB.Length > 0 Then
                                Bitmap = inhB(0)
                            End If
                            If Bitmap IsNot Nothing Then
                                itm.Image = Bitmap.GetImage(t)
                            End If
                            AddHandler itm.Click, AddressOf Add_Click
                        End If
                    Next t
                    If Not Editor.CanSelectMultipleInstances Then lstItems.SelectionMode = SelectionMode.One
                End Sub

                ''' <summary>Adds item to collection</summary>
                Private Sub Add_Click(ByVal sender As Object, ByVal e As [EventArgs])
                    Try
                        Dim index As Integer = _
                                lstItems.Items.Add(Editor.CreateInstance(CType(sender, ToolStripItem).Tag))
                        lstItems.SelectedItems.Clear()
                        lstItems.SelectedIndex = index
                    Catch ex As Exception
                        MsgBox("Cannot create instance of type " & CType(CType(sender, ToolStripItem).Tag, Type).FullName & ". " & ex.GetType.FullName & " was thrown when obtaining new instance:" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "LinkLabel Items Editor")
                    End Try
                End Sub

                ''' <summary><see cref="LinkLabelItemsEditor"/> used for editin collection</summary>
                Protected Property Editor() As LinkLabelItemsEditor
                    <DebuggerStepThrough()> Get
                        Return _Editor
                    End Get
                    <DebuggerStepThrough()> Private Set(ByVal value As LinkLabelItemsEditor)
                        _Editor = value
                    End Set
                End Property
                ''' <summary>
                ''' Provides an opportunity to perform processing when a collection value has changed.
                ''' Shows items of collection in <see cref="ListBox"/>.
                ''' </summary>
                ''' <exception cref="InvalidCastException"><see cref="System.ComponentModel.Design.CollectionEditor.CollectionForm.EditValue"/> is not of type <see cref="ListWithEvents(Of LinkLabelItem)"/></exception>
                Protected Overrides Sub OnEditValueChanged()
                    If MyBase.EditValue IsNot Nothing AndAlso Not TypeOf MyBase.EditValue Is ListWithEvents(Of LinkLabelItem) Then
                        Throw New InvalidCastException("Value of type " & Me.EditValue.GetType.FullName & " cannot be converted to " & GetType(ListWithEvents(Of LinkLabelItem)).FullName)
                    End If
                    lstItems.Items.Clear()
                    If EditValue IsNot Nothing Then
                        For Each itm As LinkLabelItem In EditValue
                            lstItems.Items.Add(itm)
                        Next itm
                    End If
                End Sub
                ''' <summary>Gets or sets the collection object to edit.</summary>
                ''' <returns>The collection object to edit.</returns>
                Public Shadows Property EditValue() As ListWithEvents(Of LinkLabelItem)
                    Get
                        Return MyBase.EditValue
                    End Get
                    Set(ByVal value As ListWithEvents(Of LinkLabelItem))
                        MyBase.EditValue = value
                    End Set
                End Property
                ''' <summary>Closes form with no changes on collection</summary>
                ''' <remarks>Changes on collection items' properties are not discarded</remarks>
                Private Sub cmdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
                    'Me.Editor.SetItems(EditValue, New ArrayList(EditValue).ToArray)
                    ClosingCancel = True
                    Me.Close()
                End Sub
                ''' <summary>If true than <see cref="CollectionForm_FormClosing"/> cancels editing</summary>
                Private ClosingCancel As Boolean = True

                Private Sub lstItems_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstItems.KeyDown
                    If e.KeyCode = Keys.Delete Then tsbRemove_Click(sender, e)
                End Sub

                Private Sub lstItems_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstItems.SelectedIndexChanged
                    If lstItems.SelectedItems.Count < 1 Then
                        pgrProperty.SelectedObject = Nothing
                        Enable(False)
                        lblItemInfo.Text = "Select item"
                    Else
                        pgrProperty.SelectedObjects = (New ArrayList(lstItems.SelectedItems)).ToArray
                        Enable(VisualBasic.iif(lstItems.SelectedItems.Count > 1, EnableMode.Multi, EnableMode.True))
                        If lstItems.SelectedItems.Count = 1 Then
                            lblItemInfo.Text = lstItems.SelectedItems(0).GetType.Name & ": " & lstItems.SelectedItems(0).ToString.Replace(vbCrLf, " ").Replace(vbCr, " ").Replace(vbLf, " ").Replace(vbTab, " ")
                        Else
                            lblItemInfo.Text = "(multiple selected items)"
                        End If
                    End If
                End Sub
                ''' <summary>Modes for <see cref="Enable"/></summary>
                Private Enum EnableMode
                    ''' <summary>Set to False</summary>
                    [False] = False
                    ''' <summary>Set to True</summary>
                    [True] = True
                    ''' <summary>Set to True (only controls that can be used when multiple items are selected)</summary>
                    Multi = 99
                End Enum
                ''' <summary>Sets <see cref="Control.Enabled"/> for item-related buttons (<see cref="cmdUp"/>, <see cref="cmdDown"/>, <see cref="tsbRemove"/>)</summary>
                ''' <param name="Enabled">Mode of setting value</param>
                Private Sub Enable(ByVal Enabled As EnableMode)
                    cmdDown.Enabled = Enabled = EnableMode.True AndAlso lstItems.SelectedIndex < lstItems.Items.Count - 1
                    cmdUp.Enabled = Enabled = EnableMode.True AndAlso lstItems.SelectedIndex > 0
                    tsbRemove.Enabled = Enabled <> EnableMode.False
                End Sub
                ''' <summary>Moves selected item up</summary>
                Private Sub cmdUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUp.Click
                    Dim index As Integer = lstItems.SelectedIndex
                    Dim item As LinkLabelItem = lstItems.SelectedItem
                    lstItems.Items.RemoveAt(index)
                    lstItems.Items.Insert(index - 1, item)
                    lstItems.SelectedIndex = index - 1
                End Sub
                ''' <summary>Moves selected item down</summary>
                Private Sub cmdDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDown.Click
                    Dim index As Integer = lstItems.SelectedIndex
                    Dim item As LinkLabelItem = lstItems.SelectedItem
                    lstItems.Items.RemoveAt(index)
                    lstItems.Items.Insert(index + 1, item)
                    lstItems.SelectedIndex = index + 1
                End Sub
                ''' <summary>Closes form and applies changes</summary>
                Private Sub cmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click
                    EditValue.Clear()
                    Me.Editor.SetItems(EditValue, New Object() {}) 'TODO: Is it necessary?
                    EditValue.AddRange(New Wrapper(Of LinkLabelItem)(lstItems.Items))
                    Me.Editor.Context.OnComponentChanged()
                    ClosingCancel = False
                    Me.Close()
                End Sub
                ''' <summary>Removes selected items</summary>
                Private Sub tsbRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsbRemove.Click
                    Dim RemoveIndex As Integer = 0
                    Dim OldMaxSI As Integer = -1
                    While lstItems.SelectedItems.Count > RemoveIndex
                        OldMaxSI = Math.Max(lstItems.SelectedIndices(RemoveIndex), OldMaxSI)
                        If Editor.CanRemoveInstance(lstItems.SelectedItems(RemoveIndex)) Then
                            lstItems.Items.Remove(lstItems.SelectedItems(RemoveIndex))
                        Else
                            RemoveIndex += 1
                        End If
                    End While
                    If lstItems.SelectedItems.Count = 0 AndAlso lstItems.Items.Count > 0 Then
                        lstItems.SelectedIndex = Math.Min(lstItems.Items.Count - 1, Math.Max(0, OldMaxSI - 1))
                    End If
                End Sub

                Private Sub pgrProperty_PropertyValueChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles pgrProperty.PropertyValueChanged
                    For Each itm As Integer In lstItems.SelectedIndices
                        lstItems.Items(itm) = lstItems.Items(itm)
                    Next itm
                End Sub

                Private Sub CollectionForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
                    If ClosingCancel Then
                        Me.Editor.SetItems(EditValue, New ArrayList(EditValue).ToArray) 'TODO: Is it necessary?
                        Me.Editor.Context.OnComponentChanged()
                    End If
                End Sub

                Private Sub CollectionForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
                    resizelabel()
                End Sub

                Private Sub splMain_SplitterMoved(ByVal sender As Object, ByVal e As System.Windows.Forms.SplitterEventArgs) Handles splMain.SplitterMoved
                    ResizeLabel()
                End Sub
                ''' <summary>Changes <see cref="Label.MaximumSize"/> of <see cref="lblItemInfo"/> in order not to be wider than its container.</summary>
                Private Sub ResizeLabel()
                    lblItemInfo.MaximumSize = New Drawing.Size(splMain.Panel2.ClientSize.Width, lblItemInfo.MaximumSize.Height)
                End Sub
            End Class
        End Class
    End Class
End Namespace
#End If