@echo off
@echo *********************************************************************
@echo **********************���ݿⱸ�ݷ�����*****************************
@echo *********************************************************************
@echo ��ܰ��ʾ����ʹ�ù���Ա������д�������
:menu
@echo =====================================================================

@echo ������Ӧ��ָ����ɲ�����
@echo 1���������ݿⱸ������
@echo 2����װ���ݿⱸ�ݷ���
@echo 3���������ݿⱸ�ݷ���
@echo 4��ֹͣ���ݿⱸ�ݷ���
@echo 5��ж�����ݿⱸ�ݷ���
@echo 0���˳�����

set /p param=��������Ҫ���в�����ָ�
if /i %param% equ 1 goto setting
if /i %param% equ 2 goto install
if /i %param% equ 3 goto start
if /i %param% equ 4 goto stop
if /i %param% equ 5 goto uninstall
if /i %param% equ 0 goto exit
goto menu

:setting
@echo �����������ù���...
start "" "%~dp0\LbBackDb.exe" setting
@echo ���ù����Ѿ�������
goto menu
:install
@echo ���ڰ�װ���ݿⱸ�ݷ���...
%~dp0\LbBackDb.exe install
@echo ���ݿⱸ�ݷ���װ��ɡ�
goto menu
:start
@echo �����������ݿⱸ�ݷ���...
rem %~dp0\LbBackDb.exe start
net start RealgoalBackupDataBaseService 
@echo ���ݿⱸ�ݷ���������ɡ�
goto menu
:stop
@echo �����������ݿⱸ�ݷ���...
rem %~dp0\LbBackDb.exe stop
net stop RealgoalBackupDataBaseService 
@echo ���ݿⱸ�ݷ���������ɡ�
goto menu
:uninstall
@echo ����ж�����ݿⱸ�ݷ���...
%~dp0\LbBackDb.exe stop
%~dp0\LbBackDb.exe uninstall
@echo ���ݿⱸ�ݷ���ж����ɡ�
goto menu
:exit
@pause 

rem  net start xxx
rem net stop xxx
rem ����ķ���������ͨ�����������������
rem ��ʼ����������壭�������ߣ������� �� ֱ��ʹ�ô����� ���� services.msc
rem �ҵ���Ҫ���Ƶķ���˫���������ĶԻ������С��������ơ�һ�ע�⣺���ܺ���ʾ�ڷ����������Ĳ�һ�����Ե����Ի�����ġ��������ơ�Ϊ׼��
