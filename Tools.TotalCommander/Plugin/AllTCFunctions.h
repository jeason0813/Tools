/*
TC_WFX, TC_WDX, TC_WLX, TC_WCX - plugin type
Single function constants
#ifdef TC_FNC_HEADER
TC_LINE_PREFIX rettype TC_NAME_PREFIX TC_FUNC_MEMBEROF
#endif
[TC_FUNC_PREFIX_A]name
#ifdef TC_FNC_HEADER
(params)
#ednif
#if defined(TC_FNC_BODY)
{return TC_FUNCTION_TARGET->[TC_FUNC_PREFIX_B]name(callparams);}
#elif defined(TC_FNC_HEADER)
;
#endif
*/
//Definitions
#ifdef TC_GETFNAME_A
    #undef TC_GETFNAME_A
#endif
#ifdef TC_GETFNAME_B
    #undef TC_GETFNAME_B
#endif
#define TC_GETFNAME_A(name) TC_FUNC_PREFIX_A##name
#define TC_GETFNAME_B(name) TC_FUNC_PREFIX_B##name
//File system  wfx
#ifdef TC_WFX
#ifdef TC_FS_INIT
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsInit
    #ifdef TC_FNC_HEADER
        (int PluginNr,tProgressProc pProgressProc, tLogProc pLogProc,tRequestProc pRequestProc)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsInit(PluginNr,pProgressProc,pLogProc,pRequestProc);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_FINDFIRST
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX HANDLE TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsFindFirst
    #ifdef TC_FNC_HEADER
        (char* Path,WIN32_FIND_DATA *FindData)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsFindFirst(Path, FindData);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_FINDNEXT
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsFindNext
    #ifdef TC_FNC_HEADER
        (HANDLE Hdl,WIN32_FIND_DATA *FindData)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsFindNext(Hdl, FindData);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_FINDCLOSE
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsFindClose
    #ifdef TC_FNC_HEADER
        (HANDLE Hdl)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsFindClose(Hdl);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_MKDIR
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsMkDir
    #ifdef TC_FNC_HEADER
        (char* Path)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsMkDir(Path);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_EXECUTEFILE
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsExecuteFile
    #ifdef TC_FNC_HEADER
        (HWND MainWin,char* RemoteName,char* Verb)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsExecuteFile(/*(HANDLE)*/MainWin, RemoteName, Verb);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_RENMOVFILE
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsRenMovFile
    #ifdef TC_FNC_HEADER
        (char* OldName,char* NewName,BOOL Move,  BOOL OverWrite,RemoteInfoStruct* ri)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsRenMovFile(OldName, NewName, Move, OverWrite, ri);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_GETFILE
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsGetFile
    #ifdef TC_FNC_HEADER
        (char* RemoteName,char* LocalName,int CopyFlags, RemoteInfoStruct* ri)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsGetFile(RemoteName, LocalName, CopyFlags, ri);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_PUTFILE
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsPutFile
    #ifdef TC_FNC_HEADER
        (char* LocalName,char* RemoteName,int CopyFlags)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsPutFile(LocalName, RemoteName, CopyFlags);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_DELETEFILE
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsDeleteFile
    #ifdef TC_FNC_HEADER
        (char* RemoteName)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsDeleteFile(RemoteName);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_REMOVEDIR
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsRemoveDir
    #ifdef TC_FNC_HEADER
        (char* RemoteName)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsRemoveDir(RemoteName);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_DISCONNECT
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsDisconnect
    #ifdef TC_FNC_HEADER
        (char* DisconnectRoot)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsDisconnect(DisconnectRoot);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_SETATTR 
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsSetAttr
    #ifdef TC_FNC_HEADER
        (char* RemoteName,int NewAttr)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsSetAttr(RemoteName,NewAttr);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_SETTIME
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsSetTime
    #ifdef TC_FNC_HEADER
        (char* RemoteName,::FILETIME *CreationTime,::FILETIME *LastAccessTime,::FILETIME *LastWriteTime)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsSetTime(RemoteName, CreationTime, LastAccessTime, LastWriteTime);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_STATUSINFO
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsStatusInfo
    #ifdef TC_FNC_HEADER
        (char* RemoteDir,int InfoStartEnd,int InfoOperation)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsStatusInfo(RemoteDir, InfoStartEnd, InfoOperation);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_GETDEFROOTNAME
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsGetDefRootName
    #ifdef TC_FNC_HEADER
        (char* DefRootName,int maxlen)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsGetDefRootName(DefRootName, maxlen);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_EXTRACTCUSTOMICON
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsExtractCustomIcon
    #ifdef TC_FNC_HEADER
        (char* RemoteName,int ExtractFlags,HICON* TheIcon)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsExtractCustomIcon(RemoteName, ExtractFlags, TheIcon);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_SETDEFAULTPARAMS
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsSetDefaultParams
    #ifdef TC_FNC_HEADER
        (FsDefaultParamStruct* dps)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsSetDefaultParams(dps);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_GETPREVIEWBITMAP
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsGetPreviewBitmap
    #ifdef TC_FNC_HEADER
        (char* RemoteName,int width,int height,HBITMAP* ReturnedBitmap)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsGetPreviewBitmap(RemoteName, width, height, ReturnedBitmap);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_LINKSTOLOCALFILES
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsLinksToLocalFiles
    #ifdef TC_FNC_HEADER
        (void)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsLinksToLocalFiles();}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_GETLOCALNAME
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsGetLocalName
    #ifdef TC_FNC_HEADER
        (char* RemoteName,int maxlen)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsGetLocalName(RemoteName, maxlen);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#ifdef TC_FS_GETDEFAULTVIEW
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX BOOL TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    FsContentGetDefaultView
    #ifdef TC_FNC_HEADER
        (char* ViewContents,char* ViewHeaders,char* ViewWidths,char* ViewOptions,int maxlen)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->FsContentGetDefaultView(ViewContents, ViewHeaders, ViewWidths, ViewOptions, maxlen);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#endif
