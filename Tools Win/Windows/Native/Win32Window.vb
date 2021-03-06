﻿Imports System.ComponentModel, Tools.CollectionsT.GenericT
Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports Tools.ComponentModelT
Imports System.Runtime.InteropServices
Imports Tools.API.Messages.Notifications

#If True
Imports System.Windows.Forms
Namespace WindowsT.NativeT
    'ASAP:  Wiki, Forum
    ''' <summary>Represents native window used in Microsoft Windows</summary>
    ''' <remarks>This class can be used to manipulate windows and controls that originates from non-.NET applications as well as .NET ones. It can be used in 64b environment as well.</remarks>
    ''' <author web="http://dzonny.cz" mail="dzonny@dzonny.cz">Đonny</author>
    ''' <version version="1.5.2" stage="Nightly"><c>VersionAttribute</c> and <c>AuthorAttribute</c> removed</version>
    <DebuggerDisplay("{ToString}")> _
    Public Class Win32Window
        Implements IWin32Window, IDisposable, System.Windows.Interop.IWin32Window
        Implements ICloneable(Of IWin32Window), ICloneable(Of Win32Window)
        Implements IEquatable(Of IWin32Window), IEquatable(Of Win32Window), IEquatable(Of Control), IEquatable(Of System.Windows.Window)
#Region "Basic"
        ''' <summary>Contains value of the <see cref="Handle"/> property</summary>
        Private _Handle As System.IntPtr
#Region "CTors & CTypes"
        ''' <summary>CTor from <see cref="Integer"/> handle</summary>
        ''' <param name="hWnd">Handle to window</param>
        Public Sub New(ByVal hWnd As Integer)
            _Handle = hWnd
        End Sub
        ''' <summary>CTor from <see cref="IntPtr"/> handle</summary>
        ''' <param name="hWnd">Handle to window</param>
        Public Sub New(ByVal hWnd As IntPtr)
            Me.New(CInt(hWnd))
        End Sub
        ''' <summary>CTor from <see cref="Control"/> (including <see cref="Form"/>)</summary>
        ''' <param name="Control">Control to create new instance from</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Control"/> is null</exception>
        Public Sub New(ByVal Control As Control)
            Me.New(chNull(Control, "Control").Handle)
        End Sub

        ''' <summary>Gets class name of the window</summary>
        ''' <remarks>When actual class name of window is longer than 1024 characters (which is inprobable) it's truncated to 1024 characters</remarks>
        ''' <exception cref="API.Win32APIException">Failed to obtain window class name (i.g. window handle is invalid)</exception>
        ''' <version version="1.5.3">This property is new in version 1.5.3</version>
        Public ReadOnly Property ClassName As String
            Get
                Dim b As New System.Text.StringBuilder(1024)
                If API.GUI.GetClassName(Handle, b, b.Capacity) = 0 Then Throw New API.Win32APIException
                Return b.ToString
            End Get
        End Property

        ''' <summary>Checks if given object is null. Throws <see cref="ArgumentNullException"/> if so.</summary>
        ''' <param name="obj">Object to check</param>
        ''' <param name="param">Name of parameter, passed to <see cref="ArgumentNullException.ParamName"/>.</param>
        ''' <typeparam name="T">Type of object being checked. Must be reference type.</typeparam>
        ''' <returns><paramref name="obj"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="obj"/> is null</exception>
        Private Shared Function chNull(Of T As Class)(ByVal obj As T, ByVal param$) As T
            If obj Is Nothing Then Throw New ArgumentNullException(param)
            Return obj
        End Function
        ''' <summary>CTor from <see cref="IWin32Window"/></summary>
        ''' <param name="Window">Window to create new instance from</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        Public Sub New(ByVal Window As IWin32Window)
            Me.New(chNull(Window, "Window").Handle)
        End Sub
        ''' <summary>CTor from <see cref="System.Windows.Window"/></summary>
        ''' <param name="Window"><see cref="System.Windows.Window"/> to create new instance from</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null</exception>
        Public Sub New(ByVal Window As System.Windows.Window)
            Me.New(New System.Windows.Interop.WindowInteropHelper(chNull(Window, "Window")).Handle)
        End Sub
        ''' <summary>Converts <see cref="Control"/> to <see cref="Win32Window"/></summary>
        ''' <param name="a">A <see cref="Control"/></param>
        ''' <returns>A <see cref="Win32Window"/> with same handle as <paramref name="a"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
        Public Shared Widening Operator CType(ByVal a As Control) As Win32Window
            Return New Win32Window(a)
        End Operator
        ''' <summary>Converts <see cref="System.Windows.Window"/> to <see cref="Win32Window"/></summary>
        ''' <param name="a">A <see cref="System.Windows.Window"/></param>
        ''' <returns>A <see cref="Win32Window"/> with same handle as <paramref name="a"/></returns>
        ''' <exception cref="ArgumentNullException"><paramref name="a"/> is null</exception>
        Public Shared Widening Operator CType(ByVal a As System.Windows.Window) As Win32Window
            Return New Win32Window(a)
        End Operator
#End Region

        ''' <summary>Gets the handle to the window represented by the implementer.</summary>
        ''' <returns>A handle to the window represented by the implementer.</returns>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "Handle_c", "Handle")> _
        Public ReadOnly Property Handle() As System.IntPtr Implements System.Windows.Forms.IWin32Window.Handle, System.Windows.Interop.IWin32Window.Handle
            Get
                Return _Handle
            End Get
        End Property
        ''' <summary>Same as <see cref="Handle"/> but <see cref="Integer"/></summary>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "Handle_c", "Handle")> _
        Public ReadOnly Property hWnd() As Integer
            Get
                Return Handle
            End Get
        End Property

