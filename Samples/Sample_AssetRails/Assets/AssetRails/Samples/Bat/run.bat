set startdir=%cd%
echo %startdir%
"C:\Program Files (x86)\Unity\Editor\Unity.exe" -quit -projectPath "%startdir%" -executeMethod AssetRailsController.RunJson -f "Assets\AssetRails\Samples\plan.json"