//File system + Content common
#if defined(TC_WFX) || defined(TC_WDX)
#if defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentGetSupportedField)
    #ifdef TC_FNC_HEADER
        (int FieldIndex,char* FieldName, char* Units,int maxlen)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(ContentGetSupportedField)(FieldIndex, FieldName, Units, maxlen);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif      
#if defined(TC_C_GETVALUE) && defined(TC_C_GETSUPPORTEDFIELD)
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentGetValue)
    #ifdef TC_FNC_HEADER
        (char* FileName,int FieldIndex,int UnitIndex, void* FieldValue,int maxlen,int flags)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(ContentGetValue)(FileName, FieldIndex, UnitIndex, FieldValue, maxlen, flags);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif   
#if defined(TC_C_STOPGETVALUE) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentStopGetValue)
    #ifdef TC_FNC_HEADER
        (char* FileName)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(ContentStopGetValue)(FileName);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif      
#if defined(TC_C_GETDEFAULTSORTORDER) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentGetDefaultSortOrder)
    #ifdef TC_FNC_HEADER
        (int FieldIndex)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(ContentGetDefaultSortOrder)(FieldIndex);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif      
#if defined(TC_C_PLUGINUNLOADING) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX void TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentPluginUnloading)
    #ifdef TC_FNC_HEADER
        (void)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(ContentPluginUnloading)();}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif   
#if defined(TC_C_GETSUPPORTEDFIELDFLAGS) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentGetSupportedFieldFlags)
    #ifdef TC_FNC_HEADER
        (int FieldIndex)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(ContentGetSupportedFieldFlags)(FieldIndex);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif       
#if defined(TC_C_SETVALUE) && defined(TC_C_GETSUPPORTEDFIELD) && defined(TC_C_GETVALUE)
    #ifdef TC_FNC_HEADER
        TC_LINE_PREFIX int TC_NAME_PREFIX TC_FUNC_MEMBEROF
    #endif
    TC_GETFNAME_A(ContentSetValue)
    #ifdef TC_FNC_HEADER
        (char* FileName,int FieldIndex,int UnitIndex,int FieldType, void* FieldValue,int flags)
    #endif
    #if defined(TC_FNC_BODY)
        {return TC_FUNCTION_TARGET->TC_GETFNAME_B(ContentSetValue)(FileName, FieldIndex, UnitIndex, FieldType, FieldValue, flags);}
    #elif defined(TC_FNC_HEADER)
        ;
    #endif
#endif
#endif
//Content wdx
#ifdef TC_WDX
#endif
//Lister wlx
#ifdef TC_WLX
#endif
//Pack wcx
#ifdef TC_WCX
#endif