#Region " IDisposable Support "
        ''' <summary>To detect redundant calls</summary>
        Private disposedValue As Boolean = False

        ''' <summary>Sets <see cref="Handle"/> to zero</summary>
        ''' <remarks>This code added by Visual Basic to correctly implement the disposable pattern.</remarks>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                End If
            End If
            _Handle = IntPtr.Zero
            Me.disposedValue = True
        End Sub
        ''' <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        ''' <remarks>Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.</remarks>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance.</returns>
        ''' <remarks>Use type-safe <see cref="Clone"/> instead</remarks>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        <Obsolete("Use type-safe Clone instead")> _
        Private Function Clone__() As Object Implements System.ICloneable.Clone
            Return Clone()
        End Function
        ''' <summary>Implements <see cref="ICloneable(Of System.Windows.Forms.IWin32Window).Clone"/></summary>
        ''' <returns><see cref="Clone"/></returns>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private Function Clone_() As System.Windows.Forms.IWin32Window Implements ICloneable(Of System.Windows.Forms.IWin32Window).Clone
            Return Clone()
        End Function
        ''' <summary>Creates new instance of <see cref="Win32Window"/> pointing to same window as curent instance</summary>
        ''' <returns>New instance pointing to same window as current instance</returns>
        ''' <remarks>In fact there is no need to clone <see cref="Win32Window"/> object, because it has no internal state other than <see cref="Handle"/></remarks>
        Public Function Clone() As Win32Window Implements ICloneable(Of Win32Window).Clone
            Return New Win32Window(Handle)
        End Function
#End Region

#Region "Basic properties and operations"
        ''' <summary>Gets or sets parent of current Window</summary>
        ''' <value>A <see cref="Win32Window"/> to reparent current window into. Can be null to un-parent current window completely.</value>
        ''' <returns>Current parent of current window. Can return null if current window has no parent or there was error when obtaining parent (ie. <see cref="Handle"/> is invalid)</returns>
        ''' <exception cref="API.Win32APIException">Setting failed. It may indicate that <see cref="hWnd"/> does not point to existing window or attempt to set parent to the same window or to one of children.</exception>
        ''' <remarks>Setting value to <see cref="Win32Window"/> with <see cref="Handle"/> of zero has the same effect as setting it to null.
        ''' Non-top-level windows (such as button) cannot be unparented. Setting null for shuch window will cause window to be parented into desktop - not by this implementation but by the OS.</remarks>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "Relationship_c", "Relationship")> _
        Public Property Parent() As Win32Window
            Get
                Dim ret As Integer = API.GetParent(Handle)
                Return If(ret <> 0, New Win32Window(ret), Nothing)
            End Get
            Set(ByVal value As Win32Window)
                If Not API.SetParent(Handle, If(value Is Nothing, IntPtr.Zero, value.Handle)) Then _
                    Throw New API.Win32APIException()
            End Set
        End Property
        ''' <summary>Adds <paramref name="item"/> to <paramref name="List"/> and returns true</summary>
        ''' <param name="List"><see cref="List(Of T)"/> to add item to</param>
        ''' <param name="item">Item to be added</param>
        ''' <typeparam name="T">Type of <paramref name="item"/></typeparam>
        ''' <returns>True</returns>
        Private Shared Function AddToList(Of T)(ByVal List As List(Of T), ByVal item As T) As Boolean
            List.Add(item)
            Return True
        End Function
        ''' <summary>Gets all childrens of current windows</summary>
        ''' <returns>Childrens of current window</returns>
        ''' <exception cref="API.Win32APIException">Error while enumerating System.Windows. Ie. <see cref="Handle"/> is invalid</exception>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "Relationship_c", "Relationship")> _
        <TypeConverter(GetType(CollectionConverter))> _
        <LDescription(GetType(WindowsT.FormsT.ControlsWin), "Children_d")> _
        Public ReadOnly Property Children() As IReadOnlyList(Of Win32Window)
            Get
                Dim List As New List(Of Win32Window)
                If API.EnumChildWindows(Handle, New API.EnumWindowsProc(Function(hwnd As IntPtr, lParam As Integer) AddToList(List, New Win32Window(hwnd))), 0) Then
                    Return New ReadOnlyListAdapter(Of Win32Window)(List)
                Else
                    Dim ex As New API.Win32APIException
                    If ex.NativeErrorCode <> 0 Then
                        Throw ex
                    Else
                        Return New ReadOnlyListAdapter(Of Win32Window)(List)
                    End If
                End If
            End Get
        End Property

        ''' <summary>Gets or sets handle of current window's parent</summary>
        ''' <value>Handle to window to parent current window into. Set to 0 if window should be parented into desktop.</value>
        ''' <returns>Handle of current window's parent. Zero if current window has no parent.</returns>
        ''' <exception cref="API.Win32APIException">Error when setting parent. It may be caused by invalid <see cref="Handle"/> or invalid <see cref="ParentHandle"/> being set</exception>
        ''' <remarks>It's recomended to use <see cref="Parent"/> instead.
        ''' Non-top-level windows (such as button) cannot be unparented. Setting zero for shuch window will cause window to be parented into desktop - not by this implementation but by the OS.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "Relationship_c", "Relationship")> _
        Public Property ParentHandle() As IntPtr
            Get
                Return API.GetParent(Handle)
            End Get
            Set(ByVal value As IntPtr)
                If Not API.SetParent(Handle, value) Then _
                    Throw New API.Win32APIException
            End Set
        End Property
        ''' <summary>Gets or sets specified window long of current window</summary>
        ''' <param name="Long">Long to get or set. Can be one of <see cref="API.[Public].WindowLongs"/> values or can be any user-defined integer</param>
        ''' <value>New value of window long</value>
        ''' <returns>Current value of window long</returns>
        ''' <exception cref="API.Win32APIException">Getting or setting of value failed (i.e. <see cref="Handle"/> is invalid or <paramref name="Long"/> is invalid)</exception>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "LowLevel_c", "Low-level")> _
        Public Property WindowLong(ByVal [Long] As API.Public.WindowLongs) As Integer
            Get
                Try
                    Return API.GetWindowLong(Handle, CType([Long], API.WindowLongs))
                Finally
                    Dim ex As New API.Win32APIException
                    If ex.NativeErrorCode <> 0 Then Throw ex
                End Try
            End Get
            Set(ByVal value As Integer)
                If API.SetWindowLong(Handle, [Long], value) = 0 Then
                    Dim ex As New API.Win32APIException
                    If ex.NativeErrorCode <> 0 Then Throw ex
                End If
            End Set
        End Property
#Region "Size & location"
        ''' <summary>Changes window position and size</summary>
        ''' <param name="Height">New height of window in px</param>
        ''' <param name="Left">New x coordinate of left edge of the window in px</param>
        ''' <param name="Repaint">Forces window to repaint its content after moving - default is true</param>
        ''' <param name="Top">New y coordinate of top edge of the window in px</param>
        ''' <param name="Width">New width of window in px</param>
        ''' <exception cref="API.Win32APIException">Moving failed, ie. <see cref="Handle"/> is invalid</exception>
        ''' <remarks>
        ''' In some multi-monitor configurations the <paramref name="Top"/> and <see cref="Left"/> can be negative and it does not necesarilly mean that window is positioned outside the desktop.
        ''' For top-level windows screen coordinates are used. For windows with <see cref="Parent"/> parent's coordinates are used.
        ''' </remarks>
        Public Sub Move(ByVal Left As Integer, ByVal Top As Integer, ByVal Width As Integer, ByVal Height As Integer, Optional ByVal Repaint As Boolean = True)
            If Not API.MoveWindow(Handle, Left, Top, Width, Height, Repaint) Then _
                Throw New API.Win32APIException
        End Sub
        ''' <summary>Changes window position and size to specified <see cref="Rectangle"/></summary>
        ''' <param name="Rectangle">Defines new window size and position</param>
        ''' <remarks><paramref name="Rectangle"/>.<see cref="Rectangle.Location">Location</see> should be in screen coordibates for top-level windows and in parent's coordinates for windows with <see cref="Parent"/></remarks>
        ''' <exception cref="API.Win32APIException">Moving failed, ie. <see cref="Handle"/> is invalid</exception>
        Public Sub Move(ByVal Rectangle As Rectangle)
            Move(Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height)
        End Sub
        ''' <summary>Gets or sets rectangle covered by the window</summary>
        ''' <returns>Current rectangle covered by the window</returns>
        ''' <value>New rectangle covered by the window</value>
        ''' <remarks>For top-level windows screen coordinates are used. For windows with <see cref="Parent"/> coordinates of parent are used.</remarks>
        ''' <exception cref="API.Win32APIException">Setting or obtaining window's rectangle failed, ie. <see cref="Handle"/> is invalid</exception>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "SizeAndPosition_c", "Size and position")> _
        Public Property Area() As Rectangle
            Get
                Dim ret As API.RECT
                If API.GetWindowRect(Handle, ret) Then
                    If Parent IsNot Nothing Then
                        Dim pos As API.POINTAPI = CType(ret, Rectangle).Location
                        If API.ScreenToClient(Parent.Handle, pos) Then
                            Return New Rectangle(pos, CType(ret, Rectangle).Size)
                        Else
                            Throw New API.Win32APIException
                        End If
                    Else
                        Return ret
                    End If
                Else
                    Throw New API.Win32APIException
                End If
            End Get
            Set(ByVal value As Rectangle)
                Move(value)
            End Set
        End Property
        ''' <summary>Gets or sets the size of the window</summary>
        ''' <value>New size of the window. Position will be unchanged</value>
        ''' <returns>Current size of the window</returns>
        ''' <exception cref="API.Win32APIException">Setting or obtaining of window's rectangle failed, ie. <see cref="Handle"/> is invalid</exception>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "SizeAndPosition_c", "Size and position")> _
        Public Property Size() As Size
            Get
                Return Area.Size
            End Get
            Set(ByVal value As Size)
                Area = New Rectangle(Location, value)
            End Set
        End Property
        ''' <summary>Gets or sets location of the window</summary>
        ''' <value>New position of top left corner of window. Size will ne unchanged.</value>
        ''' <returns>Current position of window top left corner</returns>
        ''' <remarks>For top-level windows the location is in screen coordinates, for windows with <see cref="Parent"/> in parent' coordinates.</remarks>
        ''' <exception cref="API.Win32APIException">Setting or obtaining of window's rectangle failed, ie. <see cref="Handle"/> is invalid</exception>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "SizeAndPosition_c", "Size and position")> _
        Public Property Location() As Point
            Get
                Return Area.Location
            End Get
            Set(ByVal value As Point)
                Area = New Rectangle(value, Size)
            End Set
        End Property
        ''' <summary>Gets or sets x coordinale of left edge of the window.</summary>
        ''' <value>New x coordinate of left edge of the window</value>
        ''' <returns>Current x coordinate of left edge of the window</returns>
        ''' <remarks>In some multi-monitor configurations the left edge of desktop can be negative number. In such case <see cref="Left"/> can be also negative and it does not necesarilly mean that the window is outside of the desktop.
        ''' For top-level windows the location is in screen coordinates, for windows with <see cref="Parent"/> in parent' coordinates.</remarks>
        ''' <exception cref="API.Win32APIException">Setting or obtaining of window's rectangle failed, ie. <see cref="Handle"/> is invalid</exception>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "SizeAndPosition_c", "Size and position")> _
        Public Property Left() As Integer
            Get
                Return Location.X
            End Get
            Set(ByVal value As Integer)
                Location = New Point(value, Top)
            End Set
        End Property
        ''' <summary>Gets or sets y coordinate of top edge of the window.</summary>
        ''' <value>New y coordinate of top edge of the window</value>
        ''' <returns>Current y coordinate of top edge of the window</returns>
        ''' <remarks>In some multi-monitor configurations the top edge of desktop can be negative number. In such case <see cref="Top"/> can be also negative and it does not necesarilly mean thet the window is outside of the desktop.
        ''' For top-level windows the location is in screen coordinates, for windows with <see cref="Parent"/> in parent' coordinates.</remarks>
        ''' <exception cref="API.Win32APIException">Setting or obtaining of window's rectangle failed, ie. <see cref="Handle"/> is invalid</exception>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "SizeAndPosition_c", "Size and position")> _
        Public Property Top() As Integer
            Get
                Return Location.Y
            End Get
            Set(ByVal value As Integer)
                Location = New Point(Left, value)
            End Set
        End Property
        ''' <summary>Gets or sets width of the window</summary>
        ''' <value>New width of the window</value>
        ''' <returns>Current width of the window</returns>
        ''' <exception cref="API.Win32APIException">Setting or obtaining of window's rectangle failed, ie. <see cref="Handle"/> is invalid</exception>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "SizeAndPosition_c", "Size and position")> _
        Public Property Width() As Int32
            Get
                Return Size.Width
            End Get
            Set(ByVal value As Integer)
                Size = New Size(value, Height)
            End Set
        End Property
        ''' <summary>Gets or sets height of the window</summary>
        ''' <value>New height of the window</value>
        ''' <returns>Current height of the window</returns>
        ''' <exception cref="API.Win32APIException">Setting or obtaining of window's rectangle failed, ie. <see cref="Handle"/> is invalid</exception>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "SizeAndPosition_c", "Size and position")> _
        Public Property Height() As Integer
            Get
                Return Size.Height
            End Get
            Set(ByVal value As Integer)
                Size = New Size(Width, value)
            End Set
        End Property
        ''' <summary>Gets x coordinate of right edge of the window</summary>
        ''' <returns>Current x-coordinate of right edge of the window</returns>
        ''' <remarks>For top-level windows the location is in screen coordinates, for windows with <see cref="Parent"/> in parent' coordinates.</remarks>
        ''' <exception cref="API.Win32APIException">Obtaining of window's rectangle failed, ie. <see cref="Handle"/> is invalid</exception>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "SizeAndPosition_c", "Size and position")> _
        Public ReadOnly Property Right() As Integer
            Get
                Return Area.Right
            End Get
        End Property
        ''' <summary>Gets y coordinate of bottom edge of the window</summary>
        ''' <returns>Current y-coordinate of bottom edge of the window</returns>
        ''' <remarks>For top-level windows the location is in screen coordinates, for windows with <see cref="Parent"/> in parent' coordinates.</remarks>
        ''' <exception cref="API.Win32APIException">Obtaining of window's rectangle failed, ie. <see cref="Handle"/> is invalid</exception>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "SizeAndPosition_c", "Size and position")> _
        Public ReadOnly Property Bottom() As Integer
            Get
                Return Area.Bottom
            End Get
        End Property
        ''' <summary>Gets or sets window area in screen coordinates (even for non-top-level windows)</summary>
        ''' <returns>Current area that windows covers on screen</returns>
        ''' <value>New area to cover</value>
        ''' <exception cref="API.Win32APIException">Error while setting or obtaining the area (ie. <see cref="Handle"/> is invalid)</exception>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "SizeAndPosition_c", "Size and position")> _
        Public Property ScreenArea() As Rectangle
            Get
                Dim ret As API.RECT
                If API.GetWindowRect(Handle, ret) Then
                    Return ret
                Else
                    Throw New API.Win32APIException
                End If
            End Get
            Set(ByVal value As Rectangle)
                If Parent IsNot Nothing Then
                    Dim pos As API.POINTAPI = value.Location
                    If API.ScreenToClient(Parent.Handle, pos) Then
                        value.Location = pos
                    Else
                        Throw New API.Win32APIException
                    End If
                End If
                Move(value)
            End Set
        End Property

#Region "Messages"
        ''' <summary>Sends a window message to current window</summary>
        ''' <param name="message">A message to be sent</param>
        ''' <param name="wParam">Message parameter wParam - meaning depends on <paramref name="message"/>.</param>
        ''' <param name="lParam">Message parameter lParam - meaning depends on <paramref name="message"/>.</param>
        ''' <returns>Messsage return value - meaning depends on <paramref name="message"/>.</returns>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Function SendMessage(ByVal message As API.Messages.WindowMessages, ByVal wParam%, ByVal lParam%) As Integer
            Return API.Messages.WindowMessage.Send(Me, message, wParam, lParam)
        End Function
        ''' <summary>Sends a window message with string <paramref name="lParam"/> to current window</summary>
        ''' <param name="message">A message to be sent</param>
        ''' <param name="wParam">Message parameter wParam - meaning depends on <paramref name="message"/>.</param>
        ''' <param name="lParam">Message parameter lParam - meaning depends on <paramref name="message"/>. Pointer to string is passed to message.</param>
        ''' <returns>Messsage return value - meaning depends on <paramref name="message"/>.</returns>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Function SendMessageString(ByVal message As API.Messages.WindowMessages, ByVal wParam%, ByVal lParam$) As Integer
            Return API.Messages.SendMessage(Me.Handle, message, wParam, New System.Text.StringBuilder(lParam))
        End Function
        ''' <summary>Sends a window message with <see cref="System.Text.StringBuilder"/> <paramref name="lParam"/> to current window</summary>
        ''' <param name="message">A message to be sent</param>
        ''' <param name="wParam">Message parameter wParam - meaning depends on <paramref name="message"/>.</param>
        ''' <param name="lParam">Message parameter lParam - meaning depends on <paramref name="message"/>. Pointer to string is passed to message.</param>
        ''' <returns>Messsage return value - meaning depends on <paramref name="message"/>. Messages that write to <paramref name="lParam"/> usually return number of characters written</returns>
        ''' <exception cref="ArgumentNullException"><paramref name="lParam"/> is null</exception>
        ''' <remarks>If message is expected to write characters to <paramref name="lParam"/> the <see cref="System.Text.StringBuilder.Capacity"/> must be set to maximum number of characters expected to be returned by the message before the message is sent.</remarks>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Function SendMessageString(ByVal message As API.Messages.WindowMessages, ByVal wParam%, ByVal lParam As System.Text.StringBuilder) As Integer
            If lParam Is Nothing Then Throw New ArgumentNullException("lParam")
            Return API.Messages.SendMessage(Me.Handle, message, wParam, lParam)
        End Function
        ''' <summary>Sends a window message with pointer-to-structure <paramref name="lParam"/> to current window</summary>
        ''' <param name="message">A message to be sent</param>
        ''' <param name="wParam">Message parameter wParam - meaning depends on <paramref name="message"/>.</param>
        ''' <param name="lParam">Message parameter lParam - meaning depends on <paramref name="message"/>. Pointer to given structure is passed to message.</param>
        ''' <returns>Messsage return value - meaning depends on <paramref name="message"/>.</returns>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Function SendMessageStructure(ByVal message As API.Messages.WindowMessages, ByVal wParam%, ByVal lParam As ValueType) As Integer
            Dim ptr = Marshal.AllocHGlobal(Marshal.SizeOf(lParam))
            Try
                Marshal.StructureToPtr(lParam, ptr, False)
                Return Me.SendMessage(message, wParam, ptr.ToInt32)
            Finally
                Marshal.FreeHGlobal(ptr)
            End Try
        End Function

        ''' <summary>Sends a window message with pointer-to-structure <paramref name="lParam"/> to current window and allow message target to change values in this structure</summary>
        ''' <param name="message">A message to be sent</param>
        ''' <param name="wParam">Message parameter wParam - meaning depends on <paramref name="message"/>.</param>
        ''' <param name="lParam">Message parameter lParam - meaning depends on <paramref name="message"/>. Pointer to given structure is passed to message. When message ends this parameter is assigned values of structure as altered by message handler routine.</param>
        ''' <returns>Messsage return value - meaning depends on <paramref name="message"/>.</returns>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Function SendMessageByRefStruct(ByVal message As API.Messages.WindowMessages, ByVal wParam%, ByRef lParam As ValueType) As Integer
            Dim ptr = Marshal.AllocHGlobal(Marshal.SizeOf(lParam))
            Try
                Marshal.StructureToPtr(lParam, ptr, False)
                Dim ret = Me.SendMessage(message, wParam, ptr.ToInt32)
                lParam = Marshal.PtrToStructure(ptr, lParam.GetType)
                Return ret
            Finally
                Marshal.FreeHGlobal(ptr)
            End Try
        End Function

        ''' <summary>Sends given window message to current window</summary>
        ''' <param name="message">A message to be send</param>
        ''' <returns>Messsage return value - meaning depends on <paramref name="message"/>.</returns>
        ''' <remarks>This method sets <paramref name="message"/>.<see cref="API.Messages.WindowMessage.ReturnValue">ReturnValue</see> to result of message</remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="message"/> is null</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Function SendMessage(ByVal message As API.Messages.WindowMessage) As Integer
            If message Is Nothing Then Throw New ArgumentNullException("message")
            Return message.Send(Me)
        End Function
        ''' <summary>Posts a window message to current window without waiting for the window to process the message</summary>
        ''' <param name="message">A message to be sent</param>
        ''' <param name="wParam">Message parameter wParam - meaning depends on <paramref name="message"/>.</param>
        ''' <param name="lParam">Message parameter lParam - meaning depends on <paramref name="message"/>.</param>
        ''' <exception cref="API.Win32APIException">Failed to post the message</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Sub PostMessage(ByVal message As API.Messages.WindowMessages, ByVal wParam%, ByVal lParam%)
            API.Messages.WindowMessage.Post(Me, message, wParam, lParam)
        End Sub
        ''' <summary>Posts given window message to current window without waiting for the window to process the message</summary>
        ''' <param name="message">A message to be Post</param>
        ''' <exception cref="ArgumentNullException"><paramref name="message"/> is null</exception>
        ''' <exception cref="API.Win32APIException">Failed to post the message</exception>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Sub PostMessage(ByVal message As API.Messages.WindowMessage)
            If message Is Nothing Then Throw New ArgumentNullException("message")
            message.Post(Me)
        End Sub

        ''' <summary>Sends a notification messages <see cref="API.Messages.WindowMessages.WM_NOTIFY"/> to current window</summary>
        ''' <param name="notification">Notification data to be sent</param>
        ''' <remarks>Return value of notification message</remarks>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Function SendNotification(ByVal notification As API.Messages.Notifications.NMHDR) As Integer
            Return SendMessageStructure(API.Messages.WindowMessages.WM_NOTIFY, notification.idFrom.ToInt32, notification)
            'Dim ptr = Marshal.AllocHGlobal(Marshal.SizeOf(notification))
            'Try
            '    Marshal.StructureToPtr(notification, ptr, False)
            '    Return Me.SendMessage(API.Messages.WindowMessages.WM_NOTIFY, notification.idFrom.ToInt32, ptr.ToInt32)
            'Finally
            '    Marshal.FreeHGlobal(ptr)
            'End Try
        End Function
#End Region
#End Region
        ''' <summary>Gets or sets text associated with the window</summary>
        ''' <value>New text of window</value>
        ''' <returns>Current text of the window</returns>
        ''' <remarks>For windows that represents form it is text from title bar, for other controls like labels it is text of the control. See also <seealso cref="Control.Text"/>.
        ''' This property can can get/set text for all windows in the same process as it is called from and text of windows that has title bar (forms) from any process.</remarks>
        ''' <exception cref="API.Win32APIException">Setting or obtaining of text failed. ie. <see cref="Handle"/> is invalid</exception>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "WindowProperties_c", "Window properties")> _
        Public Property Text$()
            Get
                Dim len As Integer = API.GetWindowTextLength(Handle)
                If len > 0 Then
                    Dim b As New System.Text.StringBuilder(ChrW(0), len + 1)
                    Dim ret = API.GetWindowText(Handle, b, b.Capacity)
                    If ret <> 0 Then
                        Return b.ToString
                    Else
                        Throw New API.Win32APIException
                    End If
                Else
                    Dim ex As New API.Win32APIException
                    If ex.NativeErrorCode <> 0 Then
                        Throw New API.Win32APIException
                    Else
                        Return ""
                    End If
                End If
            End Get
            Set(ByVal value$)
                If Not API.SetWindowText(Handle, value) Then _
                    Throw New Win32Exception
            End Set
        End Property
        ''' <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
        ''' <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
        ''' <version version="1.5.3">Added class name to string</version>
        Public Overrides Function ToString() As String
            If Handle = IntPtr.Zero Then Return WindowsT.FormsT.ControlsWin.NoWindow
            Try
                Return String.Format("{0} hWnd = {1}, class = {2}", Text, Handle, ClassName)
            Catch ex As Win32Exception
                Return String.Format("hWnd = {0}", Handle)
            End Try
        End Function
        ''' <summary>Gets or sets pointer to wnd proc of current window. Used for so-called sub-classing.</summary>
        ''' <returns>Pointer to current wnd proc of current window</returns>
        ''' <value>Pointer to new wnd proc. Note: Old wnd proc is lost when setting this property. You should consider backing old value up.</value>
        ''' <remarks>
        ''' wnd proc (window procedure) is procedure with signature of th <see cref="API.Messages.WndProc"/> delegate that processes all the messages. You should consider using <see cref="WndProc"/> property rather then this one.
        ''' You can do this also with <see cref="WindowLong"/> with <see cref="API.[Public].WindowLongs.WndProc"/> as argument.
        ''' </remarks>
        ''' <exception cref="API.Win32APIException">Getting or setting of value failed (i.e. <see cref="Handle"/> is invalid)</exception>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "LowLevel_c", "Low-level")> _
        <Browsable(False), EditorBrowsable(EditorBrowsableState.Advanced)> _
        Public Overridable Property WndProcPointer() As IntPtr
            Get
                Return WindowLong(API.Public.WindowLongs.WndProc)
            End Get
            Set(ByVal value As IntPtr)
                WindowLong(API.Public.WindowLongs.WndProc) = value
            End Set
        End Property
        ''' <summary>Gets or sets wnd proc of current window. Used for so-called window sub-classing.</summary>
        ''' <value>New window proc. Note: Old window proc is lost by setting this property. You should consider backing it up.
        ''' <para>Warning: By setting value of this property youar passing delegate to unmanaged code! You must keep that delegate alive as long as it is in use - that means while the window exists or until <see cref="WndProc"/> property is changed again. For example following VB code is completely invalid!</para>
        ''' <example><code>instance.WndProc = AddressOf MyReplacementProc</code></example>
        ''' <para>This example creates new delegate, passes it to unmanaged code, and forgets it. The is no reference to that delegate keeping it alive (protecting it from being garbage collected), so you can get unexpected error when the runtime garbage collector collects the delegate and the there is an attempt to call it. The proper way of setting this property is create an instance of <see cref="API.Messages.WndProc"/>, store it somewhere, pass it here and keep that 'somewhere' alive as long as window uses that replaced wnd proc.</para>
        ''' <para>The need to keep delegate alive may be problem when creating backup of previos window procedure in order to revert change of window procedure in the future. This property returns a managed delegate (to possibly onmanaged code). So, this delegate must be kept alive as long as it is used by window. That is not always the think you want to (or can) do. In such case you should considering backing up pointer to the old wnd proc. Pointer can be used to restore the procedure with no need to keep it alive. To do so use the <see cref="WndProcPointer"/> property. It is common parctise to backup old wnd proc in order to call it from new one. You cannot call a pointer. So, if you need to back up old wnd proc in order to restore it as well as in order to call it, the best think you can do is back it up as pointer as well as as delegate.</para>
        ''' </value>
        ''' <returns>Delegate to old window proc</returns>
        ''' <exception cref="API.Win32APIException">Getting or setting value failed (i.e. <see cref="Handle"/> is invalid). This is also usually thrown when window comes from another process than property is being got.</exception>
        ''' <remarks>
        ''' Window procedure is used to handle messages of current window.
        ''' <para>If current window represents .NET <see cref="Form"/> or other <see cref="Control"/> and you have chance to derive from it, you'd better to do so and the override <see cref="Control.WndProc"/>.
        ''' You are the proctedted from problems with keeping delegate alive. You can also derive from <see cref="System.Windows.Forms.NativeWindow"/> and override it's <see cref="System.Windows.Forms.NativeWindow.WndProc"/>.</para>
        ''' </remarks>
        <LCategory(GetType(WindowsT.FormsT.ControlsWin), "LowLevel_c", "Low-level")> _
        Public Overridable Property WndProc() As API.Messages.WndProc
            Get
                Dim ret As API.Messages.WndProc = API.GetWindowLong(Handle, API.WindowProcs.GWL_WNDPROC)
                If ret Is Nothing Then
                    Dim ex As New API.Win32APIException
                    If ex.NativeErrorCode <> 0 Then Throw ex
                End If
                Return ret
            End Get
            Set(ByVal value As API.Messages.WndProc)
                If value Is Nothing Then
                    WndProcPointer = 0
                Else
                    If Not API.SetWindowLong(Handle, API.WindowProcs.GWL_WNDPROC, value) Then
                        Throw New API.Win32APIException
                    End If
                End If
            End Set
        End Property
        ''' <summary>Gets default window procedure implementation that responds to all messages in defaut way. This implementation is provided by the OS.</summary>
        ''' <returns>Delegate to <see cref="API.DefWindowProc"/> (internal, PInvoke function)</returns>
        Public Shared ReadOnly Property DefWndProc() As API.Messages.WndProc
            Get
                Static ret As API.Messages.WndProc
                If ret Is Nothing Then ret = AddressOf API.DefWindowProc
                Return ret
            End Get
        End Property
        ''' <summary>Gets or sest window extended style</summary>
        ''' <returns>Current value of <see cref="API.[Public].WindowLongs.ExStyle"/> window long</returns>
        ''' <value>A new value for window long <see cref="API.[Public].WindowExtendedStyles"/></value>
        ''' <seelaso cref="WindowLong"/><seelaso cref="API.[Public].WindowLongs.ExStyle"/>
        ''' <version version="1.5.3">This property is new in version 1.5.3</version>
        ''' <exception cref="API.Win32APIException">Getting or setting of value failed (i.e. <see cref="Handle"/> is invalid)</exception>
        Public Property ExtendedStyle As API.Public.WindowExtendedStyles
            Get
                Return WindowLong(API.Public.WindowLongs.ExStyle)
            End Get
            Set(ByVal value As API.Public.WindowExtendedStyles)
                WindowLong(API.Public.WindowLongs.ExStyle) = value
            End Set
        End Property
        ''' <summary>Hides an icon of window</summary>
        ''' <remarks>This method sets window <see cref="ExtendedStyle"/> flag <see cref="API.Public.WindowExtendedStyles.DialogModalFrame"/> and updates windo non-client area</remarks>
        Public Sub HideIcon()
            ExtendedStyle = ExtendedStyle Or API.Public.WindowExtendedStyles.DialogModalFrame
            API.SetWindowPos(Handle, IntPtr.Zero, 0, 0, 0, 0, API.SetWindowPosFlags.SWP_NOMOVE Or API.SetWindowPosFlags.SWP_NOSIZE Or API.SetWindowPosFlags.SWP_NOZORDER Or API.SetWindowPosFlags.SWP_FRAMECHANGED) 'Error ignored
        End Sub
#Region "Equals"
        ''' <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
        ''' <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
        ''' <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            If TypeOf obj Is IWin32Window Then Return DirectCast(obj, IWin32Window).Handle = Me.Handle
            If TypeOf obj Is System.Windows.Window Then Return New Win32Window(DirectCast(obj, System.Windows.Window)).Handle = Me.Handle
            Return MyBase.Equals(obj)
        End Function
        ''' <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        ''' <param name="other">An object to compare with this object.</param>
        ''' <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        Public Overloads Function Equals(ByVal other As System.Windows.Forms.Control) As Boolean Implements System.IEquatable(Of System.Windows.Forms.Control).Equals
            Return Equals(CObj(other))
        End Function
        ''' <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        ''' <param name="other">An object to compare with this object.</param>
        ''' <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        Public Overloads Function Equals(ByVal other As System.Windows.Forms.IWin32Window) As Boolean Implements System.IEquatable(Of System.Windows.Forms.IWin32Window).Equals
            Return Equals(CObj(other))
        End Function
        ''' <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        ''' <param name="other">An object to compare with this object.</param>
        ''' <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        Public Overloads Function Equals(ByVal other As Win32Window) As Boolean Implements System.IEquatable(Of Win32Window).Equals
            Return Equals(CObj(other))
        End Function
        ''' <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        ''' <param name="other">An object to compare with this object.</param>
        ''' <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        Public Overloads Function Equals(ByVal other As System.Windows.Window) As Boolean Implements IEquatable(Of System.Windows.Window).Equals
            Return Equals(CObj(other))
        End Function
        ''' <summary>Compares <see cref="IWin32Window"/> and <see cref="Win32Window"/></summary>
        ''' <param name="a">A <see cref="IWin32Window"/></param>
        ''' <param name="b">A <see cref="Win32Window"/></param>
        ''' <returns>True if <paramref name="a"/>.<see cref="IWin32Window.Handle">Handle</see> equals to <paramref name="b"/>.<see cref="Win32Window.Handle">Handle</see></returns>
        Public Shared Operator =(ByVal a As IWin32Window, ByVal b As Win32Window) As Boolean
            Return b.Equals(a)
        End Operator
        ''' <summary>Compares <see cref="Win32Window"/> and <see cref="System.Windows.Window"/></summary>
        ''' <param name="a">A <see cref="IWin32Window"/></param>
        ''' <param name="b">A <see cref="System.Windows.Window"/></param>
        ''' <returns>True if <paramref name="a"/>.<see cref="Win32Window.Handle">Handle</see> equals to handle of <paramref name="b"/></returns>
        Public Shared Operator =(ByVal a As Win32Window, ByVal b As System.Windows.Window) As Boolean
            Return a.Equals(b)
        End Operator
        ''' <summary>Compares <see cref="System.Windows.Window"/> and <see cref="Win32Window"/></summary>
        ''' <param name="b">A <see cref="IWin32Window"/></param>
        ''' <param name="a">A <see cref="System.Windows.Window"/></param>
        ''' <returns>True if <paramref name="b"/>.<see cref="Win32Window.Handle">Handle</see> equals to handle of <paramref name="a"/></returns>
        Public Shared Operator =(ByVal b As System.Windows.Window, ByVal a As Win32Window) As Boolean
            Return a.Equals(b)
        End Operator
        ''' <summary>Compares <see cref="Win32Window"/> and <see cref="IWin32Window"/></summary>
        ''' <param name="a">A <see cref="Win32Window"/></param>
        ''' <param name="b">A <see cref="IWin32Window"/></param>
        ''' <returns>True if <paramref name="a"/>.<see cref="Win32Window.Handle">Handle</see> equals to <paramref name="b"/>.<see cref="IWin32Window.Handle">Handle</see></returns>
        Public Shared Operator =(ByVal a As Win32Window, ByVal b As IWin32Window) As Boolean
            Return a.Equals(b)
        End Operator
        ''' <summary>Compares <see cref="Win32Window"/> and <see cref="Win32Window"/></summary>
        ''' <param name="a">A <see cref="Win32Window"/></param>
        ''' <param name="b">A <see cref="Win32Window"/></param>
        ''' <returns>True if <paramref name="a"/>.<see cref="Win32Window.Handle">Handle</see> equals to <paramref name="b"/>.<see cref="Win32Window.Handle">Handle</see></returns>
        Public Shared Operator =(ByVal a As Win32Window, ByVal b As Win32Window) As Boolean
            Return a.Equals(b)
        End Operator
        ''' <summary>Compares <see cref="IWin32Window"/> and <see cref="Win32Window"/></summary>
        ''' <param name="a">A <see cref="IWin32Window"/></param>
        ''' <param name="b">A <see cref="Win32Window"/></param>
        ''' <returns>False if <paramref name="a"/>.<see cref="IWin32Window.Handle">Handle</see> equals to <paramref name="b"/>.<see cref="Win32Window.Handle">Handle</see></returns>
        Public Shared Operator <>(ByVal a As IWin32Window, ByVal b As Win32Window) As Boolean
            Return Not a = b
        End Operator
        ''' <summary>Compares <see cref="Win32Window"/> and <see cref="IWin32Window"/></summary>
        ''' <param name="a">A <see cref="Win32Window"/></param>
        ''' <param name="b">A <see cref="IWin32Window"/></param>
        ''' <returns>False if <paramref name="a"/>.<see cref="Win32Window.Handle">Handle</see> equals to <paramref name="b"/>.<see cref="IWin32Window.Handle">Handle</see></returns>
        Public Shared Operator <>(ByVal a As Win32Window, ByVal b As IWin32Window) As Boolean
            Return Not a = b
        End Operator
        ''' <summary>Compares <see cref="Win32Window"/> and <see cref="Win32Window"/></summary>
        ''' <param name="a">A <see cref="Win32Window"/></param>
        ''' <param name="b">A <see cref="Win32Window"/></param>
        ''' <returns>False if <paramref name="a"/>.<see cref="Win32Window.Handle">Handle</see> equals to <paramref name="b"/>.<see cref="Win32Window.Handle">Handle</see></returns>
        Public Shared Operator <>(ByVal a As Win32Window, ByVal b As Win32Window) As Boolean
            Return Not a = b
        End Operator
        ''' <summary>Compares <see cref="Win32Window"/> and <see cref="System.Windows.Window"/></summary>
        ''' <param name="a">A <see cref="IWin32Window"/></param>
        ''' <param name="b">A <see cref="System.Windows.Window"/></param>
        ''' <returns>False if <paramref name="a"/>.<see cref="Win32Window.Handle">Handle</see> equals to handle of <paramref name="b"/></returns>
        Public Shared Operator <>(ByVal a As Win32Window, ByVal b As System.Windows.Window) As Boolean
            Return Not a = b
        End Operator
        ''' <summary>Compares <see cref="System.Windows.Window"/> and <see cref="Win32Window"/></summary>
        ''' <param name="b">A <see cref="IWin32Window"/></param>
        ''' <param name="a">A <see cref="System.Windows.Window"/></param>
        ''' <returns>False if <paramref name="b"/>.<see cref="Win32Window.Handle">Handle</see> equals to handle of <paramref name="a"/></returns>
        Public Shared Operator <>(ByVal a As System.Windows.Window, ByVal b As Win32Window) As Boolean
            Return Not a = b
        End Operator
