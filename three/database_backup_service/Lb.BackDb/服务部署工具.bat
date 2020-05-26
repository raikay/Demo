@echo off
@echo *********************************************************************
@echo **********************数据库备份服务部署*****************************
@echo *********************************************************************
@echo 温馨提示：请使用管理员身份运行此批处理。
:menu
@echo =====================================================================

@echo 输入相应的指令完成操作。
@echo 1、设置数据库备份配置
@echo 2、安装数据库备份服务
@echo 3、启动数据库备份服务
@echo 4、停止数据库备份服务
@echo 5、卸载数据库备份服务
@echo 0、退出操作

set /p param=请输入您要进行操作的指令：
if /i %param% equ 1 goto setting
if /i %param% equ 2 goto install
if /i %param% equ 3 goto start
if /i %param% equ 4 goto stop
if /i %param% equ 5 goto uninstall
if /i %param% equ 0 goto exit
goto menu

:setting
@echo 正在启动配置工具...
start "" "%~dp0\LbBackDb.exe" setting
@echo 配置工具已经启动。
goto menu
:install
@echo 正在安装数据库备份服务...
%~dp0\LbBackDb.exe install
@echo 数据库备份服务安装完成。
goto menu
:start
@echo 正在启动数据库备份服务...
rem %~dp0\LbBackDb.exe start
net start RealgoalBackupDataBaseService 
@echo 数据库备份服务启动完成。
goto menu
:stop
@echo 正在启动数据库备份服务...
rem %~dp0\LbBackDb.exe stop
net stop RealgoalBackupDataBaseService 
@echo 数据库备份服务启动完成。
goto menu
:uninstall
@echo 正在卸载数据库备份服务...
%~dp0\LbBackDb.exe stop
%~dp0\LbBackDb.exe uninstall
@echo 数据库备份服务卸载完成。
goto menu
:exit
@pause 

rem  net start xxx
rem net stop xxx
rem 后面的服务名可以通过服务管理器看到：
rem 开始－》控制面板－》管理工具－》服务 或 直接使用打开运行 输入 services.msc
rem 找到你要控制的服务，双击，弹出的对话框里有“服务名称”一项（注意：可能和显示在服务管理器里的不一样，以弹出对话框里的“服务名称”为准）
