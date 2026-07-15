@echo off
chcp 65001 > nul
set "TARGET_DIR=To fix"

echo Поиск и копирование изображений...

powershell -NoProfile -ExecutionPolicy Bypass -Command ^
    "[void][Reflection.Assembly]::LoadWithPartialName('System.Drawing');" ^
    "if (!(Test-Path '%TARGET_DIR%')) { New-Item -ItemType Directory '%TARGET_DIR%' | Out-Null };" ^
    "Get-ChildItem -Filter *.png | ForEach-Object {" ^
    "  try {" ^
    "    $img = [System.Drawing.Image]::FromFile($_.FullName);" ^
    "    $w = $img.Width; $h = $img.Height;" ^
    "    $img.Dispose();" ^
    "    if ($w -ne 1024 -or $h -ne 1024) {" ^
    "      Copy-Item $_.FullName -Destination '%TARGET_DIR%';" ^
    "      Write-Host ('Скопирован: ' + $_.Name + ' [' + $w + 'x' + $h + ']') -ForegroundColor Yellow;" ^
    "    }" ^
    "  } catch {" ^
    "    Write-Host ('Ошибка обработки файла: ' + $_.Name + ' (Подробности: ' + $_.Exception.Message + ')') -ForegroundColor Red;" ^
    "  }" ^
    "}"

echo.
echo Готово! Неподходящие картинки скопированы в папку "%TARGET_DIR%".
pause