#End Region
#End Region

#Region "Searching"

        ''' <summary>Searches for immediate child of current window with given parameters</summary>
        ''' <param name="text">Text of window to search for. Ignored when null. Can contain wildcards characters - see <see cref="Microsoft.VisualBasic.CompilerServices.LikeOperator.LikeString"/>.</param>
        ''' <param name="className">Name of class of window to search for. Ignored when null.</param>
        ''' <returns>First immediate child of current window with given characteristics. Null when no such child can be found.</returns>
        ''' <remarks>When both - <paramref name="text"/> and <paramref name="className"/> are null, first child of current window is returned.</remarks>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Function FindChild(Optional ByVal text$ = Nothing, Optional ByVal className$ = Nothing) As Win32Window
            Return FindWindows(Me, text, className, True)(0)
        End Function
        ''' <summary>Searches for immediate children of current window with given parameters</summary>
        ''' <param name="text">Text of windows to search for. Ignored when null. Can contain wildcards characters - see <see cref="Microsoft.VisualBasic.CompilerServices.LikeOperator.LikeString"/>.</param>
        ''' <param name="className">Name of class of windows to search for. Ignored when null.</param>
        ''' <returns>Immediate children of current window with given characteristics.</returns>
        ''' <remarks>When both - <paramref name="text"/> and <paramref name="className"/> are null, all children returned.</remarks>
        ''' <version version="1.5.3">This function is new in version 1.5.3</version>
        Public Function FindChildren(Optional ByVal text$ = Nothing, Optional ByVal className$ = Nothing) As Win32Window()
            Return FindWindows(Me, text, className, False)
        End Function

        ''' <summary>Searches for top-leve window by class and/or text</summary>
        ''' <param name="text">Text of windows to search for. Ignored when null. Can contain wildcards characters - see <see cref="Microsoft.VisualBasic.CompilerServices.LikeOperator.LikeString"/>.</param>
        ''' <param name="className">Name of class of windows to search for. Ignored when null.</param>
        ''' <returns>First top-level window with given characteristics found. Null when no such window can be found.</returns>
        Public Shared Function FindWindow(Optional ByVal text$ = Nothing, Optional ByVal className$ = Nothing) As Win32Window
            Return FindWindows(Nothing, text, className, True)(0)
        End Function

        ''' <summary>Searches for top-level windows by class and/or text</summary>
        ''' <param name="text">Text of windows to search for. Ignored when null. Can contain wildcards characters - see <see cref="Microsoft.VisualBasic.CompilerServices.LikeOperator.LikeString"/>.</param>
        ''' <param name="className">Name of class of windows to search for. Ignored when null.</param>
        ''' <returns>All top-level windows with given characteristics.</returns>
        Public Shared Function FindWindows(Optional ByVal text$ = Nothing, Optional ByVal className$ = Nothing) As Win32Window()
            Return FindWindows(Nothing, text, className, False)
        End Function

        ''' <summary>Searches for windows in given parent</summary>
        ''' <param name="parent">Parent to serch for child windows in. When null searches for top-level System.Windows.</param>
        ''' <param name="text">Text of windows to search for. Ignored when null. Can contain wildcards characters - see <see cref="Microsoft.VisualBasic.CompilerServices.LikeOperator.LikeString"/>.</param>
        ''' <param name="className">Name of class of windows to search for. Ignored when null.</param>
        ''' <param name="onlyFirst">True to return only first matching window</param>
        ''' <returns>Array of windows matching given conditions. If both conditions are null returns <see cref="TopLevelWindows"/>. If <paramref name="onlyFirst"/> is true always returns array with exactly one member - either the window found or null.</returns>
        Private Shared Function FindWindows(ByVal parent As Win32Window, ByVal text$, ByVal className$, ByVal onlyFirst As Boolean) As Win32Window()
            Dim ret As New List(Of Win32Window)
            For Each child In If(parent Is Nothing, TopLevelWindows, parent.Children)
                Try
                    If text Is Nothing OrElse child.Text Like text Then
                        If className Is Nothing OrElse child.ClassName = className Then
                            If onlyFirst Then Return {child}
                            ret.Add(child)
                        End If
                    End If
                Catch : End Try
            Next
            If ret.Count = 0 AndAlso onlyFirst Then Return New Win32Window() {Nothing}
            Return ret.ToArray
        End Function
#End Region

#Region "Shared"
        ''' <summary>Gets window that represents the desktop</summary>
        ''' <exception cref="API.Win32APIException">An error occurred</exception>
        Public Shared ReadOnly Property Desktop() As Win32Window
            Get
                Dim dhWnd As Integer = API.GetDesktopWindow
                If dhWnd <> 0 Then
                    Return New Win32Window(dhWnd)
                Else
                    Throw New API.Win32APIException
                End If
            End Get
        End Property

        ''' <summary>Gets all the top-level windows</summary>
        ''' <returns>List of all top-level windows</returns>
        ''' <exception cref="API.Win32APIException">An error occurred</exception>
        Public Shared ReadOnly Property TopLevelWindows() As IReadOnlyList(Of Win32Window)
            Get
                Dim List As New List(Of Win32Window)
                If API.EnumWindows(New API.EnumWindowsProc(Function(hWnd As Integer, lParam As Integer) AddToList(List, New Win32Window(hWnd))), 0) Then
                    Return New ReadOnlyListAdapter(Of Win32Window)(List)
                Else
                    Dim ex As New API.Win32APIException
                    If ex.NativeErrorCode <> 0 Then
                        Throw ex
                    Else
                        Return New ReadOnlyListAdapter(Of Win32Window)(List)
                    End If
                End If
            End Get
        End Property

        ''' <summary>Retrieves the foreground window (the window with which the user is currently working)</summary>
        ''' <returns>The foreground window.  The foreground window can be null in certain circumstances, such as when a window is losing activation. </returns>
        ''' <version version="1.5.4">This function is new in version 1.5.4</version>
        Public Shared Function GetForegroundWindow() As Win32Window
            Dim hwnd = API.GetForegroundWindow
            If hwnd = IntPtr.Zero Then Return Nothing
            Return New Win32Window(hwnd)
        End Function
#End Region

        ''' <summary>Gets a GUI thread this window belongs to</summary>
        ''' <exception cref="API.Win32APIException">Failed to obtain thread id for current window</exception> 
        ''' <version version="1.5.3">This property is new in version 1.5.3</version>
        Public ReadOnly Property GuiThread As GuiThread
            Get
                Dim threadId = API.GUI.GetWindowThreadProcessId(Handle)
                If threadId = 0 Then Throw New API.Win32APIException
                Return New GuiThread(threadId)
            End Get
        End Property

        ''' <summary>Gets process this window belongs to</summary>
        ''' <exception cref="API.Win32APIException">Failed to obtain process id for current window</exception>
        ''' <exception cref="InvalidOperationException">Process with ID reported by window cannot be fond.</exception>
        Public ReadOnly Property Process As Process
            Get
                Dim processId% = 1
                If API.GUI.GetWindowThreadProcessId(processId) = 0 Then Throw New API.Win32APIException
                Try
                    Return Process.GetProcessById(processId)
                Catch ex As Exception
                    Throw New InvalidOperationException(ex.Message, ex)
                End Try
            End Get
        End Property

        ''' <summary>Sets window as foreground window</summary>
        ''' <returns>True if window was successfully set as foreground window. False otherwise.</returns>
        ''' <remarks>There are many reasons why window cannot be set foreground e.g. application is working with the window, menu is active etc.</remarks>
        ''' <version version="1.5.3">This class is new in version 1.5.3</version>
        Public Function SetForeground() As Boolean
            Return API.GUI.SetForegroundWindow(Handle)
        End Function
    End Class
    ''' <summary>Subclasses any native Win32 widow by replacing its window procedure</summary>
    ''' <remarks>You can derive your class from this class and override <see cref="SubclassedNativeWindow.NewWndProc"/> to subclas any win</remarks>
    ''' <version version="1.5.2">Class introduced</version>
    Public Class SubclassedNativeWindow
        Inherits Win32Window
        ''' <summary>CTor</summary>
        ''' <param name="Handle">Handle of native window</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Handle"/> is <see cref="IntPtr.Zero"/></exception>
        ''' <exception cref="API.Win32APIException">Unable to replace window procedure</exception>
        Public Sub New(ByVal Handle As IntPtr)
            MyBase.New(Handle)
            If Handle = IntPtr.Zero Then Throw New ArgumentNullException("Handle")
            _OldWndProc = MyBase.WndProcPointer
            MyBase.WndProc = AddressOfWndProc
        End Sub
        ''' <summary>CTor</summary>
        ''' <param name="Window">Handle to subclass</param>
        ''' <exception cref="ArgumentNullException"><paramref name="Window"/> is null -or- <paramref name="Window"/>.<see cref="IWin32Window.Handle">Handle</see> is zero</exception>
        ''' <exception cref="API.Win32APIException">Unable to replace window procedure</exception>
        Public Sub New(ByVal Window As IWin32Window)
            Me.New(Window.Handle)
        End Sub
        ''' <summary>Keeps delegate to <see cref="NewWndProc"/></summary>
        ''' <remarks>Keeping delegate in field prevents if from being garbage-collected. Delegate passed to unmanaged code must be kept alive in such way.</remarks>
        Private AddressOfWndProc As API.Messages.WndProc = AddressOf NewWndProc
        ''' <summary>Contains value of the <see cref="OldWndProc"/> property</summary>
        Private ReadOnly _OldWndProc As IntPtr
        ''' <summary>Gets pointer to original window procedure</summary>
        ''' <returns>Original window procedure of window being sublcassed prior ts replecement by <see cref="NewWndProc"/></returns>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        Protected ReadOnly Property OldWndProc() As IntPtr
            Get
                Return _OldWndProc
            End Get
        End Property
        ''' <summary>Gets wnd proc of current window. Used for so-called window sub-classing.</summary>
        ''' <remarks>Value of this proeprty cannot be changed</remarks>
        ''' <exception cref="API.Win32APIException">Error obtaining curent window procedure</exception>
        ''' <returns>Current window procedure of window being subclassed. It's usually <see cref="NewWndProc"/>, unless window was subclassed again.</returns>
        ''' <exception cref="NotSupportedException">Value is being set</exception>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public NotOverridable Overrides Property WndProc() As API.Messages.WndProc
            Get
                Return MyBase.WndProc
            End Get
            Set(ByVal value As API.Messages.WndProc)
                Throw New NotSupportedException("WndProc cannot be set on SubclassedNativeWindow")
            End Set
        End Property
        ''' <summary>Gets pointer wnd proc of current window. Used for so-called window sub-classing.</summary>
        ''' <remarks>Value of this proeprty cannot be changed</remarks>
        ''' <exception cref="API.Win32APIException">Error obtaining curent window procedure</exception>
        ''' <returns>Pointer current window procedure of window being subclassed. It's usually <see cref="NewWndProc"/>, unless window was subclassed again.</returns>
        ''' <exception cref="NotSupportedException">Value is being set</exception>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Public NotOverridable Overrides Property WndProcPointer() As System.IntPtr
            Get
                Return MyBase.WndProcPointer
            End Get
            Set(ByVal value As System.IntPtr)
                Throw New NotSupportedException("WndProc cannot be set on SubclassedNativeWindow")
            End Set
        End Property
        ''' <summary>Procedure that replaces old window procedure of window being subclassed</summary>
        ''' <param name="hWnd">Handle to the window.</param>
        ''' <param name="msg">Specifies the message.</param>
        ''' <param name="wParam">Specifies additional message information. The contents of this parameter depend on the value of the <paramref name="msg"/> parameter.</param>
        ''' <param name="lParam">Specifies additional message information. The contents of this parameter depend on the value of the <paramref name="msg"/> parameter.</param>
        ''' <returns>The return value is the result of the message processing and depends on the message sent.</returns>
        ''' <remarks><note type="inheritinfo">Call base class method, if you do not provide custom handling of message being send. This implementation calls original window procedure defined by window being subclassed.</note></remarks>
        Protected Overridable Function NewWndProc%(ByVal hwnd As IntPtr, ByVal msg As API.Messages.WindowMessages, ByVal wParam%, ByVal lParam%)
            Return API.GUI.CallWindowProc(OldWndProc, hwnd, msg, wParam, lParam)
        End Function
        ''' <summary>True when <see cref="Dispose"/> was already called</summary>
        Private Disposed As Boolean
        ''' <summary>Sets <see cref="Handle"/> to zero</summary>
        ''' <remarks>This code added by Visual Basic to correctly implement the disposable pattern.</remarks>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If Not Disposed Then MyBase.WndProcPointer = OldWndProc
            AddressOfWndProc = Nothing
            Disposed = True
            MyBase.Dispose(disposing)
        End Sub
    End Class

    ''' <summary>Provides access to information about Windows GUI thread</summary>
    ''' <remarks>This class provides dynamic information (whenever you call property getter actual infromation is obtained from the thread) rather than static snapshot of thread state in time when instance of class was constructed.</remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class GuiThread
        ''' <summary>CTor - creates a new instance of the <see cref="GuiThread"/> class</summary>
        ''' <param name="threadID">ID of GUI thread to get information from. This value can be zero - in this case this class provides information about foreground thread.</param>
        Public Sub New(ByVal threadID%)
            _ThreadID = threadID
        End Sub
        Private ReadOnly _ThreadID%
        ''' <summary>Gets ID of GUI thread this instance provides information about</summary>
        ''' <returns>ID of GUI thread this instance provides information about. Zero when this instance provides information about foreground GUI thread.</returns>
        Public ReadOnly Property ThreadID As Integer
            Get
                Return _ThreadID
            End Get
        End Property
        ''' <summary>Gets current GUI thread information</summary>
        ''' <exception cref="API.Win32APIException">Failed to obtain thread information (e.g. thread id is invalid)</exception>
        Friend ReadOnly Property ThreadInfo As API.GUI.GUITHREADINFO
            Get
                Dim info As API.GUI.GUITHREADINFO
                info.cbSize = Marshal.SizeOf(info)
                If Not API.GUI.GetGUIThreadInfo(ThreadID, info) Then Throw New API.Win32APIException
                Return info
            End Get
        End Property
        ''' <summary>Gets current active window of the GUI thread. Null when there is no active window in the GUI thread.</summary>
        ''' <exception cref="API.Win32APIException">Failed to obtain thread information (e.g. thread id is invalid)</exception>
        Public ReadOnly Property ActiveWindow As Win32Window
            Get
                With ThreadInfo
                    If .hwndActive = IntPtr.Zero Then Return Nothing
                    Return New Win32Window(.hwndActive)
                End With
            End Get
        End Property
        ''' <summary>Gets window with keyboard focus within the GUI thread. Null when there is no window with keyboard focus in the GUI thread.</summary>
        ''' <exception cref="API.Win32APIException">Failed to obtain thread information (e.g. thread id is invalid)</exception>
        Public ReadOnly Property FocusedWindow As Win32Window
            Get
                With ThreadInfo
                    If .hwndFocus = IntPtr.Zero Then Return Nothing
                    Return New Win32Window(.hwndFocus)
                End With
            End Get
        End Property
        ''' <summary>Gets window which captured the mouse within the GUI thread. Null when there is no window with mouse capture in the GUI thread.</summary>
        ''' <exception cref="API.Win32APIException">Failed to obtain thread information (e.g. thread id is invalid)</exception>
        Public ReadOnly Property CaptureWindow As Win32Window
            Get
                With ThreadInfo
                    If .hwndCapture = IntPtr.Zero Then Return Nothing
                    Return New Win32Window(.hwndCapture)
                End With
            End Get
        End Property
        ''' <summary>Gets window displaying the carret in the GUI thread. Null when there is no window displaying the carret in the GUI thread.</summary>
        ''' <exception cref="API.Win32APIException">Failed to obtain thread information (e.g. thread id is invalid)</exception>
        Public ReadOnly Property CarretWindow As Win32Window
            Get
                With ThreadInfo
                    If .hwndCaret = IntPtr.Zero Then Return Nothing
                    Return New Win32Window(.hwndCaret)
                End With
            End Get
        End Property
        ''' <summary>Gets window that owns any active menus in the GUI thread. Null when there is no window owning menus in the GUI thread.</summary>
        ''' <exception cref="API.Win32APIException">Failed to obtain thread information (e.g. thread id is invalid)</exception>
        Public ReadOnly Property MenuOwner As Win32Window
            Get
                With ThreadInfo
                    If .hwndMenuOwner = IntPtr.Zero Then Return Nothing
                    Return New Win32Window(.hwndMenuOwner)
                End With
            End Get
        End Property
        ''' <summary>Gets a window in move or size loop in the GUI thread. Null when there is no window in move or size loop in the GUI thread.</summary>
        ''' <exception cref="API.Win32APIException">Failed to obtain thread information (e.g. thread id is invalid)</exception>
        Public ReadOnly Property MoveSizeWindow As Win32Window
            Get
                With ThreadInfo
                    If .hwndMoveSize = IntPtr.Zero Then Return Nothing
                    Return New Win32Window(.hwndMoveSize)
                End With
            End Get
        End Property
        ''' <summary>Gets caret's bounding rectangle in client coordinates relative to <see cref="CarretWindow"/>.</summary>
        ''' <exception cref="API.Win32APIException">Failed to obtain thread information (e.g. thread id is invalid)</exception>
        Public ReadOnly Property Caret As Rectangle
            Get
                Return ThreadInfo.rcCaret
            End Get
        End Property
        ''' <summary>Gets value indicating caret's blink state</summary>
        ''' <returns>True when caret is visible</returns>
        ''' <exception cref="API.Win32APIException">Failed to obtain thread information (e.g. thread id is invalid)</exception>
        Public ReadOnly Property IsCaretBlinking As Boolean
            Get
                Return ThreadInfo.flags And API.GuiThreadInfoFlags.GUI_CARETBLINKING
            End Get
        End Property
        ''' <summary>Gets thread's menu state</summary>
        ''' <returns>True whne the thread is in menu mode.</returns>
        ''' <exception cref="API.Win32APIException">Failed to obtain thread information (e.g. thread id is invalid)</exception>
        Public ReadOnly Property IsInMenuMode As Boolean
            Get
                Return ThreadInfo.flags And API.GuiThreadInfoFlags.GUI_INMENUMODE
            End Get
        End Property
        ''' <summary>Gets thread's move state.</summary>
        ''' <returns>True when the thread is in move or size loop</returns>
        ''' <exception cref="API.Win32APIException">Failed to obtain thread information (e.g. thread id is invalid)</exception>
        Public ReadOnly Property IsInMoveOrSize As Boolean
            Get
                Return ThreadInfo.flags And API.GuiThreadInfoFlags.GUI_INMOVESIZE
            End Get
        End Property
        ''' <summary>Gets thread's pop-up menu state.</summary>
        ''' <returns>True if the thread has an active pop-up menu.</returns>
        ''' <exception cref="API.Win32APIException">Failed to obtain thread information (e.g. thread id is invalid)</exception>
        Public ReadOnly Property IsPopUpMenuActive As Boolean
            Get
                Return ThreadInfo.flags And API.GuiThreadInfoFlags.GUI_POPUPMENUMODE
            End Get
        End Property
        ''' <summary>Gets thread's system menu state.</summary>
        ''' <returns>True if the thread is in a system menu mode.</returns>
        ''' <exception cref="API.Win32APIException">Failed to obtain thread information (e.g. thread id is invalid)</exception>
        Public ReadOnly Property IsInSystemMenuMode As Boolean
            Get
                Return ThreadInfo.flags And API.GuiThreadInfoFlags.GUI_SYSTEMMENUMODE
            End Get
        End Property
    End Class
End Namespace
#End If

