;#DropDB SecurityDemoXEasyTest

#Application SecurityDemoXWin
#Application SecurityDemoXBlazor

*FillForm
 User Name = Admin
*Action Log In

#IfDef SecurityDemoXWin

*Action About...
 
*Action OK

#EndIf

#IfDef SecurityDemoXBlazor
*Action Navigation(My Details)
#EndIf
